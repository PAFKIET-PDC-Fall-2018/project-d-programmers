using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiddlewareLibrary.Models;
using MiddlewareLibrary.Types;
using MiddlewareLibrary.Utilities;

namespace MiddlewareLibrary
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    public class SocketServer
    {
        private TcpListener _listener;
        private TcpClient _middleware;
        public event Action<string> OnMiddlewareConnected;
        public event Action<string> OnMiddlewareDisconnected;
        public event Action<MessageModel> OnMessageReceiveFromMiddleware;
        public event Action<string> OnExceptionReceive;
        public SocketServer(int port)
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, port);
                _listener.BeginAcceptTcpClient(beginAcceptTcpClient, null);
            }
            catch(Exception e)
            {
                OnExceptionReceive?.Invoke(e.Message);
            }
        }

        private void beginAcceptTcpClient(IAsyncResult ar)
        {
           _middleware = _listener.EndAcceptTcpClient(ar);
            manageMiddlewareRead();
        }

        public void SendToMiddleware(MessageModel model)
        {
            SocketUtility.SocketStreamSend(_middleware, model);
        }

        private void manageMiddlewareRead()
        {
            var stream = _middleware.GetStream();
            try
            {
                OnMiddlewareConnected?.Invoke("Connected to Middleware");
                SocketUtility.SocketStreamReceive(_middleware, HandleMessage);
            }
            catch(Exception e)
            {
                OnExceptionReceive?.Invoke(e.Message);
                OnMiddlewareConnected?.Invoke("Discnnected from Middleware");
            }
            _listener.BeginAcceptTcpClient(beginAcceptTcpClient, null);
        }

        /// <summary>
        /// HandleMessage receives message from the middleware
        /// </summary>
        /// <param name="model"> MessageModel's data part needs to be unboxed to a specific type. </param>
        private void HandleMessage(MessageModel model)
        {
            OnMessageReceiveFromMiddleware?.Invoke(model);

            switch (model.MessageType)
            {
                case MessageType.CreatePlayList:
                    break;
                case MessageType.UploadPlayList:
                    break;
                case MessageType.UploadFile:
                    ReceiveFile(model.Data as FileModel);
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

        private void ReceiveFile(FileModel fileModel)
        {
            switch (fileModel.FileType)
            {
                case FileType.mp4:
                    IOUtility.StoreFile("/MP4s/", fileModel.filename, fileModel.Data);
                    break;
                case FileType.txt:
                    IOUtility.StoreFile("/txts/", fileModel.filename, fileModel.Data);
                    break;
                default:
                    break;
            }
        }
        
    }
}