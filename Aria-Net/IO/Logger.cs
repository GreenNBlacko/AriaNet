using Discord;

namespace Aria_Net.IO {
	public static class Logger {
		public static Task Log(LogMessage msg) {
			return Log(msg.Message + (msg.Severity != LogSeverity.Info && msg.Exception != null ? '\n' + msg.Exception.Message + '\n' + msg.Exception.ToString() : ""), msg.Severity);
		}

		public static Task Log(object msg, LogSeverity severity = LogSeverity.Info) {
			Console.WriteLine(string.Format("[{0}]  {1}   {2}", DateTime.Now.ToString("HH:mm:ss"), severity.ToString(), msg));
			return Task.CompletedTask;
		}
	}
}
