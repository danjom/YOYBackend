using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpclaimRecordLines
    {
        public Guid Id { get; set; }
        public Guid RecordId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public int RecordNumber { get; set; }
        public int DeliveryType { get; set; }
        public Guid TransactionId { get; set; }
        public string ClaimRefCode { get; set; }
        public bool Validated { get; set; }
        public Guid? ReceiptId { get; set; }
        public DateTime? ValidationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltptransactions Transaction { get; set; }
    }
}
