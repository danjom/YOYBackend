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
        public Guid? Thumbnail { set; get; }
        public string ThumbnailUrl { set; get; }
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
        public bool HasMembershipLevels { set; get; }
        public bool AcceptsPay { set; get; }
        public int ReferenceCodeType { set; get; }
        public double DefaultCommissionFeePercentage { set; get; }
        public double ConsumerCashbackPercentage { set; get; }
    }
}
