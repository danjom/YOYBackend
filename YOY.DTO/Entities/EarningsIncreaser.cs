using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class EarningsIncreaser
    {
        public Guid Id { set; get; }
        public Guid ProviderTenantId { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public int AccessType { set; get; }
        public string AccessTypeName { set; get; }
        public Guid? UnlockerId { set; get; }
        public int UnlockerType { set; get; }
        public string UnlockerTypeName { set; get; }
        public decimal IncreaserFactor { set; get; }
        public int IncreaserFactorType { set; get; }
        public string IncreaserFactorTypeName { set; get; }
        public decimal PurchasedAmountBlock { set; get; }
        public decimal? UpperEarningsLimit { set; get; }
        public int UpperEarningsLimitType { set; get; }
        public string UpperEarningsLimitTypeName { set; get; }
        public int MinLevel { set; get; }
        public string MinLevelName { set; get; }
        public bool IsActive { set; get; }
        public bool OneTimeUsage { set; get; }
        public string ValidMonthDays { set; get; }
        public string ValidWeekDays { set; get; }
        public string ValidHours { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime ExpirationDate { set; get; }
    }
}
