using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Aria_Net.Components.Buttons {
	public class VerifyButton : BaseButton {
		public override string ID => "verify_button";

		public override ButtonBuilder Create() {
			return new ButtonBuilder()
						.WithLabel("Verify")
						.WithStyle(ButtonStyle.Primary)
						.WithCustomId(ID);
		}

		public override async Task Execute(SocketMessageComponent interaction, DiscordClient client) {
			await interaction.RespondWithModalAsync(client.Modals["verify_modal"].Create());
		}
	}
}
