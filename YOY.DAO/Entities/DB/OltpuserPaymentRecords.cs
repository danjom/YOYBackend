using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserPaymentRecords
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid PaymentLogId { get; set; }
        public decimal PayedAmount { get; set; }
        public bool ConsideredToApplyIncentive { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual OltppaymentLogs PaymentLog { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
