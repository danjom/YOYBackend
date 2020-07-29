using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.CashbackIncentive.POCO
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
        public int DisplayType { set; get; }
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
        public double UnitValue { set; get; }
        [NotNull]
        [Required]
        public double PreviousUnitValue { set; get; }
        [NotNull]
        [Required]
        public int MinMembershipLevel { set; get; }
        [NotNull]
        [Required]
        public decimal MinPurchasedAmount { set; get; }
        [NotNull]
        [Required]
        public decimal PurchasedAmountBlock { set; get; }
        [NotNull]
        [Required]
        public decimal MaxValue { set; get; }
        [NotNull]
        [Required]
        public int AvailableQuantity { set; get; }
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
        public string ValidWeekDays { set; get; }
        [NotNull]
        [Required]
        public string ValidMonthDays { set; get; }
        [NotNull]
        [Required]
        public string ValidHours { set; get; }
        [NotNull]
        [Required]
        public int MaxUsagePerUser { set; get; }
        [NotNull]
        [Required]
        public int MinPurchaseCountToUse { set; get; }
        [AllowNull]
        public int? RelevanceRate { set; get; }
        [NotNull]
        [Required]
        public DateTime ReleaseDate { set; get; }
        [NotNull]
        [Required]
        public DateTime ExpirationDate { set; get; }

    }
}
