using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class MembershipLevel
    {
        public Guid Id { set; get; }
        public int Level { set; get; }
        public string Name { set; get; }
        public string IconUrl { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }

    }
}
