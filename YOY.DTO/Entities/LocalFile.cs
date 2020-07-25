using System;

namespace YOY.DTO.Entities
{
    public class LocalFile
    {
        public Guid? Id { set; get; }
        public string Message { set; get; }
        public bool Success { set; get; }

        public Guid TenantId { set; get; }
        public Guid ReferenceId { set; get; }
        public string Name { set; get; }
        public string MimeType { set; get; }
        public string FileExtension { set; get; }
        public string LocalPath { set; get; }
        public string CryptoSignature { set; get; }
        public string ImageTypeName { set; get; }
        public bool IsDisplayImage { set; get; }

    }
}
