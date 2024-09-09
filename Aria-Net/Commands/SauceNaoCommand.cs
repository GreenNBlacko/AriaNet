using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Configuration;
using SauceNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria_Net.Commands {

	[Group("saucenao", "Search for an image using SauceNAO.")]
	[CommandContextType(new InteractionContextType[] { InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm })]
	[IntegrationType(new ApplicationIntegrationType[] { ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall })]
	public class SauceNaoCommand : InteractionModuleBase<SocketInteractionContext> {
		[SlashCommand("link", "Search using a link.")]
		public async Task SauceNaoLink(
			[Summary(description:"URL of the image")] string url,
			[Summary(description: "The max amount of results to be displayed"), MinValue(1), MaxValue(10)] int count = 5) {
			await DeferAsync();
			var client = new SauceNETClient(Program.config["ARIANET:SAUCENAO:TOKEN"]);

			var response = await client.GetSauceAsync(url);

			if(response.Results.Count == 0) {
				await RespondAsync(embed: new EmbedBuilder()
					.WithTitle("Error!")
					.WithColor(Color.Red)
					.WithDescription("No results found")
					.WithFooter("404 error")
					.Build());

				return;
			}

			List<Embed> embeds = new List<Embed>();

			for(int i = 0; i < response.Results.Count && i < count; i++) {
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

			await FollowupAsync(text:"Search results:", embeds: embeds.ToArray());
		}

		[SlashCommand("file", "Search using a file.")]
		public async Task SauceNaoFile(
			[Summary(description:"Image file")] IAttachment file,
			[Summary(description: "The max amount of results to be displayed"), MinValue(1), MaxValue(10)] int count = 5) {
			if(!file.ContentType.Contains("image/")) {
				await RespondAsync(embed: new EmbedBuilder()
					.WithTitle("Error!")
					.WithColor(Color.Red)
					.WithDescription("Please attach a valid image")
					.WithFooter("Image filetype error")
					.Build());
				return;
			}
			await SauceNaoLink(file.Url, count);
		}
	}
}
