using System;

namespace YOY.DTO.Entities.Misc.Reward
{
    public class ClubReward
    {
        public Entities.Offer Offer { set; get; }
        public DateTime RaffleDate { set; get; }
        public int RaffleType { set; get; }
        public string RaffleTypeName { set; get; }
        public int RewardUsageType { set; get; }
        public string RewardUsageTypeName { set; get; }
        public int UserVisitsCount { set; get; }
        public int RequiredPurchasesToRedeem { set; get; }
        public bool RedemptionAllowed { set; get; }
        public bool UserWonIt { set; get; }
        public string PublishState { set; get; }
        public int MinMembershipLevel { set; get; }
        public string MinMembershipLevelName { set; get; }
        public int TimeOutDaysBetweenRedemption { set; get; }
        public Guid? EarningsIncreaserId { set; get; }
    }
}
