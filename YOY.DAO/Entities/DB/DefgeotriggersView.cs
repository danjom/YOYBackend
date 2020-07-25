using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefgeotriggersView
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public Guid GeofenceId { get; set; }
        public string Name { get; set; }
        public int TriggerType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string GeofenceExternalId { get; set; }
        public string GeofenceName { get; set; }
        public Guid? GeozoneId { get; set; }
        public Guid? DistrictId { get; set; }
        public decimal GeofenceCenterLatitude { get; set; }
        public decimal GeofenceCenterLongitude { get; set; }
        public decimal GeofenceRadius { get; set; }
    }
}
