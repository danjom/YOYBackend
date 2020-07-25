using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class Defkeywords
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public string Word { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public int Language { get; set; }

        public virtual Oltpcategories Category { get; set; }
    }
}
