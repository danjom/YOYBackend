using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpsearchLogs
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public Guid IndexId { get; set; }
        public DateTime Date { get; set; }
        public int SourceType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Oltpsearchables Oltpsearchables { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
