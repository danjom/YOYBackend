using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempmembershipDetails
    {
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public Guid TenantCategoryId { get; set; }
        public bool AcceptsCommunityPointsAsPayment { get; set; }
        public bool AcceptsSelfPointsAsPayment { get; set; }
        public int LoyaltyProgramType { get; set; }
        public string TenantCurrencySymbol { get; set; }
        public bool TenantHasMembershipLevels { get; set; }
        public Guid? TenantLandingImg { get; set; }
        public Guid? TenantLogo { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchPhoneNumber { get; set; }
        public Guid? MembershipId { get; set; }
        public Guid? MembershipTenantId { get; set; }
        public int? CurrentMembershipLevel { get; set; }
        public decimal? MembershipUsedPoints { get; set; }
        public bool? IsBlockedMembership { get; set; }
        public bool? IsActiveMembership { get; set; }
        public int? CustomerRanking { get; set; }
        public DateTime? MembershipLevelLastEvaluation { get; set; }
        public int? ClaimedRewards { get; set; }
        public DateTime? ClaimedRewardsStartDate { get; set; }
        public string UserId { get; set; }
        public long? AccountNumber { get; set; }
        public string AccountCode { get; set; }
        public string UserName { get; set; }
        public bool? EmailConfirmed { get; set; }
        public Guid LevelId { get; set; }
        public int LevelPos { get; set; }
        public string LevelName { get; set; }
        public string LevelIconUrl { get; set; }
        public double LevelLoyaltyCashBackPercentage { get; set; }
        public double LevelMonetaryConversionFactor { get; set; }
        public int LevelMinGeneratedPoints { get; set; }
        public int LevelMaxGeneratedPoints { get; set; }
        public int LevelMinPurchasesCount { get; set; }
        public int LevelMaxPurchasesCount { get; set; }
        public int LevelMaxRewardRedemptions { get; set; }
        public int LevelEvaluationMonts { get; set; }
        public string LevelEnabledActions { get; set; }
        public int LevelPointsLifeSpanMonths { get; set; }
        public int LevelCheckInPoints { get; set; }
        public bool LevelPointsToMoneyEnabled { get; set; }
        public string EnabledMoneyAmounts { get; set; }
    }
}
