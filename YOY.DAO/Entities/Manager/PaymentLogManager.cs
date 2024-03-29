﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class PaymentLogManager
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

        public string GetReferenceTypeName(int referenceType)
        {
            string typeName = referenceType switch
            {
                PaymentLogReferenceTypes.Purchase => Resources.IncentivePurchase,
                PaymentLogReferenceTypes.Pay => Resources.AppPay,
                _ => "--",
            };
            return typeName;

        }

        public string GetStatusName(int statusName)
        {
            string typeName = statusName switch
            {
                PaymentStatuses.Pending => Resources.PaymentPending,
                PaymentStatuses.OnProcess => Resources.OnProcessPayment,
                PaymentStatuses.Failed => Resources.FailedPayment,
                PaymentStatuses.Completed => Resources.CompletedPayment,
                _ => "--",
            };
            return typeName;

        }

        public string GetPaymentGatewayName(int statusName)
        {
            string typeName = statusName switch
            {
                PaymentGateways.Cardinal => Resources.CardinalGateway,
                PaymentGateways.Stripe => Resources.StripeGateway,
                _ => "--",
            };
            return typeName;

        }

        public List<PaymentLog> Gets(Guid? tenantId, Guid? branchId, string userId, Guid? transferId, int referenceType, Guid? referenceId, Guid? appliedCashbackId, Guid? appliedUserIncreaserEarningsId, int status, DateTime minDate, DateTime maxDate, int pageSize, int pageNumber)
        {
            List<PaymentLog> paymentLogs = null;

            try
            {

                var query = (dynamic)null;

                if (tenantId != null)
                {
                    if (branchId != null)
                    {
                        if (!string.IsNullOrWhiteSpace(userId))
                        {
                            if (transferId != null)
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {

                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.UserId == userId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (transferId != null)
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(userId))
                        {
                            if (transferId != null)
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.UserId == userId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (transferId != null)
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status == PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.LiquidationMoneyTransferId == transferId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (branchId != null)
                    {
                        if (!string.IsNullOrWhiteSpace(userId))
                        {
                            if (transferId != null)
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {

                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.UserId == userId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (transferId != null)
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.LiquidationMoneyTransferId == transferId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(userId))
                        {
                            if (transferId != null)
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.LiquidationMoneyTransferId == transferId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.UserId == userId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (transferId != null)
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status == PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.LiquidationMoneyTransferId == transferId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (referenceType != PaymentLogReferenceTypes.All)
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.ReferenceType == referenceType && x.ReferenceId == referenceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (appliedCashbackId != null)
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.AppliedCashIncentiveId == appliedCashbackId && x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.AppliedCashIncentiveId == appliedCashbackId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.AppliedCashIncentiveId == appliedCashbackId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (appliedUserIncreaserEarningsId != null)
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.AppliedUserEarningsIncreaserId == appliedUserIncreaserEarningsId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (status != PaymentStatuses.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltppaymentLogs
                                                         where x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    paymentLogs = new List<PaymentLog>();
                    PaymentLog paymentLog;

                    foreach (OltppaymentLogs item in query)
                    {
                        paymentLog = new PaymentLog
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            UserId = item.UserId,
                            LiquidationMoneyTransferId = item.LiquidationMoneyTransferId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetReferenceTypeName(item.ReferenceType),
                            CurrencyType = item.CurrencyType,
                            CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                            TotalPaymentAmount = item.TotalPaymentAmount,
                            BankingDirectDebitedAmount = item.BankingDirectDebitedAmount,
                            UserWalletUsedForPayment = item.UserWalletUsedForPayment,
                            CashIncentiveApplied = item.CashIncentiveApplied,
                            AppliedCashIncentiveId = item.AppliedCashIncentiveId,
                            UserEarningsIncreaserApplied = item.UserEarningsIncreaserApplied,
                            AppliedUserEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                            EarningsIncreasementAmount = item.EarningsIncreasementAmount,
                            UserEarnedCashbackPercentage = item.UserEarnedCashbackPercentage,
                            UserEarnedCashbackTotalAmount = item.UserEarnedCashbackTotalAmount,
                            PlatformFeeAmount = item.PlatformFeeAmount,
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            ResultCode = item.ResultCode,
                            ResultCodeName = "-",// GetResultCodeName(item.ResultCode),
                            ResultMessage = item.ResultMessage,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LiquidationDate = item.LiquidationDate,
                            PaymentGateway = item.PaymentGateway,
                            PaymentGatewayName = GetPaymentGatewayName(item.PaymentGateway),
                            PaymentInfoId = item.PaymentInfoId
                        };
                    }
                }
            }
            catch (Exception e)
            {
                paymentLogs = null;
                
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentLogs;
        }

        public PaymentLog Get(Guid id)
        {
            PaymentLog paymentLog = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltppaymentLogs
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (OltppaymentLogs item in query)
                    {
                        paymentLog = new PaymentLog
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            UserId = item.UserId,
                            LiquidationMoneyTransferId = item.LiquidationMoneyTransferId,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetReferenceTypeName(item.ReferenceType),
                            CurrencyType = item.CurrencyType,
                            CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType),
                            TotalPaymentAmount = item.TotalPaymentAmount,
                            BankingDirectDebitedAmount = item.BankingDirectDebitedAmount,
                            UserWalletUsedForPayment = item.UserWalletUsedForPayment,
                            CashIncentiveApplied = item.CashIncentiveApplied,
                            AppliedCashIncentiveId = item.AppliedCashIncentiveId,
                            UserEarningsIncreaserApplied = item.UserEarningsIncreaserApplied,
                            AppliedUserEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                            EarningsIncreasementAmount = item.EarningsIncreasementAmount,
                            UserEarnedCashbackPercentage = item.UserEarnedCashbackPercentage,
                            UserEarnedCashbackTotalAmount = item.UserEarnedCashbackTotalAmount,
                            PlatformFeeAmount = item.PlatformFeeAmount,
                            Status = item.Status,
                            StatusName = GetStatusName(item.Status),
                            ResultCode = item.ResultCode,
                            ResultCodeName = "-",// GetResultCodeName(item.ResultCode),
                            ResultMessage = item.ResultMessage,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            LiquidationDate = item.LiquidationDate,
                            PaymentGateway = item.PaymentGateway,
                            PaymentGatewayName = GetPaymentGatewayName(item.PaymentGateway),
                            PaymentInfoId = item.PaymentInfoId
                        };
                    }
                }
            }
            catch (Exception e)
            {
                paymentLog = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentLog;
        }

        public PaymentLog Post(Guid tenantId, Guid? branchId, string userId, Guid? liquidationMoneyTransferId, Guid? referenceId, int referenceType, int currencyType, decimal totalPaymentAmount,
            decimal bankingDirectDebitedAmount, bool userWalletUsedForPayment, bool cashIncentiveApplied, Guid? appliedCashIncentiveId, bool userEarningsApplied, Guid? appliedUserEarningsIncreaserId,
            decimal earningsIncreasementAmount, double userEarnedCashbackPercentage, decimal userEarnedCashbackTotalAmount, decimal platformFeeAmount, int status, int resultCode, string resultMsg, DateTime? liquidationDate, int paymentGateway, Guid? paymentInfoId)
        {
            PaymentLog paymentLog;
            try
            {
                OltppaymentLogs newPaymentLog = new OltppaymentLogs
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    BranchId = branchId,
                    UserId = userId,
                    LiquidationMoneyTransferId = liquidationMoneyTransferId,
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    CurrencyType = currencyType,
                    TotalPaymentAmount = totalPaymentAmount,
                    BankingDirectDebitedAmount = bankingDirectDebitedAmount,
                    UserWalletUsedForPayment = userWalletUsedForPayment,
                    CashIncentiveApplied = cashIncentiveApplied,
                    AppliedCashIncentiveId = appliedCashIncentiveId,
                    UserEarningsIncreaserApplied = userEarningsApplied,
                    AppliedUserEarningsIncreaserId = appliedUserEarningsIncreaserId,
                    EarningsIncreasementAmount = earningsIncreasementAmount,
                    UserEarnedCashbackPercentage = userEarnedCashbackPercentage,
                    UserEarnedCashbackTotalAmount = userEarnedCashbackTotalAmount,
                    PlatformFeeAmount = platformFeeAmount,
                    Status = status,
                    ResultCode = resultCode,
                    ResultMessage = resultMsg,
                    LiquidationDate = liquidationDate,
                    PaymentGateway = paymentGateway,
                    PaymentInfoId = paymentInfoId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltppaymentLogs.Add(newPaymentLog);
                this._businessObjects.Context.SaveChanges();

                paymentLog = new PaymentLog
                {
                    Id = newPaymentLog.Id,
                    TenantId = newPaymentLog.TenantId,
                    BranchId = newPaymentLog.BranchId,
                    UserId = newPaymentLog.UserId,
                    LiquidationMoneyTransferId = newPaymentLog.LiquidationMoneyTransferId,
                    ReferenceId = newPaymentLog.ReferenceId,
                    ReferenceType = newPaymentLog.ReferenceType,
                    ReferenceTypeName = GetReferenceTypeName(newPaymentLog.ReferenceType),
                    CurrencyType = newPaymentLog.CurrencyType,
                    CurrencyTypeName = GetCurrencyTypeName(newPaymentLog.CurrencyType),
                    TotalPaymentAmount = newPaymentLog.TotalPaymentAmount,
                    BankingDirectDebitedAmount = newPaymentLog.BankingDirectDebitedAmount,
                    UserWalletUsedForPayment = newPaymentLog.UserWalletUsedForPayment,
                    CashIncentiveApplied = newPaymentLog.CashIncentiveApplied,
                    AppliedCashIncentiveId = newPaymentLog.AppliedCashIncentiveId,
                    UserEarningsIncreaserApplied = newPaymentLog.UserEarningsIncreaserApplied,
                    AppliedUserEarningsIncreaserId = newPaymentLog.AppliedUserEarningsIncreaserId,
                    EarningsIncreasementAmount = newPaymentLog.EarningsIncreasementAmount,
                    UserEarnedCashbackPercentage = newPaymentLog.UserEarnedCashbackPercentage,
                    UserEarnedCashbackTotalAmount = newPaymentLog.UserEarnedCashbackTotalAmount,
                    PlatformFeeAmount = newPaymentLog.PlatformFeeAmount,
                    Status = newPaymentLog.Status,
                    StatusName = GetStatusName(newPaymentLog.Status),
                    ResultCode = newPaymentLog.ResultCode,
                    ResultCodeName = "-",// GetResultCodeName(item.ResultCode),
                    ResultMessage = newPaymentLog.ResultMessage,
                    CreatedDate = newPaymentLog.CreatedDate,
                    UpdatedDate = newPaymentLog.UpdatedDate,
                    LiquidationDate = newPaymentLog.LiquidationDate,
                    PaymentGateway = newPaymentLog.PaymentGateway,
                    PaymentGatewayName = GetPaymentGatewayName(newPaymentLog.PaymentGateway),
                    PaymentInfoId = newPaymentLog.PaymentInfoId
                };
            }
            catch (Exception e)
            {
                paymentLog = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentLog;
        }

        public bool Put(Guid id, Guid liquidationMoneyTransferId, DateTime liquidationDate)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltppaymentLogs
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltppaymentLogs currentPaymentLog = null;

                    foreach (OltppaymentLogs item in query)
                    {
                        currentPaymentLog = item;
                    }

                    if (currentPaymentLog != null)
                    {
                        currentPaymentLog.LiquidationMoneyTransferId = liquidationMoneyTransferId;
                        currentPaymentLog.LiquidationDate = liquidationDate;
                        currentPaymentLog.UpdatedDate = DateTime.UtcNow;

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

        public bool Put(Guid id, int status)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltppaymentLogs
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltppaymentLogs currentPaymentLog = null;

                    foreach (OltppaymentLogs item in query)
                    {
                        currentPaymentLog = item;
                    }

                    if (currentPaymentLog != null)
                    {
                        currentPaymentLog.Status = status;
                        currentPaymentLog.UpdatedDate = DateTime.UtcNow;

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
        /// Creates a new PaymentLogManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public PaymentLogManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD PAYMENT LOG MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
