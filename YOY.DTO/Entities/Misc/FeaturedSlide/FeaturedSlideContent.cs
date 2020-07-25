using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.FeaturedSlide
{
    public class FeaturedSlideContent
    {
        public Guid Id { set; get; }
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public string ReferenceTypeName { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
    }
}
