using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.TenantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.BroadcastingLog
{
    public class BroadcastingLogDataWithBranches
    {
        public Guid LogRecordReferenceId { set; get; }
        public int LogRecordReferenceType { set; get; }
        public DateTime LogRecordExpirationDate { set; get; }
        public int BroadcastingTimerType { set; get; }
        public int BroadcastingMinsToRedeem { set; get; }
        public Entities.Offer Offer { set; get; }
        public BasicTenantData Tenant { set; get; }
        public List<BasicBranchData> Branches { set; get; }
        public bool ExactLocationBased { set; get; }
    }
}
