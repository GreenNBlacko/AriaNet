using MySqlConnector;

namespace Aria_Net.DB.Classes {
	public class VerifiedUsers(MySqlConnection connection) : BaseTable(connection) {
		public async Task<bool> IsVerifed(ulong UserID) {
			return await Exists(query => query
				.SetSelect("UserID")
				.SetFrom("VerifiedUser")
				.SetWhere(("UserID", UserID))
			);
		}

		public async Task Add(ulong UserID) {
			await InsertOrUpdate(query => query
				.SetInsertInto("VerifiedUser", ("UserID", UserID))
			);
		}

		public async Task Remove(ulong UserID) {
			await Delete(query => query
				.SetDeleteFrom("VerifiedUser")
				.SetWhere(("UserID", UserID))
			);
		}
	}
}
