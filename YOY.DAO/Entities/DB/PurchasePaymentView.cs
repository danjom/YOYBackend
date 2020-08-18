using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class PurchasePaymentView
    {
        public Guid Id { get; set; }
        public string PurchaseCode { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public Guid? DispatchBranchId { get; set; }
        public string DispatchBranchName { get; set; }
        public Guid PaymentLogId { get; set; }
        public int PurchaseStatus { get; set; }
        public int DealType { get; set; }
        public int DeliveryType { get; set; }
        public Guid? AppliedUserEarningsIncreaserId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTenantEarnings { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal? TotalPaymentAmount { get; set; }
        public decimal? UserEarnedCashbackTotalAmount { get; set; }
        public decimal? PlatformFeeAmount { get; set; }
        public int? CurrencyType { get; set; }
        public int? PaymentStatus { get; set; }
        public DateTime? PaymentLiquidationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
