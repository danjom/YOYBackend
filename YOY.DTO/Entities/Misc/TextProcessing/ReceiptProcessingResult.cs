using System;

namespace YOY.DTO.Entities.Misc.TextProcessing
{
    public class ReceiptProcessingResult
    {
        public int ValidationStatus { set; get; }
        public bool IsValid { set; get; }
        public int EarnedPointsType { set; get; }
        public int WalletEarnedPoints { set; get; }
        public int ClubEarnedPoints { set; get; }
        public string BranchName { set; get; }
        public Guid CommerceId { set; get; }
        public string CommerceName { set; get; }
        public string TotalAmount { set; get; }

    }
}
