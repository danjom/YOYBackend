using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.TenantData;

namespace YOY.DTO.Entities.Misc.Transaction
{
    public class FlattenedTransactionData
    {
        public Entities.Transaction Transaction { set; get; }
        public BasicBranchData Branch { set; get; }
        public BasicTenantData Tenant { set; get; }
        public int GeosegmentationType { set; get; }
        public int AvailableQuantity { set; get; }
        public int TotalClaimCount { set; get; }
    }
}
