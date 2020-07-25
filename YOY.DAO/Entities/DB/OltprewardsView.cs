using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltprewardsView
    {
        public Guid Id { get; set; }
        public Guid RewardId { get; set; }
        public Guid TenantId { get; set; }
        public DateTime RaffleDate { get; set; }
        public int MinMembershipLevel { get; set; }
        public int TimeOutDaysBetweenRedemption { get; set; }
        public Guid? EarningsIncreaserId { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int UsageType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string RewardName { get; set; }
        public string RewardDescription { get; set; }
        public int DealType { get; set; }
        public Guid? RewardDisplayImg { get; set; }
        public int AvailableQuantity { get; set; }
        public Guid MainCategoryId { get; set; }
        public decimal Value { get; set; }
        public decimal? IncreaserFactor { get; set; }
    }
}
