using Aria_Net.DB.Classes;
using Aria_Net.IO;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Events {
	public class OnServerRemove : BaseEvent {
		private Logger _logger;

		public OnServerRemove(DiscordClient client) : base(client) { _logger = new Logger(); }

		public override void Register() {
			base.Register();

			_client.LeftGuild += Invoke;
		}

		private async Task Invoke(SocketGuild server) {
			await _logger.Log("Left guild: " + server.Name, LogSeverity.Info);
			await _client.db.GetTable<Server>().Remove(server.Id);
		}
	}
}
