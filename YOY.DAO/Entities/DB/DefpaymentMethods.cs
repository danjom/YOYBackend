using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefpaymentMethods
    {
        public DefpaymentMethods()
        {
            DefbranchPaymentMethods = new HashSet<DefbranchPaymentMethods>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowProgrammedOrders { get; set; }
        public bool? PaymentBeforeShipping { get; set; }
        public bool RequieresCall { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool OnlyOnline { get; set; }
        public string IconName { get; set; }

        public virtual ICollection<DefbranchPaymentMethods> DefbranchPaymentMethods { get; set; }
    }
}
