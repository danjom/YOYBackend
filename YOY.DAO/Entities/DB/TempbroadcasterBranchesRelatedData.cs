using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempbroadcasterBranchesRelatedData
    {
        public Guid BroadcasterId { get; set; }
        public string BroadcasterExternalId { get; set; }
        public Guid BroadcasterBranchId { get; set; }
        public bool BroadcasterMultiBrachEnabled { get; set; }
        public Guid BroadcasterDepartmentId { get; set; }
        public Guid BroadcasterTenantId { get; set; }
        public int BroadcasterBeaconType { get; set; }
        public int BroadcasterUsageType { get; set; }
        public int BroadcasterBroadcastingProtocol { get; set; }
        public Guid BranchId { get; set; }
        public Guid BranchTenantId { get; set; }
        public int BranchType { get; set; }
        public bool BranchActiveState { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Guid StateId { get; set; }
        public Guid CityId { get; set; }
        public string TenantName { get; set; }
        public int TenantLoyaltyProgramType { get; set; }
        public int TenantCheckInType { get; set; }
        public decimal TenantDefaultCommissionPercentage { get; set; }
        public int TenantBusinessStructure { get; set; }
        public string CampaignDefaultTitleMsg { get; set; }
        public string CampaignDefaultContentMsg { get; set; }
        public Guid TenantCategoryId { get; set; }
        public decimal? TenantScore { get; set; }
        public bool? TenantScoreActiveState { get; set; }
        public decimal? TenantCategoryScore { get; set; }
        public bool? TenantCategoryScoreActiveState { get; set; }
        public Guid? ContainedBranchId { get; set; }
        public Guid? ContainedBranchTenantId { get; set; }
        public int? ContainedBranchType { get; set; }
        public Guid? ContainedBranchHolderId { get; set; }
        public Guid? ContainedBranchHolderDepartmentId { get; set; }
        public string ContainedTenantName { get; set; }
        public int? ContainedTenantLoyaltyProgramType { get; set; }
        public int? ContainedTenantCheckInType { get; set; }
        public decimal? ContainedTenantDefaultCommissionPercentage { get; set; }
        public int? ContainedTenantBusinessStructure { get; set; }
        public string ContainedTenantCampaignDefaultTitleMsg { get; set; }
        public string ContainedTenantCampaignDefaultContentMsg { get; set; }
        public Guid? ContainedTenantCategoryId { get; set; }
        public decimal? ContainedTenantScore { get; set; }
        public bool? ContainedTenantScoreActiveState { get; set; }
        public decimal? ContainedTenantCategoryScore { get; set; }
        public bool? ContainedTenantCategoryScoreActiveState { get; set; }
    }
}
