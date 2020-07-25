using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Receipt;
using YOY.Values;
using YOY.Values.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;

namespace YOY.DAO.Entities.Manager
{
    public class ReceiptManager
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
        #region RECEIPTS

        private string GetPurposeName(int purpose)
        {
            string purposeName = purpose switch
            {
                ReceiptPurposes.None => Resources.None,
                ReceiptPurposes.ValidateDealClaim => Resources.ValidateDealClaim,
                ReceiptPurposes.ValidateProductPurchase => Resources.ValidateProductPurchase,
                _ => "--",
            };
            return purposeName;
        }

        private string GetStatusName(int status)
        {
            string statusName = status switch
            {
                ReceiptValidationStatuses.None => Resources.None,
                ReceiptValidationStatuses.Submitted => Resources.Submitted,
                ReceiptValidationStatuses.AutomaticallyValidated => Resources.AutomaticallyValidated,
                ReceiptValidationStatuses.RequiresManualValidation => Resources.RequiresManualValidation,
                ReceiptValidationStatuses.ManuallyValidated => Resources.ManuallyValidated,
                ReceiptValidationStatuses.Invalid => Resources.Invalid,
                ReceiptValidationStatuses.Rejected => Resources.Rejected,
                _ => "--",
            };
            return statusName;
        }

        private string GetExtrationStatusName(int status)
        {
            string statusName = status switch
            {
                ReceiptExtrationStatuses.None => Resources.None,
                ReceiptExtrationStatuses.Failed => Resources.Failed,
                ReceiptExtrationStatuses.AutomaticallyExtracted => Resources.AutomaticallyExtracted,
                ReceiptExtrationStatuses.RequiresManualExtraction => Resources.RequiresManualExtraction,
                ReceiptExtrationStatuses.ManuallyExtracted => Resources.ManuallyExtracted,
                _ => "--",
            };
            return statusName;
        }

        private string GetAmountsMatchStatusName(int status)
        {
            string statusName = status switch
            {
                AmountsMatchStatuses.NotDetermined => Resources.NotDetermined,
                AmountsMatchStatuses.TotalAmountGreater => Resources.TotalAmountGreater,
                AmountsMatchStatuses.ItemsPricesGreater => Resources.ItemsPricesGreater,
                AmountsMatchStatuses.Match => Resources.Match,
                _ => "--",
            };
            return statusName;
        }

        private string GetPointsTypeName(int type)
        {
            string typeName = type switch
            {
                EarnedPointsTypes.Wallet => Resources.YOYWallet,
                EarnedPointsTypes.Club => Resources.Club,
                EarnedPointsTypes.WalletAndClub => Resources.WalletAndClub,
                _ => "--",
            };
            return typeName;
        }

        private string GetClaimMarkTypeName(int type)
        {
            string typeName = type switch
            {
                ReceiptClaimMarkTypes.None => Resources.None,
                ReceiptClaimMarkTypes.InPurchasedItem => Resources.InPurchasedItem,
                ReceiptClaimMarkTypes.InTicketInfoAppName => Resources.InTicketInfoAppName,
                ReceiptClaimMarkTypes.InTicketInfoAccNumber => Resources.InTicketInfoAccNumber,
                ReceiptClaimMarkTypes.InTicketInfoRefCode => Resources.InTicketInfoRefCode,
                _ => "--",
            };
            return typeName;
        }

