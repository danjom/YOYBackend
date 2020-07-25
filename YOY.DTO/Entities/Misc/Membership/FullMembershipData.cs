using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.MembershipLevel;
using YOY.DTO.Entities.Misc.MembershipOperation;
using YOY.DTO.Entities.Misc.TenantData;
using System.Collections.Generic;

namespace YOY.DTO.Entities.Misc.Membership
{
    public class FullMembershipData
    {
        public TenantMembershipData TenantData { set; get; }
        public MembershipData UserMembership { set; get; }
        public List<LevelData> Levels { set; get; }
        public List<MinBranchData> Branches { set; get; }
        public List<MembershipPointsOpSummary> PointsOps { set; get; }
        public bool IsMember { set; get; }
    }
}
