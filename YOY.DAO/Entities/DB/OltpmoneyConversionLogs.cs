using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpmoneyConversionLogs
    {
        public Guid Id { get; set; }
        public Guid? OperationId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid PaymentLogId { get; set; }
        public string EmployeeUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ConversionCode { get; set; }
        public decimal RequiredPoints { get; set; }
        public decimal MoneyAmount { get; set; }
        public int State { get; set; }
        public string OwnerId { get; set; }
        public string ClaimerId { get; set; }
        public int InternalStatus { get; set; }
        public DateTime LastStatusUpdate { get; set; }

        public virtual OltpmembershipPointsOperations Operation { get; set; }
        public virtual OltppaymentLogs PaymentLog { get; set; }
        public virtual Deftenants Tenant { get; set; }
    }
}
