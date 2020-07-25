using System;

namespace YOY.DTO.Entities.Misc.MoneyConversionLog
{
    public class ConversionLogCountByCode
    {
        public Guid TenantId { set; get; }
        public string OwnerId { set; get; }
        public string ConversionCode { set; get; }
        public decimal RequiredPoints { set; get; }
        public decimal MoneyAmount { set; get; }
        public int Count { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public int State { set; get; }
        public string StateName { set; get; }
        public string TenantName { set; get; }
    }
}
