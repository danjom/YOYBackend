using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltprewards
    {
        public Oltprewards()
        {
            OltpraffleWinners = new HashSet<OltpraffleWinners>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public DateTime RaffleDate { get; set; }
        public Guid RewardId { get; set; }
        public int MinMembershipLevel { get; set; }
        public int TimeOutDaysBetweenRedemption { get; set; }
        public Guid? EarningsIncreaserId { get; set; }
        public bool? IsActive { get; set; }
        public bool Deleted { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int UsageType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual DefearningsIncreasers EarningsIncreaser { get; set; }
        public virtual Oltpoffers Reward { get; set; }
        public virtual ICollection<OltpraffleWinners> OltpraffleWinners { get; set; }
    }
}
