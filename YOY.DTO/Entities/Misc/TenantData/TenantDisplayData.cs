using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities.Misc.TenantData
{
    public class TenantDisplayData
    {
        public Guid TenantId { set; get; }
        public string Name { set; get; }
        public string CategoryName { set; get; }
        public string WhiteLogoUrl { set; get; }
        public string CarrouselImgUrl { set; get; }
        public string ThumbnailUrl { set; get; }
        public string DiscountHint { set; get; }
        public string CashbackHint { set; get; }
    }
}
