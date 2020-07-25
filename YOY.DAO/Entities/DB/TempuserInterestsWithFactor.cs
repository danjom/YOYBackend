using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class TempuserInterestsWithFactor
    {
        public string UserId { get; set; }
        public Guid InterestId { get; set; }
        public int InterestType { get; set; }
        public int HerarchyLevel { get; set; }
        public decimal Score { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public int OriginType { get; set; }
        public Guid? FactorId { get; set; }
        public Guid? FactorInterestId { get; set; }
        public double? Factor { get; set; }
        public string DatesRange { get; set; }
        public string MonthsRange { get; set; }
        public string DaysOfWeekRange { get; set; }
        public string HoursRange { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
