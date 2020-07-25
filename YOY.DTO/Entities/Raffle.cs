using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class Raffle
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid RewardId { set; get; }
        public string RewardName { set; get; }
        public DateTime RaffleDate { set; get; }
        public bool IsActive { set; get; }
        public string Notes { set; get; }
        public int Type { set; get; }
        public int UsageType { set; get; }
        public int MinMembershipLevel { set; get; }
        public int TimeOutDaysBetweenRedemption { set; get; }
        public string MinMembershipLevelName { set; get; }
        public Guid? EarningsIncreaserId { set; get; }
        public decimal IncreaserFactor { set; get; } 
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
