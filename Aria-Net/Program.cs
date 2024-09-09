using Aria_Net.Handlers;
using Aria_Net.IO;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aria_Net {
	public class Program {
		public static IConfigurationRoot config;

		public static async Task Main() {
			config = new ConfigurationBuilder().AddUserSecrets<Program>().AddEnvironmentVariables().Build();

			using IHost host = Host.CreateDefaultBuilder()
				.ConfigureServices((_, services) =>
					services
					.AddSingleton(config)
					.AddSingleton(x => 
						new DiscordSocketClient(
							new DiscordSocketConfig {
								GatewayIntents = GatewayIntents.AllUnprivileged,
								AlwaysDownloadUsers = true,
							}
						)
					)
					.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>(), new InteractionServiceConfig {
						AutoServiceScopes = true,
					}))
					.AddSingleton<InteractionHandler>()
				)
				.Build();

			await RunAsync(host);
		}

		public static async Task RunAsync(IHost host) {
			using IServiceScope serviceScope = host.Services.CreateScope();
			IServiceProvider provider = serviceScope.ServiceProvider;

			var _client = provider.GetService<DiscordSocketClient>() ?? throw new Exception("Client was not found");
			var sCommands = provider.GetRequiredService<InteractionService>() ?? throw new Exception("Slash command handler was not found");
			var config = provider.GetService<IConfigurationRoot>() ?? throw new Exception("Config was not found");

			await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

			_client.Log += Logger.Log;
			sCommands.Log += Logger.Log;

			_client.Ready += async () => {
				await Logger.Log("Bot initialized");
				await sCommands.RegisterCommandsGloballyAsync();
			};

			await _client.LoginAsync(TokenType.Bot, config["ARIANET:DISCORD:TOKEN"]);
			await _client.StartAsync();

			await Task.Delay(-1);
		}
	}
}
