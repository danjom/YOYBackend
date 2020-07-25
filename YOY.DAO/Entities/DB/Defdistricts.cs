using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defdistricts
    {
        public Defdistricts()
        {
            Defgeofences = new HashSet<Defgeofences>();
            Defgeozones = new HashSet<Defgeozones>();
        }

        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual Defcities City { get; set; }
        public virtual ICollection<Defgeofences> Defgeofences { get; set; }
        public virtual ICollection<Defgeozones> Defgeozones { get; set; }
    }
}
