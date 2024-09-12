using Aria_Net.DB.Classes;
using Aria_Net.IO;
using Discord;
using Microsoft.EntityFrameworkCore;

namespace Aria_Net.Events {
	public class Ready : BaseEvent {
		private Logger _logger;

		public Ready(DiscordClient client) : base(client) { _logger = new Logger(); }

		public override void Register() {
			base.Register();

			_client.Ready += Invoke;
		}

		public async Task Invoke() {
			var db = _client.db;

			_client._commandHandler.RegisterCommands();

			await _logger.Log("Bot initialized");

			_ = Task.Run(async () => {
				await db.Database.EnsureCreatedAsync();

				await _logger.Log("Crawling servers");
				foreach (var server in _client.Guilds.ToArray()) {
					if (server == null)
						continue;

					try {
						if ((await db.Servers.FindAsync(server.Id.ToString())) != null)
							continue;

						db.Servers.Add(new Server(server.Id.ToString(), new VerificationClass(), new List<CommandRestriction>()));
						await _logger.Log($"Added {server.Name} to the database");
					} catch (Exception e) {
						await _logger.Log(new LogMessage(LogSeverity.Error, e.Source, e.Message, e));
					}
				}

				await db.SaveChangesAsync();
				await db.Database.CloseConnectionAsync();
			});
		}
	}
}
