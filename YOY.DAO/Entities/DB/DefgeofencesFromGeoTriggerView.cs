using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefgeofencesFromGeoTriggerView
    {
        public Guid? BranchId { get; set; }
        public Guid? BranchTenantId { get; set; }
        public Guid? StateId { get; set; }
        public string BranchName { get; set; }
        public Guid ZoneId { get; set; }
        public string ZoneExternalId { get; set; }
        public string ZoneName { get; set; }
        public Guid Id { get; set; }
        public Guid? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string ExternalId { get; set; }
        public Guid? GeozoneId { get; set; }
        public string Name { get; set; }
        public decimal CenterLatitude { get; set; }
        public decimal CenterLongitude { get; set; }
        public decimal Radius { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Label { get; set; }
        public int? ActionType { get; set; }
        public bool IsActive { get; set; }
    }
}
