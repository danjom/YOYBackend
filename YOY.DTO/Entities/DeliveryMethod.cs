using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class DeliveryMethod
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid BranchId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool IsActive { set; get; }
        public decimal UnitPrice { set; get; }
        public int  MaxItemsPerDelivery{ set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public bool FixedPrice { set; get; }
        public double DistanceRange { set; get; }
        public string IconName { set; get; }
    }
}
