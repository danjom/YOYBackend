using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class DealContentCellDetail : CellContainedObject
    {
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public string DealTypeIcon { set; get; }
        public bool Favorite { set; get; }
        public string CommerceLogo { set; get; }
        public string ImgUrl { set; get; }
        public string MainHint { set; get; }
        public string ComplementaryHint { set; get; }
        public string DealName { set; get; }
        public bool DisplayPrice { set; get; }
        public decimal Price { set; get; }
        public string PriceLiteral { set; get; }
        public decimal RegularPrice { set; get; }
        public string RegularPriceLiteral { set; get; }
        public bool DisplayRegularPrice { set; get; }
        public string CurrencySymbol { set; get; }
        public string CashbackHint { set; get; }
        public bool DisplayCashbackHint { set; get; }
        public int AvailableQuantity { set; get; }
        public string AvailableQuantityHint { set; get; }
        public bool DisplayAvailableQuantityHint { set; get; }
        public string ExpirationDate { set; get; }
        public bool DisplayExpirationHint { set; get; }
        public string ExtraHint { set; get; }
        public bool DisplayExtraHint { set; get; }
        public bool HasPreferences { set; get; }

    }
}
