namespace Aria_Net.DB.Classes {
	public class Server {
		public string ServerID;
		public VerificationClass Verification { get; set; } = new VerificationClass();
		public virtual ICollection<CommandRestriction> CommandRestrictions { get; set; } = new List<CommandRestriction>();

		public Server() { }

		public Server(string serverID, VerificationClass verification, ICollection<CommandRestriction> commandRestrictions) {
			ServerID = serverID;
			Verification = verification;
			CommandRestrictions = commandRestrictions;
		}
	}
}
