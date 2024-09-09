using PD3.GUI;

namespace PD3 {
    public class Program {
        public static void Main(string[] args) {
            var gui = new ProgramGUI();

            gui.Start().Wait();
        }
    }
}
