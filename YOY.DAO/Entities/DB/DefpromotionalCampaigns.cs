using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefpromotionalCampaigns
    {
        public DefpromotionalCampaigns()
        {
            DefpromotionCampaignMembers = new HashSet<DefpromotionCampaignMembers>();
        }

        public Guid Id { get; set; }
        public bool RequiresUnlockCode { get; set; }
        public string UnlockCode { get; set; }
        public Guid? FeaturedSlideId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool Deleted { get; set; }
        public int BenefitReferenceType { get; set; }
        public int BenefitReferenceId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual DeffeaturedSlides FeaturedSlide { get; set; }
        public virtual ICollection<DefpromotionCampaignMembers> DefpromotionCampaignMembers { get; set; }
    }
}
