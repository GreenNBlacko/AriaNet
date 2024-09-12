using Discord.WebSocket;

namespace Aria_Net.Components {
	public abstract class BaseComponent<T, Y> {
		public abstract string ID { get; }

		public abstract void Register(DiscordClient client);

		public abstract T Create();

		public abstract Task Execute(Y interaction, DiscordClient client);
	}
}
