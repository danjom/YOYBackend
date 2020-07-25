using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserInterestRecords
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid InterestId { get; set; }
        public int InterestType { get; set; }
        public int HerarchyLevel { get; set; }
        public decimal Score { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Hour { get; set; }
        public int Day { get; set; }
        public int DayOfWeek { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public virtual OltpuserInterests OltpuserInterests { get; set; }
    }
}
