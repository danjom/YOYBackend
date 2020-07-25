using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class PaymentRequestManager
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

        public string GetSourceTypeName(int sourceType)
        {
            string typeName = sourceType switch
            {
                PaymentRequestsSourceTypes.Messaging => Resources.Message,
                PaymentRequestsSourceTypes.iOTDevice => Resources.iOTDevice,
                PaymentRequestsSourceTypes.Purchase => Resources.PurchasePayment,
                _ => "--",
            };
            return typeName;

        }

        public string GetStatusName(int statusName)
        {
            string typeName = statusName switch
            {
                PaymentRequestStatuses.Created => Resources.Created,
                PaymentRequestStatuses.AccesedByUser => Resources.AccesedByUser,
                PaymentRequestStatuses.Completed => Resources.CompletedPayment,
                PaymentRequestStatuses.Failed => Resources.Failed,
                _ => "--",
            };
            return typeName;

        }

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

        private string GenerateOpCode(int length)
        {
            Random random = new Random();

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public List<PaymentRequest> Gets(Guid? tenantId, Guid? branchId, int sourceType, Guid? sourceId, string payerUserId, int status, DateTime maxDate, DateTime minDate, int expireState, DateTime date, int pageSize, int pageNumber)
        {
            List<PaymentRequest> paymentRequests = null;

            try
            {
                var query = (dynamic)null;

                if(tenantId != null)
                {
                    if(branchId != null)
                    {
                        if (sourceType != PaymentRequestsSourceTypes.All)
                        {
                            if(sourceId != null)
                            {

                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (sourceId != null)
                            {

                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (sourceType != PaymentRequestsSourceTypes.All)
                        {
                            if (sourceId != null)
                            {

                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (sourceId != null)
                            {

                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
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
                        if (sourceType != PaymentRequestsSourceTypes.All)
                        {
                            if (sourceId != null)
                            {

                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (sourceId != null)
                            {

                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.TenantId == tenantId && x.BranchId == branchId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.BranchId == branchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (sourceType != PaymentRequestsSourceTypes.All)
                        {
                            if (sourceId != null)
                            {

                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceType == sourceType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (sourceId != null)
                            {

                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.SourceId == sourceId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(payerUserId))
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.PayerUserId == payerUserId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.PayerUserId == payerUserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (status != PaymentRequestStatuses.All)
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (expireState)
                                        {
                                            case ExpiredStates.All:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Valid:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate > date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                            case ExpiredStates.Expired:
                                                query = (from x in this._businessObjects.Context.OltppaymentRequests
                                                         where x.CreatedDate >= minDate && x.CreatedDate <= maxDate && x.ExpirationDate <= date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageNumber * pageNumber).Take(pageSize);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if(query != null)
                {
                    paymentRequests = new List<PaymentRequest>();
                    PaymentRequest paymentRequest;

                    foreach(OltppaymentRequests item in query)
                    {
                        paymentRequest = new PaymentRequest
                        {
                            Id = item.Id,
                            OpCode = item.OpCode,
                            TenantId = item.TenantId,
                            BranchId = item.BranchId,
                            SourceType = item.SourceType,
                            SourceTypeName = this.GetSourceTypeName(item.SourceType),
                            SourceId = item.SourceId,
                            PayerUserId = item.PayerUserId,
                            PaymentLogId = item.PaymentLogId,
                            Amount = item.Amount,
                            CurrencyType = item.CurrencyType,
                            CurrencyTypeName = this.GetCurrencyTypeName(item.CurrencyType),
                            CurrencySymbol = item.CurrencySymbol,
                            Status = item.Status,
                            StatusName = this.GetStatusName(item.Status),
                            ExpiratinDate = item.ExpirationDate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        paymentRequests.Add(paymentRequest);
                    }
                }
            }
            catch(Exception e)
            {
                paymentRequests = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentRequests;
        }

        public PaymentRequest Get(Guid id, int idType)
        {
            PaymentRequest paymentRequest;

            try
            {
                OltppaymentRequests item = null;

                switch (idType)
                {
                    case PaymentRequestIdTypes.Id:
                        item = (from x in this._businessObjects.Context.OltppaymentRequests
                                              where x.Id == id
                                              select x).FirstOrDefault();
                        break;
                    case PaymentRequestIdTypes.LogId:
                        item = (from x in this._businessObjects.Context.OltppaymentRequests
                                              where x.PaymentLogId == id
                                              select x).FirstOrDefault();
                        break;
                }

                if(item != null)
                {
                    paymentRequest = new PaymentRequest
                    {
                        Id = item.Id,
                        OpCode = item.OpCode,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        SourceType = item.SourceType,
                        SourceTypeName = this.GetSourceTypeName(item.SourceType),
                        SourceId = item.SourceId,
                        PayerUserId = item.PayerUserId,
                        PaymentLogId = item.PaymentLogId,
                        Amount = item.Amount,
                        CurrencyType = item.CurrencyType,
                        CurrencyTypeName = this.GetCurrencyTypeName(item.CurrencyType),
                        CurrencySymbol = item.CurrencySymbol,
                        Status = item.Status,
                        StatusName = this.GetStatusName(item.Status),
                        ExpiratinDate = item.ExpirationDate,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };
                }
                else
                {
                    paymentRequest = new PaymentRequest();
                }
            }
            catch(Exception e)
            {
                paymentRequest = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentRequest;
        }

        public PaymentRequest Get(string opCode, int sourceType, Guid sourceId, DateTime date)
        {
            PaymentRequest paymentRequest;

            try
            {
                OltppaymentRequests item = (from x in this._businessObjects.Context.OltppaymentRequests
                                            where x.SourceType == sourceType && x.SourceId == sourceId && x.OpCode == opCode && x.ExpirationDate > date
                                            select x).FirstOrDefault();


                if (item != null)
                {
                    paymentRequest = new PaymentRequest
                    {
                        Id = item.Id,
                        OpCode = item.OpCode,
                        TenantId = item.TenantId,
                        BranchId = item.BranchId,
                        SourceType = item.SourceType,
                        SourceTypeName = this.GetSourceTypeName(item.SourceType),
                        SourceId = item.SourceId,
                        PayerUserId = item.PayerUserId,
                        PaymentLogId = item.PaymentLogId,
                        Amount = item.Amount,
                        CurrencyType = item.CurrencyType,
                        CurrencyTypeName = this.GetCurrencyTypeName(item.CurrencyType),
                        CurrencySymbol = item.CurrencySymbol,
                        Status = item.Status,
                        StatusName = this.GetStatusName(item.Status),
                        ExpiratinDate = item.ExpirationDate,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };
                }
                else
                {
                    paymentRequest = new PaymentRequest();
                }
            }
            catch (Exception e)
            {
                paymentRequest = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentRequest;
        }

        public PaymentRequest Post(Guid tenantId, Guid? branchId, int sourceType, Guid? sourceId, string payerUserId, Guid? paymentLogId, decimal amount, int currencyType, string currencySymbol, DateTime expirationDate)
        {
            PaymentRequest paymentRequest;

            try
            {
                OltppaymentRequests newPaymentRequest = new OltppaymentRequests
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    BranchId = branchId,
                    SourceType = sourceType,
                    SourceId = sourceId,
                    PayerUserId = payerUserId,
                    Amount = amount,
                    CurrencyType = currencyType,
                    CurrencySymbol = currencySymbol, 
                    Status = PaymentRequestStatuses.Created,
                    PaymentLogId = paymentLogId,
                    ExpirationDate = expirationDate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltppaymentRequests.Add(newPaymentRequest);
                this._businessObjects.Context.SaveChanges();

                bool? codeSet = false;
                string opCode;

                do
                {
                    codeSet = this._businessObjects.StoredProcsHandler.SetPaymentRequestOpCode(newPaymentRequest.Id, opCode = GenerateOpCode(23), newPaymentRequest.CreatedDate);

                } while (codeSet == null || codeSet == false) ;


                paymentRequest = new PaymentRequest
                {
                    Id = newPaymentRequest.Id,
                    OpCode = opCode,
                    TenantId = newPaymentRequest.TenantId,
                    BranchId = newPaymentRequest.BranchId,
                    SourceType = newPaymentRequest.SourceType,
                    SourceTypeName = this.GetSourceTypeName(newPaymentRequest.SourceType),
                    SourceId = newPaymentRequest.SourceId,
                    PayerUserId = newPaymentRequest.PayerUserId,
                    PaymentLogId = newPaymentRequest.PaymentLogId,
                    Amount = newPaymentRequest.Amount,
                    CurrencyType = newPaymentRequest.CurrencyType,
                    CurrencyTypeName = this.GetCurrencyTypeName(newPaymentRequest.CurrencyType),
                    CurrencySymbol = newPaymentRequest.CurrencySymbol,
                    Status = newPaymentRequest.Status,
                    StatusName = this.GetStatusName(newPaymentRequest.Status),
                    ExpiratinDate = newPaymentRequest.ExpirationDate,
                    CreatedDate = newPaymentRequest.CreatedDate,
                    UpdatedDate = newPaymentRequest.UpdatedDate
                };
            }
            catch(Exception e)
            {
                paymentRequest = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return paymentRequest;
        }

        public bool Put(Guid id, int status, Guid? paymentLogId)
        {
            bool success;

            try
            {
                OltppaymentRequests oltppaymentRequest = (from x in this._businessObjects.Context.OltppaymentRequests
                                                          where x.Id == id
                                                          select x).FirstOrDefault();

                if(oltppaymentRequest != null)
                {
                    oltppaymentRequest.Status = status;
                    oltppaymentRequest.UpdatedDate = DateTime.UtcNow;

                    if(oltppaymentRequest.PaymentLogId !=null)
                        oltppaymentRequest.PaymentLogId = paymentLogId;

                    this._businessObjects.Context.SaveChanges();

                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch(Exception e)
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
        /// Creates a new PaymetRequestManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public PaymentRequestManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD PAYMENT REQUEST MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
