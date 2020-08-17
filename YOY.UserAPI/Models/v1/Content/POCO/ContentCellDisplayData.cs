using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class ContentCellDisplayData : CellDisplayData
    {
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public bool Favorite { set; get; }
        public string CommerceLogo { set; get; }
        public string ImgUrl { set; get; }
        public string MainHint { set; get; }
        public string ComplementaryHint { set; get; }
        public string ExtraHint { set; get; }
        public bool DisplayExtraHint { set; get; }
        public int AvailableQuantity { set; get; }
        public string AvailableQuantityHint { set; get; }
        public bool DisplayAvailableQuantityHint { set; get; }
        public string ExpirationDate { set; get; }
        public bool DisplayExpirationHint { set; get; }
    }
}
