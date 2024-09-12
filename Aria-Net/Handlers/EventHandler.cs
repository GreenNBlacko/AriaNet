using Aria_Net.Events;
using Aria_Net.IO;
using System.Reflection;

namespace Aria_Net.Handlers {
	public class EventHandler {
		private DiscordClient _client;

		public EventHandler(DiscordClient client) {
			_client = client;
		}

		public void RegisterEvents() {
			var logger = new Logger();

			logger.Log("Registering events", Discord.LogSeverity.Info).Wait();

			var assembly = Assembly.GetExecutingAssembly();

			var eventTypes = assembly.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(BaseEvent)) && !t.IsAbstract);

			foreach (var eventType in eventTypes) {
				var instance = Activator.CreateInstance(eventType, _client) as BaseEvent;
				if (instance != null) {
					instance.Register();
				}
			}
		}
	}
}
