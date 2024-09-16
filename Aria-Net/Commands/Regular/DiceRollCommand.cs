using Aria_Net.Commands.Options;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Regular {
	public class DiceRollCommand : BaseCommand {
		public override string Name => "diceroll";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Roll a dice with a specified number of sides")
				.AddOption("sides", ApplicationCommandOptionType.Integer, "Number of sides on the dice", true)
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var sides = (int)options["sides"].GetValue((long)6); // Default to 6 sides if not provided
			var result = new Random().Next(1, sides + 1); // Rolls the dice

			var embed = new EmbedBuilder()
				.WithTitle("Dice Roll")
				.WithDescription($"You rolled a {result} on a {sides}-sided dice.")
				.Build();

			await interaction.RespondAsync(embed: embed);
		}
	}
}
