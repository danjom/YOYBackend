using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class ContentFilterTypeCell : CellDisplayData
    {
        public int FilterType { set; get; }
        public string FilterName { set; get; }
    }
}
