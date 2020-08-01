using System;

namespace YOY.DTO.Entities.Misc.TenantData
{
    public class MinTenantInfo
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid CategoryId { set; get; }
        public Guid ClassificationId { set; get; }
        public Guid PreferenceId { set; get; }
        public Guid CountryId { set; get; }
        public string CountryName { set; get; }
        public Guid? Logo { set; get; }
        public string LogoUrl { set; get; }
        public Guid? WhiteLogo { set; get; }
        public string WhiteLogoUrl { set; get; }
        public Guid? CarrouselImgId { set; get; }
        public string CarrouselImgUrl { set; get; }
        public Guid? LandingImg { set; get; }
        public string Name { set; get; }
        public string CategoryName { set; get; }
        public bool IsActive { set; get; }
        public string CurrencySymbol { set; get; }
        public int CurrencyType { set; get; }
        public string CurrencyTypeName { set; get; }
        public int TypeId { set; get; }
        public int BusinessStructureType { set; get; }
        public int PayerType { set; get; }
        public int InstanceType { set; get; }
        public int Language { set; get; }
        public bool Released { set; get; }
        public decimal? RelevanceScore { set; get; }
        public string CampaignDefaultTitleMsg { set; get; }
        public string CampaignDefaultContentMsg { set; get; }
        public int LoyaltyProgramType { set; get; }
        public bool HasMembershipLevels { set; get; }
        public bool AcceptsCommunityPointsAsPayment { set; get; }
        public bool AcceptsSelfPointsAsPayment { set; get; }
        public int CheckInType { set; get; }
        public int ReferenceCodeType { set; get; }
        public decimal DefaultCommissionFeePercentage { set; get; }
        public decimal ConsumerCashbackPercentage { set; get; }
        public int DealClaimMethod { set; get; }
    }
}
