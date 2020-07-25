using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defalliances
    {
        public Guid Id { get; set; }
        public Guid SponsorId { get; set; }
        public Guid SponsoredId { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
        public bool Bidirectional { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Deftenants Sponsor { get; set; }
        public virtual Deftenants Sponsored { get; set; }
    }
}
