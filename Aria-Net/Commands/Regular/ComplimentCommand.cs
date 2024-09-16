using Aria_Net.Commands.Options;
using Aria_Net.Services;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace Aria_Net.Commands.Regular {
	public class ComplimentCommand : BaseCommand {
		public override string Name => "compliment";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Get a random compliment")
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var complimentClient = new ComplimentService();
			var response = complimentClient.GetRandomComplimentAsync();
			var embed = new EmbedBuilder()
				.WithTitle("Compliment")
				.WithDescription(response["compliment"].Value<string>())
				.Build();

			await interaction.RespondAsync(embed: embed);
		}
	}
}
