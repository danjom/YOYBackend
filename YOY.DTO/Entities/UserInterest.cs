using System;

namespace YOY.DTO.Entities
{
    public class UserInterest
    {
        public string UserId { set; get; }
        public Guid InterestId { set; get; }
        public int InterestType { set; get; }
        public int HerarchyLevel { set; get; }
        public decimal Score { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string InterestName { set; get; }
        public string Icon { set; get; }
        public int OriginType { set; get; }
    }
}
