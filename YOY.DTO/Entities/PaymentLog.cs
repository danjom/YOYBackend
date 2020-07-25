using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class PaymentLog
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid? BranchId { set; get; }
        public string UserId { set; get; }
        public Guid? LiquidationMoneyTransferId { set; get; }
        public Guid? ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public string ReferenceTypeName { set; get; }
        public int CurrencyType { set; get; }
        public string CurrencyTypeName { set; get; }
        public decimal PaymentAmount { set; get; }
        public decimal DebitedAmount { set; get; }
        public bool CashbackUsedAsPayment { set; get; }
        public bool CashbackIncentiveApplied { set; get; }
        public Guid? AppliedCashbackIncentiveId { set; get; }
        public bool UserEarningsIncreaserApplied { set; get; }
        public Guid? AppliedUserEarningsIncreaserId { set; get; }
        public decimal? EarningsIncreasementAmount { set; get; }
        public double CashbackPercentage { set; get; }
        public decimal CashbackTotalAmount { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public int ResultCode { set; get; }
        public string ResultCodeName { set; get; }
        public string ResultMessage { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime? LiquidationDate { set; get; }
        public int PaymentGateway { set; get; }
        public string PaymentGatewayName { set; get; }
        public Guid? PaymentInfoId { set; get; }
    }
}
