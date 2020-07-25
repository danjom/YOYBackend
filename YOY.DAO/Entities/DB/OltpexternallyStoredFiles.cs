using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpexternallyStoredFiles
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public string ExternalId { get; set; }
        public int StorageSource { get; set; }
        public int FileType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string MimeType { get; set; }

        public virtual Deftenants Tenant { get; set; }
    }
}
