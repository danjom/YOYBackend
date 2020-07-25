using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.TenantData;
using System.Collections.Generic;

namespace YOY.DTO.Entities.Misc.Transaction
{
    public class TransactionDataWithBranches
    {
        public Entities.Transaction Transaction { set; get; }
        public BasicTenantData Tenant { set; get; }
        public List<BasicBranchData> Branches { set; get; }
        public int GeosegmentationType { set; get; }
        public int AvailableQuantity { set; get; }
        public int TotalClaimCount { set; get; }
    }
}
