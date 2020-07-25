using System;
using System.IO;

namespace YOY.DTO.Entities
{
    public class FileContent
    {
        public string Id { set; get; }
        public string MimeType { set; get; }
        public string Name { set; get; }
        public DateTime? CreationDate { set; get; }
        public MemoryStream Content { set; get; }
    }
}
