using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Services.Search.Algolia
{
    public class UpdateSearchableObjectCategoryData
    {
        public string ObjectID { set; get; }
        public string Categories { set; get; }
        public string Classifications { set; get; }
    }
}
