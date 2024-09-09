namespace PD1.IO {
    public static class ConsoleOutput {
        public static void SendMessage(string Message, bool FullLine = false) {
            if (FullLine) Console.WriteLine(Message);
            else Console.Write(Message);
        }
    }
}
