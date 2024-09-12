using Aria_Net.IO;
using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;

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
			_client.db.Servers.Remove(await _client.db.Servers.FindAsync(server.Id.ToString()));
			await _client.db.SaveChangesAsync();
			await _client.db.Database.CloseConnectionAsync();
		}
	}
}
