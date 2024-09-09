using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3.Classes {
    public class Admin : User {
        public Admin(string fName, string lName, Date birthDate, string password, string profilePicture) : base(fName, lName, birthDate, password, profilePicture) { }
    }
}
