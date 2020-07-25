using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.TenantData;
using System;
using System.Collections.Generic;

namespace YOY.DTO.Entities.Misc.Reward
{
    public class FullRewardData
    {
        public Entities.Offer Offer { set; get; }
        public BasicTenantData Tenant { set; get; }
        public List<BasicBranchData> Branches { set; get; }
        public DateTime RaffleDate { set; get; }
        public int RaffleType { set; get; }
        public int MinMembershipLevel { set; get; }
        public string MinMembershipLevelName { set; get; }
        public int TimeOutDaysBetweenRedemption { set; get; }
        public bool RedemptionAllowed { set; get; }
        public bool UserWonIt { set; get; }
        public int RequiredPurchasesToRedeem { set; get; }
        public int UserPurchasesCount { set; get; }
    }
}
