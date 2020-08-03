using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class CommerceCellDetailContent : CellDetailContent
    {
        public string ImgUrl { set; get; }
        public string Name { set; get; }
        public string CategoryName { set; get; }
        public string DiscountHint { set; get; }
        public string CashbackHint { set; get; }
        public bool ShowRate { set; get; }
        public double Rate { set; get; }
    }
}
