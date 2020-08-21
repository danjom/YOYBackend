using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.UserAPI.Models.v1.Content.POCO
{
    public class CashIncentiveContentCellDetail : CellContainedObject
    {
        public int DealType { set; get; }
        public string DealTypeName { set; get; }
        public string DealTypeIcon { set; get; }
        public bool Favorite { set; get; }
        public string CommerceLogo { set; get; }
        public string MainHint { set; get; }
        public string ComplementaryHint { set; get; }
        public string MinPurchaseAmountToApplyHint { set; get; }
        public bool DisplayMinPurchaseAmountToApplyHint { set; get; }
        public string MembershipLevelHint { set; get; }
        public int MinMembershipLevel { set; get; }
        public bool DisplayMembershipLevelHint { set; get; }
        public bool AppliesToInAppPurchases { set; get; }
        public bool DisplayValue { set; get; }
        public double Value { set; get; }
        public string ValueLiteral { set; get; }
        public double RegularValue { set; get; }
        public string RegularValueLiteral { set; get; }
        public bool DisplayRegularValue { set; get; }
        public string MaxValueHint { set; get; }
        public bool DisplayMaxValueHint { set; get; }
        public string MainUnlockHint { set; get; }
        public bool DisplayMainUnlockHint { set; get; }
        public string ComplementaryUnlockHint { set; get; }
        public bool DisplayComplementaryUnlockHint { set; get; }
        public string ExpirationDate { set; get; }
        public bool DisplayExpirationHint { set; get; }
        public string AvailabiltySchedule { set; get; }
        public bool DisplayAvailabilitySchedule { set; get; }
        public bool AvailableToUse { set; get; }
    }
}
