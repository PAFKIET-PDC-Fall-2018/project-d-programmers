using MiddlewareLibrary.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareLibrary.Models
{
    /// <summary>
    /// Developer: Syed Monis Azhar [59485]
    /// </summary>
    public class FileModel
    {
        public FileType FileType { get; set; }
        public string filename { get; set; }
        public byte[] Data { get; set; }
    }
}
