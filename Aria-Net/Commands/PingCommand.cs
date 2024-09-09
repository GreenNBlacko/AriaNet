using Discord;
using Discord.Interactions;

namespace Aria_Net.Modules {
	public class PingCommand : InteractionModuleBase<SocketInteractionContext> {
		[CommandContextType(new InteractionContextType[] { InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm })]
		[IntegrationType(new ApplicationIntegrationType[] { ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall })]
		[SlashCommand("ping", "Receive a ping message")]
		public async Task Ping(string? message = null) {
			message ??= "Pong";

			await RespondAsync(message);
		}		
	}
}
