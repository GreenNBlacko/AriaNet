namespace PD3.Classes {
    public class User {
        public string fName;
        public string lName;
        public Date BirthDate;
        public string Password;
        public string ProfilePicture;

        public User(string fName, string lName, Date birthDate, string password, string profilePicture) {
            this.fName = fName;
            this.lName = lName;
            BirthDate = birthDate;
            Password = password;
            ProfilePicture = profilePicture;
        }
    }
}