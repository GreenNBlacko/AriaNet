using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria_Net.Events {
	public class ModalSubmitted : BaseEvent {
		public ModalSubmitted(DiscordClient client) : base(client) { }

		public override void Register() {
			base.Register();

			_client.ModalSubmitted += Invoke;
		}

		private async Task Invoke(SocketModal interaction) {
			await _client.Modals[interaction.Data.CustomId].Execute(interaction, _client);
		}
	}
}
