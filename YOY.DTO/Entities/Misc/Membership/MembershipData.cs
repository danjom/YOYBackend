using System;

namespace YOY.DTO.Entities.Misc.Membership
{
    public class MembershipData
    {
        public Guid? Id { set; get; }
        public Guid? TenantId { set; get; }
        public int? CurrentLevel { set; get; }
        public string CurrentLevelName { set; get; }
        public DateTime? LastLevelEvaluation { set; get; }
        public int? DaysForNextEvaluation { set; get; }
        public decimal? UsedPoints { set; get; }
        public bool? IsBlocked { set; get; }
        public bool? IsActive { set; get; }
        public int? CustomerRanking { set; get; }
        public string UserId { set; get; }
        public long? AccountNumber { set; get; }
        public string AccountCode { set; get; }
        public string UserName { set; get; }
        public bool? EmailConfirmed { set; get; }
        public bool PointsToMoneyEnabled { set; get; }
        public int UserGeneratedPoints { set; get; }
        public int MinGeneratedPoints { set; get; }
        public int MaxGeneratedPoints { set; get; }
        public int UserPurchasesCount { set; get; }
        public int MinPurchasesCount { set; get; }
        public int MaxPurchasesCount { set; get; }
        public int MaxRewardRedemptions { set; get; }
        public double MonetaryConversionFactor { set; get; }
        public long AvailablePoints { set; get; }
        public int AvailablePurchasesCount { set; get; }
        public long SoonToExpirePoints { set; get; }
        public string EnabledMoneyAmounts { set; get; }
        public int? ClaimedRewards { set; get; }
        public DateTime? ClaimedRewardsStartDate { set; get; }
    }
}
