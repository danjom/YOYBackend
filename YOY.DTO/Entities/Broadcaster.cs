using System;

namespace YOY.DTO.Entities
{
    public class Broadcaster
    {
        public Guid Id { set; get; }
        public string ExternalId { set; get; }
        public string UUID { set; get; }
        public int? Minor { set; get; }
        public int? Major { set; get; }
        public string NamespaceId { set; get; }
        public string InstanceId { set; get; }
        public string URL { set; get; }
        public string InStoreLocation { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public DateTime LastCheckDate { set; get; }
        public string Name { set; get; }
        public string FriendlyName { set; get; }
        public bool MultiBranchEnabled { set; get; }
        public Guid CountryId { set; get; }
        public string CountryName { set; get; }
        public Guid StateId { set; get; }
        public string StateName { set; get; }
        public Guid? BranchId { set; get; }
        public string BranchName { set; get; }
        public Guid? DepartmentId { set; get; }
        public string DepartmentName { set; get; }
        public Guid TenantId { set; get; }
        public string TenantName { set; get; }
        //Can be iBean, Eddystone...
        public int BroadcastingProtocol { set; get; }
        public string BroadcastingProtocolName { set; get; }
        //Can be BLE, sonic,...
        public int BeaconType { set; get; }
        public string BeaconTypeName { set; get; }
        public int UsageType { set; get; }
        public string UsageTypeName { set; get; }
        public int PurposeType { set; get; }
        public string PurposeTypeName { set; get; }
        public bool IsActive { set; get; }
        public bool Default { set; get; }
        public string FileId { set; get; }
        public string FileMimeType { set; get; }
    }
}
