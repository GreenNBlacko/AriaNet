using Aria_Net.Commands.Options;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Regular {
	public abstract class BaseCommand {
		public abstract string Name { get; }

		public virtual void Register(DiscordClient client) {
			var command = Create();

			client.CreateGlobalApplicationCommandAsync(command);
			client.Commands[command.Name.Value] = LoadCommand;
		}

		protected abstract SlashCommandProperties Create();

		private Task LoadCommand(SocketSlashCommand interaction, DiscordClient client) {
			return Execute(interaction, new(interaction.Data.Options), client);
		}

		protected abstract Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client);

		protected async void Timeout(Func<Task> action, int TimeMs = 30000) {
			await Task.Delay(TimeMs);

			await action.Invoke();
		}
	}
}
