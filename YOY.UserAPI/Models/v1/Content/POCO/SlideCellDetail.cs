using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class SlideCellDetail : CellContainedObject
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public bool RetrievePromotionalContent { set; get; }
    }
}
