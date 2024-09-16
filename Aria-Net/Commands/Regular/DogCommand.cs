using Aria_Net.Commands.Options;
using Aria_Net.Services;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace Aria_Net.Commands.Regular {
	public class DogCommand : BaseCommand {
		public override string Name => "dog";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Get a random dog image")
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var dogClient = new DogService();
			var response = await dogClient.GetRandomDogImageAsync();
			var embed = new EmbedBuilder()
				.WithTitle("Dog")
				.WithImageUrl(response["message"].Value<string>())
				.Build();

			await interaction.RespondAsync(embed: embed);
		}
	}
}
