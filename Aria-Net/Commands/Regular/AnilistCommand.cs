using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using Aria_Net.Commands.Options;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Regular {
	public class AnilListCommand : BaseCommand {
		public override string Name => "anilist";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Search for anime/manga entries in the AniList database.")
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.AddOption(new SlashCommandOptionBuilder()
					.WithName("anime")
					.WithDescription("Search for anime.")
					.WithType(ApplicationCommandOptionType.SubCommand)
					.AddOption("title", ApplicationCommandOptionType.String, "Title of the anime", isRequired: true)
					.AddOption("count", ApplicationCommandOptionType.Integer, "The max amount of results to be displayed", isRequired: false, minValue: 1, maxValue: 10)
				)
				.AddOption(new SlashCommandOptionBuilder()
					.WithName("manga")
					.WithDescription("Search for manga.")
					.WithType(ApplicationCommandOptionType.SubCommand)
					.AddOption("title", ApplicationCommandOptionType.String, "Title of the manga", isRequired: true)
					.AddOption("count", ApplicationCommandOptionType.Integer, "The max amount of results to be displayed", isRequired: false, minValue: 1, maxValue: 10)
				)
				.Build();
		}

		protected override Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var subCommand = options.First();
			var subCommandOptions = subCommand.Options;
			var title = subCommandOptions["title"].GetValue<string>();
			var count = (int)subCommandOptions["count"].GetValue<long>(defaultValue: 5);

			var type = subCommand.Name switch {
				"anime" => MediaType.Anime,
				"manga" => MediaType.Manga,
				_ => throw new NotSupportedException()
			};

			return SearchMedia(title, type, count, interaction, client);
		}

		private async Task SearchMedia(string title, MediaType type, int count, SocketSlashCommand interaction, DiscordClient client) {
			await interaction.DeferAsync();

			var AL_Client = GetClient();

			var response = await AL_Client.SearchMediaAsync(new SearchMediaFilter {
				Type = type,
				Query = title
			}, new AniPaginationOptions(1, count));

			if (response.Data.Count() == 0) {
				await interaction.FollowupAsync(embed: new EmbedBuilder()
					.WithTitle("Error!")
					.WithColor(Color.Red)
					.WithDescription("No results found")
					.WithFooter("404 error")
					.Build());

				return;
			}

			List<Embed> embeds = new List<Embed>();

			foreach (var entry in response.Data) {
				embeds.Add(new EmbedBuilder()
					.WithTitle(entry.Title.EnglishTitle != null ? entry.Title.EnglishTitle : entry.Title.PreferredTitle)
					.WithColor(Color.Green)
					.WithFields(new List<EmbedFieldBuilder> {
						new EmbedFieldBuilder()
						.WithName("Type")
						.WithIsInline(true)
						.WithValue(entry.Type.ToString()),
						new EmbedFieldBuilder()
						.WithName("Format")
						.WithIsInline(true)
						.WithValue(entry.Format != null ? entry.Format.ToString() : "N/A"),
						new EmbedFieldBuilder()
						.WithName("Popularity")
						.WithIsInline(true)
						.WithValue(entry.Popularity),
						new EmbedFieldBuilder()
						.WithName(entry.Type == MediaType.Anime ? "Episodes" : "Chapters")
						.WithIsInline(true)
						.WithValue(entry.Type == MediaType.Anime ? entry.Episodes != null ? entry.Episodes : "N/A" : entry.Chapters != null ? entry.Chapters : "N/A"),
						new EmbedFieldBuilder()
						.WithName("Status")
						.WithIsInline(true)
						.WithValue(entry.Status.ToString()),
						new EmbedFieldBuilder()
						.WithName("Average score")
						.WithIsInline(true)
						.WithValue(entry.AverageScore != null ? entry.AverageScore : "N/A")
						})
					.WithImageUrl(entry.Cover.LargeImageUrl.AbsoluteUri)
					.WithUrl(entry.Url.AbsoluteUri)
					.WithFooter(string.Format("{0} favorite{1}", entry.Favorites, entry.Favorites % 10 != 1 || entry.Favorites % 100 == 11 ? "s" : ""))
					.WithAuthor("AniList")
					.Build());
			}

			await interaction.FollowupAsync(text: "Search results:", embeds: embeds.ToArray());
		}

		private AniClient GetClient() => new AniClient();
	}
}
