using Aria_Net.Commands.Options;
using Aria_Net.DB.Classes;
using Aria_Net.IO;
using Aria_Net.Services;
using CaptchaGen;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Regular {
	public class VerificationCommand : BaseCommand {
		public override string Name => "verify";

		protected override SlashCommandProperties Create() {
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription("Start the verification process")
				.WithContextTypes(InteractionContextType.Guild, InteractionContextType.BotDm)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall, ApplicationIntegrationType.UserInstall)
				.Build();
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			await interaction.DeferAsync(ephemeral: true);

			var verifiedUsersTable = client.db.GetTable<VerifiedUsers>();

			if ((await verifiedUsersTable.IsVerifed(interaction.User.Id))) {
				var followup = await interaction.FollowupAsync("You are already verified!", ephemeral: true);
				Timeout(followup.DeleteAsync, 5000);
				return;
			}

			var captcha = CaptchaCodeFactory.GenerateCaptchaCode(8);
			var image = new MemoryStream(new CaptchaService().GenerateCaptcha(captcha));

			await new Logger().Log("Code for skill issue: " + captcha);

			client.captchas[interaction.User.Id] = captcha;

			var component = new ComponentBuilder()
				.WithButton(
					client.Buttons["verify_button"].Create()
				)
				.Build();

			var embed = new EmbedBuilder()
				.WithTitle("Verification")
				.WithImageUrl("attachment://image.png")
				.Build();


			var message = await interaction.FollowupWithFileAsync(image, "image.png", embed: embed, ephemeral: true, components: component);

			Timeout(message.DeleteAsync);
		}
	}
}
