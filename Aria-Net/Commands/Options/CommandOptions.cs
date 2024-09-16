using Discord.WebSocket;

namespace Aria_Net.Commands.Options {
	public class CommandOptions {
		private IReadOnlyCollection<SocketSlashCommandDataOption> _options;

		public CommandOptions(IReadOnlyCollection<SocketSlashCommandDataOption> options) {
			_options = options;
		}

		public CommandOption? this[string key] {
			get {
				var option = _options.Where(x => x.Name == key);

				if(option.Count() == 0)
					return new(null);

				return new(option.First());
			}
		}

		public CommandOption First() {
			return new(_options.First());
		}
	}
}
