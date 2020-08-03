using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TemptenantDisplayContents
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public int? Relevance { get; set; }
        public string WhiteLogoUrl { get; set; }
        public string CarrouselImgUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string DiscountHint { get; set; }
        public string CashbackHint { get; set; }
        public string CategoryName { get; set; }
        public double PurchaseCashback { get; set; }
        public bool? IsActive { get; set; }
        public decimal? Score { get; set; }
    }
}
