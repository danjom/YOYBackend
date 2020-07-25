using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpclaimRecordLinesView
    {
        public Guid Id { get; set; }
        public Guid RecordId { get; set; }
        public string ClaimRefCode { get; set; }
        public Guid TransactionId { get; set; }
        public Guid TenantId { get; set; }
        public int RecordNumber { get; set; }
        public int DeliveryType { get; set; }
        public bool Validated { get; set; }
        public DateTime? ValidationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Name { get; set; }
        public int DealType { get; set; }
        public Guid? CategoryId { get; set; }
        public int TransactionType { get; set; }
    }
}
