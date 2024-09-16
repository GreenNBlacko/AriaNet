using Discord;
using Microsoft.Extensions.Configuration;

namespace Aria_Net {
	public class Program {
		public static async Task Main() {
			var config = new ConfigurationBuilder().AddUserSecrets<Program>().AddEnvironmentVariables().Build();

			var cts = new CancellationTokenSource();
			AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => cts.Cancel(); // Handle graceful shutdown on process exit
			Console.CancelKeyPress += (sender, eventArgs) => {
				eventArgs.Cancel = true;
				cts.Cancel(); // Handle Ctrl+C or stop command
			};

			try {
				await new DiscordClient(config, new() {
					GatewayIntents = GatewayIntents.All ^ (GatewayIntents.GuildScheduledEvents | GatewayIntents.GuildInvites | GatewayIntents.GuildPresences)
				}, cts.Token).Start();
			} catch (OperationCanceledException) {
				Console.WriteLine("Shutdown triggered.");
			} finally {
				Console.WriteLine("Bot stopped.");
			}
		}
	}
}
