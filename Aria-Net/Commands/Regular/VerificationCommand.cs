﻿using Aria_Net.Commands.Options;
using Aria_Net.IO;
using CaptchaGen;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;

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

			if((await client.db.VerifiedUsers.FindAsync(interaction.User.Id.ToString())) != null) {
				var followup = await interaction.FollowupAsync("You are already verified!", ephemeral: true);
				Timeout(followup.DeleteAsync, 5000);
				await client.db.Database.CloseConnectionAsync();
				return;
			}

			var captcha = CaptchaCodeFactory.GenerateCaptchaCode(8);
			var image = ImageFactory.GenerateImage($"   {captcha}   ", 300, 700, 80);

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