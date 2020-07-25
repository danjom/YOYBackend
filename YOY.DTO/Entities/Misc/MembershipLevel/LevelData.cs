using System;

namespace YOY.DTO.Entities.Misc.MembershipLevel
{
    public class LevelData
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public int Pos { set; get; }
        public string IconUrl { set; get; }
        public double LoyaltyCashBackPercentage { set; get; }
        public double MonetaryConversionFactor { set; get; }
        public int MinGeneratedPoints { set; get; }
        public int MaxGeneratedPoints { set; get; }
        public int MinPurchasesCount { set; get; }
        public int MaxPurchasesCount { set; get; }
        public int MaxRewardRedemptions { set; get; }
        public int EvaluationMonths { set; get; }
        public string EnabledActions { set; get; }
        public int PointsLifeSpanMonths { set; get; }
        public int CheckInPoints { set; get; }
        public bool PointsToMoneyEnabled { set; get; }
        public string EnabledMonetaryAmounts { set; get; }

    }
}
