using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefbroadcastersView
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public Guid? BranchId { get; set; }
        public bool MultiBranchEnabled { get; set; }
        public string BrachName { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public string InStoreLocation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastCheckDate { get; set; }
        public bool IsActive { get; set; }
        public int? Mayor { get; set; }
        public int? Minor { get; set; }
        public string NamespaceId { get; set; }
        public string InstanceId { get; set; }
        public string Url { get; set; }
        public int BeaconType { get; set; }
        public int UsageType { get; set; }
        public int PurposeType { get; set; }
        public int BroadcastingProtocol { get; set; }
        public bool Default { get; set; }
        public string FileId { get; set; }
        public string FileMimeType { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
    }
}
