using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class Offer
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid MainCategoryId { set; get; }
        public string MainCategoryName { set; get; }
        public int OfferType { set; get; }
        public string OfferTypeName { set; get; }
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public int RewardType { set; get; }//By default for offers that aren't rewards, this field value will be 1
        public string RewardTypeName { set; get; }
        public int PurposeType { set; get; }//By default for offers that aren't rewards, this field value will be 2
        public string PurposeTypeName { set; get; }
        public int GeoSegmentationType { set; get; }
        public string GeoSegmentationTypeName { set; get; }
        public int DisplayType { set; get; }
        public string DisplayTypeName { set; get; }
        public string Name { set; get; }
        public string MainHint { set; get; }
        public string ComplementaryHint { set; get; }
        public string Keywords { set; get; }
        public string Code { set; get; }
        public Guid? CodeImg { set; get; }
        public string Description { set; get; }
        public int MinsToUnlock { set; get; }
        public bool IsActive { set; get; }
        public bool IsExclusive { set; get; }
        public bool IsSponsored { set; get; }
        public bool HasUniqueCodes { set; get; }
        public bool HasPreferences { set; get; }

        //AVAILABLE QUANTITY
        public int AvailableQuantity { set; get; }
        public bool OneTimeRedemption { set; get; }
        public int MaxClaimsPerUser { set; get; }
        public int MinPurchasesCountToRedeem { set; get; }
        public DateTime? PurchasesCountStartDate { set; get; }
        public string ClaimLocation { set; get; }

        //VALUE COMPONENTS
        public decimal Value { set; get; }
        public bool SetRegularValue { set; get; }
        public decimal? RegularValue { set; get; }
        public double ExtraBonus { set; get; }
        public int ExtraBonusType { set; get; }
        public string ExtraBonusTypeName { set; get; }
        public decimal MinIncentive { set; get; }
        public decimal MaxIncentive { set; get; }
        public int IncentiveVariationType { set; get; }
        public string IncentiveVarationTypeName { set; get; }
        public decimal? IncentiveVariation { set; get; }
        public int SecondsIncentiveVariationFrame { set; get; }


        public int RedeemCount { set; get; }
        public int ClaimCount { set; get; }
        public Guid? DisplayImgId { set; get; }
        public string DisplayImgUrl { set; get; }
        public string TargettingParams { set; get; }

        //SCHEDULING COMPONENTS
        public DateTime ReleaseDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string PublishState { set; get; }


        public string Rules { set; get; }
        public string Conditions { set; get; }
        public string ClaimInstructions { set; get; }


        //FOR BROADCASTING PURPOSES
        public DateTime? LastBroadcastingUsage { set; get; }
        public int BroadcastingScheduleType { set; get; }
        public string BroadcastingScheduleTypeName { set; get; }
        public int BroadcastingTimerType { set; get; }
        public string BroadcastingTimerTypeName { set; get; }
        public int BroadcastingMinsToRedeem { set; get; }
        public string BroadcastingTitle { set; get; }
        public string BroadcastingMsg { set; get; }


        public double RelevanceRate { set; get; }

        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }


        public double? SatisfactionScore { set; get; }
        public decimal? RelevanceScore { set; get; }
    }
}
