using Aria_Net.Commands.Options;
using Discord;
using Discord.WebSocket;
using SauceNET;

namespace Aria_Net.Commands.Regular {
	public class SauceNaoCommand : BaseCommand {
		public override string Name => "saucenao";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Search for an image using SauceNAO.")
				.AddOption(new SlashCommandOptionBuilder()
					.WithName("link")
					.WithDescription("Search using a link.")
					.WithType(ApplicationCommandOptionType.SubCommand)
					.AddOption("url", ApplicationCommandOptionType.String, "URL of the image", isRequired: true)
					.AddOption("count", ApplicationCommandOptionType.Integer, "The max amount of results to be displayed", minValue: 1, maxValue: 10, isRequired: false)
				)
				.AddOption(new SlashCommandOptionBuilder()
					.WithName("file")
					.WithDescription("Search using a file.")
					.WithType(ApplicationCommandOptionType.SubCommand)
					.AddOption("file", ApplicationCommandOptionType.Attachment, "Image file", isRequired: true)
					.AddOption("count", ApplicationCommandOptionType.Integer, "The max amount of results to be displayed", minValue: 1, maxValue: 10, isRequired: false)
				)
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var subCommand = options.First();
			var subCommandOptions = subCommand.Options;
			var count = (int)subCommandOptions["count"].GetValue<long>(defaultValue: 5);

			switch (subCommand.Name) {
				case "link":
					var url = subCommandOptions["url"].GetValue<string>() ?? string.Empty;

					await SauceNaoLink(url, count, interaction, client);
					break;

				case "file":
					var file = subCommandOptions["file"].GetValue<IAttachment>() ?? throw new Exception("File was not uploaded");

					await SauceNaoFile(file, count, interaction, client);
					break;
			}
		}

		private async Task SauceNaoLink(string url, int count, SocketSlashCommand interaction, DiscordClient client) {
			await interaction.DeferAsync();
			var sauceClient = new SauceNETClient(client._config["ARIANET:SAUCENAO:TOKEN"]);

			var response = await sauceClient.GetSauceAsync(url);

			if (response.Results.Count == 0) {
				await interaction.FollowupAsync(embed: new EmbedBuilder()
					.WithTitle("Error!")
					.WithColor(Color.Red)
					.WithDescription("No results found")
					.WithFooter("404 error")
					.Build());

				return;
			}

			List<Embed> embeds = new List<Embed>();

			for (int i = 0; i < response.Results.Count && i < count; i++) {
				var title = response.Results[i];

				embeds.Add(new EmbedBuilder()
					.WithTitle(title.DatabaseName)
					.WithImageUrl(title.ThumbnailURL)
					.WithUrl(title.SourceURL)
					.WithFooter(string.Format("{0}% similarity", title.Similarity))
					.WithColor(Color.Green)
					.WithAuthor("SauceNAO")
					.Build());
			}

			await interaction.FollowupAsync(text: "Search results:", embeds: embeds.ToArray());
		}

		private async Task SauceNaoFile(IAttachment file, int count, SocketSlashCommand interaction, DiscordClient client) {
			if (!file.ContentType.Contains("image/")) {
				await interaction.RespondAsync(embed: new EmbedBuilder()
					.WithTitle("Error!")
					.WithColor(Color.Red)
					.WithDescription("Please attach a valid image")
					.WithFooter("Image filetype error")
					.Build());
				return;
			}
			await SauceNaoLink(file.Url, count, interaction, client);
		}
	}
}
