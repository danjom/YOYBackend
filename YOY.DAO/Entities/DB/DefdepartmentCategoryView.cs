using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefdepartmentCategoryView
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid TenantId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryCarrouselImg { get; set; }
        public string CategoryDescription { get; set; }
        public bool CategoryIsActive { get; set; }
        public bool CategoryIsSystem { get; set; }
        public int CategoryPurposeType { get; set; }
        public int? CategoryRelevanceStatus { get; set; }
        public Guid? CategoryParentId { get; set; }
        public int? CategoryHerarchyLevel { get; set; }
        public string ParentCategoryName { get; set; }
        public int? ParentCategoryRelevanceStatus { get; set; }
        public int? ParentCategoryPurposeType { get; set; }
        public bool? ParentCategoryIsActive { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        public bool DepartmentIsActive { get; set; }
        public bool DepartmentConverLocarion { get; set; }
    }
}
