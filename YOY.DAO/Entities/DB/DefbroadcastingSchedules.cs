using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefbroadcastingSchedules
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }
        public int ContentType { get; set; }
        public int FromDay { get; set; }
        public int ToDay { get; set; }
        public int FromHour { get; set; }
        public int ToHour { get; set; }
        public int FromMinute { get; set; }
        public int ToMinute { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
