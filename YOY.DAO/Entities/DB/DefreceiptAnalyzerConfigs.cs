using System;
using System.Collections.Generic;

namespace YOY.DAO.Entities.DB
{
    public partial class DefreceiptAnalyzerConfigs
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? FranchiseeId { get; set; }
        public int StructureType { get; set; }
        public int ClaimMarkType { get; set; }
        public string ClaimMark { get; set; }
        public bool RewardClaimerExclusively { get; set; }
        public string TaxId { get; set; }
        public string TaxIdRegex { get; set; }
        public string CommerceName { get; set; }
        public string CommerceNameLinePos { get; set; }
        public string LegalName { get; set; }
        public string LegalPostalCode { get; set; }
        public string BranchNameLinePos { get; set; }
        public string BranchPostalCodePos { get; set; }
        public string PostalCodeRegex { get; set; }
        public string DateRegex { get; set; }
        public bool ContainsMultipleDates { get; set; }
        public string TimeRegex { get; set; }
        public bool DateAndTimeInSameLine { get; set; }
        public string DateTimeRegex { get; set; }
        public bool HasTicketNumber { get; set; }
        public string TicketNumberRegex { get; set; }
        public bool? TicketNumberLabelAndValueInSameLine { get; set; }
        public string ExtraUniqueFields { get; set; }
        public bool SpecialCharsToRemove { get; set; }
        public string CharsToRemoveRegex { get; set; }
        public string PurchasedItemsStartMark { get; set; }
        public string PurchasedItemsStartLineRegex { get; set; }
        public string PurchaseItemRegex { get; set; }
        public string LinesPerPurchaseItem { get; set; }
        public string MoneyAmountRegex { get; set; }
        public bool IsGreatestMoneyAmountTotal { get; set; }
        public string CurrencySymbol { get; set; }
        public string TotalAmountFullDataRegex { get; set; }
        public string TotalAmountValueRegex { get; set; }
        public bool TotalAmountLabelAndValueInSameLine { get; set; }
        public string TotalAmountInverseOrderPos { get; set; }
        public bool? TotalAmountHasCurrencySymbol { get; set; }
        public decimal TaxesPercentage { get; set; }
        public string PreTaxAmountFullDataRegex { get; set; }
        public string PreTaxAmountValueRegex { get; set; }
        public bool PreTaxAmountLabelAndValueInSameLine { get; set; }
        public string PreTaxAmountInverseOrderPos { get; set; }
        public bool? PreTaxAmountHasCurrencySymbol { get; set; }
        public int AmountsCurrencySymbolUsageType { get; set; }
        public string TotalAmountToleranceRange { get; set; }
        public bool ReleaseSurveyAfterValidation { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Deffranchisees Franchisee { get; set; }
    }
}
