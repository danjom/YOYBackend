using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.Values;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class ReceiptAnalyzerConfigManager
    {
        #region PROPERTIES_AND_RESOURCES

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private readonly BusinessObjects _businessObjects;

        #endregion

        #region METHODS

        public ReceiptAnalyzerConfig Get(Guid tenantId)
        {
            ReceiptAnalyzerConfig analyzerConfig = null;

            try
            {
                var query = from x in this._businessObjects.Context.DefreceiptAnalyzerConfigs
                            where x.TenantId == tenantId
                            select x;

                if (query != null)
                {
                    foreach (DefreceiptAnalyzerConfigs item in query)
                    {
                        analyzerConfig = new ReceiptAnalyzerConfig
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            FranchiseeId = item.FranchiseeId,
                            StructureType = item.StructureType,
                            ClaimMarkType = item.ClaimMarkType,
                            ClaimMark = item.ClaimMark,
                            RewardClaimerExclusively = item.RewardClaimerExclusively,
                            TaxId = item.TaxId,
                            TaxIdRegex = item.TaxIdRegex,
                            CommerceName = item.CommerceName,
                            CommerceNameLinePos = item.CommerceNameLinePos,
                            LegalName = item.LegalName,
                            LegalPostalCode = item.LegalPostalCode,
                            BranchNameLinePos = item.BranchNameLinePos,
                            BranchPostalCodePos = item.BranchPostalCodePos,
                            PostalCodeRegex = item.PostalCodeRegex,
                            DateRegex = item.DateRegex,
                            ContainsMultipleDates = item.ContainsMultipleDates,
                            TimeRegex = item.TimeRegex,
                            DateAndTimeInSameLine = item.DateAndTimeInSameLine,
                            DateTimeRegex = item.DateTimeRegex,
                            HasTicketNumber = item.HasTicketNumber,
                            TicketNumberRegex = item.TicketNumberRegex,
                            TicketNumberLabelAndValueInSameLine = item.TicketNumberLabelAndValueInSameLine,
                            ExtraUniqueFields = item.ExtraUniqueFields,
                            SpecialCharsToRemove = item.SpecialCharsToRemove,
                            CharsToRemoveRegex = item.CharsToRemoveRegex,
                            PurchasedItemsStartMark = item.PurchasedItemsStartMark,
                            PurchasedItemsStartLineRegex = item.PurchasedItemsStartLineRegex,
                            PurchaseItemRegex = item.PurchaseItemRegex,
                            LinesPerPurchaseItem = item.LinesPerPurchaseItem,
                            MoneyAmountRegex = item.MoneyAmountRegex,
                            IsGreatestMoneyAmountTotal = item.IsGreatestMoneyAmountTotal,
                            CurrencySymbol = item.CurrencySymbol,
                            TotalAmountFullDataRegex = item.TotalAmountFullDataRegex,
                            TotalAmountValueRegex = item.TotalAmountValueRegex,
                            TotalAmountLabelAndValueInSameLine = item.TotalAmountLabelAndValueInSameLine,
                            TotalAmountInverseOrderPos = item.TotalAmountInverseOrderPos,
                            TotalAmountHasCurrencySymbol = item.TotalAmountHasCurrencySymbol,
                            TaxesPercentage = item.TaxesPercentage,
                            PreTaxAmountFullDataRegex = item.PreTaxAmountFullDataRegex,
                            PreTaxAmountValueRegex = item.PreTaxAmountFullDataRegex,
                            PreTaxAmountLabelAndValueInSameLine = item.PreTaxAmountLabelAndValueInSameLine,
                            PreTaxAmountInverseOrderPos = item.PreTaxAmountInverseOrderPos,
                            PreTaxAmountHasCurrencySymbol = item.PreTaxAmountHasCurrencySymbol,
                            AmountsCurrencySymbolUsageType = item.AmountsCurrencySymbolUsageType,
                            TotalAmountToleranceRange = item.TotalAmountToleranceRange,
                            ReleaseSurveyAfterValidation = item.ReleaseSurveyAfterValidation,
                            CreatedDate = item.CreatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                analyzerConfig = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return analyzerConfig;
        }

        public bool Post(Guid tenantId, Guid? franchiseeId, int structureType, int claimMarkType, string claimMark, bool rewardClaimerExclusively, string taxId, string taxIdRegex, string commerceName, string commerceNameLinePos,
            string legalName, string legalPostalCode, string branchNameLinePos, string branchPostalCodePos, string postalCodeRegex, string dateRegex, bool containsMultipleDates, string timeRegex,
            bool dateAndTimeInSameLine, string dateTimeRegex, bool hasTicketNumber, string ticketNumberRegex, bool? ticketNumberLabelAndValueInSameLine, string extraUniqueFields, bool specialCharsToRemove, string charsToRemoveRegex,
            string purchasedItemsStartMark, string purchasedItemsStartLineRegex, string purchaseItemRegex, string linesPerPurchaseItem, string moneyAmountRegex, bool isGreatestMoneyAmountTotal, string currencySymbol,
            string totalAmountFullDataRegex, string totalAmountValueRegex, bool totalAmountLabelAndValueInSameLine, string totalAmountInverseOrderPos, bool totalAmountHasCurrencySymbol, decimal taxesPercentage,
            string preTaxAmountFullDataRegex, string preTaxAmountValueRegex, bool preTaxAmountLabelAndValueInSameLine, string preTaxAmountInverseOrderPos, bool? preTaxAmountHasCurrencySymbol, int amountsCurrencySymbolUsageType,
            string totalAmountToleranceRange, bool releaseSurveyAfterValidation)
        {
            bool success;
            try
            {
                DefreceiptAnalyzerConfigs newAnalyzerConfig = new DefreceiptAnalyzerConfigs
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    FranchiseeId = franchiseeId,
                    StructureType = structureType,
                    ClaimMarkType = claimMarkType,
                    ClaimMark = claimMark,
                    RewardClaimerExclusively = rewardClaimerExclusively,
                    TaxId = taxId,
                    TaxIdRegex = taxIdRegex,
                    CommerceName = commerceName,
                    CommerceNameLinePos = commerceNameLinePos,
                    LegalName = legalName,
                    LegalPostalCode = legalPostalCode,
                    BranchNameLinePos = branchNameLinePos,
                    BranchPostalCodePos = branchPostalCodePos,
                    PostalCodeRegex = postalCodeRegex,
                    DateRegex = dateRegex,
                    ContainsMultipleDates = containsMultipleDates,
                    TimeRegex = timeRegex,
                    DateAndTimeInSameLine = dateAndTimeInSameLine,
                    DateTimeRegex = dateTimeRegex,
                    HasTicketNumber = hasTicketNumber,
                    TicketNumberRegex = ticketNumberRegex,
                    TicketNumberLabelAndValueInSameLine = ticketNumberLabelAndValueInSameLine,
                    ExtraUniqueFields = extraUniqueFields,
                    SpecialCharsToRemove = specialCharsToRemove,
                    CharsToRemoveRegex = charsToRemoveRegex,
                    PurchasedItemsStartMark = purchasedItemsStartMark,
                    PurchasedItemsStartLineRegex = purchasedItemsStartLineRegex,
                    PurchaseItemRegex = purchaseItemRegex,
                    LinesPerPurchaseItem = linesPerPurchaseItem,
                    MoneyAmountRegex = moneyAmountRegex,
                    IsGreatestMoneyAmountTotal = isGreatestMoneyAmountTotal,
                    CurrencySymbol = currencySymbol,
                    TotalAmountFullDataRegex = totalAmountFullDataRegex,
                    TotalAmountValueRegex = totalAmountValueRegex,
                    TotalAmountToleranceRange = totalAmountToleranceRange,
                    TotalAmountLabelAndValueInSameLine = totalAmountLabelAndValueInSameLine,
                    TotalAmountInverseOrderPos = totalAmountInverseOrderPos,
                    TotalAmountHasCurrencySymbol = totalAmountHasCurrencySymbol,
                    TaxesPercentage = taxesPercentage,
                    PreTaxAmountFullDataRegex = preTaxAmountFullDataRegex,
                    PreTaxAmountValueRegex = preTaxAmountValueRegex,
                    PreTaxAmountLabelAndValueInSameLine = preTaxAmountLabelAndValueInSameLine,
                    PreTaxAmountInverseOrderPos = preTaxAmountInverseOrderPos,
                    PreTaxAmountHasCurrencySymbol = preTaxAmountHasCurrencySymbol,
                    AmountsCurrencySymbolUsageType = amountsCurrencySymbolUsageType,
                    ReleaseSurveyAfterValidation = releaseSurveyAfterValidation,
                    CreatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.DefreceiptAnalyzerConfigs.Add(newAnalyzerConfig);
                this._businessObjects.Context.SaveChanges();

                success = true;
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new FileManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public ReceiptAnalyzerConfigManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD FILE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
