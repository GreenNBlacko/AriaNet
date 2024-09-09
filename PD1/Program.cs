using PD1.Classes;
using PD1.IO;

namespace PD1 {
    public class Program {
        public static void Main(string[] args) {
            string fName, lName;
            int year, month, day;

            fName = ConsoleInput.NextString("Enter first name: ");
            lName = ConsoleInput.NextString("Enter last name: ");

            ConsoleOutput.SendMessage("", true);
            ConsoleOutput.SendMessage("Enter birth date:", true);
            ConsoleOutput.SendMessage("", true);

            year = ConsoleInput.NextInt("Year: ");
            month = ConsoleInput.NextInt("Month: ");
            day = ConsoleInput.NextInt("Day: ");

            ConsoleOutput.SendMessage("", true);
            try {
                Person p = new Person(fName, lName, year, month, day);
                ConsoleOutput.SendMessage(p.GetPersonInfo(), true);
                ConsoleOutput.SendMessage(string.Format("Days until next birthday: {0}", p.BirthDate.DaysUntilBirthday), true);
            } catch { }
        }
    }
}
