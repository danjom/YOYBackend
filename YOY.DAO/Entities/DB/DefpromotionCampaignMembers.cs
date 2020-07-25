using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefpromotionCampaignMembers
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid PromotionalCampaignId { get; set; }
        public int BenefitReferenceType { get; set; }
        public Guid? BenefitReferenceId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual DefpromotionalCampaigns PromotionalCampaign { get; set; }
    }
}
