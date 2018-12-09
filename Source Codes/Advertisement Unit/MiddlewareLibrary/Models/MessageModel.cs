using MiddlewareLibrary.Types;
using System;

namespace MiddlewareLibrary
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    [Serializable]
    public class MessageModel
    {
        public MessageType MessageType { get; set; }
        public object Data { get; set; }
    }
}
