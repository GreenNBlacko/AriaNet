namespace PD1.IO {
    public static class ConsoleInput {
        private static Stack<string> buffer = new();

        public static string NextString() => NextString(null);
        public static string NextString(string? prompt) {
            if (prompt != null) ConsoleOutput.SendMessage(prompt);
            return GetInput();
        }

        public static int NextInt() => NextInt(null);
        public static int NextInt(string? prompt) {
            if (prompt != null) ConsoleOutput.SendMessage(prompt);
            return int.Parse(GetInput());
        }

        private static string GetInput() {
            if (buffer.Count == 0) {
                string? response;

                do {
                    response = Console.ReadLine();

                    if (response == null)
                        throw new IOException("Failed to pull text from console");

                    if(response.Trim() == string.Empty)
                        ConsoleOutput.SendMessage("Invalid input. Try again", true);
                } while (response.Trim() == string.Empty);

                ParseInput(response);
            }

            return buffer.Pop();
        }

        private static void ParseInput(string input) {
            var items = new List<string>(input.Split(' ').Reverse());

            foreach (var item in items)
                buffer.Push(item);
        }
    }
}
