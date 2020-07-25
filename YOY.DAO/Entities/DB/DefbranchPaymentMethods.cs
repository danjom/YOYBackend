using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefbranchPaymentMethods
    {
        public Guid BranchId { get; set; }
        public Guid MethodId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Defbranches Branch { get; set; }
        public virtual DefpaymentMethods Method { get; set; }
    }
}
