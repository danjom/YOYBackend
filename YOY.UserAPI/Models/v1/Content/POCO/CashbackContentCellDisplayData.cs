using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class CashbackContentCellDisplayData : CellDisplayData
    {
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public string DealTypeIcon { set; get; }
        public bool Favorite { set; get; }
        public string CommerceLogo { set; get; }
        public string MainHint { set; get; }
        public string ComplementaryHint { set; get; }
        public string UnlockHint { set; get; }
        public bool DisplayUnlockHint { set; get; }
        public string ExpirationDate { set; get; }
        public bool DisplayExpirationHint { set; get; }
    }
}
