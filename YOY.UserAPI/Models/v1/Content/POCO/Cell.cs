using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class Cell
    {
        public int Type { set; get; }
        public int OnSelectAction { set; get; }
        public Guid Id { set; get; }
        public bool IsSponsored { set; get; }
        public bool IsNew { set; get; }
        public double OverAllScore { set; get; }
        public int PurchaseCount { set; get; }
        public CellDisplayData DisplayData { set; get; }
        public CellContainedObject DetailedContent { set; get; }
    }
}
