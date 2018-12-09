using MiddlewareLibrary.Types;
using MiddlewareLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareLibrary
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    class SocketClient
    {
        private TcpClient tcpSocket;
        public event Action<string> OnExceptionReceive;
        public event Action<MessageModel> OnMessageReceive;
        public List<string> Servers { get; set; }
        public SocketClient(string ip, int port)
        {
            try
            {
                tcpSocket = new TcpClient(ip, port);
                Task.Factory.StartNew(new Action(() =>
                {
                    SocketUtility.SocketStreamReceive(tcpSocket, MessageReceiveHandler);
                }));
            }
            catch (Exception e)
            {
                OnExceptionReceive?.Invoke(e.Message);
            }
        }
        public void MessageSend(MessageModel model)
        {
            //UpdateCommand;
            SocketUtility.SocketStreamSend(tcpSocket, model);
        }

        public void MessageReceiveHandler(MessageModel model)
        {
            OnMessageReceive?.Invoke(model);
            switch (model.MessageType)
            {
                case MessageType.CreatePlayList:
                    break;
                case MessageType.UploadPlayList:
                    break;
                case MessageType.UploadFile:
                    break;
                case MessageType.PlayRandomVideo:
                    break;
                case MessageType.Query:
                    break;
                case MessageType.MiddlewareCommand:
                    break;
                case MessageType.ServerList:
                    var d = model.Data as ServerListModel;
                    Servers = d.ClientIPs;
                    break;
                default:
                    break;
            }
        }
        
    }
}
