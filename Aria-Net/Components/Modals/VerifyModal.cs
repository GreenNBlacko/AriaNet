using Aria_Net.DB.Classes;
using Discord;
using Discord.WebSocket;

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
				await client.db.GetTable<VerifiedUsers>().Add(interaction.User.Id);

				foreach (var entry in await client.db.GetTable<Server>().GetServerList()) {
					var useVerif = await client.db.GetTable<VerificationOptions>().GetUseVerification(entry.verificationOptionsID);

					if (!useVerif)
						continue;

					var roleID = await client.db.GetTable<VerificationOptions>().GetUnverifiedRoleID(entry.verificationOptionsID);

					if (roleID == 0)
						continue;

					var entryGuild = client.Guilds.Where(x => x.Id == (ulong)entry.id).First();

					var member = entryGuild.Users.Where(x => x.Id == interaction.User.Id).First();

					await member.RemoveRoleAsync(roleID);
				}
			} else
				await interaction.RespondAsync("Verification failed! Try again.", ephemeral: true);
		}
	}
}
