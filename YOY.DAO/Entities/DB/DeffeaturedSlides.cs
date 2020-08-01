using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DeffeaturedSlides
    {
        public DeffeaturedSlides()
        {
            DefpromotionalCampaigns = new HashSet<DefpromotionalCampaigns>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ImageId { get; set; }
        public string ImageUrl { get; set; }
        public Guid? TenantId { get; set; }
        public Guid CountryId { get; set; }
        public Guid? StateId { get; set; }
        public int Type { get; set; }
        public int MaxViews { get; set; }
        public bool? IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpimages Image { get; set; }
        public virtual ICollection<DefpromotionalCampaigns> DefpromotionalCampaigns { get; set; }
    }
}
