using System;
using System.IO;

namespace YOY.DTO.Entities
{
    public class File
    {
        public Guid Id { set; get; }
        public Stream FileContent { set; get; }
        public Guid TenantId { set; get; }
        public Guid? Reference { set; get; }
        public int? ReferenceType { set; get; }
        public int Type { set; get; }
        public DateTime CreationDate { set; get; }
        public string ContentType { set; get; }
        public string Checksum { set; get; }
        public string FileExtension { set; get; }
        public byte[] SerializedContent { set; get; }
        public bool Success { set; get; }
        public bool IsImage { set; get; }
        public bool IsDisplayImg { set; get; }
    }
}
