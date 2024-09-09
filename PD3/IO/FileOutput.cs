using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3.IO {
    public static partial class FileIO {
        public static void WriteFile(string Path, string Data) => File.WriteAllText(path: Path, contents: Data);
    }
}
