using Discord.Interactions;
using Discord;
using Discord.WebSocket;
using Aria_Net.Commands.Options;

namespace Aria_Net.Commands.Admin
{
#nullable enable
    [Group("admin", "Manage the bot's settings on the server.")]
    [DefaultMemberPermissions(GuildPermission.Administrator)]
    public abstract class BaseAdminCommand
    {
        public abstract string Name { get; }

        public virtual void Register(DiscordClient client, SlashCommandBuilder command)
        {
            command.AddOption(Create());
            client.AdminCommands[Name] = LoadCommand;
        }

        protected abstract SlashCommandOptionBuilder Create();

        private Task LoadCommand(SocketSlashCommand interaction, DiscordClient client)
        {
            return Execute(interaction, new CommandOptions(interaction.Data.Options).First().Options, client);
        }

        protected abstract Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client);
    }
#nullable restore
}
