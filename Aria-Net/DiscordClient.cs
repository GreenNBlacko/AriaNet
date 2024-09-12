using Aria_Net.Components.Buttons;
using Aria_Net.Components.Modals;
using Aria_Net.Components.SelectMenus;
using Aria_Net.DB;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Aria_Net
{
    public class DiscordClient : DiscordSocketClient {
		public IConfigurationRoot _config { get; private set; }
		public Dictionary<string, Func<SocketSlashCommand, DiscordClient, Task>> Commands { get; private set; } = new();
		public Dictionary<string, Func<SocketSlashCommand, DiscordClient, Task>> AdminCommands { get; private set; } = new();
		public Dictionary<string, BaseButton>									 Buttons { get; private set; } = new();
		public Dictionary<string, BaseSelectMenu>								 SelectMenus { get; private set; } = new();
		public Dictionary<string, BaseModal>									 Modals { get; private set; } = new();
		public Dictionary<ulong, string>										 captchas { get; private set; } = new();

		public DBContext db;

		public Handlers.EventHandler _eventHandler { get; private set; }
		public Handlers.CommandHandler _commandHandler { get; private set; }
		public Handlers.ComponentHandler _componentHandler { get; private set; }

		public DiscordClient(IConfigurationRoot config) : this(config, new DiscordSocketConfig()) {
			_config = config;
		}

		public DiscordClient(IConfigurationRoot config, DiscordSocketConfig clientConfig) : base(clientConfig) {
			_config = config;
		}

		public async Task Start() {
			db = new DBContext(this);

			_eventHandler = new Handlers.EventHandler(this);
			_commandHandler = new Handlers.CommandHandler(this);
			_componentHandler = new Handlers.ComponentHandler(this);

			_eventHandler.RegisterEvents();
			_componentHandler.RegisterComponents();

			await LoginAsync(TokenType.Bot, _config["ARIANET:DISCORD:TOKEN"]);
			await StartAsync();

			await Task.Delay(-1);
		}
	}
}
