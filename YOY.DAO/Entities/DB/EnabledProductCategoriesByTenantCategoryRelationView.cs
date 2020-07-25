using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class EnabledProductCategoriesByTenantCategoryRelationView
    {
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public Guid TenantId { get; set; }
        public Guid ReferenceMainCategoryId { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? HerarchyLevel { get; set; }
        public Guid? RelationReferenceId { get; set; }
        public int? RelationReferenceType { get; set; }
    }
}
