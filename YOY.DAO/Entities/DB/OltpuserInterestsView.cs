using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserInterestsView
    {
        public Guid InterestId { get; set; }
        public int InterestType { get; set; }
        public string UserId { get; set; }
        public int HerarchyLevel { get; set; }
        public decimal Score { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long AdditionRecordsCount { get; set; }
        public long SubstractionRecordsCount { get; set; }
        public bool IsActive { get; set; }
        public int OriginType { get; set; }
        public long AccountNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? StateId { get; set; }
    }
}
