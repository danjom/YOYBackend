using System;
using System.Collections.Generic;
using System.Text;

namespace YOY.DTO.Entities
{
    class PurchaseDeliveryDetails
    {
        public Guid Id { set; get; }
        public Guid PurchaseLogId { set; get; }
        public string UserId { set; get; }
        public Guid TenantId { set; get; }
        public Guid? BranchId { set; get; }
        public Guid DeliveryMethodId { set; get; }
        public int DeliveryProvider { set; get; }
        public string ProviderCommunicationPayloads { set; get; }
        public decimal DeliveryPrice { set; get; }
        public decimal CancellationFee { set; get; }
        public DateTime? ScheduledDeliveryDate { set; get; }
        public DateTime? RealDeliveryDate { set; get; }
        public double DispatchLatitude { set; get; }
        public double DispatchLongitude { set; get; }
        public string DispatchAddress { set; get; }
        public string DispatchContactPhoneNumberPrefix { set; get; }
        public string DispatchContactPhoneNumber { set; get; }
        public double DeliveryLatitude { set; get; }
        public double DeliveryLongitude { set; get; }
        public double DeliveryAddress { set; get; }
        public string DeliveryContactPhoneNumberPrefix { set; get; }
        public string DeliveryContactPhoneNumber { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }


    }
}
