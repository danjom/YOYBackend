using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpreceiptRequestedValidations
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public int Status { get; set; }
        public bool Validated { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpreceipts Receipt { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
