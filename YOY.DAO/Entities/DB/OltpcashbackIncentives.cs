using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpcashbackIncentives
    {
        public OltpcashbackIncentives()
        {
            OltppaymentLogs = new HashSet<OltppaymentLogs>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public int EarningType { get; set; }
        public int Type { get; set; }
        public int DealType { get; set; }
        public int CombineType { get; set; }
        public decimal UnitValue { get; set; }
        public decimal PreviousUnitValue { get; set; }
        public decimal MinPurchasedAmount { get; set; }
        public decimal PurchasedAmountBlock { get; set; }
        public decimal MaxValue { get; set; }
        public int AvailableQuantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public bool IsSponsored { get; set; }
        public bool IsActive { get; set; }
        public int PublishingStatus { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ValidWeekDays { get; set; }
        public string ValidMonthDays { get; set; }
        public string ValidHours { get; set; }
        public int MaxUsagesPerUser { get; set; }
        public int UsageCount { get; set; }
        public int GeoSegmentationType { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public string ClaimInstructions { get; set; }
        public bool OneTimeUsagePerUser { get; set; }
        public int MinPurchasesCountToUse { get; set; }
        public DateTime? PurchasesCountStartDate { get; set; }
        public int MinPurchasesHoursTimeoutToUse { get; set; }
        public int MaxPurchasesDaysTimeoutToUse { get; set; }
        public double RelevanceRate { get; set; }

        public virtual Deftenants Tenant { get; set; }
        public virtual ICollection<OltppaymentLogs> OltppaymentLogs { get; set; }
    }
}
