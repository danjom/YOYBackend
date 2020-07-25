using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserInterests
    {
        public OltpuserInterests()
        {
            OltpuserInterestRecords = new HashSet<OltpuserInterestRecords>();
        }

        public string UserId { get; set; }
        public Guid InterestId { get; set; }
        public int InterestType { get; set; }
        public int HerarchyLevel { get; set; }
        public decimal Score { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long AdditionRecordsCount { get; set; }
        public long SubstractionRecordsCount { get; set; }
        public bool? IsActive { get; set; }
        public int OriginType { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<OltpuserInterestRecords> OltpuserInterestRecords { get; set; }
    }
}
