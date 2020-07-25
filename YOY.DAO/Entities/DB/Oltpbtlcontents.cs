using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpbtlcontents
    {
        public Oltpbtlcontents()
        {
            OltpbtlcontentItems = new HashSet<OltpbtlcontentItems>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public int DealType { get; set; }
        public int ContentType { get; set; }
        public int ObjectiveType { get; set; }
        public int GeoSegmentationType { get; set; }
        public Guid? DisplayImageId { get; set; }
        public int ViewCount { get; set; }
        public int SaveCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Oltpcategories Category { get; set; }
        public virtual Oltpimages DisplayImage { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual ICollection<OltpbtlcontentItems> OltpbtlcontentItems { get; set; }
    }
}
