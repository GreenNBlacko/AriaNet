using Aria_Net.IO;
using Discord;

namespace Aria_Net.Events {
	public class Log : BaseEvent {
		private Logger _logger;

		public Log(DiscordClient client) : base(client) { _logger = new Logger(); }

		public override void Register() {
			base.Register();

			_client.Log += Invoke;
		}

		private async Task Invoke(LogMessage msg) {
			await _logger.Log(msg);
		}
	}
}
