using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltppurchaseDeliveryDetails
    {
        public Guid Id { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? DispatchBranchId { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public int DeliveryProvider { get; set; }
        public string ProviderCommunicationPayloads { get; set; }
        public decimal DeliverPrice { get; set; }
        public decimal CancellationFee { get; set; }
        public DateTime? ScheduledDeliveryDate { get; set; }
        public DateTime? RealDeliveryDate { get; set; }
        public double? DispatchLatitude { get; set; }
        public double? DispatchLongitude { get; set; }
        public string DispatchAddress { get; set; }
        public string DispatchContactPhoneNumberPrefix { get; set; }
        public string DispatchContactPhoneNumber { get; set; }
        public double? DeliveryLatitude { get; set; }
        public double? DeliveryLongitude { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryContactPhoneNumberPrefix { get; set; }
        public string DeliveryContactPhoneNumber { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defbranches DispatchBranch { get; set; }
        public virtual Oltppurchases Purchase { get; set; }
    }
}
