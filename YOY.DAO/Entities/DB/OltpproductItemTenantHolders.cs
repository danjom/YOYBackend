using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpproductItemTenantHolders
    {
        public OltpproductItemTenantHolders()
        {
            OltpsavedItems = new HashSet<OltpsavedItems>();
        }

        public Guid Id { get; set; }
        public Guid ProductItemId { get; set; }
        public Guid TenantHolderId { get; set; }
        public decimal? Value { get; set; }
        public decimal? RegularValue { get; set; }
        public decimal AdditionalScanningPoints { get; set; }
        public decimal AdditionalPurchasePoints { get; set; }
        public int AvailableQuantity { get; set; }
        public string InStoreLocation { get; set; }
        public string NameInReceipt { get; set; }
        public int CommissionFeeType { get; set; }
        public decimal CommissionFee { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual OltpproductItems ProductItem { get; set; }
        public virtual Deftenants TenantHolder { get; set; }
        public virtual ICollection<OltpsavedItems> OltpsavedItems { get; set; }
    }
}
