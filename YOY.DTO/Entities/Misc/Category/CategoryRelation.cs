﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.Category
{
    public class CategoryRelation
    {
        public Guid CategoryId { set; get; }
        public Guid? ParentCategoryId { set; get; }
        public string CategoryName { set; get; }
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
    }
}
