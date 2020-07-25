using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpemployeesView
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? CreatorId { get; set; }
        public string AuthorizedValidatorPhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid MembershipId { get; set; }
        public string RoleId { get; set; }
        public bool AccessAllowed { get; set; }
        public string AccessKey { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string BrachName { get; set; }
        public string TenantName { get; set; }
        public string CurrencySymbol { get; set; }
        public int CurrencyType { get; set; }
    }
}
