using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class ContentFilterValueCell : CellDisplayData
    {
        public int FilterType { set; get; }
        public string Name { set; get; }
        public string UnselectedImgUrl { set; get; }
        public string SelectedImgUrl { set; get; }
        public string UnselectedIcon { set; get; }
        public string SelectedIcon { set; get; }

    }
}
