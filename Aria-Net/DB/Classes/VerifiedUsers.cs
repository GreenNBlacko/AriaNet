namespace Aria_Net.DB.Classes {
	public class VerifiedUsers {
		public string UserID { get; set; } = "";

		public VerifiedUsers() { }

		public VerifiedUsers(string userID) {
			UserID = userID;
		}
	}
}
