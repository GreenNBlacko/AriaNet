using Aria_Net.IO;
using MySqlConnector;

namespace Aria_Net.DB.Classes {
	public class Server(MySqlConnection connection) : BaseTable(connection) {
		public async Task<bool> Exists(ulong ServerID) {
			return await Exists(query => query
				.SetSelect("ServerID")
				.SetFrom("Server")
				.SetWhere(("ServerID", ServerID))
			);
		}

		public async Task<List<(long id, int verificationOptionsID)>> GetServerList() {
			var response = await Select(query => query
				.SetSelect("ServerID", "VerificationOptionsID")
				.SetFrom("Server")
			);

			var ids = response["ServerID"] as object[];
			var options = response["VerificationOptionsID"] as object[];

			var result = new List<(long id, int verificationOptionsID)>();

			for(int i = 0; i < ids.Length; i++) {
				result.Add(((long)ids[i], (int)options[i]));
			}

			return result;
		}

		public async Task Add(ulong ServerID) {
			var verificationOptionsID = (long)await InsertOrUpdate(query => query
				.SetInsertInto("VerificationOptions", ("UseVerification", 0), ("UnverifiedRoleID", 0), ("VerificationChannel", 0))
			);

			await InsertOrUpdate(query => query
				.SetInsertInto("Server", ("ServerID", ServerID), ("VerificationOptionsID", verificationOptionsID))
			);
		}

		public async Task<int> GetVerificationOptionsID(ulong ServerID) {
			var response = await Select(query => query
				.SetSelect("VerificationOptionsID")
				.SetFrom("Server")
				.SetWhere(("ServerID", ServerID))
			);

			return (int)response["VerificationOptionsID"];
		}

		public async Task Remove(ulong ServerID) {
			var verificationOptionsID = await GetVerificationOptionsID(ServerID);

			await Delete(query => query
				.SetDeleteFrom("VerificationOptions")
				.SetWhere(("VerificationOptionsID", verificationOptionsID))
			);

			await Delete(query => query
				.SetDeleteFrom("Server")
				.SetWhere(("ServerID", ServerID))
			);
		}
	}
}
