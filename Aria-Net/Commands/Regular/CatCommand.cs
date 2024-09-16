using Aria_Net.Commands.Options;
using Aria_Net.Services;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace Aria_Net.Commands.Regular {
	public class CatCommand : BaseCommand {
		public override string Name => "cat";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Get a random cat image")
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.PrivateChannel, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			var catClient = new CatService();
			var response = await catClient.GetRandomCatImageAsync();
			var embed = new EmbedBuilder()
				.WithTitle("Cat")
				.WithImageUrl(response["url"].Value<string>())
				.Build();

			await interaction.RespondAsync(embed: embed);
		}
	}
}
