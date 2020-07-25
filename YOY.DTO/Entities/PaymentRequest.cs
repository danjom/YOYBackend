using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities
{
    public class PaymentRequest
    {
        public Guid Id { set; get; }
        public string OpCode { set; get; }
        public Guid? TenantId { set; get; }
        public Guid? BranchId { set; get; }
        public int SourceType { set; get; }
        public string SourceTypeName { set; get; }
        public Guid? SourceId { set; get; }
        public string PayerUserId { set; get; }
        public Guid? PaymentLogId { set; get; }
        public decimal? Amount { set; get; }
        public int CurrencyType { set; get; }
        public string CurrencyTypeName { set; get; }
        public string CurrencySymbol { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public DateTime ExpiratinDate { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
