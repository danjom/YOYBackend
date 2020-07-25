using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class ExternallyStoredFile
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public int StorageSource { set; get; }
        public int FileType { set; get; }
        public string ExternalId { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string MimeType { set; get; }
    }
}
