using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities.Misc.Branch
{
    public class BasicBranchHolderData
    {
        public Guid Id { set; get; }
        public string TenantName { set; get; }
        public string Name { set; get; }
        public int RelevanceStatus { set; get; }

    }
}
