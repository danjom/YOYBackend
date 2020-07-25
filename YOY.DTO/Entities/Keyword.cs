using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class Keyword
    {
        public Guid Id { set; get; }
        public string Word { set; get; }
        public Guid? CategoryId { set; get; }
        public string CategoryName { set; get; }
        public int? CategoryRelevanceStatus { set; get; }
        public Guid? ParentCategoryId { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public int Language { set; get; }
        public string LanguageName { set; get; }
    }
}
