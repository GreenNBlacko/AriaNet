using MySqlConnector;

namespace Aria_Net.DB.Classes {
	public class VerificationOptions(MySqlConnection connection) : BaseTable(connection) {
		public async Task<bool> GetUseVerification(long VerificationOptionsID) {
			return (bool)(await Select(query => query
				.SetSelect("UseVerification")
				.SetFrom("VerificationOptions")
				.SetWhere(("VerificationOptionsID", VerificationOptionsID))
			))["UseVerification"];
		}

		public async Task SetUseVerification(long VerificationOptionsID, bool UseVerification) {
			await InsertOrUpdate(query => query
				.SetUpdate("VerificationOptions", ("UseVerification", UseVerification ? 1 : 0))
				.SetWhere(("VerificationOptionsID", VerificationOptionsID))
			);
		}

		public async Task<ulong> GetUnverifiedRoleID(long VerificationOptionsID) {
			return (ulong)(await Select(query => query
				.SetSelect("UnverifiedRoleID")
				.SetFrom("VerificationOptions")
				.SetWhere(("VerificationOptionsID", VerificationOptionsID))
			))["UnverifiedRoleID"];
		}

		public async Task SetUnverifiedRoleID(long VerificationOptionsID, ulong UnverifiedRoleID) {
			await InsertOrUpdate(query => query
				.SetUpdate("VerificationOptions", ("UnverifiedRoleID", UnverifiedRoleID))
				.SetWhere(("VerificationOptionsID", VerificationOptionsID))
			);
		}

		public async Task<ulong> GetVerificationChannel(long VerificationOptionsID) {
			return (ulong)(await Select(query => query
				.SetSelect("VerificationChannel")
				.SetFrom("VerificationOptions")
				.SetWhere(("VerificationOptionsID", VerificationOptionsID))
			))["VerificationChannel"];
		}

		public async Task SetVerificationChannel(long VerificationOptionsID, ulong VerificationChannel) {
			await InsertOrUpdate(query => query
				.SetUpdate("VerificationOptions", ("VerificationChannel", VerificationChannel))
				.SetWhere(("VerificationOptionsID", VerificationOptionsID))
			);
		}
	}
}
