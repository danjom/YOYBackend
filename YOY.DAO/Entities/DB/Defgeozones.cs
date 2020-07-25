using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defgeozones
    {
        public Defgeozones()
        {
            Defgeofences = new HashSet<Defgeofences>();
        }

        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public Guid? DistrictId { get; set; }
        public string LocationAddress { get; set; }
        public string DescriptiveAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int MinRetriggeredMins { get; set; }
        public bool? IsActive { get; set; }

        public virtual Defcountries Country { get; set; }
        public virtual Defdistricts District { get; set; }
        public virtual ICollection<Defgeofences> Defgeofences { get; set; }
    }
}
