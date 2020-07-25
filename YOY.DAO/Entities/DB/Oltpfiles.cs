using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpfiles
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? Reference { get; set; }
        public int Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ContentType { get; set; }
        public string Checksum { get; set; }
        public string FileExtension { get; set; }

        public virtual Deftenants Tenant { get; set; }
    }
}
