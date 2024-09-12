using Discord;
using Discord.WebSocket;

namespace Aria_Net.Components.SelectMenus {
	public abstract class BaseSelectMenu : BaseComponent<SelectMenuBuilder, SocketMessageComponent> {
		public override void Register(DiscordClient client) {
			client.SelectMenus[ID] = this;
		}
	}
}
