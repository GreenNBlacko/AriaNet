using Aria_Net.Commands.Options;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Admin {
	public class VerifyOptionsCommand : BaseAdminCommand {
		public override string Name => "verification_options";

		protected override SlashCommandOptionBuilder Create() {
			return new SlashCommandOptionBuilder()
				.WithName(Name)
				.WithDescription("Edit verification settings")
				.WithType(ApplicationCommandOptionType.SubCommand)
				.AddOption("use-verification", ApplicationCommandOptionType.Boolean, "Select whether to use the bot's verification service", isRequired: false)
				.AddOption("verification-role", ApplicationCommandOptionType.Role, "Select a role to be used for non-verified users", isRequired: false)
				.AddOption("verification-channel", ApplicationCommandOptionType.Channel, "Select a channel to be used for verification", isRequired: false);
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {

		}
	}
}
