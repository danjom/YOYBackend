using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TemprewardOverviews
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid MainCategoryId { get; set; }
        public int OfferType { get; set; }
        public int DealType { get; set; }
        public int RewardType { get; set; }
        public int PurposeType { get; set; }
        public int DisplayType { get; set; }
        public int GeoSegmentationType { get; set; }
        public string Name { get; set; }
        public string Keywords { get; set; }
        public string MainHint { get; set; }
        public string ComplementaryHint { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal? RegularValue { get; set; }
        public int MinsToUnlock { get; set; }
        public bool IsActive { get; set; }
        public bool IsExclusive { get; set; }
        public bool IsSponsored { get; set; }
        public int AvailableQuantity { get; set; }
        public bool OneTimeRedemption { get; set; }
        public int MaxClaimsPerUser { get; set; }
        public int MinPurchasesCountToRedeem { get; set; }
        public DateTime? PurchasesCountStartDate { get; set; }
        public Guid? DisplayImageId { get; set; }
        public int RedeemCount { get; set; }
        public int ClaimCount { get; set; }
        public double RelevanceRate { get; set; }
        public string ClaimLocation { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public string ClaimInstructions { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid RaffleId { get; set; }
        public string Notes { get; set; }
        public DateTime RaffleDate { get; set; }
        public int RaffleType { get; set; }
        public int RewardUsageType { get; set; }
        public int RewardMinMembershipLevel { get; set; }
        public int RewardTimeOutDaysBetweenRedemption { get; set; }
        public Guid? RewardEarningsIncreaserId { get; set; }
        public int? EarningsIncreaserAccessType { get; set; }
        public int? EarningsIncreaserType { get; set; }
        public DateTime? EarningsIncreaserReleaseDate { get; set; }
        public DateTime? EarningsIncreaserExpirationDate { get; set; }
        public int? EarningsIncreaserMinLevel { get; set; }
        public double? EarningsIncreaserFactor { get; set; }
        public int? WinnersCount { get; set; }
        public string CategoryName { get; set; }
    }
}
