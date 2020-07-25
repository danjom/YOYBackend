using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defstates
    {
        public Defstates()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            Defbranches = new HashSet<Defbranches>();
            Defbroadcasters = new HashSet<Defbroadcasters>();
            Defcities = new HashSet<Defcities>();
        }

        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int UtcTimeZone { get; set; }
        public decimal CenterLatitude { get; set; }
        public decimal CenterLongitude { get; set; }
        public bool? IsActive { get; set; }
        public bool InOperation { get; set; }
        public Guid? NearestStateId { get; set; }

        public virtual Defcountries Country { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<Defbranches> Defbranches { get; set; }
        public virtual ICollection<Defbroadcasters> Defbroadcasters { get; set; }
        public virtual ICollection<Defcities> Defcities { get; set; }
    }
}
