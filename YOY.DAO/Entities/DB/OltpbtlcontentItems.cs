using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpbtlcontentItems
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }
        public string ReferenceUrl { get; set; }
        public Guid? ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public int ContentType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ViewCount { get; set; }
        public int Position { get; set; }
        public string ContainedProducts { get; set; }

        public virtual Oltpbtlcontents Content { get; set; }
    }
}
