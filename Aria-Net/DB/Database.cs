using Aria_Net.DB.Classes;
using Aria_Net.IO;
using MySqlConnector;
using System.Reflection;

namespace Aria_Net.DB {
	public class Database {
		private string _server;
		private string _database;
		private string _username;
		private string _password;

		private MySqlConnection _connection;

		private Logger _logger;

		private Dictionary<Type, BaseTable> _tables = new();

		public Database(string server, string database, string username, string password) {
			_server = server;
			_database = database;
			_username = username;
			_password = password;

			_logger = new Logger();

			_connection = new MySqlConnection($"server={_server};user={_username};password={_password};database={_database}");

			CrawlTables();
		}

		public T? GetTable<T>() where T : BaseTable {
			var type = typeof(T);
			if (_tables.TryGetValue(type, out BaseTable table)) {
				return table as T;
			}
			return null;
		}

		private void CrawlTables() {
			var logger = new Logger();

			logger.Log("Registering tables", Discord.LogSeverity.Info).Wait();

			var assembly = Assembly.GetExecutingAssembly();

			var tableTypes = assembly.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(BaseTable)) && !t.IsAbstract);

			foreach (var tableType in tableTypes) {
				var instance = Activator.CreateInstance(tableType, _connection) as BaseTable;
				if (instance != null) {
					_tables[tableType] = instance;
					logger.Log("Registered table: " + tableType.Name, Discord.LogSeverity.Info).Wait();
				}
			}
		}
	}
}
