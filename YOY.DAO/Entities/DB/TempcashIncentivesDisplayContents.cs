using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempcashIncentivesDisplayContents
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public int ApplyType { get; set; }
        public int BenefitAmountType { get; set; }
        public int DisplayType { get; set; }
        public int Type { get; set; }
        public int DealType { get; set; }
        public string MainHint { get; set; }
        public string ComplementaryHint { get; set; }
        public decimal UnitValue { get; set; }
        public decimal PreviousUnitValue { get; set; }
        public int MinMembershipLevel { get; set; }
        public decimal MinPurchasedAmountToBeApplied { get; set; }
        public decimal PurchasedAmountBlock { get; set; }
        public decimal MaxValue { get; set; }
        public int AvailableQuantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public bool AppliesToInAppPurchases { get; set; }
        public bool IsSponsored { get; set; }
        public bool IsActive { get; set; }
        public string ValidWeekDays { get; set; }
        public string ValidHours { get; set; }
        public int MaxUsagesPerUser { get; set; }
        public DateTime? PurchasesCountStartDate { get; set; }
        public int MinPurchasesCountToUse { get; set; }
        public decimal MinPurchasedAmountToCount { get; set; }
        public int UsageCount { get; set; }
        public double RelevanceRate { get; set; }
        public int GeoSegmentationType { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TenantName { get; set; }
        public string TenantLogoUrl { get; set; }
        public Guid TenantCountryId { get; set; }
        public int TenantType { get; set; }
        public Guid TenantCategoryId { get; set; }
        public int TenantRelevanceStatus { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal? TenantScore { get; set; }
        public Guid? BranchHolderId { get; set; }
        public string TenantHolderName { get; set; }
        public string BranchHolderName { get; set; }
        public int? TenantHolderRelevanceStatus { get; set; }
    }
}
