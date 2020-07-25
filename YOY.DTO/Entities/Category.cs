using YOY.Values.Strings;
using System;
using System.ComponentModel.DataAnnotations;

namespace YOY.DTO.Entities
{
    public class Category
    {
        public Guid Id { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public bool IsActive { set; get; }
        public bool IsSystemCategory { set; get; }
        public Guid? ParentCategoryId { set; get; }
        public string ParentCategoryName { set; get; }
        public int ProductCount { set; get; }
        public int PurposeType { set; get; }
        public string Icon { set; get; }
        public string CarrouselImg { set; get; }
        public int? Herarchy { set; get; }
        public string HerarchyName { set; get; }
        public int? RelevanceStatus { set; get; }
        public string RelevanceStatusName { set; get; }
        public decimal? RelevanceScore { set; get; }
    }
}
