using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpproductItems
    {
        public OltpproductItems()
        {
            OltpproductItemContents = new HashSet<OltpproductItemContents>();
            OltpproductItemTenantHolders = new HashSet<OltpproductItemTenantHolders>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public string Conditions { get; set; }
        public string Code { get; set; }
        public int CodeType { get; set; }
        public Guid? DisplayImgId { get; set; }
        public Guid TenantId { get; set; }
        public Guid? CategoryId { get; set; }
        public int DealType { get; set; }
        public string ClaimLocation { get; set; }
        public decimal ScanningPoints { get; set; }
        public decimal PurchasePoints { get; set; }
        public int CommissionFeeType { get; set; }
        public decimal CommissionFee { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpcategories Category { get; set; }
        public virtual Oltpimages DisplayImg { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual ICollection<OltpproductItemContents> OltpproductItemContents { get; set; }
        public virtual ICollection<OltpproductItemTenantHolders> OltpproductItemTenantHolders { get; set; }
    }
}
