using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class Image
    {
        public Guid? Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid ReferenceId { set; get; }
        public string Folder { set; get; }
        public string Version { set; get; }
        public string Format { set; get; }
        public string ExternalId { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
        public string WebTransformation { set; get; }
        public string AppTransformation { set; get; }
        public string ImageTypeName { set; get; }
        public string Url { set; get; }
        public string Message { set; get; }
        public bool Success { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
