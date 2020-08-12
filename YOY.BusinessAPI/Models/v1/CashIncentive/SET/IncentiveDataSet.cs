using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.CashIncentive.POCO;

namespace YOY.BusinessAPI.Models.v1.CashIncentive.SET
{
    public class IncentiveDataSet
    {
        public int Count { set; get; }
        public int TotalRecords { set; get; }
        public Guid TenantId { set; get; }
        public Guid BranchId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public List<IncentiveData> Incentives { set; get; }
    }
}
