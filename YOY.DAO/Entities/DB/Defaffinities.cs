using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defaffinities
    {
        public Guid FirstReferenceId { get; set; }
        public Guid SecondReferenceId { get; set; }
        public int Type { get; set; }
        public double AffinityIndex { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
