using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class UserInterestFactor
    {
        public Guid Id { set; get; }
        public Guid InterestId { set; get; }
        public int InterestType { set; get; }
        public double Factor { set; get; }
        public string MonthsRange { set; get; }
        public string DatesRange { set; get; }
        public string DaysOfWeekRange { set; get; }
        public string HoursInterval { set; get; }
        public DateTime? ExpirationDate { set; get; }
        public DateTime CreatedDate { set; get; }
        public DateTime UpdatedDate { set; get; }
        public bool IsActive { set; get; }
    }
}
