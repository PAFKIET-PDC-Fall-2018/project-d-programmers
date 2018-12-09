using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareLibrary.Utilities
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    public class DataUtilities
    {
        private static BinaryFormatter formatter;
        static DataUtilities()
        {
            formatter = new BinaryFormatter();
        }
        public static T DeserializeModel<T>(byte[] data)
        {
            return (T)formatter.Deserialize(new MemoryStream(data));
        }

        public static List<byte[]> SerializeModel<T>(T model)
        {
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, model);
                stream.Seek(0, SeekOrigin.Begin);
                List<byte[]> chunks = new List<byte[]>();

                byte[] chunk = new byte[1024];
                int bytesRead = stream.Read(chunk, 0, 1023);
                byte code = 0x97;
                while (bytesRead > 0)
                {
                    if (bytesRead != 1023)
                    {
                        byte[] tail = new byte[bytesRead];
                        Array.Copy(chunk, tail, bytesRead);
                        chunk = tail;
                        code = 0x97;
                    }
                    Array.Copy(chunk, 1, chunk, 1, 1023);
                    chunk[0] = code;
                    chunks.Add(chunk);

                    bytesRead = stream.Read(chunk, 0, 1023);
                }
                return chunks;
            }
        }
    }
}
