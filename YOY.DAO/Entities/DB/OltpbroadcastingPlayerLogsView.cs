using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpbroadcastingPlayerLogsView
    {
        public Guid Id { get; set; }
        public DateTime EventDate { get; set; }
        public int EventType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid BroadcasterId { get; set; }
        public string BroadcasterName { get; set; }
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeName { get; set; }
    }
}
