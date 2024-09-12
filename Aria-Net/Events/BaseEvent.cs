using Aria_Net.IO;
using Discord;

namespace Aria_Net.Events {
	public abstract class BaseEvent {
		protected DiscordClient _client;

		public BaseEvent(DiscordClient client) {
			_client = client;
		}

		public virtual void Register() => new Logger().Log("Registering event: " + GetType().Name, LogSeverity.Info);
	}
}
