using Discord;
using Microsoft.Extensions.Configuration;

namespace Aria_Net {
	public class Program {
		public static async Task Main() {
			var config = new ConfigurationBuilder().AddUserSecrets<Program>().AddEnvironmentVariables().Build();

			await new DiscordClient(config, new() {
				GatewayIntents = GatewayIntents.All ^ (GatewayIntents.GuildScheduledEvents | GatewayIntents.GuildInvites | GatewayIntents.GuildPresences)
			}).Start();
		}
	}
}
