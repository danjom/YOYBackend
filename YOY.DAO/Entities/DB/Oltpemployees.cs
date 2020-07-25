using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Oltpemployees
    {
        public Oltpemployees()
        {
            InverseCreator = new HashSet<Oltpemployees>();
            OltpbroadcastingPlayerLogs = new HashSet<OltpbroadcastingPlayerLogs>();
            Oltptransactions = new HashSet<Oltptransactions>();
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? CreatorId { get; set; }
        public string AuthorizedValidatorPhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string AccessKey { get; set; }
        public Guid MembershipId { get; set; }
        public string RoleId { get; set; }
        public bool? AccessAllowed { get; set; }
        public bool Deleted { get; set; }

        public virtual Defbranches Branch { get; set; }
        public virtual Oltpemployees Creator { get; set; }
        public virtual Oltpmemberships Membership { get; set; }
        public virtual AspNetRoles Role { get; set; }
        public virtual ICollection<Oltpemployees> InverseCreator { get; set; }
        public virtual ICollection<OltpbroadcastingPlayerLogs> OltpbroadcastingPlayerLogs { get; set; }
        public virtual ICollection<Oltptransactions> Oltptransactions { get; set; }
    }
}
