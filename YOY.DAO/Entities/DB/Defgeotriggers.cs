using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defgeotriggers
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public Guid GeofenceId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int TriggerType { get; set; }

        public virtual Defgeofences Geofence { get; set; }
    }
}
