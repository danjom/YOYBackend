using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class Slide : CellDisplayData
    {
        public string ImgUrl { set; get; }
        public Guid CountryId { set; get; }
        public Guid? StateId { set; get; }
        public string ExpirationDate { set; get; }
    }
}
