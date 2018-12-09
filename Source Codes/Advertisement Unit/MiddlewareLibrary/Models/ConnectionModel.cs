using System;
using System.Net.Sockets;

namespace MiddlewareLibrary
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    [Serializable]
    public class ConnectionModel
    {
        public Types.SocketType Type { get; set; }
        public string Target { get; set; }
    }
}
