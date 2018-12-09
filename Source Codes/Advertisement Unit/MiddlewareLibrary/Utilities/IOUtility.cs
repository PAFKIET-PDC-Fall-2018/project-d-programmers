using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareLibrary.Utilities
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    public class IOUtility
    {
        public static void StoreFile(string path, string file, byte[] data)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                File.WriteAllBytes(path + file, data);
            }
            catch(Exception e)
            {
                File.WriteAllText("error.txt",File.Exists("error.txt") ? File.ReadAllText("error.txt") + e.Message : e.Message);
            }
        }
    }
}
