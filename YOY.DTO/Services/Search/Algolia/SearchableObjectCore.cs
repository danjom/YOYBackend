using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Services.Search.Algolia
{
    public class SearchableObjectCore
    {
        public string ObjectID { set; get; }
        public string CommerceId { set; get; }
        public string CountryId { set; get; }
        public string Keywords { set; get; }
        public string ImgUrl { set; get; }
        public bool IsSponsored { set; get; }
        public bool IsActive { set; get; }
        public double RelevanceRate { set; get; }
        public int UsageCount { set; get; }
        public int ReleaseDate { set; get; }
        public int ExpirationDate { set; get; }
    }
}
