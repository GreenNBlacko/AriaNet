using PD2.GUI;

namespace PD2 {
    public class Program {
        public static void Main(string[] args) {
            Console.WriteLine("program is starting...");

            var gui = new ProgramGUI();

            gui.Start().Wait();
        }
    }
}
