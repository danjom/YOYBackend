using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefuserInterestFactors
    {
        public Guid Id { get; set; }
        public Guid InterestId { get; set; }
        public int InterestType { get; set; }
        public double Factor { get; set; }
        public string MonthsRange { get; set; }
        public string DatesRange { get; set; }
        public string DaysOfWeekRange { get; set; }
        public string HoursRange { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
