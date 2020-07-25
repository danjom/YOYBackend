using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempoffersPreferenceBranches
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
        public string MainHint { get; set; }
        public string ComplementaryHint { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public int MinsToUnlock { get; set; }
        public bool IsActive { get; set; }
        public bool IsExclusive { get; set; }
        public bool IsSponsored { get; set; }
        public bool HasPreferences { get; set; }
        public decimal Value { get; set; }
        public decimal? RegularValue { get; set; }
        public double ExtraBonus { get; set; }
        public int ExtraBonusType { get; set; }
        public int AvailableQuantity { get; set; }
        public bool OneTimeRedemption { get; set; }
        public int MaxClaimsPerUser { get; set; }
        public int MinPurchasesCountToRedeem { get; set; }
        public DateTime? PurchasesCountStartDate { get; set; }
        public int RedeemCount { get; set; }
        public int ClaimCount { get; set; }
        public string ClaimLocation { get; set; }
        public Guid? DisplayImageId { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public string ClaimInstructions { get; set; }
        public double RelevanceRate { get; set; }
        public string TargettingParams { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal? RelevanceScore { get; set; }
        public double? SatisfactionScore { get; set; }
        public string TenantName { get; set; }
        public Guid TenantLogo { get; set; }
        public Guid TenantCountryId { get; set; }
        public int TenantType { get; set; }
        public Guid TenantCategoryId { get; set; }
        public string TenantCategoryName { get; set; }
        public int TenantRelevanceStatus { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal? TenantScore { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchInquiriesPhoneNumber { get; set; }
        public string BranchDescriptiveAddress { get; set; }
        public Guid BranchCityId { get; set; }
        public Guid BranchStateId { get; set; }
        public decimal BranchLatitude { get; set; }
        public decimal BranchLongitude { get; set; }
        public double? Distance { get; set; }
        public Guid PreferenceId { get; set; }
        public string PreferenceName { get; set; }
        public string PreferenceIcon { get; set; }
        public decimal? PreferenceScore { get; set; }
    }
}
