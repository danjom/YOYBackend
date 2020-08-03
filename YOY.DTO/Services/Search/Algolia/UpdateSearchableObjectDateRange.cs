using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Services.Search.Algolia
{
    public class UpdateSearchableObjectDateRange
    {
        public string ObjectID { set; get; }
        public int ReleaseDate { set; get; }
        public int ExpirationDate { set; get; }
    }
}
