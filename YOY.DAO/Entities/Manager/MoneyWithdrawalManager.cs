using YOY.DTO.Entities;
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
    public class MoneyWithdrawalManager
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

        private string GetCurrencyTypeName(int currencyType)
        {
            string typeName = currencyType switch
            {
                CurrencyTypes.CostaRicanColon => Resources.CostaRicaColons,
                CurrencyTypes.USDollar => Resources.USDollars,
                CurrencyTypes.GuatemalanQuetzal => Resources.GuatemalanQuetzal,
                CurrencyTypes.HonduranLempira => Resources.HonduranLempira,
                CurrencyTypes.NicaraguanCordoba => Resources.NicaraguanCordoba,
                CurrencyTypes.MexicanPeso => Resources.MexicanPeso,
                CurrencyTypes.ColombianPeso => Resources.ColombianPeso,
                _ => "--",
            };
            return typeName;

        }

        public string GetTransferTypeName(int type)
        {
            string typeName = type switch
            {
                TransferTypes.WireTransfer => Resources.WiredTransfer,
                TransferTypes.SINPE => Resources.SINPETransfer,
                _ => "--",
            };
            return typeName;
        }

        public string GetWithdrawalServiceTypeName(int type)
        {
            string typeName = type switch
            {
                WithdrawalServiceTypes.BankTransfer => Resources.RegularWireTransfer,
                WithdrawalServiceTypes.PaymentGateway => Resources.PaymentGateway,
                WithdrawalServiceTypes.MoneyExpiditTransfer => Resources.MoneyExpiditTransfer,
                _ => "--",
            };
            return typeName;
        }

        public string GetStatusName(int status)
        {
            string statusName = status switch
            {
                MoneyWithdrawalStatuses.Requested => Resources.Requested,
                MoneyWithdrawalStatuses.Approved => Resources.Approved,
                MoneyWithdrawalStatuses.Rejected => Resources.RejectedWithdrawal,
                MoneyWithdrawalStatuses.Completed => Resources.Completed,
                _ => "--",
            };
            return statusName;
        }

        public List<MoneyWithdrawal> Gets(string userId, int status, int transferType, DateTime dateLimit, int pageSize, int pageNumber)
        {
            List<MoneyWithdrawal> withdrawals = null;

            try
            {
                var query = (dynamic)null;

                if (status != MoneyWithdrawalStatuses.All)
                {
                    if (transferType != TransferTypes.All)
                    {

                        query = (from x in this._businessObjects.Context.OltpmoneyWithdrawals
                                 where x.UserId == userId && x.CreatedDate >= dateLimit && x.Status == status && x.TransferType == transferType
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);

                    }
                    else
                    {
                        query = (from x in this._businessObjects.Context.OltpmoneyWithdrawals
                                 where x.UserId == userId && x.CreatedDate >= dateLimit && x.Status == status
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                    }
                }
                else
                {
                    if (transferType != TransferTypes.All)
                    {
                        query = (from x in this._businessObjects.Context.OltpmoneyWithdrawals
                                 where x.UserId == userId && x.CreatedDate >= dateLimit && x.TransferType == transferType
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                    }
                    else
                    {
                        query = (from x in this._businessObjects.Context.OltpmoneyWithdrawals
                                 where x.UserId == userId && x.CreatedDate >= dateLimit
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                    }
                }

                if (query != null)
                {
                    MoneyWithdrawal withdrawal = null;
                    withdrawals = new List<MoneyWithdrawal>();

                    foreach (OltpmoneyWithdrawals item in query)
                    {
                        withdrawal = new MoneyWithdrawal
                        {
                            Id = item.Id,
                            UserId = item.UserId,
                            MembershipId = item.MembershipId,
                            LiquidationMoneyTransferId = item.LiquidationMoneyTransferId,
                            TransferType = item.TransferType,
                            TransferTypeName = GetTransferTypeName(item.TransferType),
                            RequiredPoints = (long)item.RequiredPoints,
                            MonetaryConversionFactor = item.MonetaryConversionFactor,
                            BeneficiaryName = item.BeneficiaryName,
                            BeneficiaryId = item.BeneficiaryId,
                            PhoneNumber = item.PhoneNumber,
                            Email = item.Email,
                            CountryId = item.CountryId,
                            LocalCurrencyAmount = item.LocalCurrencyAmount,
                            LocalCurrencyRetainedTaxesAmount = item.LocalCurrencyRetainedTaxesAmount,
                            LocalCurrencyCommissionFee = item.LocalCurrencyCommissionFee,
                            CurrencySymbol = item.CurrencySymbol,
                            CurrencyType = item.CurrencyType,
                            CurrrencyTypeName = this.GetCurrencyTypeName(item.CurrencyType),
                            Status = item.Status,
                            StatusName = this.GetStatusName(item.Status),
                            ServiceAccontRefType = item.ServiceAccountRefType,
                            ServiceAccountRefId = item.ServiceAccountRefId,
                            ServiceAccountType = item.ServiceAccountType,
                            ServiceInstanceName = item.ServiceInstanceName,
                            WithdrawCode = item.WithdrawalCode,
                            FollowUpCode = item.FollowUpCode,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            UpdatedUserModifierId = item.UpdateUserModifierId,
                            ExpirationDate = item.ExpirationDate
                        };

                        withdrawals.Add(withdrawal);
                    }
                }
            }
            catch (Exception e)
            {
                withdrawals = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return withdrawals;
        }

        public MoneyWithdrawal Post(string userId, Guid membershipId, int transferType, decimal requiredPoints, double monetaryConversionFactor, string beneficiaryName, string beneficiaryId, string phoneNumber, string email,
            Guid countryId, decimal localCurrencyAmount, decimal localCurrencyRetainedTaxesAmount, decimal localCurrencyCommissionFee, string currencySymbol, int currencyType, int status, int serviceAccountRefType,
            string serviceAccountRefId, string serviceAccountType, string serviceInstanceName, string withdrawalCode, string followUpCode, int validDays)
        {
            MoneyWithdrawal withdrawal;
            try
            {
                OltpmoneyWithdrawals moneyWithdrawal = new OltpmoneyWithdrawals
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    MembershipId = membershipId,
                    LiquidationMoneyTransferId = null,
                    TransferType = transferType,
                    RequiredPoints = requiredPoints,
                    MonetaryConversionFactor = monetaryConversionFactor,
                    BeneficiaryName = beneficiaryName,
                    BeneficiaryId = beneficiaryId,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    CountryId = countryId,
                    LocalCurrencyAmount = localCurrencyAmount,
                    LocalCurrencyRetainedTaxesAmount = localCurrencyRetainedTaxesAmount,
                    LocalCurrencyCommissionFee = localCurrencyCommissionFee,
                    CurrencySymbol = currencySymbol,
                    CurrencyType = currencyType,
                    Status = status,
                    ServiceAccountRefType = serviceAccountRefType,
                    ServiceAccountRefId = serviceAccountRefId,
                    ServiceAccountType = serviceAccountType,
                    ServiceInstanceName = serviceInstanceName,
                    WithdrawalCode = withdrawalCode,
                    FollowUpCode = followUpCode,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    UpdateUserModifierId = userId,
                    ExpirationDate = DateTime.UtcNow.AddDays(validDays)
                };

                this._businessObjects.Context.OltpmoneyWithdrawals.Add(moneyWithdrawal);
                this._businessObjects.Context.SaveChanges();

                withdrawal = new MoneyWithdrawal
                {
                    Id = moneyWithdrawal.Id,
                    UserId = moneyWithdrawal.UserId,
                    MembershipId = moneyWithdrawal.MembershipId,
                    LiquidationMoneyTransferId = moneyWithdrawal.LiquidationMoneyTransferId,
                    TransferType = moneyWithdrawal.TransferType,
                    TransferTypeName = GetTransferTypeName(moneyWithdrawal.TransferType),
                    RequiredPoints = moneyWithdrawal.RequiredPoints,
                    MonetaryConversionFactor = moneyWithdrawal.MonetaryConversionFactor,
                    BeneficiaryName = moneyWithdrawal.BeneficiaryName,
                    BeneficiaryId = moneyWithdrawal.BeneficiaryId,
                    PhoneNumber = moneyWithdrawal.PhoneNumber,
                    Email = moneyWithdrawal.Email,
                    CountryId = moneyWithdrawal.CountryId,
                    LocalCurrencyAmount = moneyWithdrawal.LocalCurrencyAmount,
                    LocalCurrencyRetainedTaxesAmount = moneyWithdrawal.LocalCurrencyRetainedTaxesAmount,
                    LocalCurrencyCommissionFee = moneyWithdrawal.LocalCurrencyCommissionFee,
                    CurrencySymbol = moneyWithdrawal.CurrencySymbol,
                    CurrencyType = moneyWithdrawal.CurrencyType,
                    CurrrencyTypeName = GetCurrencyTypeName(moneyWithdrawal.CurrencyType),
                    Status = moneyWithdrawal.Status,
                    StatusName = GetStatusName(moneyWithdrawal.Status),
                    ServiceAccontRefType = moneyWithdrawal.ServiceAccountRefType,
                    ServiceAccountRefId = moneyWithdrawal.ServiceAccountRefId,
                    ServiceAccountType = moneyWithdrawal.ServiceAccountType,
                    ServiceInstanceName = moneyWithdrawal.ServiceInstanceName,
                    WithdrawCode = moneyWithdrawal.WithdrawalCode,
                    FollowUpCode = moneyWithdrawal.FollowUpCode,
                    CreatedDate = moneyWithdrawal.CreatedDate,
                    UpdatedDate = moneyWithdrawal.UpdatedDate,
                    UpdatedUserModifierId = moneyWithdrawal.UpdateUserModifierId,
                    ExpirationDate = moneyWithdrawal.ExpirationDate
                };
            }
            catch (Exception e)
            {
                withdrawal = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return withdrawal;

        }

        public bool Put(Guid id, Guid? liquidationMoneyTransferId, int status, string withdrawalCode, string modifierUserId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmoneyWithdrawals
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltpmoneyWithdrawals moneyWithdrawal = null;

                    foreach (OltpmoneyWithdrawals item in query)
                    {
                        moneyWithdrawal = item;
                    }

                    if (moneyWithdrawal != null)
                    {
                        moneyWithdrawal.Status = status;

                        if (!string.IsNullOrWhiteSpace(withdrawalCode))
                        {
                            moneyWithdrawal.WithdrawalCode = withdrawalCode;
                        }

                        if (moneyWithdrawal.LiquidationMoneyTransferId == null)
                        {
                            moneyWithdrawal.LiquidationMoneyTransferId = liquidationMoneyTransferId;
                        }

                        moneyWithdrawal.UpdatedDate = DateTime.UtcNow;
                        moneyWithdrawal.UpdateUserModifierId = modifierUserId;

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

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new MonetaryCreditCode with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public MoneyWithdrawalManager(BusinessObjects businessObjects)
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
