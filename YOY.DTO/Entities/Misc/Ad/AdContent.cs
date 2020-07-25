using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.Ad
{
    public class AdContent
    {
        public Guid Id { set; get; }
        public string PromotionalTextProductSimple { set; get; }
        public string PromotionalTextProductFeatured { set; get; }
        public string PromotionalTextCommerceSimple { set; get; }
        public string CommerceLogoUrl { set; get; }
        public string ProductImgUrl { set; get; }
        public DateTime CreationDate { set; get; }
    }
}
