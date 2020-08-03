using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefmembershipLevelsView
    {
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public Guid? TenantLogo { get; set; }
        public Guid? TenantThumbnail { get; set; }
        public bool HasMembershipLevels { get; set; }
        public string CurrencySymbol { get; set; }
        public bool AcceptsCommunityPointsAsPayment { get; set; }
        public bool AcceptsSelfPointsAsPayment { get; set; }
        public int LoyaltyProgramType { get; set; }
        public int TenantType { get; set; }
        public Guid TenantCategoryId { get; set; }
        public Guid LevelId { get; set; }
        public string LevelName { get; set; }
        public int LevelPos { get; set; }
        public string IconUrl { get; set; }
        public string EnabledActions { get; set; }
        public double? LoyaltyCashBackPercentage { get; set; }
        public int? MinGeneratedPoints { get; set; }
        public int? MaxGeneratedPoints { get; set; }
        public int? MinPurchasesCount { get; set; }
        public int? MaxPurchasesCount { get; set; }
        public int? MaxRewardRedemptions { get; set; }
        public int? EvaluationMonths { get; set; }
        public int? PointsLifeSpanMonths { get; set; }
        public int? CheckInPoints { get; set; }
        public double? MonetaryConversionFactor { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? PointsToMoneyEnabled { get; set; }
        public string EnabledMoneyAmounts { get; set; }
    }
}
