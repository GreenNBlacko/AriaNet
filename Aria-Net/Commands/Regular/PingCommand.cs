using Aria_Net.Commands.Options;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Regular {
	public class PingCommand : BaseCommand {
		public override string Name => "ping";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Receive a ping message")
				.AddOption("message", ApplicationCommandOptionType.String, "Message to respond with", isRequired: false)
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var message = options["message"].GetValue(defaultValue: "Pong");

			await interaction.RespondAsync(message);
		}
	}
}
