using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class PaymentRequestPaymentView
    {
        public Guid Id { get; set; }
        public string OpCode { get; set; }
        public string PayerUserId { get; set; }
        public Guid? TenantId { get; set; }
        public string TenantName { get; set; }
        public Guid? BranchId { get; set; }
        public string DispatchBranchName { get; set; }
        public Guid? PaymentLogId { get; set; }
        public int RequestStatus { get; set; }
        public Guid? AppliedUserEarningsIncreaserId { get; set; }
        public decimal? RequestAmount { get; set; }
        public decimal? TotalPaymentAmount { get; set; }
        public decimal? UserEarnedCashbackTotalAmount { get; set; }
        public decimal? PlatformFeeAmount { get; set; }
        public DateTime RequestDate { get; set; }
        public int? CurrencyType { get; set; }
        public int? PaymentStatus { get; set; }
        public DateTime? PaymentLiquidationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
