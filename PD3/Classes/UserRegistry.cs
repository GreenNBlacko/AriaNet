using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3.Classes {
    public class UserRegistry {
        public List<User> Users = new();

        public void RegisterUser(User user) {
            if (!UserIsValid(user))
                throw new RegistryException("User is not of age to register on this platform");

            Users.Add(user);
        }

        protected virtual bool UserIsValid(User user) => user.BirthDate.Age > 13;

        internal class RegistryException(string message) : Exception(message) { }
    }
}
