using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.BusinessAPI.Models.v1.Sale.POCO
{
    public class SaleData
    {
        public Guid Id { set; get; }
        public int SaleType { set; get; }
        public string SaleTypeName { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedDateLiteral { set; get; }
        public DateTime? CompletedDate { set; get; }
        public string CompletedDateLiteral { set; get; }
        public string SaleStatusName { set; get; }
        public string PaymentStatusName { set; get; }
        public string ReferenceCode { set; get; }
        public string CommerceName { set; get; }
        public string BranchName { set; get; }
        public decimal TotalAmount { set; get; }
        public decimal CommerceEarnings { set; get; }
        public string LiquidationStatusName { set; get; }
        public string LiquidationDateLiteral { set; get; }
        public string LiquidationReferenceCode { set; get; }

    }
}
