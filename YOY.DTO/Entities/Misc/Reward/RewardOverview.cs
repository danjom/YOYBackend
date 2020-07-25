using System;

namespace YOY.DTO.Entities.Misc.Reward
{
    public class RewardOverview
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public decimal Value { set; get; }
        public int RaffleType { set; get; }
        public string RaffleTypeName { set; get; }
        public int RewardUsageType { set; get; }
        public string RewardUsageTypeName { set; get; }
        public int MinMembershipLevel { set; get; }
        public string MinMembershipLevelName { set; get; }
        public int TimeOutDaysBetweenRedemption { set; get; }
        public int WinnersCount { set; get; }
        public string PublishState { set; get; }
    }
}
