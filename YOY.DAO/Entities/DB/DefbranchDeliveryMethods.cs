using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefbranchDeliveryMethods
    {
        public Guid BranchId { get; set; }
        public Guid MethodId { get; set; }
        public bool FixedPrice { get; set; }
        public int MaxItemsPerDelivery { get; set; }
        public bool? IsActive { get; set; }
        public decimal UnitPrice { get; set; }
        public double UnitDistance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defbranches Branch { get; set; }
        public virtual DefdeliveryMethods Method { get; set; }
    }
}
