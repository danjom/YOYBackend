using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefsearchIndexes
    {
        public DefsearchIndexes()
        {
            Oltpsearchables = new HashSet<Oltpsearchables>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AppName { get; set; }
        public int Type { get; set; }
        public int Service { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Oltpsearchables> Oltpsearchables { get; set; }
    }
}
