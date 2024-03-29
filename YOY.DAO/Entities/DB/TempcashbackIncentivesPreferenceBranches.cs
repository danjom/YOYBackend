﻿using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempcashbackIncentivesPreferenceBranches
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public int ApplyType { get; set; }
        public int BenefitAmountType { get; set; }
        public int DisplayType { get; set; }
        public int Type { get; set; }
        public int DealType { get; set; }
        public int MaxCombinedIncentives { get; set; }
        public decimal UnitValue { get; set; }
        public decimal PreviousUnitValue { get; set; }
        public int MinMembershipLevel { get; set; }
        public decimal MinPurchasedAmount { get; set; }
        public decimal PurchasedAmountBlock { get; set; }
        public decimal MaxValue { get; set; }
        public int AvailableQuantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public bool IsSponsored { get; set; }
        public bool IsActive { get; set; }
        public string ValidWeekDays { get; set; }
        public string ValidMonthDays { get; set; }
        public string ValidHours { get; set; }
        public int MaxUsagesPerUser { get; set; }
        public int MinPurchasesCountToUse { get; set; }
        public int UsageCount { get; set; }
        public double RelevanceRate { get; set; }
        public int GeoSegmentationType { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
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
