using System;

namespace YOY.DTO.Entities.Misc.Category
{
    public class EnabledCategoryForRelation
    {
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public Guid TenantId { set; get; }
        public Guid ReferenceMainCategoryId { set; get; }
        public Guid CategoryId { set; get; }
        public string CategoryName { set; get; }
        public int? HerarchyLevel { set; get; }
        public Guid? RelationReferenceId { set; get; }
        public int? RelationReferenceType { set; get; }
    }
}
