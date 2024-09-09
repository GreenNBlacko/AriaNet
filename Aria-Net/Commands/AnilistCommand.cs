using AniListNet;
using AniListNet.Objects;
using Discord;
using Discord.Interactions;
using AniListNet.Parameters;

namespace Aria_Net.Commands {

	[Group("anilist", "Search for anime/manga entries in the AniList database.")]
	[CommandContextType(new InteractionContextType[] { InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm })]
	[IntegrationType(new ApplicationIntegrationType[] { ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall })]
	public class AnilListCommand : InteractionModuleBase<SocketInteractionContext> {
		[SlashCommand("anime", "Search using a link.")]
		public async Task AniListAnime(
			[Summary(description: "Title of the anime")] string title,
			[Summary(description: "The max amount of results to be displayed"), MinValue(1), MaxValue(10)] int count = 5) {
			await SearchMedia(title, MediaType.Anime, count);
		}

		[SlashCommand("manga", "Search using a file.")]
		public async Task AniListManga(
			[Summary(description: "Title of the manga")] string title,
			[Summary(description: "The max amount of results to be displayed"), MinValue(1), MaxValue(10)] int count = 5) {
			await SearchMedia(title, MediaType.Manga, count);
		}

		private async Task SearchMedia(string title, MediaType type, int count) {
			await DeferAsync();

			var client = GetClient();

			var response = await client.SearchMediaAsync(new SearchMediaFilter{
				Type = type,
				Query = title
			}, new AniPaginationOptions(1, count));

			if (response.Data.Count() == 0) {
				await RespondAsync(embed: new EmbedBuilder()
					.WithTitle("Error!")
					.WithColor(Color.Red)
					.WithDescription("No results found")
					.WithFooter("404 error")
					.Build());

				return;
			}

			List<Embed> embeds = new List<Embed>();

			foreach(var entry in response.Data) {
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

			await FollowupAsync(text: "Search results:", embeds: embeds.ToArray());
		}

		private AniClient GetClient() => new AniClient();
	}
}
