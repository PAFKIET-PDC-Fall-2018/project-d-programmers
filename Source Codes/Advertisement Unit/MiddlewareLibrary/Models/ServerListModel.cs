using System;
using System.Collections.Generic;

namespace MiddlewareLibrary
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    [Serializable]
    public class ServerListModel
    {
        public ConnectionModel connection { get; set; }
        public List<string> ClientIPs { get; set; }
    }
}
