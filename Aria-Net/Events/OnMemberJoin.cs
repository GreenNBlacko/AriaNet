using Aria_Net.DB.Classes;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria_Net.Events {
	public class OnMemberJoin : BaseEvent {
		public OnMemberJoin(DiscordClient client) : base(client) { }

		public override void Register() {
			base.Register();

			_client.UserJoined += Invoke;
		}

		private async Task Invoke(SocketGuildUser member) {
			var guild = member.Guild;

			var verificationOptionsID = await _client.db.GetTable<Server>().GetVerificationOptionsID(member.Guild.Id);
			var verificationOptions = _client.db.GetTable<VerificationOptions>();

			if(await verificationOptions.GetUseVerification(verificationOptionsID)) {
				ulong roleID = 0;

				if ((roleID = await verificationOptions.GetUnverifiedRoleID(verificationOptionsID)) == 0)
					return;

				if ((await _client.db.GetTable<VerifiedUsers>().IsVerifed(member.Id)))
					return;

				var UnverifiedRole = guild.Roles.Where(x => x.Id == roleID).First();

				await member.AddRoleAsync(UnverifiedRole);

				var verificationChannel = await _client.db.GetTable<VerificationOptions>().GetVerificationChannel(verificationOptionsID);

				if(verificationChannel != 0) {
					var channel = guild.Channels.Where(x => x.Id == verificationChannel).First() as SocketTextChannel;

					await channel.SendMessageAsync($"{member.Mention} before you proceed into the server, you need to verify. To do that, use the '/verify' command.");
				} else {
					await member.SendMessageAsync($"{member.Mention} before you proceed into the server, you need to verify. To do that, use the '/verify' command.");
				}
			}
		} 
	}
}
