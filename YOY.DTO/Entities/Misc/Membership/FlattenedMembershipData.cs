using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.MembershipLevel;
using YOY.DTO.Entities.Misc.TenantData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities.Misc.Membership
{
    public class FlattenedMembershipData
    {
        public TenantMembershipData TenantData { set; get; }
        public MembershipData UserMembership { set; get; }
        public LevelData Level { set; get; }
        public MinBranchData Branch { set; get; }
        public bool IsMember { set; get; }
        public int UserLevel { set; get; }
    }
}
