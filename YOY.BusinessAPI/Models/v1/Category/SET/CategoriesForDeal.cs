using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.Category.POCO;

namespace YOY.BusinessAPI.Models.v1.Category.SET
{
    public class CategoriesForDeal
    {
        public Guid? DealId { set; get; }
        public int Count { set; get; }
        public List<CategoryData> Categories { set; get; }
    }
}
