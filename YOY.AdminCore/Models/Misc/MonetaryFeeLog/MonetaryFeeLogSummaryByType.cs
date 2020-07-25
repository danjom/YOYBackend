
namespace YOY.AdminCore.Models.Misc.MonetaryFeeLog
{
    public class MonetaryFeeLogSummaryByType
    {
        public int Reason { set; get; }
        public string ReasonName { set; get; }
        public int Status { set; get; }
        public string StatusName { set; get; }
        public int Count { set; get; }
        public decimal TotalAmount { set; get; }
        public string OldestDate { set; get; }
        public string MostRecentDate { set; get; }
    }
}