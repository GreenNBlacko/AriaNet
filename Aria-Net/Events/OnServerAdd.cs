using Aria_Net.DB.Classes;
using Aria_Net.IO;
using Discord.WebSocket;

namespace Aria_Net.Events {
	public class OnServerAdd : BaseEvent {
		private Logger _logger;

		public OnServerAdd(DiscordClient client) : base(client) { _logger = new Logger(); }

		public override void Register() {
			base.Register();

			_client.JoinedGuild += Invoke;
		}

		private async Task Invoke(SocketGuild server) {
			await _logger.Log("Joined guild: " + server.Name, Discord.LogSeverity.Info);

			await _client.db.GetTable<Server>().Add(server.Id);
		}
	}
}
