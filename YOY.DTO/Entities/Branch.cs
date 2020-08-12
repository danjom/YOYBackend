using YOY.Values.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace YOY.DTO.Entities
{
    public class Branch
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public string Name { set; get; }
        public string PostCode { set; get; }

        public string TenantName { set; get; }
        [RegularExpression(".+@.+\\..+", ErrorMessageResourceType = typeof(Resources),
                                     ErrorMessageResourceName = "InvalidEmail")]
        public string Email { set; get; }
        public string ContactName { set; get; }
        public string ContactPhoneNumber { set; get; }
        public string ContactEmail { set; get; }
        public string OrderInquiriesPhoneNumber { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public decimal Latitude { set; get; }
        public decimal Longitude { set; get; }
        public XElement LocationAddress { set; get; }

        [DataType(DataType.Text)]
        public string LocationAddressData { set; get; }
        public string DescriptiveAddress { set; get; }
        public List<BranchSchedule> OpenningSchedule { set; get; }
        public bool IsOpen { set; get; }
        public int OrderTakingType { set; get; }
        public int UtcTimeZone { set; get; }
        public Guid? GeofenceId { set; get; }
        public string GeofenceName { set; get; }
        public string HashedCode { set; get; }
        public Guid StateId { set; get; }
        public string StateName { set; get; }
        public Guid CityId { set; get; }
        public string CityName { set; get; }
        public int Type { set; get; }
        public string TypeName { set; get; }
        public bool HasBranchHolder { set; get; }
        public Guid? BranchHolderId { set; get; }
        public string BranchHolderName { set; get; }
        public string BranchHolderContactPhoneNumber { set; get; }
        public string BranchHolderEmail { set; get; }
        public bool? BranchHolderActiveState { set; get; }
        public Guid? BrachHolderDepartmentId { set; get; }
        public string BranchHolderDepartmentName { set; get; }
        public bool? BranchHolderDepartmentActiveState { set; get; }
        public bool IsFranchise { set; get; }
        public Guid? FranchiseeId { set; get; }
        public string FranchiseeLegalName { set; get; }
    }
}
