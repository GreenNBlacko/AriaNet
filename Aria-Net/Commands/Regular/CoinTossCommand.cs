using Aria_Net.Commands.Options;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Regular {
	public class CoinTossCommand : BaseCommand {
		public override string Name => "cointoss";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Toss a coin and get either Heads or Tails")
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var result = new Random().Next(2) == 0 ? "Heads" : "Tails";
			var embed = new EmbedBuilder()
				.WithTitle("Coin Toss")
				.WithDescription($"The result is: {result}")
				.Build();

			await interaction.RespondAsync(embed: embed);
		}
	}
}
