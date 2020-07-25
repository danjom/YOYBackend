using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefearningsIncreasers
    {
        public DefearningsIncreasers()
        {
            OltppaymentLogs = new HashSet<OltppaymentLogs>();
            OltppurchasedItems = new HashSet<OltppurchasedItems>();
            Oltppurchases = new HashSet<Oltppurchases>();
            Oltprewards = new HashSet<Oltprewards>();
        }

        public Guid Id { get; set; }
        public Guid ProviderTenantId { get; set; }
        public Guid? ApplierTenantId { get; set; }
        public int Type { get; set; }
        public int AccessType { get; set; }
        public Guid? UnlockerId { get; set; }
        public int UnlockerType { get; set; }
        public string UnlockCode { get; set; }
        public decimal IncreaserFactor { get; set; }
        public int IncreaserFactorType { get; set; }
        public decimal PurchasedAmountBlock { get; set; }
        public decimal? UpperEarningsLimit { get; set; }
        public int UpperEarningsLimitType { get; set; }
        public int MinLevel { get; set; }
        public bool OneTimeUsage { get; set; }
        public string ValidMonthDays { get; set; }
        public string ValidWeekDays { get; set; }
        public string ValidHours { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Deftenants ProviderTenant { get; set; }
        public virtual ICollection<OltppaymentLogs> OltppaymentLogs { get; set; }
        public virtual ICollection<OltppurchasedItems> OltppurchasedItems { get; set; }
        public virtual ICollection<Oltppurchases> Oltppurchases { get; set; }
        public virtual ICollection<Oltprewards> Oltprewards { get; set; }
    }
}
