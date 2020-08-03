using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defalliances
    {
        public Guid Id { get; set; }
        public Guid FirstPurchaseTenantId { get; set; }
        public Guid SecondPurchaseTenantId { get; set; }
        public int FirstPurchaseReferenceType { get; set; }
        public Guid FirstPurchaseReferenceId { get; set; }
        public int SecondPurchaseReferenceType { get; set; }
        public Guid SecondPurchaseReferenceId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Deftenants FirstPurchaseTenant { get; set; }
        public virtual Deftenants SecondPurchaseTenant { get; set; }
    }
}
