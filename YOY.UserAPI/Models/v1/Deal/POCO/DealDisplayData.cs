using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Deal.POCO
{
    public class DealDisplayData
    {
        public Guid Id { set; get; }
        public Guid CommerceId { set; get; }
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public string DealTypeIcon { set; get; }
        public bool Favorite { set; get; }
        public string CommerceLogoUrl { set; get; }
        public string CommerceWhiteLogoUrl { set; get; }
        public string DisplayImgUrl { set; get; }
        public string MainHint { set; get; }
        public string ComplementaryHint { set; get; }
        public string Name { set; get; }
        public bool DisplayPrice { set; get; }
        public decimal Price { set; get; }
        public string PriceLiteral { set; get; }
        public bool DisplayRegularPrice { set; get; }
        public decimal RegularPrice { set; get; }
        public string RegularPriceLiteral { set; get; }
        public string CurrencySymbol { set; get; }
        public string CashbackHint { set; get; }
        public bool DisplayCashbackHint { set; get; }
        public int AvailableQuantity { set; get; }
        public string AvailableQuantityHint { set; get; }
        public bool DisplayAvailableQuantityHint { set; get; }
        public bool IsNew { set; get; }
        public string ExpirationDate { set; get; }
        public bool DisplayExpirationHint { set; get; }
        public bool HasPreferences { set; get; }
        public int MinAge { set; get; }
        public int MaxAge { set; get; }
        public char GenderFocus { set; get; }
        public double Score { set; get; }
    }
}
