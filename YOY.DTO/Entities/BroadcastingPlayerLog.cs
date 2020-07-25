using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class BroadcastingPlayerLog
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string TenantName { set; get; }
        public Guid BeaconId { set; get; }
        public string BeaconName { set; get; }
        public Guid BranchId { set; get; }
        public string BranchName { set; get; }
        public Guid DepartmentId { set; get; }
        public string DepartmentName { set; get; }
        public Guid? EmployeeId { set; get; }
        public string EmployeeName { set; get; }
        public string EmployeeEmail { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime EventDate { set; get; }
        public int EventType { set; get; }
        public string EventTypeName { set; get; }

    }
}
