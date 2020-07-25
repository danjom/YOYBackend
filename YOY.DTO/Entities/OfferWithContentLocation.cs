using YOY.DTO.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class OfferWithContentLocation
    {
        public Guid Id { set; get; }
        public Guid CommerceId { set; get; }
        public string CommerceName { set; get; }
        public string CommerceLogo { set; get; }
        public  Guid? CategoryId { set; get; }
        public string CategoryName { set; get; }
        public int OfferType { set; get; }
        public string OfferTypeName { set; get; }
        public string Name { set; get; }
        public string Incentive { set; get; }
        public  string Keywords { set; get; }
        public string Description { set; get; }
        public int AvailableQuantity { set; get; }
        public decimal? Value { set; get; }
        public decimal? RegularValue { set; get; }
        public Guid? ImgId { set; get; }
        public DateTime ReleaseDate { set; get; }
        public DateTime? ExpirationDate { set; get; }
        public DateTime CreationDate { set; get; }
        public int RedemptionCount { set; get; }
        public int ClaimCount { set; get; }
        public bool Exclusive { set; get; }
        public string Rules { set; get; }
        public string Conditions { set; get; }
        public string PublishState { set; get; }
        public List<Misc.Structure.POCO.Pair<int, Guid>> GeoSegmentation { set; get; }
    }
}
