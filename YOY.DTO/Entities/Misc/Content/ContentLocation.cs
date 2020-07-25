using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.Content
{
    public class ContentLocation
    {
        public Guid ReferenceId { set; get; }
        public int ReferenceType { set; get; }
        public int LocationId { set; get; }
        public int LocationType { set; get; }
        public string LocationTypeName { set; get; }
        public bool IsActive { set; get; }
    }
}
