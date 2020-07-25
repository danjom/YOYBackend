using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefbranchSchedules
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid BranchId { get; set; }
        public int FromDay { get; set; }
        public int ToDay { get; set; }
        public int FromHour { get; set; }
        public int FromMinutes { get; set; }
        public int ToHour { get; set; }
        public int ToMinutes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defbranches Branch { get; set; }
    }
}
