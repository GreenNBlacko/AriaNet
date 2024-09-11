using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aria_Net.DB.Classes {
	public class VerificationClass {
		public int VerificationID { get; set; }
		public bool UseVerification { get; set; }
		public string UnverifiedRoleID { get; set; } = "";
		public string VerificationChannel { get; set; } = "";

		public VerificationClass() { }

		public VerificationClass(bool useVerification = false, string unverifiedRoleID = "", string verificationChannel = "") {
			UseVerification = useVerification;
			UnverifiedRoleID = unverifiedRoleID;
			VerificationChannel = verificationChannel;
		}
	}
}
