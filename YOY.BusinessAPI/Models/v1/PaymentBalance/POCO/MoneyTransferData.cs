using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.PaymentBalance.POCO
{
    public class MoneyTransferData
    {
        public Guid Id { set; get; }
        public string Subject { set; get; }
        public string Details { set; get; }
        public string TransferredAmount { set; get; }
        public string BeneficiaryId { set; get; }
        public string BeneficiaryTypeName { set; get; }
        public string BeneficiaryName { set; get; }
        public string DestinationId { set; get; }
        public string DestinationTypeName { set; get; }
        public string DestinationName { set; get; }
        public string ReferenceCode { set; get; }
        public string StatusName { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedDateLiteral { set; get; }
        public DateTime UpdatedDate { set; get; }
        public string UpdatedDateLiteral { set; get; }
        public int TotalSalesCount { set; get; }
        public bool EnableReportRequest { set; get; }
    }
}
