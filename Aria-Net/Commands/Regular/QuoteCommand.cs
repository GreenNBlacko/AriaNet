using Aria_Net.Commands.Options;
using Aria_Net.Services;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace Aria_Net.Commands.Regular {
	public class QuoteCommand : BaseCommand {
		public override string Name => "quote";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Get a random qoute")
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var zq_client = new ZenQuoteService();

			var quoteArray = JArray.Parse(await zq_client.GetQuoteAsync());
			var quote = quoteArray[0] as JObject;

			var embed = new EmbedBuilder()
				.WithTitle("Quote")
				.WithDescription(quote["q"].Value<string>())
				.WithFooter(quote["a"].Value<string>())
				.Build();

			await interaction.RespondAsync(embed: embed);
		}
	}
}
