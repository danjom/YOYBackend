using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Services.Search.Algolia
{
    public class UpdateSearchableObjectCore
    {
        public string ObjectID { set; get; }
        public string Keywords { set; get; }
        public bool IsSponsored { set; get; }
        public bool IsActive { set; get; }
        public int DealType { set; get; }
        public double RelevanceRate { set; get; }
        public int ReleaseDate { set; get; }
        public int ExpirationDate { set; get; }
    }
}
