using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.CashIncentive.POCO
{
    public class NewIncentive
    {
        [NotNull]
        [Required]
        public Guid TenantId { set; get; }
        [NotNull]
        [Required]
        public Guid EmployeeId { set; get; }
        [NotNull]
        [Required]
        public string UserId { set; get; }
        [NotNull]
        [Required]
        public int ApplyType { set; get; }
        [NotNull]
        [Required]
        public int BenefitAmountType { set; get; }
        [NotNull]
        [Required]
        public int Type { set; get; }
        [NotNull]
        [Required]
        public int DealType { set; get; }
        [NotNull]
        [Required]
        public int MaxCombinedIncentives { set; get; }
        [NotNull]
        [Required]
        public string UnitValue { set; get; }
        [NotNull]
        [Required]
        public string PreviousUnitValue { set; get; }
        [NotNull]
        [Required]
        public int MinMembershipLevel { set; get; }
        [NotNull]
        [Required]
        public string MinPurchasedAmount { set; get; }
        [NotNull]
        [Required]
        public string PurchasedAmountBlock { set; get; }
        [NotNull]
        [Required]
        public int AvailableQuantity { set; get; }
        [NotNull]
        [Required]
        public string MainHint { set; get; }
        [NotNull]
        [Required]
        public string ComplementaryHint { set; get; }
        [NotNull]
        [Required]
        public string Name { set; get; }
        [NotNull]
        [Required]
        public string Description { set; get; }
        [NotNull]
        [Required]
        public string Keywords { set; get; }
        [NotNull]
        [Required]
        public bool IsSponsored { set; get; }
        [NotNull]
        [Required]
        public List<string> ValidWeekDays { set; get; }
        [NotNull]
        [Required]
        public List<string> ValidMonthDays { set; get; }
        [NotNull]
        [Required]
        public List<string> ValidHours { set; get; }
        [NotNull]
        [Required]
        public int MaxUsagePerUser { set; get; }
        [AllowNull]
        public DateTime? PurchasesCountStartDate { set; get; }
        [NotNull]
        [Required]
        public int MinPurchaseCountToUse { set; get; }
        [NotNull]
        [Required]
        public string MinPurchasedTotalAmount { set; get; }
        [AllowNull]
        public string RelevanceRate { set; get; }
        [NotNull]
        [Required]
        public DateTime ReleaseDate { set; get; }
        [NotNull]
        [Required]
        public DateTime ExpirationDate { set; get; }

    }
}
