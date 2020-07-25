using System;

namespace YOY.DTO.Entities
{
    public class Employee
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid? BranchId { set; get; }
        public Guid? CreatorId { set; get; }
        public string RoleId { set; get; }
        public int RolePos { set; get; }
        public string AuthorizedValidatorPhoneNumber { set; get; }
        public Guid MembershipId { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public bool AccessAllowed { set; get; }
        public string UserId { set; get; }
        public string UserName { set; get; }
        public string RoleName{set;get;}
        public string BranchName { set; get; }
        public string Name { set; get; }
        public string AccessKey { set; get; }
        public string CurrencySymbol { set; get; }
        public string CurrencyTypeName { set; get; }
    }
}
