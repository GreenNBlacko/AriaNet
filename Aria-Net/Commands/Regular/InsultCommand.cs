using Aria_Net.Commands.Options;
using Aria_Net.Services;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace Aria_Net.Commands.Regular {
	public class InsultCommand : BaseCommand {
		public override string Name => "insult";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Get a random insult")
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var insultClient = new InsultService();
			var response = await insultClient.GetRandomInsultAsync();
			var embed = new EmbedBuilder()
				.WithTitle("Insult")
				.WithDescription(response["insult"].Value<string>())
				.Build();

			await interaction.RespondAsync(embed: embed);
		}
	}
}
