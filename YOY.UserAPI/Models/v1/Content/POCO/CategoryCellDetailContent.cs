using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class CategoryCellDetailContent : CellDetailContent
    {
        public string Name { set; get; }
        public string ImgUrl { set; get; }
    }
}
