using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class EnabledProductCategoriesForNewOfferView
    {
        public Guid TenantId { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? HerarchyLevel { get; set; }
    }
}
