using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.PaymentBalance.POCO
{
    public class ReportRequest
    {
        public Guid TenantId { set; get; }
        public Guid BranchId { set; get; }
        public Guid EmployeeId { set; get; }
        public string UserId { set; get; }
        public Guid TransferId { set; get; }
    }
}
