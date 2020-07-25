using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltprewardToAwards
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string UserId { get; set; }
        public Guid? OfferId { get; set; }
        public Guid OriginatorId { get; set; }
        public int? OriginatorType { get; set; }
        public string OriginatorName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Oltpoffers Offer { get; set; }
        public virtual Deftenants Tenant { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
