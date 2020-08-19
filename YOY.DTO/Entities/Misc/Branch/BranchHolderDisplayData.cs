using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities.Misc.Branch
{
    public class BranchHolderDisplayData
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string Name { set; get; }
        public string TenantName { set; get; }
        public string LogoUrl { set; get; }
        public string CarrouselImgUrl { set; get; }
    }
}
