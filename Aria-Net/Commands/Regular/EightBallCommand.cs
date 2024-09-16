using Aria_Net.Commands.Options;
using Aria_Net.Services;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Regular {
	public class EightBallCommand : BaseCommand {
		public override string Name => "8ball";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Ask the 8ball a question")
				.AddOption("question", ApplicationCommandOptionType.String, "the question you want to ask", isRequired: true)
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var service = new EightBallService();

			var question = options["question"].GetValue("");

			var response = await service.GetAnswerAsync(question);

			var embed = new EmbedBuilder()
				.WithTitle("8ball")
				.WithDescription(question)
				.WithFooter(response)
				.Build();

			await interaction.RespondAsync(embed: embed);
		}
	}
}
