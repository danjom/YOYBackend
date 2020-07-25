using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class BranchesRelationData
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public int Type { get; set; }
        public Guid? BranchHolderId { get; set; }
        public Guid? BranchHolderDepartmentId { get; set; }
        public Guid? FranchiseeId { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public bool IndependantOwner { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Guid StateId { get; set; }
        public Guid CityId { get; set; }
        public int UtcTimeZone { get; set; }
        public string BranchHolderName { get; set; }
        public bool? BranchHolderActiveState { get; set; }
        public string BranchHolderDepartmentName { get; set; }
        public bool? BranchHolderDepartmentActiveState { get; set; }
    }
}
