using Discord.WebSocket;

namespace Aria_Net.Events {
	public class ButtonPressed : BaseEvent {
		public ButtonPressed(DiscordClient client) : base(client) { }

		public override void Register() {
			base.Register();

			_client.ButtonExecuted += Invoke;
		}

		private async Task Invoke(SocketMessageComponent interaction) {
			await _client.Buttons[interaction.Data.CustomId].Execute(interaction, _client);
		}
	}
}
