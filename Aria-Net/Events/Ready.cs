using Aria_Net.DB.Classes;
using Aria_Net.IO;
using Discord;

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
				await _logger.Log("Crawling servers");
				foreach (var server in _client.Guilds.ToArray()) {
					if (server == null)
						continue;

					try {
						if (await db.GetTable<Server>().Exists(server.Id))
							continue;

						await db.GetTable<Server>().Add(server.Id);
						await _logger.Log($"Added {server.Name} to the database");
					} catch (Exception e) {
						await _logger.Log(new LogMessage(LogSeverity.Error, e.Source, e.Message, e));
					}
				}

				await _logger.Log("DB setup done");
			});
		}
	}
}
