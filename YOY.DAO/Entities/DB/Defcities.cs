using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defcities
    {
        public Defcities()
        {
            Defbranches = new HashSet<Defbranches>();
            Defdistricts = new HashSet<Defdistricts>();
        }

        public Guid Id { get; set; }
        public Guid StateId { get; set; }
        public string Name { get; set; }
        public int UtctimeDifference { get; set; }
        public bool? IsActive { get; set; }

        public virtual Defstates State { get; set; }
        public virtual ICollection<Defbranches> Defbranches { get; set; }
        public virtual ICollection<Defdistricts> Defdistricts { get; set; }
    }
}
