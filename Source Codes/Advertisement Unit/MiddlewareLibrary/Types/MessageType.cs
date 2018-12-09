using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareLibrary.Types
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    public enum MessageType
    {
        CreatePlayList,
        UploadPlayList,
        UploadFile,
        PlayRandomVideo,
        Query,
        MiddlewareCommand,
        ServerList,
    }
}
