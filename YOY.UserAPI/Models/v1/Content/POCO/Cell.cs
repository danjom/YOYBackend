using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class Cell
    {
        public int ContentType { set; get; }
        public int OnSelectAction { set; get; }
        public Guid Id { set; get; }
        public CellDisplayData DisplayData { set; get; }
        public CellDetailContent DetailedContent { set; get; }
    }
}
