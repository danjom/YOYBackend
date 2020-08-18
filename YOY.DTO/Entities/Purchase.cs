using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities
{
    public class Purchase
    {
        public Guid Id { set; get; }
        public string PurchaseCode { set; get; }
        public string PurchaseNumericCode { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public Guid? DispatchBranchId { set; get; }
        public Guid? DispatchValidationSourceId { set; get; }
        public int? DispatchValidationSourceType { set; get; }
        public string DispatchValidationSourceTypeName { set; get; }
        public Guid? PaymentLogId { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public int DeliveryType { set; get; }
        public string DeliveryTypeName { set; get; }
        public Guid? AppliedEarningsIncreaserId { set; get; }
        public decimal TotalAmount { set; get; }
        public decimal TotalTenantEarnings { set; get; }
        public double TotalCashbackPercentage { set; get; }
        public decimal TotalCashbackAmount { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
