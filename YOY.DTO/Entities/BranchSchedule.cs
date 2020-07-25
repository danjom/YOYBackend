using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class BranchSchedule
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid BranchId { set; get; }
        public int FromDay { set; get; }
        public int ToDay { set; get; }
        public int FromHour { set; get; }
        public int FromMinutes { set; get; }
        public int ToHour { set; get; }
        public int ToMinutes { set; get; }

        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }

    }
}
