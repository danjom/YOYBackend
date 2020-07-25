using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defgeofences
    {
        public Defgeofences()
        {
            Defbranches = new HashSet<Defbranches>();
            Defgeotriggers = new HashSet<Defgeotriggers>();
        }

        public Guid Id { get; set; }
        public Guid? GeozoneId { get; set; }
        public string Name { get; set; }
        public decimal CenterLatitude { get; set; }
        public decimal CenterLongitude { get; set; }
        public decimal Radius { get; set; }
        public string ExternalId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Label { get; set; }
        public int? ActionType { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public Guid? DistrictId { get; set; }

        public virtual Defdistricts District { get; set; }
        public virtual Defgeozones Geozone { get; set; }
        public virtual ICollection<Defbranches> Defbranches { get; set; }
        public virtual ICollection<Defgeotriggers> Defgeotriggers { get; set; }
    }
}
