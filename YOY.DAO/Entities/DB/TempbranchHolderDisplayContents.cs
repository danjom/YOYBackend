using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempbranchHolderDisplayContents
    {
        public Guid TenantId { get; set; }
        public Guid BranchId { get; set; }
        public string TenantName { get; set; }
        public string BranchName { get; set; }
        public int? Relevance { get; set; }
        public string LogoUrl { get; set; }
        public string CarrouselImgUrl { get; set; }
        public bool? IsActive { get; set; }
        public double? Distance { get; set; }
    }
}
