using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.CashIncentive.POCO
{
    public class IncentiveData
    {
        [Required]
        public Guid Id { set; get; }
        [Required]
        public Guid TenantId { set; get; }
        [Required]
        public int ApplyType { set; get; }
        [Required]
        public int BenefitAmountType { set; get; }
        [Required]
        public string BenefitAmountTypeName { set; get; }
        [Required]
        public int DisplayType { set; get; }
        [Required]
        public string DisplayTypeName { set; get; }
        [Required]
        public int Type { set; get; }
        [Required]
        public string TypeName { set; get; }
        [Required]
        public int DealType { set; get; }
        [Required]
        public string DealTypeName { set; get; }
        [Required]
        public int MaxCombinedIncentives { set; get; }
        [Required]
        public decimal UnitValue { set; get; }
        [AllowNull]
        public decimal? PreviousUnitValue { set; get; }
        [Required]
        public int MinMembershipLevel { set; get; }
        [Required]
        public string MinMembershipLevelName { set; get; }
        [Required]
        public decimal MinPurchasedAmount { set; get; }
        [AllowNull]
        public decimal? PurchasedAmountBlock { set; get; }
        [Required]
        public decimal? MaxValue { set; get; }
        [Required]
        public int AvailableQuantity { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string MainHint { set; get; }
        [Required]
        public string ComplementaryHint { set; get; }
        [Required]
        public string Description { set; get; }
        [Required]
        public string Keywords { set; get; }
        [Required]
        public bool IsSponsored { set; get; }
        [Required]
        public bool IsActive { set; get; }
        [Required]
        public string ValidWeekDays { set; get; }
        [Required]
        public string ValidMonthDays { set; get; }
        [Required]
        public string ValidHours { set; get; }
        [Required]
        public int MaxUsagePerUser { set; get; }
        [Required]
        public int MinPurchasesCountToUse { set; get; }
        [Required]
        public decimal MinPurchasedTotalAmount { set; get; }
        [Required]
        public int UsageCount { set; get; }
        [Required]
        public int GeoSegmentationType { set; get; }
        [Required]
        public string GeoSegmentationTypeName { set; get; }
        [Required]
        public string Rules { set; get; }
        [Required]
        public string Conditions { set; get; }
        [AllowNull]
        public double? RelevanceRate { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseFullDateTime { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public string ReleaseDateComponent { set; get; }
        [Required]
        public string ReleaseHourComponent { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationFullDateTime { set; get; }
        [Required]
        [DataType(DataType.DateTime)]
        public string ExpirationDateComponent { set; get; }
        [Required]
        public string ExpirationHourComponent { set; get; }
        [AllowNull]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { set; get; }
        [AllowNull]
        public string PublishState { set; get; }
    }
}
