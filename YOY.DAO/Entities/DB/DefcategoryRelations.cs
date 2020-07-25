using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefcategoryRelations
    {
        public Guid CategoryId { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Oltpcategories Category { get; set; }
    }
}
