using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class OltpuserDisplayedContents
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ReferenceId { get; set; }
        public int ReferenceType { get; set; }
        public int TargetScreen { get; set; }
        public Guid? OwnerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
