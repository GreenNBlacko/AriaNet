using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria_Net.DB.Classes {
	public class CommandRestriction {
		public int CommandRestrictionID { get; set; }
		public string CommandName { get; set; } = "";
		public bool ServerWideBan { get; set; } = false;
		public ICollection<Channel> Channels { get; set; } = new List<Channel>();

		public CommandRestriction() { }

		public CommandRestriction(int commandRestrictionID, string commandName, bool serverWideBan, ICollection<Channel> channels) {
			CommandRestrictionID = commandRestrictionID;
			CommandName = commandName;
			ServerWideBan = serverWideBan;
			Channels = channels;
		}
	}
}
