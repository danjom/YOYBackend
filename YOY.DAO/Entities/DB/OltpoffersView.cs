using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpoffersView
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid MainCategoryId { get; set; }
        public int OfferType { get; set; }
        public int DealType { get; set; }
        public int RewardType { get; set; }
        public int PurposeType { get; set; }
        public int GeoSegmentationType { get; set; }
        public int DisplayType { get; set; }
        public string Name { get; set; }
        public string ProductHint { get; set; }
        public string MainHint { get; set; }
        public string ComplementaryHint { get; set; }
        public string Keywords { get; set; }
        public string Code { get; set; }
        public Guid? CodeImg { get; set; }
        public string Description { get; set; }
        public int MinsToUnlock { get; set; }
        public bool IsActive { get; set; }
        public bool IsExclusive { get; set; }
        public bool IsSponsored { get; set; }
        public bool HasUniqueCodes { get; set; }
        public bool HasPreferences { get; set; }
        public int AvailableQuantity { get; set; }
        public bool OneTimeRedemption { get; set; }
        public int MaxClaimsPerUser { get; set; }
        public int MinPurchasesCountToRedeem { get; set; }
        public DateTime? PurchasesCountStartDate { get; set; }
        public string ClaimLocation { get; set; }
        public decimal Value { get; set; }
        public decimal? RegularValue { get; set; }
        public double ExtraBonus { get; set; }
        public int ExtraBonusType { get; set; }
        public decimal MinIncentive { get; set; }
        public decimal MaxIncentive { get; set; }
        public decimal? IncentiveVariation { get; set; }
        public int IncentiveVariationType { get; set; }
        public int ClaimCount { get; set; }
        public int RedeemCount { get; set; }
        public Guid? DisplayImageId { get; set; }
        public string DisplayImageUrl { get; set; }
        public string TargettingParams { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public string ClaimInstructions { get; set; }
        public DateTime? LastBroadcastingUsage { get; set; }
        public int BroadcastingTimerType { get; set; }
        public int BroadcastingScheduleType { get; set; }
        public int BroadcastingMinsToRedeem { get; set; }
        public string BroadcastingTitle { get; set; }
        public string BroadcastingMsg { get; set; }
        public double RelevanceRate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CategoryName { get; set; }
        public Guid? CategoryParentId { get; set; }
        public int? CategoryRelevanceStatus { get; set; }
    }
}
