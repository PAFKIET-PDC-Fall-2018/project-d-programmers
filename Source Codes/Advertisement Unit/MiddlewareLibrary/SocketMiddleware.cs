using MiddlewareLibrary.Types;
using MiddlewareLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace MiddlewareLibrary
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    class SocketMiddleware
    {
        private TcpListener serverListener;
        private TcpListener clientListener;
        private List<TcpClient> servers;
        private List<TcpClient> clients;

        public event Action<string> OnExceptionReceive;
        public event Action<string> OnServerConnected;
        public event Action<string> OnServerDisconnected;
        public event Action<string> OnClientConnected;
        public event Action<string> OnClientDisconnected;

        public SocketMiddleware(int serverPort, int clientPort)
        {
            try
            {
                serverListener = new TcpListener(IPAddress.Any, serverPort);
                clientListener = new TcpListener(IPAddress.Any, clientPort);
                serverListener.BeginAcceptTcpClient(beginAcceptTcpServer, null);
                clientListener.BeginAcceptTcpClient(beginAcceptTcpClient, null);
            }
            catch(Exception e)
            {
                OnExceptionReceive?.Invoke(e.Message);
            }
        }

        public void SendServer(MessageModel model, int ServerIndex)
        {
            SocketUtility.SocketStreamSend(servers[ServerIndex], model);
        }

        public void SendClient(MessageModel model, int ClientIndex)
        {
            SocketUtility.SocketStreamSend(clients[ClientIndex], model);
        }

        private void beginAcceptTcpClient(IAsyncResult ar)
        {
            TcpClient client = serverListener.EndAcceptTcpClient(ar);
            clientListener.BeginAcceptTcpClient(beginAcceptTcpClient, null);
            clients.Add(client);
            OnClientConnected?.Invoke(client.Client.RemoteEndPoint.AddressFamily.ToString());
            try
            {
                SocketUtility.SocketStreamReceive(client, ClientHandleMessage);
            }
            catch(Exception e)
            {
                OnExceptionReceive?.Invoke(e.Message);
                OnClientDisconnected?.Invoke(client.Client.RemoteEndPoint.AddressFamily.ToString());
                clients.Remove(client);
            }
        }
        /// <summary>
        /// ClientHandleMessage is invoked when a Client has sent a message to the middle ware, you need to forward it to the according server
        /// the data part should contain a target server it'll tell for which the message is intended for.
        /// </summary>
        /// <param name="model"></param>
        private void ClientHandleMessage(MessageModel model)
        {
            switch (model.MessageType)
            {
                case MessageType.CreatePlayList:
                    //TODO: Create a PlayListModel and Receive it from the Client, then forward it to the according server. 
                    //E.G MessageModel { Data = YourPlayListModel } the Data is a Object Type so your data can be boxed inside it 
                    //On each Receivers end you will have to unbox it. e.g  YourPlayList yourPlayList = model.Data as YourPlayList;
                    // See ServerListModel and set the Target as per your need. e.g from the Form the selected server will be the target, 
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
                    d.ClientIPs.AddRange(servers.Select(t => t.Client.AddressFamily.ToString()).ToArray());
                    SocketUtility.SocketStreamSend(clients.FirstOrDefault(t => t.Client.AddressFamily.ToString() == d.connection.Target),model);
                    break;
                default:
                    break;
            }
        }
        private void ServerHandleMessage(MessageModel model)
        {
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
                default:
                    break;
            }
        }
        private void beginAcceptTcpServer(IAsyncResult ar)
        {
            TcpClient server = serverListener.EndAcceptTcpClient(ar);
            serverListener.BeginAcceptTcpClient(beginAcceptTcpServer, null);

            servers.Add(server);
            OnServerConnected?.Invoke(server.Client.RemoteEndPoint.AddressFamily.ToString());

            try
            {
                SocketUtility.SocketStreamReceive(server, ServerHandleMessage);
            }
            catch(Exception e)
            {
                OnExceptionReceive?.Invoke(e.Message);
                OnServerDisconnected?.Invoke(server.Client.RemoteEndPoint.AddressFamily.ToString());
                servers.Remove(server);
            }
        }

    }
}
