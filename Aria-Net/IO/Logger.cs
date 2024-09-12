using Discord;

namespace Aria_Net.IO {
	public class Logger {
		public Task Log(LogMessage msg) {
			return Log(msg.Message + (msg.Severity != LogSeverity.Info && msg.Exception != null ? '\n' + msg.Exception.Message + '\n' + msg.Exception.ToString() : ""), msg.Severity);
		}

		public Task Log(object msg, LogSeverity severity = LogSeverity.Info) {
			Console.WriteLine(
				string.Format("{0}  {1}  {2}", 
					DateTime.Now.ToString("HH:mm:ss"), 
					string.Format("{0}{1}",
						severity.ToString(), 
						"".PadRight(LogSeverity.Critical.ToString().Length - severity.ToString().Length)
					), 
					msg
				)
			);
			return Task.CompletedTask;
		}
	}
}
