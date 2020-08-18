using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltppurchases
    {
        public Oltppurchases()
        {
            OltppurchaseDeliveryDetails = new HashSet<OltppurchaseDeliveryDetails>();
            OltppurchasedItems = new HashSet<OltppurchasedItems>();
        }

        public Guid Id { get; set; }
        public string PurchaseCode { get; set; }
        public string PurchaseNumericCode { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? DispatchBranchId { get; set; }
        public Guid? DispatchValidationSourceId { get; set; }
        public int? DispatchValidationSourceType { get; set; }
        public Guid PaymentLogId { get; set; }
        public int Status { get; set; }
        public int DealType { get; set; }
        public int DeliveryType { get; set; }
        public Guid? AppliedUserEarningsIncreaserId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTenantEarnings { get; set; }
        public double TotalCashbackPercentage { get; set; }
        public decimal TotalCashbackTotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual DefearningsIncreasers AppliedUserEarningsIncreaser { get; set; }
        public virtual Defbranches DispatchBranch { get; set; }
        public virtual OltppaymentLogs PaymentLog { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OltppurchaseDeliveryDetails> OltppurchaseDeliveryDetails { get; set; }
        public virtual ICollection<OltppurchasedItems> OltppurchasedItems { get; set; }
    }
}
