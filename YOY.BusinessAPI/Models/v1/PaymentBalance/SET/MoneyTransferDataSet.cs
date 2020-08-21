using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.BusinessAPI.Models.v1.PaymentBalance.POCO;

namespace YOY.BusinessAPI.Models.v1.PaymentBalance.SET
{
    public class MoneyTransferDataSet
    {
        public int TotalRecords { set; get; }
        public decimal TotalAmount { set; get; }
        public Guid TenantId { set; get; }
        public Guid BranchId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public List<MoneyTransferData> Transfers { set; get; }
    }
}
