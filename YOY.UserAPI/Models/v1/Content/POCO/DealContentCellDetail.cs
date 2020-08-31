using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class DealContentCellDetail : CellContainedObject
    {
        public string CommerceLogo { set; get; }
        public string DealName { set; get; }
        public bool DisplayPrice { set; get; }
        public decimal Price { set; get; }
        public string PriceLiteral { set; get; }
        public decimal RegularPrice { set; get; }
        public string RegularPriceLiteral { set; get; }
        public bool DisplayRegularPrice { set; get; }
        public string CurrencySymbol { set; get; }
        public bool HasPreferences { set; get; }


    }
}
