using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class Geotrigger
    {
        public Guid Id { set; get; }
        public Guid GeofenceId { set; get; }
        public string ExternalId { set; get; }
        public string Name { set; get; }
        public string GeofenceName { set; get; }
        public decimal GeofenceCenterLatitude { set; get; }
        public decimal GeofenceCenterLongitude { set; get; }
        public decimal GeofenceRadius { set; get; }
        public string ExternalGeofenceId { set; get; }
        public bool Enabled { set; get; }
        public bool IsActive { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public int TriggerType { set; get; }
        public string TriggerTypeName { set; get; }
    }
}
