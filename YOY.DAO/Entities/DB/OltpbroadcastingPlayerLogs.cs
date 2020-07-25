using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpbroadcastingPlayerLogs
    {
        public Guid Id { get; set; }
        public Guid BroadcasterId { get; set; }
        public Guid BranchId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime EventDate { get; set; }
        public int EventType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defbranches Branch { get; set; }
        public virtual Defdepartments Department { get; set; }
        public virtual Oltpemployees Employee { get; set; }
    }
}
