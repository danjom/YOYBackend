using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities.Misc.Offer
{
    public class BasicOfferData
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid MainCategoryId { set; get; }
        public int OfferType { set; get; }
        public int DealType { set; get; }
        public int RewardType { set; get; }
        public int PurposeType { set; get; }
        public int GeoSegmentationType { set; get; }
        public int DisplayType { set; get; }
        public string Name { set; get; }
        public string MainHint { set; get; }
        public string ComplementaryHint { set; get; }
        public string Keywords { set; get; }
        public string Description { set; get; }
        public int MinsToUnlock { set; get; }
        public bool IsActive { set; get; }
        public bool IsExclusive { set; get; }
        public bool IsSponsored { set; get; }
        public bool HasPreferences { set; get; }
        public decimal Value { set; get; }
        public decimal? RegularValue { set; get; }
        public double ExtraBonus { set; get; }
        public int ExtraBonusType { set; get; }
        public int AvailableQuantity { set; get; }
        public bool OneTimeRedemption { set; get; }
        public int MaxClaimsPerUser { set; get; }
        public int MinPurchasesCountToRedeem { set; get; }
        public DateTime? PurchasesCountStartDate { set; get; }
        public int ClaimCount { set; get; }
        public string ClaimLocation { set; get; }
        public string DisplayImgUrl { set; get; }
        public string Rules { set; get; }
        public string Conditions { set; get; }
        public string ClaimInstructions { set; get; }
        public double RelevanceRate { set; get; }
        public string TargettingParams { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public DateTime CreatedDate { set; get; }



        //FOR BROADCASTING PURPOSES
        public DateTime? LastBroadcastingUsage { set; get; }
        public int BroadcastingScheduleType { set; get; }
        public int BroadcastingTimerType { set; get; }
        public int BroadcastingMinsToRedeem { set; get; }
        public string BroadcastingTitle { set; get; }
        public string BroadcastingMsg { set; get; }

    }
}