        public List<Receipt> Gets(Guid? tenantId, string userId, int status, int purpose, int extractionStatus, DateTime start, DateTime end, int pageSize, int pageNumber)
        {
            List<Receipt> receipts = null;

            try
            {
                var query = (dynamic)null;

                if (tenantId != null)
                {
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        if (status != ReceiptValidationStatuses.All)
                        {
                            if (purpose != ReceiptPurposes.All)
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.UserId == userId && x.Status == status && x.ExtractionStatus == extractionStatus && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.UserId == userId && x.Status == status && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.UserId == userId && x.Status == status && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.UserId == userId && x.Status == status && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                        else
                        {
                            if (purpose != ReceiptPurposes.All)
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.UserId == userId && x.ExtractionStatus == extractionStatus && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.UserId == userId && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.UserId == userId && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.UserId == userId && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (status != ReceiptValidationStatuses.All)
                        {
                            if (purpose != ReceiptPurposes.All)
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.Status == status && x.ExtractionStatus == extractionStatus && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.Status == status && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.Status == status && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.Status == status && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                        else
                        {
                            if (purpose != ReceiptPurposes.All)
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.ExtractionStatus == extractionStatus && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.TenantId == (Guid)tenantId && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        if (status != ReceiptValidationStatuses.All)
                        {
                            if (purpose != ReceiptPurposes.All)
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.UserId == userId && x.Status == status && x.ExtractionStatus == extractionStatus && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.UserId == userId && x.Status == status && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.UserId == userId && x.Status == status && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.UserId == userId && x.Status == status && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                        else
                        {
                            if (purpose != ReceiptPurposes.All)
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.UserId == userId && x.Purpose == purpose && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.UserId == userId && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.UserId == userId && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.UserId == userId && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (status != ReceiptValidationStatuses.All)
                        {
                            if (purpose != ReceiptPurposes.All)
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.Status == status && x.ExtractionStatus == extractionStatus && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.Status == status && x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.Status == status && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.Status == status && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                        else
                        {
                            if (purpose != ReceiptPurposes.All)
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.Purpose == purpose && x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.Purpose == purpose && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                            else
                            {
                                if (extractionStatus != ReceiptExtrationStatuses.All)
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.ExtractionStatus == extractionStatus && x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                                else
                                {
                                    query = (from x in this._businessObjects.Context.Oltpreceipts
                                             where x.CreatedDate >= start && x.CreatedDate <= end
                                             orderby x.CreatedDate descending
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    Receipt receipt;
                    receipts = new List<Receipt>();

                    foreach (Oltpreceipts item in query)
                    {
                        receipt = new Receipt
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            PointsOpId = item.PointsOpId,
                            FranchiseeId = item.FranchiseeId,
                            ClaimMarkType = item.ClaimMarkType,
                            ClaimMarkTypeName = GetClaimMarkTypeName(item.ClaimMarkType),
                            ClaimMark = item.ClaimMark,
                            ClaimerSubmit = item.ClaimerSubmit,
                            ValidStructure = item.ValidStructure,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            PicturesCount = item.PicturesCount,
                            PictureExtractionsCount = item.PictureExtractionsCount,
                            ExtrationStatus = item.ExtractionStatus,
                            ExtrationStatusName = GetExtrationStatusName(item.ExtractionStatus),
                            Purpose = item.Purpose,
                            PurposeName = GetPurposeName(item.Purpose),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            BusinessName = item.BusinessName,
                            LegalName = item.LegalName,
                            TaxId = item.TaxId,
                            PostalCode = item.PostalCode,
                            BranchName = item.BranchName,
                            TicketNumber = item.TicketNumber,
                            ContainedUniqueValues = item.ContainedUniqueValues,
                            PurchaseDate = item.PurchaseDate,
                            PurchasedItems = item.PurchasedItems,
                            ContainedDeals = item.ContainedDeals,
                            ContainsDeals = item.ContainsDeals,
                            LoyaltyValidation = item.LoyaltyValidation,
                            TaxAmount = item.TaxAmount,
                            PreTaxAmountFound = item.PreTaxAmountFound,
                            PreTaxAmount = item.PreTaxAmount,
                            TotalAmountFound = item.TotalAmountFound,
                            TotalAmount = item.TotalAmount,
                            PurchasedAmountPrices = item.PurchaseItemsPrices,
                            AmountsMatchStatus = item.AmountsMatchStatus,
                            AmountsMatchStatusName = item.AmountsMatchStatus != null ? GetAmountsMatchStatusName((int)item.AmountsMatchStatus) : "-",
                            TotalAmountInRange = item.TotalAmountInRange,
                            ConfirmedByUser = item.ConfirmedByUser,
                            UserEarnedPoints = item.UserEarnedPoints,
                            PointsType = item.PointsType,
                            PointsTypeName = item.PointsType != null ? GetPointsTypeName((int)item.PointsType) : "",
                            CommissionFeeAmount = item.CommissionFeeAmount,
                            RetainedTaxAmount = item.RetainedTaxAmount,
                            UserEarnedMoneyAmount = item.UserEarnedMoneyAmount,
                            NetworkEarningsInvolved = item.NetworkEarningInvolved
                        };

                        receipts.Add(receipt);
                    }
                }
            }
            catch (Exception e)
            {
                receipts = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receipts;
        }

        public Receipt Get(Guid id)
        {
            Receipt receipt = null;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpreceipts
                            where x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (Oltpreceipts item in query)
                    {
                        receipt = new Receipt
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            PointsOpId = item.PointsOpId,
                            FranchiseeId = item.FranchiseeId,
                            ClaimMarkType = item.ClaimMarkType,
                            ClaimMarkTypeName = GetClaimMarkTypeName(item.ClaimMarkType),
                            ClaimMark = item.ClaimMark,
                            ClaimerSubmit = item.ClaimerSubmit,
                            ValidStructure = item.ValidStructure,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            PicturesCount = item.PicturesCount,
                            PictureExtractionsCount = item.PictureExtractionsCount,
                            ExtrationStatus = item.ExtractionStatus,
                            ExtrationStatusName = GetExtrationStatusName(item.ExtractionStatus),
                            Purpose = item.Purpose,
                            PurposeName = GetPurposeName(item.Purpose),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            BusinessName = item.BusinessName,
                            LegalName = item.LegalName,
                            TaxId = item.TaxId,
                            PostalCode = item.PostalCode,
                            BranchName = item.BranchName,
                            TicketNumber = item.TicketNumber,
                            ContainedUniqueValues = item.ContainedUniqueValues,
                            PurchaseDate = item.PurchaseDate,
                            PurchasedItems = item.PurchasedItems,
                            ContainedDeals = item.ContainedDeals,
                            ContainsDeals = item.ContainsDeals,
                            LoyaltyValidation = item.LoyaltyValidation,
                            TaxAmount = item.TaxAmount,
                            PreTaxAmountFound = item.PreTaxAmountFound,
                            PreTaxAmount = item.PreTaxAmount,
                            TotalAmountFound = item.TotalAmountFound,
                            TotalAmount = item.TotalAmount,
                            PurchasedAmountPrices = item.PurchaseItemsPrices,
                            AmountsMatchStatus = item.AmountsMatchStatus,
                            AmountsMatchStatusName = item.AmountsMatchStatus != null ? GetAmountsMatchStatusName((int)item.AmountsMatchStatus) : "-",
                            TotalAmountInRange = item.TotalAmountInRange,
                            ConfirmedByUser = item.ConfirmedByUser,
                            UserEarnedPoints = item.UserEarnedPoints,
                            PointsType = item.PointsType,
                            PointsTypeName = item.PointsType != null ? GetPointsTypeName((int)item.PointsType) : "",
                            CommissionFeeAmount = item.CommissionFeeAmount,
                            RetainedTaxAmount = item.RetainedTaxAmount,
                            UserEarnedMoneyAmount = item.UserEarnedMoneyAmount,
                            NetworkEarningsInvolved = item.NetworkEarningInvolved
                        };

                    }
                }
            }
            catch (Exception e)
            {
                receipt = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receipt;
        }

        public Receipt Post(string userId, Guid tenantId, Guid? pointsOpId, Guid? franchiseeId, int claimMarkType, string claimMark, bool claimerSubmit, bool validStructure, int picturesCount, int pictureExtrationsCount,
            int extractionStatus, int purpose, int status, string businessName, string legalName, string taxId, string postalCode, string branchName, string ticketNumber, string containedUniqueValues, DateTime purchasedDate,
            string purchasedItems, string containedDeals, bool containsDeals, bool loyaltyValidation, decimal? taxAmount, bool? preTaxAmountFound, decimal? preTaxAmount, bool? totalAmountFound, decimal? totalAmount,
            string purchasedItemsPrices, int? amountMatchStatus, bool? totalAmountInRange, bool? confirmedAmountByUser, decimal? UserEarnedPoints, int? pointsType, decimal? commissionFeeAmount, decimal? retainedTaxAmount,
            decimal? userEarnedMoneyAmount, bool? networkEarningsInvolved)
        {
            Receipt receipt;
            try
            {
                Oltpreceipts newReceipt = new Oltpreceipts
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    TenantId = tenantId,
                    PointsOpId = pointsOpId,
                    FranchiseeId = franchiseeId,
                    ClaimMarkType = claimMarkType,
                    ClaimMark = claimMark,
                    ClaimerSubmit = claimerSubmit,
                    ValidStructure = validStructure,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    PicturesCount = picturesCount,
                    PictureExtractionsCount = pictureExtrationsCount,
                    ExtractionStatus = extractionStatus,
                    Purpose = purpose,
                    Status = status,
                    BusinessName = businessName,
                    LegalName = legalName,
                    TaxId = taxId,
                    PostalCode = postalCode,
                    BranchName = branchName,
                    TicketNumber = ticketNumber,
                    ContainedUniqueValues = containedUniqueValues,
                    PurchaseDate = purchasedDate,
                    PurchasedItems = purchasedItems,
                    ContainedDeals = containedDeals,
                    ContainsDeals = containsDeals,
                    LoyaltyValidation = loyaltyValidation,
                    TaxAmount = taxAmount,
                    PreTaxAmountFound = preTaxAmountFound,
                    PreTaxAmount = preTaxAmount,
                    TotalAmountFound = totalAmountFound,
                    TotalAmount = totalAmount,
                    PurchaseItemsPrices = purchasedItemsPrices,
                    AmountsMatchStatus = amountMatchStatus,
                    TotalAmountInRange = totalAmountInRange,
                    ConfirmedByUser = confirmedAmountByUser,
                    UserEarnedPoints = UserEarnedPoints,
                    PointsType = pointsType,
                    CommissionFeeAmount = commissionFeeAmount,
                    RetainedTaxAmount = retainedTaxAmount,
                    UserEarnedMoneyAmount = userEarnedMoneyAmount,
                    NetworkEarningInvolved = networkEarningsInvolved
                };

                this._businessObjects.Context.Oltpreceipts.Add(newReceipt);
                this._businessObjects.Context.SaveChanges();

                receipt = new Receipt
                {
                    Id = newReceipt.Id,
                    UserId = newReceipt.UserId,
                    TenantId = newReceipt.TenantId,
                    PointsOpId = newReceipt.PointsOpId,
                    FranchiseeId = newReceipt.FranchiseeId,
                    ClaimMarkType = newReceipt.ClaimMarkType,
                    ClaimMarkTypeName = GetClaimMarkTypeName(newReceipt.ClaimMarkType),
                    ClaimMark = newReceipt.ClaimMark,
                    ClaimerSubmit = newReceipt.ClaimerSubmit,
                    ValidStructure = newReceipt.ValidStructure,
                    CreatedDate = newReceipt.CreatedDate,
                    UpdatedDate = newReceipt.UpdatedDate,
                    PicturesCount = newReceipt.PicturesCount,
                    PictureExtractionsCount = newReceipt.PictureExtractionsCount,
                    ExtrationStatus = newReceipt.ExtractionStatus,
                    ExtrationStatusName = GetExtrationStatusName(newReceipt.ExtractionStatus),
                    Purpose = newReceipt.Purpose,
                    PurposeName = GetPurposeName(newReceipt.Purpose),
                    Status = newReceipt.Status,
                    StatusName = GetStatusName(newReceipt.Status),
                    BusinessName = newReceipt.BusinessName,
                    LegalName = newReceipt.LegalName,
                    TaxId = newReceipt.TaxId,
                    PostalCode = newReceipt.PostalCode,
                    BranchName = newReceipt.BranchName,
                    TicketNumber = newReceipt.TicketNumber,
                    ContainedUniqueValues = newReceipt.ContainedUniqueValues,
                    PurchaseDate = newReceipt.PurchaseDate,
                    PurchasedItems = newReceipt.PurchasedItems,
                    ContainedDeals = newReceipt.ContainedDeals,
                    ContainsDeals = newReceipt.ContainsDeals,
                    LoyaltyValidation = newReceipt.LoyaltyValidation,
                    TaxAmount = newReceipt.TaxAmount,
                    PreTaxAmountFound = newReceipt.PreTaxAmountFound,
                    PreTaxAmount = newReceipt.PreTaxAmount,
                    TotalAmountFound = newReceipt.TotalAmountFound,
                    TotalAmount = newReceipt.TotalAmount,
                    PurchasedAmountPrices = newReceipt.PurchaseItemsPrices,
                    AmountsMatchStatus = newReceipt.AmountsMatchStatus,
                    AmountsMatchStatusName = newReceipt.AmountsMatchStatus != null ? GetAmountsMatchStatusName((int)newReceipt.AmountsMatchStatus) : "-",
                    TotalAmountInRange = newReceipt.TotalAmountInRange,
                    ConfirmedByUser = newReceipt.ConfirmedByUser,
                    UserEarnedPoints = newReceipt.UserEarnedPoints,
                    PointsType = newReceipt.PointsType,
                    PointsTypeName = newReceipt.PointsType != null ? GetPointsTypeName((int)newReceipt.PointsType) : "",
                    CommissionFeeAmount = newReceipt.CommissionFeeAmount,
                    RetainedTaxAmount = newReceipt.RetainedTaxAmount,
                    UserEarnedMoneyAmount = newReceipt.UserEarnedMoneyAmount,
                    NetworkEarningsInvolved = newReceipt.NetworkEarningInvolved
                };
            }
            catch (Exception e)
            {
                receipt = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receipt;
        }

        public Receipt Post(string userId, Guid tenantId, Guid? pointsOpId, int picturesCount, int pictureExtrationsCount, int extractionStatus, int purpose, int status)
        {
            Receipt receipt;
            try
            {
                Oltpreceipts newReceipt = new Oltpreceipts
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    TenantId = tenantId,
                    PointsOpId = pointsOpId,
                    FranchiseeId = null,
                    ClaimMarkType = 0,
                    ClaimMark = "",
                    ClaimerSubmit = false,
                    ValidStructure = false,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    PicturesCount = picturesCount,
                    PictureExtractionsCount = pictureExtrationsCount,
                    ExtractionStatus = extractionStatus,
                    Purpose = purpose,
                    Status = status,
                    BusinessName = "",
                    LegalName = "",
                    TaxId = "",
                    PostalCode = "",
                    BranchName = "",
                    TicketNumber = "",
                    ContainedUniqueValues = "",
                    PurchaseDate = null,
                    PurchasedItems = "",
                    ContainedDeals = "",
                    ContainsDeals = false,
                    LoyaltyValidation = false,
                    TaxAmount = null,
                    PreTaxAmountFound = null,
                    PreTaxAmount = null,
                    TotalAmountFound = null,
                    TotalAmount = null,
                    PurchaseItemsPrices = "",
                    AmountsMatchStatus = null,
                    TotalAmountInRange = null,
                    ConfirmedByUser = null,
                    UserEarnedPoints = null,
                    PointsType = null,
                    CommissionFeeAmount = null,
                    RetainedTaxAmount = null,
                    UserEarnedMoneyAmount = null,
                    NetworkEarningInvolved = null
                };

                this._businessObjects.Context.Oltpreceipts.Add(newReceipt);
                this._businessObjects.Context.SaveChanges();

                receipt = new Receipt
                {
                    Id = newReceipt.Id,
                    UserId = newReceipt.UserId,
                    TenantId = newReceipt.TenantId,
                    PointsOpId = newReceipt.PointsOpId,
                    FranchiseeId = newReceipt.FranchiseeId,
                    ClaimMarkType = newReceipt.ClaimMarkType,
                    ClaimMarkTypeName = GetClaimMarkTypeName(newReceipt.ClaimMarkType),
                    ClaimMark = newReceipt.ClaimMark,
                    ClaimerSubmit = newReceipt.ClaimerSubmit,
                    ValidStructure = newReceipt.ValidStructure,
                    CreatedDate = newReceipt.CreatedDate,
                    UpdatedDate = newReceipt.UpdatedDate,
                    PicturesCount = newReceipt.PicturesCount,
                    PictureExtractionsCount = newReceipt.PictureExtractionsCount,
                    ExtrationStatus = newReceipt.ExtractionStatus,
                    ExtrationStatusName = GetExtrationStatusName(newReceipt.ExtractionStatus),
                    Purpose = newReceipt.Purpose,
                    PurposeName = GetPurposeName(newReceipt.Purpose),
                    Status = newReceipt.Status,
                    StatusName = GetStatusName(newReceipt.Status),
                    BusinessName = newReceipt.BusinessName,
                    LegalName = newReceipt.LegalName,
                    TaxId = newReceipt.TaxId,
                    PostalCode = newReceipt.PostalCode,
                    BranchName = newReceipt.BranchName,
                    TicketNumber = newReceipt.TicketNumber,
                    ContainedUniqueValues = newReceipt.ContainedUniqueValues,
                    PurchaseDate = newReceipt.PurchaseDate,
                    PurchasedItems = newReceipt.PurchasedItems,
                    ContainedDeals = newReceipt.ContainedDeals,
                    ContainsDeals = newReceipt.ContainsDeals,
                    LoyaltyValidation = newReceipt.LoyaltyValidation,
                    TaxAmount = newReceipt.TaxAmount,
                    PreTaxAmountFound = newReceipt.PreTaxAmountFound,
                    PreTaxAmount = newReceipt.PreTaxAmount,
                    TotalAmountFound = newReceipt.TotalAmountFound,
                    TotalAmount = newReceipt.TotalAmount,
                    PurchasedAmountPrices = newReceipt.PurchaseItemsPrices,
                    AmountsMatchStatus = newReceipt.AmountsMatchStatus,
                    AmountsMatchStatusName = newReceipt.AmountsMatchStatus != null ? GetAmountsMatchStatusName((int)newReceipt.AmountsMatchStatus) : "-",
                    TotalAmountInRange = newReceipt.TotalAmountInRange,
                    ConfirmedByUser = newReceipt.ConfirmedByUser,
                    UserEarnedPoints = newReceipt.UserEarnedPoints,
                    PointsType = newReceipt.PointsType,
                    PointsTypeName = newReceipt.PointsType != null ? GetPointsTypeName((int)newReceipt.PointsType) : "",
                    CommissionFeeAmount = newReceipt.CommissionFeeAmount,
                    RetainedTaxAmount = newReceipt.RetainedTaxAmount,
                    UserEarnedMoneyAmount = newReceipt.UserEarnedMoneyAmount,
                    NetworkEarningsInvolved = newReceipt.NetworkEarningInvolved
                };
            }
            catch (Exception e)
            {
                receipt = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receipt;
        }

        public bool Put(Guid id, Guid? pointsOpId, Guid? franchiseeId, int claimMarkType, string claimMark, bool claimerSubmit, bool validStructure, int extractionStatus, int status, string businessName,
            string legalName, string taxId, string postalCode, string branchName, string ticketNumber, string containedUniqueValues, DateTime purchaseDate, string purchasedItems, string containedDeals,
            bool containsDeals, bool loyaltyValidation, decimal? taxAmount, bool preTaxAmountFound, decimal? preTaxAmount, bool totalAmountFound, decimal? totalAmount, string purchasedItemPrices,
            int amountMatchStatus, bool? totalAmountInRange, bool? confirmedByUser, decimal? userEarnedPoints, int? pointsType, decimal? commissionFeeAmount, decimal retainedTaxesAmount,
            decimal? userEarnedMoneyAmount, bool? networkEarningsInvolved)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpreceipts
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    Oltpreceipts receipt = null;

                    foreach (Oltpreceipts item in query)
                    {
                        receipt = item;
                    }

                    if (receipt != null)
                    {
                        receipt.PointsOpId = pointsOpId;
                        receipt.FranchiseeId = franchiseeId;
                        receipt.ClaimMarkType = claimMarkType;
                        receipt.ClaimMark = claimMark;
                        receipt.ClaimerSubmit = claimerSubmit;
                        receipt.ValidStructure = validStructure;
                        receipt.ExtractionStatus = extractionStatus;
                        receipt.Status = status;
                        receipt.BusinessName = businessName;
                        receipt.LegalName = legalName;
                        receipt.TaxId = taxId;
                        receipt.PostalCode = postalCode;
                        receipt.BranchName = branchName;
                        receipt.TicketNumber = ticketNumber;
                        receipt.ContainedUniqueValues = containedUniqueValues;
                        receipt.PurchaseDate = purchaseDate;
                        receipt.PurchasedItems = purchasedItems;
                        receipt.ContainedDeals = containedDeals;
                        receipt.ContainsDeals = containsDeals;
                        receipt.LoyaltyValidation = loyaltyValidation;
                        receipt.TaxAmount = taxAmount;
                        receipt.PreTaxAmountFound = preTaxAmountFound;
                        receipt.PreTaxAmount = preTaxAmount;
                        receipt.TotalAmountFound = totalAmountFound;
                        receipt.TotalAmount = totalAmount;
                        receipt.PurchaseItemsPrices = purchasedItemPrices;
                        receipt.AmountsMatchStatus = amountMatchStatus;
                        receipt.TotalAmountInRange = totalAmountInRange;
                        receipt.ConfirmedByUser = confirmedByUser;
                        receipt.UserEarnedPoints = userEarnedPoints;
                        receipt.PointsType = pointsType;
                        receipt.CommissionFeeAmount = commissionFeeAmount;
                        receipt.RetainedTaxAmount = retainedTaxesAmount;
                        receipt.UserEarnedMoneyAmount = userEarnedMoneyAmount;
                        receipt.NetworkEarningInvolved = networkEarningsInvolved;
                        receipt.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }

                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Put(Guid id, Guid? tenantId, bool confirmedByUser)
        {
            bool success = false;

            try
            {
                var query = (dynamic)null;

                if (tenantId != null)
                {
                    query = from x in this._businessObjects.Context.Oltpreceipts
                            where x.TenantId == tenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.Oltpreceipts
                            where x.Id == id
                            select x;
                }

                if (query != null)
                {
                    Oltpreceipts receipt = null;

                    foreach (Oltpreceipts item in query)
                    {
                        receipt = item;
                    }

                    if (receipt != null && receipt.ConfirmedByUser == null)
                    {

                        receipt.ConfirmedByUser = confirmedByUser;
                        receipt.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }

                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Put(Guid id, Guid? tenantId, bool confirmedByUser, int status)
        {
            bool success = false;

            try
            {
                var query = (dynamic)null;

                if (tenantId != null)
                {
                    query = from x in this._businessObjects.Context.Oltpreceipts
                            where x.TenantId == tenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.Oltpreceipts
                            where x.Id == id
                            select x;
                }

                if (query != null)
                {
                    Oltpreceipts receipt = null;

                    foreach (Oltpreceipts item in query)
                    {
                        receipt = item;
                    }

                    if (receipt != null && receipt.ConfirmedByUser == null)
                    {

                        receipt.ConfirmedByUser = confirmedByUser;
                        receipt.Status = status;
                        receipt.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }

                }
            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }

        public bool Put(Guid id, Guid? tenantId, Guid pointsOpId)
        {
            bool success = false;

            try
            {
                var query = (dynamic)null;

                if (tenantId != null)
                {
                    query = from x in this._businessObjects.Context.Oltpreceipts
                            where x.TenantId == tenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.Oltpreceipts
                            where x.Id == id
                            select x;
                }

                if (query != null)
                {
                    Oltpreceipts receipt = null;

                    foreach (Oltpreceipts item in query)
                    {
                        receipt = item;
                    }

                    if (receipt != null && receipt.PointsOpId == null)
                    {

                        receipt.PointsOpId = pointsOpId;
                        receipt.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }

                }
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

        #region RECEIPTPICTURES

        public List<ReceiptPicture> Gets(Guid receiptId, int extractionStatus)
        {
            List<ReceiptPicture> receiptPictures = null;

            try
            {
                var query = (dynamic)null;

                if (extractionStatus != ReceiptExtrationStatuses.All)
                {
                    query = from x in this._businessObjects.Context.OltpreceiptPictures
                            where x.ReceiptId == receiptId && x.ExtractionStatus == extractionStatus
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpreceiptPictures
                            where x.ReceiptId == receiptId
                            select x;
                }

                if (query != null)
                {
                    ReceiptPicture receiptPicture;
                    receiptPictures = new List<ReceiptPicture>();

                    foreach (OltpreceiptPictures item in query)
                    {
                        receiptPicture = new ReceiptPicture
                        {
                            Id = item.Id,
                            ReceiptId = item.ReceiptId,
                            ImgId = item.ImgId,
                            ExtractionStatus = item.ExtractionStatus,
                            ExtrationStatusName = GetExtrationStatusName(item.ExtractionStatus),
                            Position = item.Position,
                            FullContent = item.FullContent,
                            ContainedUniqueValues = item.ContainedUniqueValues,
                            RelevantContent = item.RelevantContent,
                            PurchasedItems = item.PurchasedItems,
                            ContainsDeals = item.ContainsDeals,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        receiptPictures.Add(receiptPicture);
                    }
                }
            }
            catch (Exception e)
            {
                receiptPictures = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receiptPictures;
        }

        public ReceiptPicture Get(Guid receiptId, int picturePos)
        {
            ReceiptPicture receiptPicture = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpreceiptPictures
                            where x.ReceiptId == receiptId && x.Position == picturePos
                            select x;

                if (query != null)
                {

                    foreach (OltpreceiptPictures item in query)
                    {
                        receiptPicture = new ReceiptPicture
                        {
                            Id = item.Id,
                            ReceiptId = item.ReceiptId,
                            ImgId = item.ImgId,
                            ExtractionStatus = item.ExtractionStatus,
                            ExtrationStatusName = GetExtrationStatusName(item.ExtractionStatus),
                            Position = item.Position,
                            FullContent = item.FullContent,
                            ContainedUniqueValues = item.ContainedUniqueValues,
                            RelevantContent = item.RelevantContent,
                            PurchasedItems = item.PurchasedItems,
                            ContainsDeals = item.ContainsDeals,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                receiptPicture = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receiptPicture;
        }

        public ReceiptPicture Get(Guid receiptId, Guid id)
        {
            ReceiptPicture receiptPicture = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpreceiptPictures
                            where x.ReceiptId == receiptId && x.Id == id
                            select x;

                if (query != null)
                {

                    foreach (OltpreceiptPictures item in query)
                    {
                        receiptPicture = new ReceiptPicture
                        {
                            Id = item.Id,
                            ReceiptId = item.ReceiptId,
                            ImgId = item.ImgId,
                            ExtractionStatus = item.ExtractionStatus,
                            ExtrationStatusName = GetExtrationStatusName(item.ExtractionStatus),
                            Position = item.Position,
                            FullContent = item.FullContent,
                            ContainedUniqueValues = item.ContainedUniqueValues,
                            RelevantContent = item.RelevantContent,
                            PurchasedItems = item.PurchasedItems,
                            ContainsDeals = item.ContainsDeals,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                receiptPicture = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receiptPicture;
        }

        public ReceiptPicture Post(Guid receiptId, Guid imageId, int position, int extractionStatus, string fullContent, string containedUniqueValues, string relevantContent, string purchasedItems, bool? containsDeals)
        {
            ReceiptPicture receiptPicture;
            try
            {
                OltpreceiptPictures newReceiptPicture = new OltpreceiptPictures
                {
                    Id = Guid.NewGuid(),
                    ReceiptId = receiptId,
                    ImgId = imageId,
                    Position = position,
                    ExtractionStatus = extractionStatus,
                    FullContent = fullContent,
                    ContainedUniqueValues = containedUniqueValues,
                    RelevantContent = relevantContent,
                    PurchasedItems = purchasedItems,
                    ContainsDeals = containsDeals,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpreceiptPictures.Add(newReceiptPicture);
                this._businessObjects.Context.SaveChanges();

                receiptPicture = new ReceiptPicture
                {
                    Id = newReceiptPicture.Id,
                    ReceiptId = newReceiptPicture.ReceiptId,
                    ImgId = newReceiptPicture.ImgId,
                    ExtractionStatus = newReceiptPicture.ExtractionStatus,
                    ExtrationStatusName = GetExtrationStatusName(newReceiptPicture.ExtractionStatus),
                    Position = newReceiptPicture.Position,
                    FullContent = newReceiptPicture.FullContent,
                    ContainedUniqueValues = newReceiptPicture.ContainedUniqueValues,
                    RelevantContent = newReceiptPicture.RelevantContent,
                    PurchasedItems = newReceiptPicture.PurchasedItems,
                    ContainsDeals = newReceiptPicture.ContainsDeals,
                    CreatedDate = newReceiptPicture.CreatedDate,
                    UpdatedDate = newReceiptPicture.UpdatedDate

                };
            }
            catch (Exception e)
            {
                receiptPicture = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receiptPicture;
        }

        public ReceiptPicture Post(Guid receiptId, Guid imageId, int position, int extractionStatus)
        {
            ReceiptPicture receiptPicture;
            try
            {
                OltpreceiptPictures newReceiptPicture = new OltpreceiptPictures
                {
                    Id = Guid.NewGuid(),
                    ReceiptId = receiptId,
                    ImgId = imageId,
                    Position = position,
                    ExtractionStatus = extractionStatus,
                    FullContent = "",
                    ContainedUniqueValues = "",
                    RelevantContent = "",
                    PurchasedItems = "",
                    ContainsDeals = null,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpreceiptPictures.Add(newReceiptPicture);
                this._businessObjects.Context.SaveChanges();

                receiptPicture = new ReceiptPicture
                {
                    Id = newReceiptPicture.Id,
                    ReceiptId = newReceiptPicture.ReceiptId,
                    ImgId = newReceiptPicture.ImgId,
                    ExtractionStatus = newReceiptPicture.ExtractionStatus,
                    ExtrationStatusName = GetExtrationStatusName(newReceiptPicture.ExtractionStatus),
                    Position = newReceiptPicture.Position,
                    FullContent = newReceiptPicture.FullContent,
                    ContainedUniqueValues = newReceiptPicture.ContainedUniqueValues,
                    RelevantContent = newReceiptPicture.RelevantContent,
                    PurchasedItems = newReceiptPicture.PurchasedItems,
                    ContainsDeals = newReceiptPicture.ContainsDeals,
                    CreatedDate = newReceiptPicture.CreatedDate,
                    UpdatedDate = newReceiptPicture.UpdatedDate
                };
            }
            catch (Exception e)
            {
                receiptPicture = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receiptPicture;
        }

        public bool Put(Guid id, Guid receiptId, int extractionStatus, string fullContent, string containedUniqueValues, string relevantContent, string purchasedItems, bool containsDeals)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpreceiptPictures
                            where x.ReceiptId == receiptId && x.Id == id
                            select x;

                if (query != null)
                {
                    OltpreceiptPictures receiptPicture = null;

                    foreach (OltpreceiptPictures item in query)
                    {
                        receiptPicture = item;
                    }

                    if (receiptPicture != null)
                    {
                        receiptPicture.ExtractionStatus = extractionStatus;
                        receiptPicture.FullContent = fullContent;
                        receiptPicture.ContainedUniqueValues = containedUniqueValues;
                        receiptPicture.RelevantContent = relevantContent;
                        receiptPicture.PurchasedItems = purchasedItems;
                        receiptPicture.ContainsDeals = containsDeals;
                        receiptPicture.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
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

        #region RECEIPTREQUESTEDVALIDATIONS

        private string GetRequestedValidationReferenceTypeName(int referenceType)
        {
            string typeName = "";

            switch (referenceType)
            {
                case ReceiptRequestedValidationReferenceTypes.Deal:
                    typeName = Resources.Deal;
                    break;
                case ReceiptRequestedValidationReferenceTypes.Loyalty:
                    typeName = Resources.CommerceLoyalty;
                    break;
            }

            return typeName;
        }

        public List<ReceiptRequestedValidation> Gets(Guid? receiptId, string userId, int referenceType, Guid? referenceId, int status, int validatedState)
        {
            List<ReceiptRequestedValidation> requestedValidations;

            try
            {
                var query = (dynamic)null;

                switch (validatedState)
                {
                    case ValidatedStates.All:
                        if (status != ReceiptValidationStatuses.All)
                        {
                            if (referenceType != ReceiptRequestedValidationReferenceTypes.All)
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.Status == status && x.ReceiptId == receiptId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == (Guid)referenceId
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.Status == status && x.ReceiptId == receiptId && x.UserId == userId
                                        select x;
                            }
                        }
                        else
                        {
                            if (referenceType != ReceiptRequestedValidationReferenceTypes.All)
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == (Guid)referenceId
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId
                                        select x;
                            }
                        }
                        break;
                    case ValidatedStates.Validated:
                        if (status != ReceiptValidationStatuses.All)
                        {
                            if (referenceType != ReceiptRequestedValidationReferenceTypes.All)
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == (Guid)referenceId && x.Validated && x.Status == status
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && x.Validated && x.Status == status
                                        select x;
                            }
                        }
                        else
                        {
                            if (referenceType != ReceiptRequestedValidationReferenceTypes.All)
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == (Guid)referenceId && x.Validated
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && x.Validated
                                        select x;
                            }
                        }
                        break;
                    case ValidatedStates.NotValidated:
                        if (status != ReceiptValidationStatuses.All)
                        {
                            if (referenceType != ReceiptRequestedValidationReferenceTypes.All)
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == (Guid)referenceId && x.Status == status && !x.Validated
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && x.Status == status && !x.Validated
                                        select x;
                            }
                        }
                        else
                        {
                            if (referenceType != ReceiptRequestedValidationReferenceTypes.All)
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == (Guid)referenceId && !x.Validated
                                        select x;
                            }
                            else
                            {
                                query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                                        where x.ReceiptId == receiptId && x.UserId == userId && !x.Validated
                                        select x;
                            }
                        }
                        break;
                }

                if (query != null)
                {
                    ReceiptRequestedValidation requestedValidation;
                    requestedValidations = new List<ReceiptRequestedValidation>();

                    foreach (OltpreceiptRequestedValidations item in query)
                    {
                        requestedValidation = new ReceiptRequestedValidation
                        {
                            Id = item.Id,
                            ReceiptId = item.ReceiptId,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetRequestedValidationReferenceTypeName(item.ReferenceType),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            Validated = item.Validated,
                            RegisteredDate = item.RegisteredDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        requestedValidations.Add(requestedValidation);
                    }
                }

            }
            catch (Exception e)
            {
                requestedValidations = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return requestedValidations = null;
        }

        public ReceiptRequestedValidation Get(Guid id, string nothing)
        {
            ReceiptRequestedValidation requestedValidation = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (OltpreceiptRequestedValidations item in query)
                    {
                        requestedValidation = new ReceiptRequestedValidation
                        {
                            Id = item.Id,
                            ReceiptId = item.ReceiptId,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetRequestedValidationReferenceTypeName(item.ReferenceType),
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            Validated = item.Validated,
                            RegisteredDate = item.RegisteredDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch (Exception e)
            {
                requestedValidation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return requestedValidation;
        }

        public ReceiptRequestedValidation Post(Guid receiptId, Guid tenantId, string userId, int referenceType, Guid referenceId, DateTime registeredDate, int status, bool validated)
        {
            ReceiptRequestedValidation requestedValidation;

            try
            {
                OltpreceiptRequestedValidations newReceiptRequestedValidation = new OltpreceiptRequestedValidations
                {
                    Id = Guid.NewGuid(),
                    ReceiptId = receiptId,
                    TenantId = tenantId,
                    UserId = userId,
                    ReferenceType = referenceType,
                    ReferenceId = referenceId,
                    Status = status,
                    Validated = validated,
                    RegisteredDate = registeredDate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpreceiptRequestedValidations.Add(newReceiptRequestedValidation);
                this._businessObjects.Context.SaveChanges();

                requestedValidation = new ReceiptRequestedValidation
                {
                    Id = newReceiptRequestedValidation.Id,
                    ReceiptId = newReceiptRequestedValidation.ReceiptId,
                    UserId = newReceiptRequestedValidation.UserId,
                    TenantId = newReceiptRequestedValidation.TenantId,
                    ReferenceId = newReceiptRequestedValidation.ReferenceId,
                    ReferenceType = newReceiptRequestedValidation.ReferenceType,
                    ReferenceTypeName = GetRequestedValidationReferenceTypeName(newReceiptRequestedValidation.ReferenceType),
                    Status = newReceiptRequestedValidation.Status,
                    StatusName = GetStatusName(newReceiptRequestedValidation.Status),
                    Validated = newReceiptRequestedValidation.Validated,
                    RegisteredDate = newReceiptRequestedValidation.RegisteredDate,
                    CreatedDate = newReceiptRequestedValidation.CreatedDate,
                    UpdatedDate = newReceiptRequestedValidation.UpdatedDate,
                };
            }
            catch (Exception e)
            {
                requestedValidation = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return requestedValidation;
        }

        public bool Put(Guid id, int status, bool validated)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpreceiptRequestedValidations
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltpreceiptRequestedValidations receiptRequestedValidation = null;

                    foreach (OltpreceiptRequestedValidations item in query)
                    {
                        receiptRequestedValidation = item;
                    }

                    if (receiptRequestedValidation != null)
                    {
                        receiptRequestedValidation.Status = status;
                        receiptRequestedValidation.Validated = validated;
                        receiptRequestedValidation.UpdatedDate = DateTime.UtcNow;

                        this._businessObjects.Context.SaveChanges();

                        success = true;
                    }
                }
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

        #region RECEIPTSUMMARY

        public List<FullReceiptSummary> Gets(string userId, DateTime minDateTime, int extractionStatus, int status, int purpose)
        {
            List<FullReceiptSummary> receiptSummaries = null;

            try
            {
                var query = (dynamic)null;

                if (extractionStatus != ReceiptExtrationStatuses.All)
                {
                    if (status != ReceiptValidationStatuses.All)
                    {
                        if (purpose != ReceiptPurposes.All)
                        {
                            query = from x in this._businessObjects.Context.OltpreceiptSummariesView
                                    where x.UserId == userId && x.CreatedDate >= minDateTime && x.ExtractionStatus == extractionStatus && x.Status == status && x.Purpose == purpose
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpreceiptSummariesView
                                    where x.UserId == userId && x.CreatedDate >= minDateTime && x.ExtractionStatus == extractionStatus && x.Status == status
                                    select x;
                        }
                    }
                    else
                    {
                        if (purpose != ReceiptPurposes.All)
                        {
                            query = from x in this._businessObjects.Context.OltpreceiptSummariesView
                                    where x.UserId == userId && x.CreatedDate >= minDateTime && x.ExtractionStatus == extractionStatus && x.Purpose == purpose
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpreceiptSummariesView
                                    where x.UserId == userId && x.CreatedDate >= minDateTime && x.ExtractionStatus == extractionStatus
                                    select x;
                        }
                    }
                }
                else
                {
                    if (status != ReceiptValidationStatuses.All)
                    {
                        if (purpose != ReceiptPurposes.All)
                        {
                            query = from x in this._businessObjects.Context.OltpreceiptSummariesView
                                    where x.UserId == userId && x.CreatedDate >= minDateTime && x.Status == status && x.Purpose == purpose
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpreceiptSummariesView
                                    where x.UserId == userId && x.CreatedDate >= minDateTime && x.Status == status
                                    select x;
                        }
                    }
                    else
                    {
                        if (purpose != ReceiptPurposes.All)
                        {
                            query = from x in this._businessObjects.Context.OltpreceiptSummariesView
                                    where x.UserId == userId && x.CreatedDate >= minDateTime && x.Purpose == purpose
                                    select x;
                        }
                        else
                        {
                            query = from x in this._businessObjects.Context.OltpreceiptSummariesView
                                    where x.UserId == userId && x.CreatedDate >= minDateTime
                                    select x;
                        }
                    }
                }

                if (query != null)
                {
                    List<FlatReceiptSummary> flatReceiptSummaries = new List<FlatReceiptSummary>();
                    FlatReceiptSummary flatReceiptSummary;

                    foreach (OltpreceiptSummariesView item in query)
                    {
                        flatReceiptSummary = new FlatReceiptSummary
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            TenantName = item.TenantName,
                            TenantLogoId = item.TenantLogoId,
                            CreatedDate = item.CreatedDate,
                            PicturesCount = item.PicturesCount,
                            PictureExtractionsCount = item.PictureExtractionsCount,
                            ExtractionStatus = item.ExtractionStatus,
                            Purpose = item.Purpose,
                            Status = item.Status,
                            PreTaxAmount = item.PreTaxAmount,
                            TotalAmount = item.TotalAmount,
                            AmountsMatchStatus = item.AmountsMatchStatus,
                            ConfirmedByUser = item.ConfirmedByUser,
                            UserEarnedPoints = item.UserEarnedPoints,
                            PointsType = item.PointsType,
                            UserEarnedMoneyAmount = item.UserEarnedMoneyAmount,
                            ValidationId = item.ValidationId,
                            RequestValidated = item.RequestValidated,
                            RequestedValidationStatus = item.RequestValidationStatus,
                            RecordLineId = item.RecordLineId,
                            RecordLineRefCode = item.RecordLineRefCode,
                            RequestClaimRecordId = item.RequestClaimRecordId,
                            TransactionId = item.TransactionId,
                            ClaimedDealName = item.TransactionName
                        };

                        flatReceiptSummaries.Add(flatReceiptSummary);
                    }

                    if (flatReceiptSummaries?.Count > 0)
                    {
                        //If there are receipt summaries data
                        receiptSummaries = new List<FullReceiptSummary>();
                        FullReceiptSummary currentReceiptSummary;
                        IEnumerable<IGrouping<Guid, FlatReceiptSummary>> groupedByReceiptId = flatReceiptSummaries.GroupBy(x => x.Id);
                        FlatReceiptSummary[] summariesGroup;

                        foreach (IGrouping<Guid, FlatReceiptSummary> offerDataGroup in groupedByReceiptId)
                        {
                            summariesGroup = offerDataGroup.ToArray();

                            currentReceiptSummary = new FullReceiptSummary
                            {
                                Id = summariesGroup[0].Id,
                                UserId = summariesGroup[0].UserId,
                                TenantId = summariesGroup[0].TenantId,
                                TenantName = summariesGroup[0].TenantName,
                                TenantLogoId = summariesGroup[0].TenantLogoId,
                                CreatedDate = summariesGroup[0].CreatedDate,
                                PicturesCount = summariesGroup[0].PicturesCount,
                                PictureExtractionsCount = summariesGroup[0].PictureExtractionsCount,
                                ExtractionStatus = summariesGroup[0].ExtractionStatus,
                                ExtractionStatusName = GetExtrationStatusName((int)summariesGroup[0].ExtractionStatus),
                                Purpose = summariesGroup[0].Purpose,
                                PurposeName = summariesGroup[0] != null ? GetPurposeName((int)summariesGroup[0].Purpose) : "",
                                Status = summariesGroup[0].Status,
                                StatusName = GetStatusName(summariesGroup[0].Status),
                                PreTaxAmount = summariesGroup[0].PreTaxAmount,
                                TotalAmount = summariesGroup[0].TotalAmount,
                                AmountsMatchStatus = summariesGroup[0].AmountsMatchStatus,
                                AmountsMatchStatusName = summariesGroup[0].AmountsMatchStatus != null ? GetAmountsMatchStatusName((int)summariesGroup[0].AmountsMatchStatus) : "",
                                ConfirmedByUser = summariesGroup[0].ConfirmedByUser,
                                UserEarnedPoints = summariesGroup[0].UserEarnedPoints,
                                PointsType = summariesGroup[0].PointsType,
                                PointsTypeName = summariesGroup[0].PointsType != null ? GetPointsTypeName((int)summariesGroup[0].PointsType) : "",
                                UserEarnedMoneyAmount = summariesGroup[0].UserEarnedMoneyAmount,
                                RequestedValidations = new List<RequestedValidationData>()
                            };

                            for (int i = 0; i < summariesGroup.Length; ++i)
                            {
                                currentReceiptSummary.RequestedValidations.Add(new RequestedValidationData
                                {
                                    Id = summariesGroup[i].ValidationId,
                                    Validated = summariesGroup[i].RequestValidated,
                                    Status = summariesGroup[i].Status,
                                    StatusName = GetStatusName(summariesGroup[i].Status),
                                    RecordLineId = summariesGroup[i].RecordLineId,
                                    RefCode = summariesGroup[i].RecordLineRefCode,
                                    ClaimRecordId = summariesGroup[i].RequestClaimRecordId,
                                    TransactionId = summariesGroup[i].TransactionId,
                                    ClaimedDealName = summariesGroup[i].ClaimedDealName
                                });
                            }

                            receiptSummaries.Add(currentReceiptSummary);
                        }

                        receiptSummaries = receiptSummaries.OrderByDescending(x => x.CreatedDate).ToList();
                    }
                    else
                    {
                        receiptSummaries = new List<FullReceiptSummary>();
                    }
                }


            }
            catch (Exception e)
            {
                receiptSummaries = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return receiptSummaries;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new TableManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public ReceiptManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException("businessObjects");
            } // ELSE ENDS
        } // METHOD TABLE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
