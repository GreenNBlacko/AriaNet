using Discord;
using Discord.WebSocket;

namespace Aria_Net.Components.Modals {
	public abstract class BaseModal : BaseComponent<Modal, SocketModal> {
		public override void Register(DiscordClient client) {
			client.Modals[ID] = this;
		}
	}
}
