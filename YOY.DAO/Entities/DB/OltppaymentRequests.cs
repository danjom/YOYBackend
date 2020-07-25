using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltppaymentRequests
    {
        public Guid Id { get; set; }
        public string OpCode { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public int SourceType { get; set; }
        public Guid? SourceId { get; set; }
        public string PayerUserId { get; set; }
        public Guid? PaymentLogId { get; set; }
        public decimal? Amount { get; set; }
        public int CurrencyType { get; set; }
        public string CurrencySymbol { get; set; }
        public int Status { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defbranches Branch { get; set; }
        public virtual OltppaymentLogs PaymentLog { get; set; }
    }
}
