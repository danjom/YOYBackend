using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DeftenantMembershipLevels
    {
        public Guid TenantId { get; set; }
        public Guid LevelId { get; set; }
        public double LoyaltyCashBackPercentage { get; set; }
        public double MonetaryConversionFactor { get; set; }
        public int MinGeneratedPoints { get; set; }
        public int MaxGeneratedPoints { get; set; }
        public int MinPurchasesCount { get; set; }
        public int MaxPurchasesCount { get; set; }
        public int MaxRewardRedemptions { get; set; }
        public int EvaluationMonths { get; set; }
        public string EnabledActions { get; set; }
        public int PointsLifeSpanMonths { get; set; }
        public int? CheckInPoints { get; set; }
        public bool? IsActive { get; set; }
        public bool? PointsToMoneyEnabled { get; set; }
        public string EnabledMoneyAmounts { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual DefmembershipLevels Level { get; set; }
        public virtual Deftenants Tenant { get; set; }
    }
}
