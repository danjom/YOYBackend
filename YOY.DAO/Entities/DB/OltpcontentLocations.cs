using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpcontentLocations
    {
        public Guid ReferenceId { get; set; }
        public Guid LocationId { get; set; }
        public int ReferenceType { get; set; }
        public int LocationType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
