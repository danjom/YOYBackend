using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.TenantData;
using System;

namespace YOY.DTO.Entities.Misc.Reward
{
    public class FlattenedRewardData
    {
        public Entities.Offer Offer { set; get; }
        public BasicBranchData Branch { set; get; }
        public BasicTenantData Tenant { set; get; }
        public DateTime RaffleDate { set; get; }
        public int RaffleType { set; get; }
        public int UsageType { set; get; }
        public int UserPurchasesCount { set; get; }
        public int RequiredPurchasesToRedeem { set; get; }
        public int MinMembershipLevel { set; get; }
        public string MinMembershipLevelName { set; get; }
        public int TimeOutDaysBetweenRedemption { set; get; }
        public bool RedemptionAllowed { set; get; }
        public bool UserWonIt { set; get; }
    }
}
