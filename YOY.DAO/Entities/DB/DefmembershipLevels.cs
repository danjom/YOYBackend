using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefmembershipLevels
    {
        public DefmembershipLevels()
        {
            DeftenantMembershipLevels = new HashSet<DeftenantMembershipLevels>();
        }

        public Guid Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<DeftenantMembershipLevels> DeftenantMembershipLevels { get; set; }
    }
}
