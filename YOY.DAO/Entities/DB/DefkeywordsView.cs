using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefkeywordsView
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public string Word { get; set; }
        public int Language { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CategoryName { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public int? CategoryHerarchy { get; set; }
        public int? CategoryRelevanceStatus { get; set; }
    }
}
