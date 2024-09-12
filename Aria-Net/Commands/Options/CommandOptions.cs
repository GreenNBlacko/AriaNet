using Discord.WebSocket;

namespace Aria_Net.Commands.Options {
	public class CommandOptions {
		private IReadOnlyCollection<SocketSlashCommandDataOption> _options;

		public CommandOptions(IReadOnlyCollection<SocketSlashCommandDataOption> options) {
			_options = options;
		}

		public CommandOption this[string key] {
			get {
				return new(_options.Where(x => x.Name == key).First());
			}
		}

		public CommandOption First() {
			return new(_options.First());
		}
	}
}
