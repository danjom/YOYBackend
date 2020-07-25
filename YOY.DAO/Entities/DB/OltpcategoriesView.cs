using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpcategoriesView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? RelevanceStatus { get; set; }
        public int PurposeType { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemCategory { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string Icon { get; set; }
        public string CarrouselImg { get; set; }
        public int? HerarchyLevel { get; set; }
        public string ParentCategoryName { get; set; }
        public bool? ParentCategoryIsActive { get; set; }
        public int? ParentCategoryRelevanceStatus { get; set; }
        public int? ParentCategoryPurposeType { get; set; }
        public Guid? GrandparentCategoryId { get; set; }
    }
}
