using Discord;
using Discord.WebSocket;

namespace Aria_Net.Components.Buttons {
	public abstract class BaseButton : BaseComponent<ButtonBuilder, SocketMessageComponent> {
		public override void Register(DiscordClient client) {
			client.Buttons[ID] = this;
		}
	}
}
