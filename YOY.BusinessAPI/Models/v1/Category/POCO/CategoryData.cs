using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Category.POCO
{
    public class CategoryData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public bool Selected { set; get; }

    }
}
