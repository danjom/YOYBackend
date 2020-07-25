using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class BTLContentItem
    {
        public Guid Id { set; get; }
        public Guid ContentId { set; get; }
        public string ReferenceURL { set; get; }
        public Guid? ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public int ContentType { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public int ViewCount { set; get; }
        public int Position { set; get; }
        public string ContainedProducts { set; get; }
    }
}
