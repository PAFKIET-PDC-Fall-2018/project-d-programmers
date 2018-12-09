using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareLibrary.Utilities
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    public class SocketUtility
    {
        public static void SocketStreamSend(TcpClient client, MessageModel model)
        {
            foreach (var bytes in DataUtilities.SerializeModel(model))
                client.GetStream().Write(bytes, 0, bytes.Length);
        }

        public static void SocketStreamReceive(TcpClient client, Action<MessageModel> method)
        {
            var stream = client.GetStream();
            List<byte> receiverBuffer = new List<byte>();
            var buffer = new byte[1024];
            while (client.Connected)
            {
                int size = stream.Read(buffer, 0, buffer.Length);
                receiverBuffer.AddRange(buffer.Skip(1).Take(size - 1));
                while (!stream.DataAvailable)
                {
                    size = stream.Read(buffer, 0, buffer.Length);
                    if (buffer[0] == 'a')
                        receiverBuffer.AddRange(buffer.Skip(1).Take(size - 1));
                    else
                    {
                        receiverBuffer.AddRange(buffer.Skip(1).Take(size - 1));
                        method(DataUtilities.DeserializeModel<MessageModel>(receiverBuffer.ToArray()));
                        receiverBuffer.Clear();
                    }
                }
            }
        }
    }
}
