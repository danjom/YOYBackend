using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities.Misc.CashIncentive
{
    public class DisplayCashIncentiveData
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public int ApplyType { set; get; }
        public int BenefitAmountType { set; get; }
        public int DisplayType { set; get; }
        public int Type { set; get; }
        public int DealType { set; get; }
        public int MaxCombinedIncentives { set; get; }
        public string MainHint { set; get; }
        public string ComplementaryHint { set; get; }
        public decimal UnitValue { set; get; }
        public decimal PreviousUnitValue { set; get; }
        public int MinMembershipLevel { set; get; }
        public decimal MinPurchasedAmount { set; get; }
        public decimal PurchasedAmountBlock { set; get; }
        public decimal MaxValue { set; get; }
        public int AvailableQuantity { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Keywords { set; get; }
        public bool IsSponsored { set; get; }
        public bool IsActive { set; get; }
        public string ValidWeekDays { set; get; }
        public string ValidMonthDays { set; get; }
        public string ValidHours { set; get; }
        public int MaxUsagePerUser { set; get; }
        public DateTime? PurchasesCountStartDate { set; get; }
        public int MinPurchasesCountToUse { set; get; }
        public decimal MinPurchasedTotalAmount { set; get; }
        public int UsageCount { set; get; }
        public double RelevanceRate { set; get; }
        public int GeoSegmentationType { set; get; }
        public string Rules { set; get; }
        public string Conditions { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public DateTime CreatedDate { set; get; }
    }
}
