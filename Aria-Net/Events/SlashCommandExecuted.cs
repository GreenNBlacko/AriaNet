using Discord.WebSocket;

namespace Aria_Net.Events {
	public class OnSlashCommandCreated : BaseEvent {
		public OnSlashCommandCreated(DiscordClient client) : base(client) { }

		public override void Register() {
			base.Register();

			_client.SlashCommandExecuted += Invoke;
		}

		private async Task Invoke(SocketSlashCommand interaction) {
			switch (interaction.Data.Name) {
				case "admin":
					await _client.AdminCommands[interaction.Data.Options.First().Name].Invoke(interaction, _client);
					break;

				default:
					await _client.Commands[interaction.Data.Name].Invoke(interaction, _client);
					break;
			}
		}
	}
}
