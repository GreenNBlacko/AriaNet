using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;

namespace Aria_Net.Components.Modals {
	public class VerifyModal : BaseModal {
		public override string ID => "verify_modal";

		public override Modal Create() {
			return new ModalBuilder()
				.WithTitle("Verification")
				.WithCustomId(ID)
				.AddTextInput(new TextInputBuilder()
					.WithLabel("Enter the captcha")
					.WithCustomId("captcha_answer")
					.WithMinLength(8)
					.WithMaxLength(8)
					.WithRequired(true)
					.WithStyle(TextInputStyle.Short)
				)
				.Build();
		}

		public override async Task Execute(SocketModal interaction, DiscordClient client) {
			var correctAnswer = client.captchas[interaction.User.Id];

			if (correctAnswer.ToLower() == interaction.Data.Components.Where(x => x.CustomId == "captcha_answer").Select(x => x.Value).First().ToLower()) {
				await interaction.RespondAsync("Verification successful!", ephemeral: true);
				await client.db.VerifiedUsers.AddAsync(new (interaction.User.Id.ToString()));
				await client.db.SaveChangesAsync();
				await client.db.Database.CloseConnectionAsync();
			} else
				await interaction.RespondAsync("Verification failed! Try again.", ephemeral: true);
		}
	}
}
