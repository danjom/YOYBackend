using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpshoppingCartItems
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string UserId { get; set; }
        public Guid OfferId { get; set; }
        public int Quantity { get; set; }
        public bool HasPreferences { get; set; }
        public string ChosenPreferences { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpoffers Offer { get; set; }
        public virtual Deftenants Tenant { get; set; }
    }
}
