using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpmemberships
    {
        public Oltpmemberships()
        {
            Oltpemployees = new HashSet<Oltpemployees>();
            OltpmembershipPointsOperationsBeneficiaryMembership = new HashSet<OltpmembershipPointsOperations>();
            OltpmembershipPointsOperationsProviderMembership = new HashSet<OltpmembershipPointsOperations>();
            OltpmoneyWithdrawals = new HashSet<OltpmoneyWithdrawals>();
            OltpvalidatePurchaseRegistries = new HashSet<OltpvalidatePurchaseRegistries>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string UserId { get; set; }
        public int MembershipLevel { get; set; }
        public decimal UsedLoyaltyPoints { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool Blocked { get; set; }
        public int OriginType { get; set; }
        public int ClaimedPromos { get; set; }
        public int ClaimedRewards { get; set; }
        public DateTime? ClaimedRewardsStartDate { get; set; }
        public DateTime? LastPromoClaimed { get; set; }
        public DateTime? LastPromoReserved { get; set; }
        public DateTime LastLevelEvaluation { get; set; }
        public bool? ReceiveSmsmarketing { get; set; }
        public bool? ReceiveEmailMarketing { get; set; }
        public int CustomerRanking { get; set; }

        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Oltpemployees> Oltpemployees { get; set; }
        public virtual ICollection<OltpmembershipPointsOperations> OltpmembershipPointsOperationsBeneficiaryMembership { get; set; }
        public virtual ICollection<OltpmembershipPointsOperations> OltpmembershipPointsOperationsProviderMembership { get; set; }
        public virtual ICollection<OltpmoneyWithdrawals> OltpmoneyWithdrawals { get; set; }
        public virtual ICollection<OltpvalidatePurchaseRegistries> OltpvalidatePurchaseRegistries { get; set; }
    }
}
