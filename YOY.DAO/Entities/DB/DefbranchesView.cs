using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefbranchesView
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public int Type { get; set; }
        public Guid? BranchHolderId { get; set; }
        public Guid? BranchHolderDepartmentId { get; set; }
        public Guid? FranchiseeId { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public string OrdersPhoneNumber { get; set; }
        public bool IndependantOwner { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string LocationAddress { get; set; }
        public string DescriptiveAddress { get; set; }
        public int OrderTakingType { get; set; }
        public Guid? GeofenceId { get; set; }
        public string HashedCode { get; set; }
        public Guid CountryId { get; set; }
        public string ContryName { get; set; }
        public Guid StateId { get; set; }
        public string StateName { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public int UtcTimeZone { get; set; }
        public string GeofenceExternalId { get; set; }
        public string GeofenceName { get; set; }
        public string BranchHolderName { get; set; }
        public string BranchHolderContactPhoneNumber { get; set; }
        public string BranchHolderEmail { get; set; }
        public bool? BranchHolderActiveState { get; set; }
        public string BranchHolderDepartmentName { get; set; }
        public bool? BranchHolderDepartmentActiveState { get; set; }
        public string FranchiseeLegalName { get; set; }
        public string FranchiseeContactName { get; set; }
        public string FranchiseeContactEmail { get; set; }
        public string FranchiseeContactPhone { get; set; }
        public bool? FranchiseeActiveState { get; set; }
    }
}
