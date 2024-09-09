namespace PD2.Classes {
    public class Date {
        public DateTime BirthDate { get; private set; }
        public int Age => DateTime.Today.Year - BirthDate.Year;
        public DateTime NearestBirthday => new(DateTime.Today.Year, BirthDate.Month, BirthDate.Day);
        public int DaysUntilBirthday => (new DateTime((NearestBirthday < DateTime.Today ? 1 : 0) + DateTime.Today.Year, BirthDate.Month, BirthDate.Day) - DateTime.Today).Days;

        public Date(int year, int month, int day) {
            try {
                BirthDate = new DateTime(year, month, day);
                CheckValidity();
            } catch {
                throw new DateException("Date does not exist! Try again.");
            }
        }

        public Date(DateTime date) {
            BirthDate = date;
            CheckValidity();
        }

        private void CheckValidity() {
            if (BirthDate > DateTime.Now)
                throw new DateException("The date is invalid, please try again");
        }

        internal class DateException : Exception {
            public DateException(string message) : base(message) { }
        }
    }
}
