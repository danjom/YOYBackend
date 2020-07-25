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
    public class MonetaryFeeLogManager
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

        private string GetRefTypeName(int refType)
        {
            string refTypeName = refType switch
            {
                MonetaryFeeLogRefTypes.UsageRecordLine => Resources.UsageRecordLine,
                MonetaryFeeLogRefTypes.MoneyConversionLog => Resources.MoneyConversionLog,
                MonetaryFeeLogRefTypes.SurveyResponse => Resources.SurveyResponse,
                _ => "--",
            };
            return refTypeName;
        }

        private string GetTypeName(int type)
        {
            string typeName = type switch
            {
                MonetaryFeeLogTypes.AccountPayable => Resources.AccountPayable,
                MonetaryFeeLogTypes.AccountReceivable => Resources.AccountReceivable,
                _ => "--",
            };
            return typeName;
        }

        private string GetReasonName(int reason)
        {
            string reasonName = reason switch
            {
                MonetaryFeeLogReasons.DealClaim => Resources.DealClaim,
                MonetaryFeeLogReasons.PaymentWithPoints => Resources.PaymentWithPoints,
                MonetaryFeeLogReasons.SurveyResponded => Resources.SurveyResponded,
                MonetaryFeeLogReasons.AdDisplay => Resources.AdDisplay,
                MonetaryFeeLogReasons.LoyaltyProgramFee => Resources.LoyaltyProgramFee,
                _ => "--",
            };
            return reasonName;
        }

        private string GetStatusName(int status)
        {
            string statusName = status switch
            {
                MonetaryFeeLogStatuses.PaymentPending => Resources.PaymentPending,
                MonetaryFeeLogStatuses.Payed => Resources.Payed,
                MonetaryFeeLogStatuses.Disputed => Resources.Disputed,
                MonetaryFeeLogStatuses.Uncollectible => Resources.Uncollectible,
                _ => "--",
            };
            return statusName;
        }

        public List<MonetaryFeeLog> Gets(DateTime start, DateTime end, int type, int reason, Guid? refId, int refType, int status, int pageSize, int pageNumber)
        {
            List<MonetaryFeeLog> feeLogs = null;

            try
            {
                var query = (dynamic)null;

                if (type != MonetaryFeeLogTypes.All)
                {
                    if (reason != MonetaryFeeLogReasons.All)
                    {
                        if (refId != null)
                        {
                            if (status != MonetaryFeeLogStatuses.All)
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.RefType == refType && x.RefId == (Guid)refId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                        else
                        {
                            if (status != MonetaryFeeLogStatuses.All)
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.Status == status
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                    }
                    else
                    {
                        if (refId != null)
                        {
                            if (status != MonetaryFeeLogStatuses.All)
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.RefType == refType && x.RefId == (Guid)refId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                        else
                        {
                            if (status != MonetaryFeeLogStatuses.All)
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Status == status
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                    }
                }
                else
                {
                    if (reason != MonetaryFeeLogReasons.All)
                    {
                        if (refId != null)
                        {
                            if (status != MonetaryFeeLogStatuses.All)
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.RefType == refType && x.RefId == (Guid)refId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                        else
                        {
                            if (status != MonetaryFeeLogStatuses.All)
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.Status == status
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                    }
                    else
                    {
                        if (refId != null)
                        {
                            if (status != MonetaryFeeLogStatuses.All)
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.RefType == refType && x.RefId == (Guid)refId
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                        else
                        {
                            if (status != MonetaryFeeLogStatuses.All)
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end && x.Status == status
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                         where x.CreatedDate >= start && x.CreatedDate <= end
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                    }
                }

                if (query != null)
                {
                    feeLogs = new List<MonetaryFeeLog>();
                    MonetaryFeeLog feeLog = null;

                    foreach (OltpmonetaryFeeLogsView item in query)
                    {
                        feeLog = new MonetaryFeeLog
                        {
                            Id = item.Id,
                            GeneratorTenantId = item.GeneratorTenantId,
                            GeneratorTenantName = item.GeneratorTenantName,
                            GeneratorContactName = item.GeneratorContactName,
                            GeneratorContactEmail = item.GeneratorContactEmail,
                            GeneratorContactPhone = item.GeneratorContactPhone,
                            GeneratorBranchId = item.GeneratorBranchId,
                            GeneratorBranchName = item.GeneratorBranchName,
                            DebtorTenantId = item.DebtorTenantId,
                            DebtorTenantName = item.DebtorTenantName,
                            DebtorContactName = item.DebtorContactName,
                            DebtorContactEmail = item.DebtorContactEmail,
                            DebtorContactPhone = item.DebtorContactPhone,
                            DebtorTenantPaymentDay = item.DebtorPaymentDay,
                            CollectorTenantId = item.CollectorTenantId,
                            CollectorTenantName = item.CollectorTenantName,
                            CollectorContactName = item.CollectorContactName,
                            CollectorContactEmail = item.CollectorContactEmail,
                            CollectorContactPhone = item.CollectorContactPhone,
                            CollectionDueDate = item.CollectionDueDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            EmployeeId = item.EmployeeId,
                            UserId = item.UserId,
                            RefId = item.RefId,
                            RefType = item.RefType,
                            RefTypeName = this.GetRefTypeName(item.RefType),
                            Type = item.Type,
                            TypeName = this.GetTypeName(item.Type),
                            Reason = item.Reason,
                            ReasonName = this.GetReasonName(item.Reason),
                            Status = item.Status,
                            StatusName = this.GetStatusName(item.Status),
                            Amount = item.Amount,
                            CurrencySymbol = item.CurrencySymbol,
                            CurrencyType = item.CurrencyType,
                            CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType)
                        };

                        feeLogs.Add(feeLog);
                    }
                }
            }
            catch (Exception e)
            {
                feeLogs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return feeLogs;
        }

        public List<MonetaryFeeLog> Gets(Guid tenantId, int tenantType, DateTime start, DateTime end, int type, int reason, Guid? refId, int refType, int status, int pageSize, int pageNumber)
        {
            List<MonetaryFeeLog> feeLogs = null;

            try
            {
                var query = (dynamic)null;

                switch (tenantType)
                {
                    case MonetaryFeeLogsTenantTypes.All:

                        if (type != MonetaryFeeLogTypes.All)
                        {
                            if (reason != MonetaryFeeLogReasons.All)
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (reason != MonetaryFeeLogReasons.All)
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where (x.DebtorTenantId == tenantId || x.CollectorTenantId == tenantId) && x.CreatedDate >= start && x.CreatedDate <= end
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }

                        break;
                    case MonetaryFeeLogsTenantTypes.Debtor:

                        if (type != MonetaryFeeLogTypes.All)
                        {
                            if (reason != MonetaryFeeLogReasons.All)
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (reason != MonetaryFeeLogReasons.All)
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.DebtorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }

                        break;
                    case MonetaryFeeLogsTenantTypes.Collector:

                        if (type != MonetaryFeeLogTypes.All)
                        {
                            if (reason != MonetaryFeeLogReasons.All)
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Reason == reason
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Type == type
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (reason != MonetaryFeeLogReasons.All)
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Reason == reason
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (refId != null)
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Status == status && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.RefType == refType && x.RefId == (Guid)refId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (status != MonetaryFeeLogStatuses.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end && x.Status == status
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                                                 where x.CollectorTenantId == tenantId && x.CreatedDate >= start && x.CreatedDate <= end
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }

                        break;
                }

                if (query != null)
                {
                    feeLogs = new List<MonetaryFeeLog>();
                    MonetaryFeeLog feeLog = null;

                    foreach (OltpmonetaryFeeLogsView item in query)
                    {
                        feeLog = new MonetaryFeeLog
                        {
                            Id = item.Id,
                            GeneratorTenantId = item.GeneratorTenantId,
                            GeneratorTenantName = item.GeneratorTenantName,
                            GeneratorContactName = item.GeneratorContactName,
                            GeneratorContactEmail = item.GeneratorContactEmail,
                            GeneratorContactPhone = item.GeneratorContactPhone,
                            GeneratorBranchId = item.GeneratorBranchId,
                            GeneratorBranchName = item.GeneratorBranchName,
                            DebtorTenantId = item.DebtorTenantId,
                            DebtorTenantName = item.DebtorTenantName,
                            DebtorContactName = item.DebtorContactName,
                            DebtorContactEmail = item.DebtorContactEmail,
                            DebtorContactPhone = item.DebtorContactPhone,
                            DebtorTenantPaymentDay = item.DebtorPaymentDay,
                            CollectorTenantId = item.CollectorTenantId,
                            CollectorTenantName = item.CollectorTenantName,
                            CollectorContactName = item.CollectorContactName,
                            CollectorContactEmail = item.CollectorContactEmail,
                            CollectorContactPhone = item.CollectorContactPhone,
                            CollectionDueDate = item.CollectionDueDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            EmployeeId = item.EmployeeId,
                            UserId = item.UserId,
                            RefId = item.RefId,
                            RefType = item.RefType,
                            RefTypeName = this.GetRefTypeName(item.RefType),
                            Type = item.Type,
                            TypeName = this.GetTypeName(item.Type),
                            Reason = item.Reason,
                            ReasonName = this.GetReasonName(item.Reason),
                            Status = item.Status,
                            StatusName = this.GetStatusName(item.Status),
                            Amount = item.Amount,
                            CurrencySymbol = item.CurrencySymbol,
                            CurrencyType = item.CurrencyType,
                            CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType)
                        };

                        feeLogs.Add(feeLog);
                    }
                }
            }
            catch (Exception e)
            {
                feeLogs = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return feeLogs;
        }

        public MonetaryFeeLog Get(Guid id)
        {
            MonetaryFeeLog feeLog = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmonetaryFeeLogsView
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (OltpmonetaryFeeLogsView item in query)
                    {
                        feeLog = new MonetaryFeeLog
                        {
                            Id = item.Id,
                            GeneratorTenantId = item.GeneratorTenantId,
                            GeneratorTenantName = item.GeneratorTenantName,
                            GeneratorContactName = item.GeneratorContactName,
                            GeneratorContactEmail = item.GeneratorContactEmail,
                            GeneratorContactPhone = item.GeneratorContactPhone,
                            GeneratorBranchId = item.GeneratorBranchId,
                            GeneratorBranchName = item.GeneratorBranchName,
                            DebtorTenantId = item.DebtorTenantId,
                            DebtorTenantName = item.DebtorTenantName,
                            DebtorContactName = item.DebtorContactName,
                            DebtorContactEmail = item.DebtorContactEmail,
                            DebtorContactPhone = item.DebtorContactPhone,
                            DebtorTenantPaymentDay = item.DebtorPaymentDay,
                            CollectorTenantId = item.CollectorTenantId,
                            CollectorTenantName = item.CollectorTenantName,
                            CollectionDueDate = item.CollectionDueDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            EmployeeId = item.EmployeeId,
                            UserId = item.UserId,
                            RefId = item.RefId,
                            RefType = item.RefType,
                            RefTypeName = this.GetRefTypeName(item.RefType),
                            Type = item.Type,
                            TypeName = this.GetTypeName(item.Type),
                            Reason = item.Reason,
                            ReasonName = this.GetReasonName(item.Reason),
                            Status = item.Status,
                            StatusName = this.GetStatusName(item.Status),
                            Amount = item.Amount,
                            CurrencySymbol = item.CurrencySymbol,
                            CurrencyType = item.CurrencyType,
                            CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType)
                        };

                    }
                }
            }
            catch (Exception e)
            {
                feeLog = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return feeLog;
        }

        public Guid? Post(Guid generatorTenantId, Guid? generatorBranchId, Guid debtorTenantId, Guid collectorTenantId, string userId, Guid? employeeId, DateTime? collectionDueDate, int type, int reason, int refType, Guid refId, int status, decimal amount, string currencySymbol, int currencyType)
        {
            Guid? id;
            try
            {
                OltpmonetaryFeeLogs newFeeLog = new OltpmonetaryFeeLogs
                {
                    Id = Guid.NewGuid(),
                    GeneratorTenantId = generatorTenantId,
                    GeneratorBranchId = generatorBranchId,
                    DebtorTenantId = debtorTenantId,
                    CollectorTenantId = collectorTenantId,
                    UserId = userId,
                    EmployeeId = employeeId,
                    CollectionDueDate = collectionDueDate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Type = type,
                    Reason = reason,
                    RefId = refId,
                    RefType = refType,
                    Status = status,
                    Amount = amount,
                    CurrencySymbol = currencySymbol,
                    CurrencyType = currencyType
                };

                this._businessObjects.Context.OltpmonetaryFeeLogs.Add(newFeeLog);
                this._businessObjects.Context.SaveChanges();

                id = newFeeLog.Id;
            }
            catch (Exception e)
            {
                id = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return id;
        }

        public bool Put(Guid id, int status)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltpmonetaryFeeLogs
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    OltpmonetaryFeeLogs feeLog = null;

                    foreach (OltpmonetaryFeeLogs item in query)
                    {
                        feeLog = item;
                    }

                    if (feeLog != null)
                    {
                        feeLog.Status = status;
                        feeLog.UpdatedDate = DateTime.UtcNow;

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
        /// Creates a new MonetaryFeeLog with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public MonetaryFeeLogManager(BusinessObjects businessObjects)
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
