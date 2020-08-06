using System;

namespace YOY.DTO.Entities.Misc.TenantData
{
    public class BasicTenantData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string LogoUrl { set; get; }
        public string WhiteLogoUrl { set; get; }
        public Guid CountryId { set; get; }
        public int Type { set; get; }
        public Guid CategoryId { set; get; }
        public string CurrencySymbol { set; get; }
        public int RelevanceStatus { set; get; }
        public double CashbackPercentage { set; get; }
        public decimal? RelevanceScore { set; get; }
    }
}
