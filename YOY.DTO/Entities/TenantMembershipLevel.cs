using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class TenantMembershipLevel
    {
        public Guid TenantId { set; get; }
        public string TenantName { set; get; }
        public Guid? TenantLogo { set; get; }
        public Guid TenantCategoryId { set; get; }
        public bool HasMembershipLevels { set; get; }
        public string CurrencySymbol { set; get; }
        public Guid LevelId { set; get; }
        public string LevelName { set; get; }
        public bool AcceptCommunityPointsAsPayment { set; get; }
        public bool AcceptsSelfPointsAsPayment { set; get; }
        public int LoyaltyProgramType { set; get; }
        public int LevelPos { set; get; }
        public string IconUrl { set; get; }
        public string EnabledActions { set; get; }
        public double? LoyaltyCashBackPercentage { set; get; } 
        public int? MinGeneratedPoints { set; get; }
        public int? MaxGeneratedPoints { set; get; }
        public int? MinPurchasesCount { set; get; }
        public int? MaxPurchasesCount { set; get; }
        public int? MaxRewardRedemptions { set; get; }
        public int? EvaluationMonths { set; get; }
        public int? PointsLifeSpanMonths { set; get; }
        public double? MonetaryConversionFactor { set; get; }
        public int? CheckInPoints { set; get; }
        public bool? IsActive { set; get; }
        public bool PointsToMoneyEnabled { set; get; }
        public string EnabledMoneyAmounts { set; get; }
        public DateTime? CreatedDate { set; get; }
        public DateTime? UpdatedDate { set; get; }

    }
}
