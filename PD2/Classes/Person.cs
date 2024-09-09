namespace PD2.Classes {
	public class Person {
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public Date BirthDate { get; private set; }

		public string GetPersonInfo() {
			return string.Format("Name: {0} {1}\nAge: {2}",
				FirstName,
				LastName,
				string.Format("{0} year{1}",
				BirthDate.Age,
				BirthDate.Age % 10 == 1 ? "" : "s"));
		}

		public Person(string firstName, string lastName, DateTime birthDate) {
			FirstName = firstName;
			LastName = lastName;
			CheckValidity();
			BirthDate = new Date(birthDate);
		}

		public Person(string firstName, string lastName, int year, int month, int day) {
			FirstName = firstName;
			LastName = lastName;
			CheckValidity();
			BirthDate = new Date(year, month, day);
		}

		private void CheckValidity() {
			if (FirstName == null || FirstName.Trim() == string.Empty)
				throw new PersonException("First name cannot be null!");

			if (LastName == null || LastName.Trim() == string.Empty)
				throw new PersonException("Last name cannot be null!");
		}

		internal class PersonException : Exception {
			public PersonException(string message) : base(message) { }
		}
	}
}
