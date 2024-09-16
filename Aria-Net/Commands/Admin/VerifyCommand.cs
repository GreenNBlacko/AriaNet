using Aria_Net.Commands.Options;
using Aria_Net.DB.Classes;
using Discord;
using Discord.WebSocket;

namespace Aria_Net.Commands.Admin {
	public class VerifyCommand : BaseAdminCommand {
		public override string Name => "verification";

		protected override SlashCommandOptionBuilder Create() {
			return new SlashCommandOptionBuilder()
				.WithName(Name)
				.WithDescription("Verification system control")
				.WithType(ApplicationCommandOptionType.SubCommandGroup)
				.AddOption(new SlashCommandOptionBuilder()
					.WithName("options")
					.WithDescription("Edit verification settings")
					.WithType(ApplicationCommandOptionType.SubCommand)
					.AddOption("use-verification", ApplicationCommandOptionType.Boolean, "Select whether to use the bot's verification service", isRequired: false)
					.AddOption("verification-role", ApplicationCommandOptionType.Role, "Select a role to be used for non-verified users", isRequired: false)
					.AddOption("verification-channel", ApplicationCommandOptionType.Channel, "Select a channel to be used for verification", isRequired: false)
				)
				.AddOption(new SlashCommandOptionBuilder()
					.WithName("add")
					.WithDescription("Manually verify a member")
					.WithType(ApplicationCommandOptionType.SubCommand)
					.AddOption("user", ApplicationCommandOptionType.User, "User to verify", isRequired: true)
				)
				.AddOption(new SlashCommandOptionBuilder()
					.WithName("remove")
					.WithDescription("Remove verification from a member")
					.WithType(ApplicationCommandOptionType.SubCommand)
					.AddOption("user", ApplicationCommandOptionType.User, "User to remove verification from", isRequired: true)
				);
		}

		protected override async Task Execute(SocketSlashCommand interaction, CommandOptions options, DiscordClient client) {
			await interaction.DeferAsync(ephemeral: true);

			var subCommand = options.First();
			var subCommandOptions = subCommand.Options;

			var guild = (await interaction.GetChannelAsync() as SocketGuildChannel).Guild;

			var verificationOptionsID = await client.db.GetTable<Server>().GetVerificationOptionsID(guild.Id);

			var verificationTable = client.db.GetTable<VerificationOptions>();

			switch (subCommand.Name) {
				case "options":
					var _enable = await verificationTable.GetUseVerification(verificationOptionsID);

					var enable = subCommandOptions["use-verification"]?.GetValue(_enable) ?? _enable;
					var verificationRole = subCommandOptions["verification-role"]?.GetValue<IRole>();
					var verificationChannel = subCommandOptions["verification-channel"]?.GetValue<IChannel>();

					var role = verificationRole != null ? verificationRole.Id : await verificationTable.GetUnverifiedRoleID(verificationOptionsID);
					var channel = verificationChannel != null ? verificationChannel.Id : await verificationTable.GetVerificationChannel(verificationOptionsID);

					if (enable != _enable)
						await verificationTable.SetUseVerification(verificationOptionsID, enable);
					await verificationTable.SetUnverifiedRoleID(verificationOptionsID, role);
					await verificationTable.SetVerificationChannel(verificationOptionsID, channel);

					var embed = new EmbedBuilder()
						.WithTitle("Verification options")
						.WithDescription("Settings changed successfully!")
						.AddField("Enabled: ", enable ? "Yes" : "No")
						.AddField("Unverified role: ", role != 0 ? $"<@&{role}>" : "None")
						.AddField("Verification channel: ", channel != 0 ? $"<#{channel}>" : "None")
						.Build();

					await interaction.FollowupAsync(embed: embed, ephemeral: true);
					break;

				case "add":
					var user = subCommandOptions["user"].GetValue<IUser>(true);

					var verifiedUsers = client.db.GetTable<VerifiedUsers>();

					if (await verifiedUsers.IsVerifed(user.Id)) {
						await interaction.FollowupAsync("This user is already verified!", ephemeral: true);
						return;
					}

					await verifiedUsers.Add(user.Id);

					foreach (var entry in await client.db.GetTable<Server>().GetServerList()) {
						var useVerif = await client.db.GetTable<VerificationOptions>().GetUseVerification(entry.verificationOptionsID);


						if (!useVerif)
							continue;

						var roleID = await client.db.GetTable<VerificationOptions>().GetUnverifiedRoleID(entry.verificationOptionsID);

						if (roleID == 0)
							continue;

						var entryGuild = client.Guilds.Where(x => x.Id == (ulong)entry.id).First();

						var member = entryGuild.Users.Where(x => x.Id == user.Id).First();

						await member.RemoveRoleAsync(roleID);
					}

					await user.SendMessageAsync("You have been verified by a system administrator.");
					await interaction.FollowupAsync("User successfully verified!", ephemeral: true);
					break;

				case "remove":
					user = subCommandOptions["user"].GetValue<IUser>(true);

					verifiedUsers = client.db.GetTable<VerifiedUsers>();

					if (!await verifiedUsers.IsVerifed(user.Id)) {
						await interaction.FollowupAsync("This user isn't verified!", ephemeral: true);
						return;
					}

					await verifiedUsers.Remove(user.Id);

					foreach (var entry in await client.db.GetTable<Server>().GetServerList()) {
						var useVerif = await client.db.GetTable<VerificationOptions>().GetUseVerification(entry.verificationOptionsID);

						if (!useVerif)
							continue;

						var roleID = await client.db.GetTable<VerificationOptions>().GetUnverifiedRoleID(entry.verificationOptionsID);

						if (roleID == 0)
							continue;

						var entryGuild = client.Guilds.Where(x => x.Id == (ulong)entry.id).First();

						var member = entryGuild.Users.Where(x => x.Id == user.Id).First();

						await member.AddRoleAsync(roleID);
					}

					await user.SendMessageAsync("Your verification has been invalidated by a system administrator.");
					await interaction.FollowupAsync("User verification successfully removed!", ephemeral: true);
					break;
			}
		}
	}
}
