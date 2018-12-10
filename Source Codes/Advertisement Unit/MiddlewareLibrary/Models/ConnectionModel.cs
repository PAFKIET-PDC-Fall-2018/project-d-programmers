using System;
using System.Net.Sockets;

namespace MiddlewareLibrary
{
    /// <summary>
    /// Developer: Arbaz Ahmed [59008]
    /// </summary>
    [Serializable]
    public class ConnectionModel
    {
        public Types.SocketType Type { get; set; }
        public string Target { get; set; }
    }
}
