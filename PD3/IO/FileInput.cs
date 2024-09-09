using System.Drawing;

namespace PD3.IO {
    public static partial class FileIO {
        public static string ReadFile(string Path) => File.ReadAllText(path: Path);

        public static Bitmap ReadImage(string Path) => new Bitmap(filename: Path);
    }
}
