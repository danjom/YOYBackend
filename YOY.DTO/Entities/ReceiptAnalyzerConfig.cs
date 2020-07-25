using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YOY.DTO.Entities
{
    public class ReceiptAnalyzerConfig
    {
        public Guid Id { set; get; }
        public Guid TenantId { set; get; }
        public Guid? FranchiseeId { set; get; }
        public int StructureType { set; get; }
        public int ClaimMarkType { set; get; }
        public string ClaimMark { set; get; }
        public bool RewardClaimerExclusively { set; get; }
        public string TaxId { set; get; }
        public string TaxIdRegex { set; get; }
        public string CommerceName { set; get; }
        public string CommerceNameLinePos { set; get; }
        public string LegalName { set; get; }
        public string LegalPostalCode { set; get; }
        public string BranchNameLinePos { set; get; }
        public string BranchPostalCodePos { set; get; }
        public string PostalCodeRegex { set; get; }
        public string DateRegex { set; get; }
        public bool ContainsMultipleDates { set; get; }
        public string TimeRegex { set; get; }
        public bool DateAndTimeInSameLine { set; get; }
        public string DateTimeRegex { set; get; }
        public bool HasTicketNumber { set; get; }
        public string TicketNumberRegex { set; get; }
        public bool? TicketNumberLabelAndValueInSameLine { set; get; }
        public string ExtraUniqueFields { set; get; }
        public bool SpecialCharsToRemove { set; get; }
        public string CharsToRemoveRegex { set; get; }
        public string PurchasedItemsStartMark { set; get; }
        public string PurchasedItemsStartLineRegex { set; get; }
        public string PurchaseItemRegex { set; get; }
        public string LinesPerPurchaseItem { set; get; }
        public string MoneyAmountRegex { set; get; }
        public bool IsGreatestMoneyAmountTotal { set; get; }
        public string CurrencySymbol { set; get; }
        public string TotalAmountFullDataRegex { set; get; }
        public string TotalAmountValueRegex { set; get; }
        public bool TotalAmountLabelAndValueInSameLine { set; get; }
        public string TotalAmountInverseOrderPos { set; get; }
        public bool? TotalAmountHasCurrencySymbol { set; get; }
        public decimal TaxesPercentage { set; get; }
        public string PreTaxAmountFullDataRegex { set; get; }
        public string PreTaxAmountValueRegex { set; get; }
        public bool PreTaxAmountLabelAndValueInSameLine { set; get; }
        public string PreTaxAmountInverseOrderPos { set; get; }
        public bool? PreTaxAmountHasCurrencySymbol { set; get; }
        public int AmountsCurrencySymbolUsageType { set; get; }
        public string TotalAmountToleranceRange { set; get; }
        public bool ReleaseSurveyAfterValidation { set; get; }
        public DateTime CreatedDate { set; get; }
    }
}
