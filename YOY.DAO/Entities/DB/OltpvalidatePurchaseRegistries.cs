using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpvalidatePurchaseRegistries
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid MembershipId { get; set; }
        public Guid ReceiptId { get; set; }
        public decimal TotalAmount { get; set; }
        public int PointsGenerationType { get; set; }
        public decimal? ClubPointsGenerated { get; set; }
        public decimal? WalletPointsGenerated { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Oltpmemberships Membership { get; set; }
        public virtual Oltpreceipts Receipt { get; set; }
        public virtual Deftenants Tenant { get; set; }
    }
}
