using Aria_Net.Commands.Admin;
using Aria_Net.Commands.Regular;
using Aria_Net.IO;
using Discord;
using System.Reflection;

namespace Aria_Net.Handlers {
	public class CommandHandler {
		private DiscordClient _client;

		public CommandHandler(DiscordClient client) {
			_client = client;
		}

		public void RegisterCommands() {
			var logger = new Logger();

			logger.Log("Registering commands", Discord.LogSeverity.Info).Wait();

			var assembly = Assembly.GetExecutingAssembly();

			var commandTypes = assembly.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(BaseCommand)) && !t.IsAbstract);

			foreach (var commandType in commandTypes) {
				var instance = Activator.CreateInstance(commandType) as BaseCommand;
				if (instance != null) {
					instance.Register(_client);
					logger.Log("Command registered: " + instance.Name, Discord.LogSeverity.Info).Wait();
				}
			}

			logger.Log("Registering admin commands", Discord.LogSeverity.Info).Wait();

			commandTypes = assembly.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(BaseAdminCommand)) && !t.IsAbstract);

			var command = new SlashCommandBuilder()
				.WithName("admin")
				.WithDescription("Administrator only commands")
				.WithContextTypes(InteractionContextType.Guild)
				.WithIntegrationTypes(ApplicationIntegrationType.GuildInstall)
				.WithDefaultMemberPermissions(GuildPermission.Administrator);

			foreach (var commandType in commandTypes) {
				var instance = Activator.CreateInstance(commandType) as BaseAdminCommand;
				if (instance != null) {
					instance.Register(_client, command);
					logger.Log("Admin command registered: " + instance.Name, Discord.LogSeverity.Info).Wait();
				}
			}

			var finishedCommand = command.Build();

			_client.CreateGlobalApplicationCommandAsync(finishedCommand);
		}
	}
}
