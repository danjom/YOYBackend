using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Purchase;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class PurchaseManager
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

        public string GetPurchaseStatusName(int statusName)
        {
            string typeName = statusName switch
            {
                PurchaseStatuses.Placed => Resources.PlacedOrder,
                PurchaseStatuses.Payed => Resources.PayedPurchase,
                PurchaseStatuses.PartiallyIssueRaised => Resources.PartiallyIssueRaised,
                PurchaseStatuses.PartiallyCancelled => Resources.PartiallyCancelled,
                PurchaseStatuses.FullyIssueRaised => Resources.FullyIssueRaised,
                PurchaseStatuses.DispatchValidationRequested => Resources.DispatchValidationRequested,
                PurchaseStatuses.Delivered => Resources.PurchaseDelivered,
                PurchaseStatuses.FullyCancelled => Resources.FullyCancelled,
                _ => "--",
            };
            return typeName;

        }

        public string GetPurchaseItemStatusName(int statusName)
        {
            string typeName = statusName switch
            {
                PurchaseItemStatuses.Placed => Resources.PlacedOrder,
                PurchaseItemStatuses.Payed => Resources.PayedPurchase,
                PurchaseItemStatuses.DispatchValidationRequested => Resources.DispatchValidationRequested,
                PurchaseItemStatuses.Delivered => Resources.PurchaseDelivered,
                PurchaseItemStatuses.IssueRaised => Resources.PurchaseWithIssues,
                PurchaseItemStatuses.Cancelled => Resources.Cancelled,
                PurchaseItemStatuses.Returned => Resources.PurchaseReturned,
                _ => "--",
            };
            return typeName;

        }

        public string GetDeliveryTypeName(int deliveryType)
        {
            string typeName = deliveryType switch
            {
                DeliveryTypes.ClaimInStore => Resources.ClaimInStore,
                DeliveryTypes.ClaimPickup => Resources.ClaimPickup,
                DeliveryTypes.ClaimOnline => Resources.ClaimOnline,
                DeliveryTypes.ClaimByPhone => Resources.ClaimByPhone,
                _ => "--",
            };
            return typeName;

        }

        private string GetDealTypeName(int dealType)
        {
            string typeName = dealType switch
            {
                DealTypes.InStore => Resources.Instore,
                DealTypes.Online => Resources.Online,
                DealTypes.Phone => Resources.PhoneCall,
                _ => "--",
            };
            return typeName;
        }

        private string GetDispatchValidatorSourceTypeName(int sourType)
        {
            string typeName = sourType switch
            {
                PurchaseDispatchValidatorSourceTypes.Messaging => Resources.Message,
                PurchaseDispatchValidatorSourceTypes.iOTDevice => Resources.iOTDevice,
                _ => "--",
            };
            return typeName;
        }

        public List<Purchase> Gets(string userId, int status, int deliveryType, Guid? appliedUserEarningsIncreaserId, DateTime minDate, DateTime maxDate, int pageSize, int pageNumber)
        {
            List<Purchase> purchases;

            try
            {
                var query = (dynamic)null;


                if (!string.IsNullOrWhiteSpace(userId))
                {
                    if(status != PurchaseStatuses.All)
                    {
                        if (deliveryType != DeliveryTypes.All)
                        {
                            if(appliedUserEarningsIncreaserId != null)
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.UserId == userId && x.Status == status && x.DeliveryType == deliveryType && x.AppliedUserEarningsIncreaserId == appliedUserEarningsIncreaserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.UserId == userId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                        else
                        {
                            if (appliedUserEarningsIncreaserId != null)
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.UserId == userId && x.Status == status && x.AppliedUserEarningsIncreaserId == appliedUserEarningsIncreaserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.UserId == userId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                    }
                    else
                    {
                        if (deliveryType != DeliveryTypes.All)
                        {
                            if (appliedUserEarningsIncreaserId != null)
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.UserId == userId && x.DeliveryType == deliveryType && x.AppliedUserEarningsIncreaserId == appliedUserEarningsIncreaserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.UserId == userId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                        else
                        {
                            if (appliedUserEarningsIncreaserId != null)
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.UserId == userId && x.AppliedUserEarningsIncreaserId == appliedUserEarningsIncreaserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.UserId == userId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                    }
                }
                else
                {
                    if (status != PurchaseStatuses.All)
                    {
                        if (deliveryType != DeliveryTypes.All)
                        {
                            if (appliedUserEarningsIncreaserId != null)
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.Status == status && x.DeliveryType == deliveryType && x.AppliedUserEarningsIncreaserId == appliedUserEarningsIncreaserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                        else
                        {
                            if (appliedUserEarningsIncreaserId != null)
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.Status == status && x.AppliedUserEarningsIncreaserId == appliedUserEarningsIncreaserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                    }
                    else
                    {
                        if (deliveryType != DeliveryTypes.All)
                        {
                            if (appliedUserEarningsIncreaserId != null)
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.DeliveryType == deliveryType && x.AppliedUserEarningsIncreaserId == appliedUserEarningsIncreaserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                        else
                        {
                            if (appliedUserEarningsIncreaserId != null)
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.AppliedUserEarningsIncreaserId == appliedUserEarningsIncreaserId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                            else
                            {
                                query = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                            }
                        }
                    }
                }

                if(query != null)
                {
                    purchases = new List<Purchase>();
                    Purchase purchase;

                    foreach(Oltppurchases item in query)
                    {
                        purchase = new Purchase
                        {
                            Id = item.Id,
                            PurchaseCode = item.PurchaseCode,
                            PurchaseNumericCode = item.PurchaseNumericCode,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            DispatchBranchId = item.DispatchBranchId,
                            DispatchValidationSourceId = item.DispatchValidationSourceId,
                            DispatchValidationSourceType = item.DispatchValidationSourceType,
                            DispatchValidationSourceTypeName = item.DispatchValidationSourceType != null ? GetDispatchValidatorSourceTypeName((int)item.DispatchValidationSourceType) : "",
                            PaymentLogId = item.PaymentLogId,
                            Status = item.Status,
                            StatusName = GetPurchaseStatusName(item.Status),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            DeliveryType = item.DeliveryType,
                            DeliveryTypeName = GetDeliveryTypeName(item.DeliveryType),
                            AppliedEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                            TotalPayedAmount = item.TotalPayedAmount,
                            TotalTenantEarnings = item.TotalTenantEarnings,
                            TotalCashbackPercentage = item.TotalCashbackPercentage,
                            TotalCashbackAmount = item.TotalCashbackTotalAmount,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        purchases.Add(purchase);
                    }
                }
                else
                {
                    purchases = new List<Purchase>();
                }
            }
            catch(Exception e)
            {
                purchases = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchases;
        }

        public Purchase Get(Guid id)
        {
            Purchase purchase;

            try
            {
                Oltppurchases item = (from x in this._businessObjects.Context.Oltppurchases
                                        where x.Id == id
                                        select x).FirstOrDefault();

                if(item != null)
                {
                    purchase = new Purchase
                    {
                        Id = item.Id,
                        PurchaseCode = item.PurchaseCode,
                        PurchaseNumericCode = item.PurchaseNumericCode,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        DispatchBranchId = item.DispatchBranchId,
                        DispatchValidationSourceId = item.DispatchValidationSourceId,
                        DispatchValidationSourceType = item.DispatchValidationSourceType,
                        DispatchValidationSourceTypeName = item.DispatchValidationSourceType != null ? GetDispatchValidatorSourceTypeName((int)item.DispatchValidationSourceType) : "",
                        PaymentLogId = item.PaymentLogId,
                        Status = item.Status,
                        StatusName = GetPurchaseStatusName(item.Status),
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        DeliveryType = item.DeliveryType,
                        DeliveryTypeName = GetDeliveryTypeName(item.DeliveryType),
                        AppliedEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                        TotalPayedAmount = item.TotalPayedAmount,
                        TotalTenantEarnings = item.TotalTenantEarnings,
                        TotalCashbackPercentage = item.TotalCashbackPercentage,
                        TotalCashbackAmount = item.TotalCashbackTotalAmount,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };
                }
                else
                {
                    purchase = new Purchase();
                }
            }
            catch(Exception e)
            {
                purchase = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchase;
        }

        public Purchase Get(Guid? tenantId, string code, int codeType, int maxStatus)
        {
            Purchase purchase;

            try
            {
                Oltppurchases item = null;

                switch (codeType)
                {
                    case PurchaseCodeTypes.AlphaNumeric:
                        if(tenantId != null)
                        {
                            item = (from x in this._businessObjects.Context.Oltppurchases
                                    where x.TenantId == tenantId && x.PurchaseCode == code && x.Status <= maxStatus
                                    select x).FirstOrDefault();
                        }
                        else
                        {

                            item = (from x in this._businessObjects.Context.Oltppurchases
                                    where x.PurchaseCode == code && x.Status <= maxStatus
                                    select x).FirstOrDefault();
                        }
                        break;
                    case PurchaseCodeTypes.NumericOnly:
                        if(tenantId != null)
                        {
                            item = (from x in this._businessObjects.Context.Oltppurchases
                                    where x.TenantId == tenantId && x.PurchaseNumericCode == code && x.Status <= maxStatus
                                    select x).FirstOrDefault();
                        }
                        else
                        {
                            item = (from x in this._businessObjects.Context.Oltppurchases
                                    where x.PurchaseNumericCode == code && x.Status <= maxStatus
                                    select x).FirstOrDefault();
                        }
                        break;
                }
                    

                if (item != null)
                {
                    purchase = new Purchase
                    {
                        Id = item.Id,
                        PurchaseCode = item.PurchaseCode,
                        PurchaseNumericCode = item.PurchaseNumericCode,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        DispatchBranchId = item.DispatchBranchId,
                        DispatchValidationSourceId = item.DispatchValidationSourceId,
                        DispatchValidationSourceType = item.DispatchValidationSourceType,
                        DispatchValidationSourceTypeName = item.DispatchValidationSourceType != null ? GetDispatchValidatorSourceTypeName((int)item.DispatchValidationSourceType) : "",
                        PaymentLogId = item.PaymentLogId,
                        Status = item.Status,
                        StatusName = GetPurchaseStatusName(item.Status),
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        DeliveryType = item.DeliveryType,
                        DeliveryTypeName = GetDeliveryTypeName(item.DeliveryType),
                        AppliedEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                        TotalPayedAmount = item.TotalPayedAmount,
                        TotalTenantEarnings = item.TotalTenantEarnings,
                        TotalCashbackPercentage = item.TotalCashbackPercentage,
                        TotalCashbackAmount = item.TotalCashbackTotalAmount,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };
                }
                else
                {
                    purchase = new Purchase();
                }
            }
            catch (Exception e)
            {
                purchase = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchase;
        }

        public Purchase Post(string purchaseCode, string numericPurchaseCode, string userId, Guid tenantId, Guid paymentLogId, int status, int dealType, int deliveryType, Guid? appliedUserEarningsIncreaserId, decimal totalPayedAmount, decimal totalTenantEarnings, double totalCashbackPercentage,  decimal totalCashbackAmount)
        {
            Purchase purchase;

            try
            {
                Oltppurchases newPurchase = new Oltppurchases
                {
                    Id = Guid.NewGuid(),
                    PurchaseCode = purchaseCode,
                    PurchaseNumericCode = numericPurchaseCode,
                    UserId = userId,
                    TenantId = tenantId,
                    DispatchBranchId = null,
                    DispatchValidationSourceId = null,
                    DispatchValidationSourceType = PurchaseDispatchValidatorSourceTypes.Undefined,
                    PaymentLogId = paymentLogId,
                    Status = status,
                    DealType = dealType,
                    DeliveryType = deliveryType,
                    AppliedUserEarningsIncreaserId = appliedUserEarningsIncreaserId,
                    TotalPayedAmount = totalPayedAmount,
                    TotalTenantEarnings = totalTenantEarnings,
                    TotalCashbackPercentage = totalCashbackPercentage,
                    TotalCashbackTotalAmount = totalCashbackAmount,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.Oltppurchases.Add(newPurchase);
                this._businessObjects.Context.SaveChanges();

                purchase = new Purchase
                {
                    Id = newPurchase.Id,
                    PurchaseCode = newPurchase.PurchaseCode,
                    PurchaseNumericCode = newPurchase.PurchaseNumericCode,
                    UserId = newPurchase.UserId,
                    TenantId = newPurchase.TenantId,
                    DispatchBranchId = newPurchase.DispatchBranchId,
                    DispatchValidationSourceId = newPurchase.DispatchValidationSourceId,
                    DispatchValidationSourceType = newPurchase.DispatchValidationSourceType,
                    DispatchValidationSourceTypeName = newPurchase.DispatchValidationSourceType != null ? GetDispatchValidatorSourceTypeName((int)newPurchase.DispatchValidationSourceType) : "",
                    PaymentLogId = newPurchase.PaymentLogId,
                    Status = newPurchase.Status,
                    StatusName = GetPurchaseStatusName(newPurchase.Status),
                    DealType = newPurchase.DealType,
                    DealTypeName = GetDealTypeName(newPurchase.DealType),
                    DeliveryType = newPurchase.DeliveryType,
                    DeliveryTypeName = GetDeliveryTypeName(newPurchase.DeliveryType),
                    AppliedEarningsIncreaserId = newPurchase.AppliedUserEarningsIncreaserId,
                    TotalPayedAmount = newPurchase.TotalPayedAmount,
                    TotalTenantEarnings = newPurchase.TotalTenantEarnings,
                    TotalCashbackPercentage = newPurchase.TotalCashbackPercentage,
                    TotalCashbackAmount = newPurchase.TotalCashbackTotalAmount,
                    CreatedDate = newPurchase.CreatedDate,
                    UpdatedDate = newPurchase.UpdatedDate
                };
            }
            catch(Exception e)
            {
                purchase = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchase;
        }

        public bool Put(Guid id, Guid dispatchBranchId, Guid validationDispatchSourceId, int validationDispatchSourceType, int status)
        {
            bool success = false;

            try
            {
                Oltppurchases purchase = (from x in this._businessObjects.Context.Oltppurchases
                                         where x.Id == id
                                         select x).FirstOrDefault();

                if(purchase != null)
                {
                    purchase.DispatchBranchId = dispatchBranchId;
                    purchase.DispatchValidationSourceId = validationDispatchSourceId;
                    purchase.DispatchValidationSourceType = validationDispatchSourceType;
                    purchase.Status = status;

                    this._businessObjects.Context.SaveChanges();

                    success = true;
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

        #region PURCHASEITEMS

        public List<PurchaseItem> Gets(string userId, Guid? purchaseId, Guid? tenantId, Guid? dispatchBranchId, int status, int deliveryType, DateTime minDate, DateTime maxDate, int pageSize, int pageNumber)
        {
            List<PurchaseItem> purchaseItems = null;

            try
            {
                var query = (dynamic)null;

                if (!string.IsNullOrEmpty(userId))
                {
                    if (purchaseId != null)
                    {

                        if (tenantId != null)
                        {
                            if (dispatchBranchId != null)
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.TenantId == tenantId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dispatchBranchId != null)
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.DispatchBranchId == dispatchBranchId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.DispatchBranchId == dispatchBranchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.PurchaseId == purchaseId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                        if (tenantId != null)
                        {
                            if (dispatchBranchId != null)
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dispatchBranchId != null)
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.DispatchBranchId == dispatchBranchId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.DispatchBranchId == dispatchBranchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.UserId == userId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
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
                    if(purchaseId != null)
                    {
                        if (tenantId != null)
                        {
                            if (dispatchBranchId != null)
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.TenantId == tenantId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.TenantId == tenantId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dispatchBranchId != null)
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where  x.PurchaseId == purchaseId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.DispatchBranchId == dispatchBranchId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.DispatchBranchId == dispatchBranchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.PurchaseId == purchaseId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (tenantId != null)
                        {
                            if (dispatchBranchId != null)
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.TenantId == tenantId && x.DispatchBranchId == dispatchBranchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.TenantId == tenantId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.TenantId == tenantId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.TenantId == tenantId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.TenantId == tenantId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dispatchBranchId != null)
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.DispatchBranchId == dispatchBranchId && x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.DispatchBranchId == dispatchBranchId && x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.DispatchBranchId == dispatchBranchId && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.DispatchBranchId == dispatchBranchId && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                            else
                            {
                                if (status != PurchaseStatuses.All)
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.Status == status && x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.Status == status && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (deliveryType != DeliveryTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.DeliveryType == deliveryType && x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltppurchasedItemsView
                                                 where x.CreatedDate >= minDate && x.CreatedDate <= maxDate
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                            }
                        }
                    }
                }

                if (query != null)
                {
                    purchaseItems = new List<PurchaseItem>();
                    PurchaseItem purchaseItem;

                    foreach (OltppurchasedItemsView item in query)
                    {
                        purchaseItem = new PurchaseItem
                        {
                            Id = item.Id,
                            PurchaseId = item.PurchaseId,
                            OfferId = item.OfferId,
                            OfferMainCategoryId = item.OfferMainCategoryId,
                            OfferMainHint = item.OfferMainHint,
                            OfferComplementaryHint = item.OfferComplementaryHint,
                            OfferKeywords = item.OfferKeywords,
                            OfferPurchasedQuantity = item.OfferPurchasedQuantity,
                            OfferPrice = item.OfferPrice,
                            OfferRegularPrice = item.OfferRegularPrice,
                            OfferImgId = item.OfferImg,
                            OfferImgUrl = item.OfferImgUrl,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            DispatchBranchId = item.DispatchBranchId,
                            DispatchValidationSourceId = item.DispatchValidationSourceId,
                            DispatchValidationSourceType = item.DispatchValidationSourceType,
                            DispatchValidationSourceTypeName = item.DispatchValidationSourceType != null ? GetDispatchValidatorSourceTypeName((int)item.DispatchValidationSourceType) : "",
                            ShoppingCartItemId = item.ShoppingCartItemId,
                            UserEarningsIncreserApplied = item.UserEarningsIncreaserApplied,
                            AppliedUserEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                            IncreasementAmount = item.IncreasementAmount,
                            HasPreferences = item.HasPreferences,
                            ChosenPreferences = XElement.Parse(item.ChosenPreferences),
                            Status = item.Status,
                            StatusName = GetPurchaseItemStatusName(item.Status),
                            DeliveryType = item.DeliveryType,
                            DeliveryTypeName = GetDeliveryTypeName(item.DeliveryType),
                            PayedAmount = item.PayedAmount,
                            TenantEarnings = item.TenantEarnings,
                            CashbackPercentage = item.CashbackPercentage,
                            CashbackAmount = item.CashbackAmount,
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            ClaimLocation = item.ClaimLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        purchaseItems.Add(purchaseItem);
                    }
                }
            }
            catch (Exception e)
            {
                purchaseItems = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchaseItems;
        }

        public List<PurchaseItem> Gets(Guid relatedReferenceId, int relatedReferenceType)
        {
            List<PurchaseItem> purchaseItems = null;

            try
            {
                var query = (dynamic)null;

                switch (relatedReferenceType)
                {
                    case PurchaseItemRelatedReferences.PurchaseId:
                        query = from x in this._businessObjects.Context.OltppurchasedItemsView
                                where x.PurchaseId == relatedReferenceId
                                select x;
                        break;
                    case PurchaseItemRelatedReferences.TenantId:
                        query = from x in this._businessObjects.Context.OltppurchasedItemsView
                                where x.TenantId == relatedReferenceId
                                select x;
                        break;
                    case PurchaseItemRelatedReferences.DispatchBranchId:
                        query = from x in this._businessObjects.Context.OltppurchasedItemsView
                                where x.DispatchBranchId == relatedReferenceId
                                select x;
                        break;
                    case PurchaseItemRelatedReferences.ValidatorSourceId:
                        query = from x in this._businessObjects.Context.OltppurchasedItemsView
                                where x.DispatchValidationSourceId == relatedReferenceId
                                select x;
                        break;

                }

                if (query != null)
                {
                    purchaseItems = new List<PurchaseItem>();
                    PurchaseItem purchaseItem;

                    foreach (OltppurchasedItemsView item in query)
                    {
                        purchaseItem = new PurchaseItem
                        {
                            Id = item.Id,
                            PurchaseId = item.PurchaseId,
                            OfferId = item.OfferId,
                            OfferMainCategoryId = item.OfferMainCategoryId,
                            OfferMainHint = item.OfferMainHint,
                            OfferComplementaryHint = item.OfferComplementaryHint,
                            OfferKeywords = item.OfferKeywords,
                            OfferPurchasedQuantity = item.OfferPurchasedQuantity,
                            OfferPrice = item.OfferPrice,
                            OfferRegularPrice = item.OfferRegularPrice,
                            OfferImgId = item.OfferImg,
                            OfferImgUrl = item.OfferImgUrl,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            DispatchBranchId = item.DispatchBranchId,
                            DispatchValidationSourceId = item.DispatchValidationSourceId,
                            DispatchValidationSourceType = item.DispatchValidationSourceType,
                            DispatchValidationSourceTypeName = item.DispatchValidationSourceType != null ? GetDispatchValidatorSourceTypeName((int)item.DispatchValidationSourceType) : "",
                            ShoppingCartItemId = item.ShoppingCartItemId,
                            UserEarningsIncreserApplied = item.UserEarningsIncreaserApplied,
                            AppliedUserEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                            IncreasementAmount = item.IncreasementAmount,
                            HasPreferences = item.HasPreferences,
                            ChosenPreferences = XElement.Parse(item.ChosenPreferences),
                            Status = item.Status,
                            StatusName = GetPurchaseItemStatusName(item.Status),
                            DeliveryType = item.DeliveryType,
                            DeliveryTypeName = GetDeliveryTypeName(item.DeliveryType),
                            PayedAmount = item.PayedAmount,
                            TenantEarnings = item.TenantEarnings,
                            CashbackPercentage = item.CashbackPercentage,
                            CashbackAmount = item.CashbackAmount,
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            ClaimLocation = item.ClaimLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        purchaseItems.Add(purchaseItem); 
                    }
                }
            }
            catch (Exception e)
            {
                purchaseItems = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchaseItems;
        }

        public PurchaseItem Get(Guid id, int nothing)
        {
            PurchaseItem purchaseItem = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltppurchasedItemsView
                            where x.Id == id
                            select x;

                if (query != null)
                {
                    foreach (OltppurchasedItemsView item in query)
                    {
                        purchaseItem = new PurchaseItem
                        {
                            Id = item.Id,
                            PurchaseId = item.PurchaseId,
                            OfferId = item.OfferId,
                            OfferMainCategoryId = item.OfferMainCategoryId,
                            OfferMainHint = item.OfferMainHint,
                            OfferComplementaryHint = item.OfferComplementaryHint,
                            OfferKeywords = item.OfferKeywords,
                            OfferPurchasedQuantity = item.OfferPurchasedQuantity,
                            OfferPrice = item.OfferPrice,
                            OfferRegularPrice = item.OfferRegularPrice,
                            OfferImgId = item.OfferImg,
                            OfferImgUrl = item.OfferImgUrl,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            DispatchBranchId = item.DispatchBranchId,
                            DispatchValidationSourceId = item.DispatchValidationSourceId,
                            DispatchValidationSourceType = item.DispatchValidationSourceType,
                            DispatchValidationSourceTypeName = item.DispatchValidationSourceType != null ? GetDispatchValidatorSourceTypeName((int)item.DispatchValidationSourceType) : "",
                            ShoppingCartItemId = item.ShoppingCartItemId,
                            UserEarningsIncreserApplied = item.UserEarningsIncreaserApplied,
                            AppliedUserEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                            IncreasementAmount = item.IncreasementAmount,
                            HasPreferences = item.HasPreferences,
                            ChosenPreferences = XElement.Parse(item.ChosenPreferences),
                            Status = item.Status,
                            StatusName = GetPurchaseItemStatusName(item.Status),
                            DeliveryType = item.DeliveryType,
                            DeliveryTypeName = GetDeliveryTypeName(item.DeliveryType),
                            PayedAmount = item.PayedAmount,
                            TenantEarnings = item.TenantEarnings,
                            CashbackPercentage = item.CashbackPercentage,
                            CashbackAmount = item.CashbackAmount,
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            ClaimLocation = item.ClaimLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                    }
                }
            }
            catch (Exception e)
            {
                purchaseItem = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchaseItem;
        }

        public List<PurchaseItem> Post(List<NewPurchaseItem> newPurchaseItems)
        {
            List<PurchaseItem> purchaseItems = new List<PurchaseItem>();

            try
            {
                OltppurchasedItems newPurchaseItem;
                PurchaseItem purchaseItem;


                foreach (NewPurchaseItem item in newPurchaseItems)
                {
                    newPurchaseItem = new OltppurchasedItems
                    {
                        Id = Guid.NewGuid(),
                        PurchaseId = item.PurchaseId,
                        OfferId = item.OfferId,
                        OfferMainCategoryId = item.OfferMainCategoryId,
                        OfferMainHint = item.OfferMainHint,
                        OfferComplementaryHint = item.OfferComplementaryHint,
                        OfferKeywords = item.OfferKeywords,
                        OfferPurchasedQuantity = item.OfferPurchasedQuantity,
                        OfferPrice = item.OfferPrice,
                        OfferRegularPrice = item.OfferRegularPrice,
                        OfferImg = item.OfferImgId,
                        OfferImgUrl = item.OfferImgUrl,
                        UserId = item.UserId,
                        TenantId = item.TenantId,
                        ShoppingCartItemId = item.ShoppingCartItemId,
                        UserEarningsIncreaserApplied = item.UserEarningsIncreaserApplied,
                        AppliedUserEarningsIncreaserId = item.AppliedEarningsIncreaserId,
                        IncreasementAmount = item.IncreasementAmount,
                        HasPreferences = item.HasPreferences,
                        ChosenPreferences = item.ChosenPreferences.ToString(),
                        Status = item.Status,
                        DeliveryType = item.DeliveryType,
                        PayedAmount = item.PayedAmount,
                        TenantEarnings = item.TenantEarnings,
                        CashbackPercentage = item.CashbackPercentage,
                        CashbackAmount = item.CashbackAmount,
                        DealType = item.DealType,
                        ClaimLocation = item.ClaimLocation,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };

                    this._businessObjects.Context.OltppurchasedItems.Add(newPurchaseItem);

                    purchaseItem = new PurchaseItem
                    {
                        Id = newPurchaseItem.Id,
                        PurchaseId = newPurchaseItem.PurchaseId,
                        OfferId = newPurchaseItem.OfferId,
                        OfferMainCategoryId = newPurchaseItem.OfferMainCategoryId,
                        OfferMainHint = newPurchaseItem.OfferMainHint,
                        OfferComplementaryHint = newPurchaseItem.OfferComplementaryHint,
                        OfferKeywords = newPurchaseItem.OfferKeywords,
                        OfferPurchasedQuantity = newPurchaseItem.OfferPurchasedQuantity,
                        OfferPrice = newPurchaseItem.OfferPrice,
                        OfferRegularPrice = newPurchaseItem.OfferRegularPrice,
                        OfferImgId = newPurchaseItem.OfferImg,
                        OfferImgUrl = newPurchaseItem.OfferImgUrl,
                        UserId = newPurchaseItem.UserId,
                        TenantId = newPurchaseItem.TenantId,
                        DispatchBranchId = null,
                        DispatchValidationSourceId = null,
                        DispatchValidationSourceType = PurchaseDispatchValidatorSourceTypes.Undefined,
                        DispatchValidationSourceTypeName = "",
                        ShoppingCartItemId = newPurchaseItem.ShoppingCartItemId,
                        UserEarningsIncreserApplied = newPurchaseItem.UserEarningsIncreaserApplied,
                        AppliedUserEarningsIncreaserId = newPurchaseItem.AppliedUserEarningsIncreaserId,
                        IncreasementAmount = newPurchaseItem.IncreasementAmount,
                        HasPreferences = newPurchaseItem.HasPreferences,
                        ChosenPreferences = XElement.Parse(newPurchaseItem.ChosenPreferences),
                        Status = newPurchaseItem.Status,
                        StatusName = GetPurchaseItemStatusName(newPurchaseItem.Status),
                        DeliveryType = newPurchaseItem.DeliveryType,
                        DeliveryTypeName = GetDeliveryTypeName(newPurchaseItem.DeliveryType),
                        PayedAmount = newPurchaseItem.PayedAmount,
                        TenantEarnings = newPurchaseItem.TenantEarnings,
                        CashbackPercentage = newPurchaseItem.CashbackPercentage,
                        CashbackAmount = newPurchaseItem.CashbackAmount,
                        DealType = newPurchaseItem.DealType,
                        DealTypeName = GetDealTypeName(newPurchaseItem.DealType),
                        ClaimLocation = newPurchaseItem.ClaimLocation,
                        CreatedDate = newPurchaseItem.CreatedDate,
                        UpdatedDate = newPurchaseItem.UpdatedDate
                    };

                    purchaseItems.Add(purchaseItem);
                }

                this._businessObjects.Context.SaveChanges();

            }
            catch (Exception e)
            {
                purchaseItems = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return purchaseItems;
        }

        public bool Put(Guid purchaseItemId, int status)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltppurchasedItems
                            where x.Id == purchaseItemId
                            select x;

                if (query != null)
                {
                    OltppurchasedItems currentPurchaseItem = null;

                    foreach (OltppurchasedItems item in query)
                    {
                        currentPurchaseItem = item;
                    }

                    if (currentPurchaseItem != null)
                    {
                        currentPurchaseItem.Status = status;
                        currentPurchaseItem.UpdatedDate = DateTime.UtcNow;

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



        public bool Put(int status, Guid purchaseId)
        {
            bool success = false;

            try
            {
                var query = from x in this._businessObjects.Context.OltppurchasedItems
                            where x.PurchaseId == purchaseId
                            select x;

                if (query != null)
                {

                    foreach (OltppurchasedItems item in query)
                    {
                        item.Status = status;
                        item.UpdatedDate = DateTime.UtcNow;
                    }

                    this._businessObjects.Context.SaveChanges();

                    success = true;
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

        #region FULLPURCHASES

        public List<PurchaseWithItems> Gets(Guid purchaseId, string userId)
        {
            List<PurchaseWithItems> purchaseWithItems = null;

            try
            {

                var query = (dynamic)null;
                
                if(purchaseId != null)
                {
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        query = from x in this._businessObjects.Context.OltppurchasedItemsView
                                where x.PurchaseId == purchaseId && x.UserId == userId
                                orderby x.OfferPrice ascending
                                select x;
                    }
                    else
                    {
                        query = from x in this._businessObjects.Context.OltppurchasedItemsView
                                where x.PurchaseId == purchaseId
                                orderby x.OfferPrice ascending
                                select x;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        query = from x in this._businessObjects.Context.OltppurchasedItemsView
                                where x.UserId == userId
                                orderby x.OfferPrice ascending
                                select x;
                    }
                    else
                    {
                        query = from x in this._businessObjects.Context.OltppurchasedItemsView
                                orderby x.OfferPrice ascending
                                select x;
                    }
                }
                

                if (query != null)
                {
                    List<FlattenedPurchaseItem> flattenedPurchaseItems = new List<FlattenedPurchaseItem>();
                    FlattenedPurchaseItem flattenedPurchaseItem;


                    foreach (OltppurchasedItemsView item in query)
                    {
                        flattenedPurchaseItem = new FlattenedPurchaseItem
                        {
                            Id = item.Id,
                            PurchaseId = item.PurchaseId,
                            UserId = item.UserId,
                            TenantId = item.TenantId,
                            ShoppingCartItemId = item.ShoppingCartItemId,
                            OfferId = item.OfferId,
                            OfferMainCategoryId = item.OfferMainCategoryId,
                            OfferMainHint = item.OfferMainHint,
                            OfferComplementaryHint = item.OfferComplementaryHint,
                            OfferKeywords = item.OfferKeywords,
                            OfferPrice = item.OfferPrice,
                            OfferRegularPrice = item.OfferRegularPrice,
                            OfferPurchasedQuantity = item.OfferPurchasedQuantity,
                            OfferImgId = item.OfferImg,
                            OfferImgUrl = item.OfferImgUrl,
                            UserEarningsIncreserApplied = item.UserEarningsIncreaserApplied,
                            AppliedUserEarningsIncreaserId = item.AppliedUserEarningsIncreaserId,
                            IncreasementAmount = item.IncreasementAmount,
                            HasPreferences = item.HasPreferences,
                            ChosenPreferences = XElement.Parse(item.ChosenPreferences),
                            Status = item.Status,
                            StatusName = GetPurchaseItemStatusName(item.Status),
                            DeliveryType = item.DeliveryType,
                            DeliveryTypeName = GetDeliveryTypeName(item.DeliveryType),
                            PayedAmount = item.PayedAmount,
                            TenantEarnings = item.TenantEarnings,
                            CashbackPercentage = item.CashbackPercentage,
                            CashbackAmount = item.CashbackAmount,
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            ClaimLocation = item.ClaimLocation,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            PurchaseCode = item.PurchaseCode,
                            PurchaseNumericCode = item.PurchaseNumericCode,
                            DispatchBranchId = item.DispatchBranchId,
                            DispatchValidationSourceId = item.DispatchValidationSourceId,
                            DispatchValidationSourceType = item.DispatchValidationSourceType,
                            DispatchValidationSourceTypeName = item.DispatchValidationSourceType != null ? GetDispatchValidatorSourceTypeName((int)item.DispatchValidationSourceType) : "",
                            PaymentLogId = item.PaymentLogId,
                            PurchaseStatus = item.PurchaseStatus,
                            PurchaseStatusName = GetPurchaseItemStatusName(item.PurchaseStatus),
                            PurchaseDealType = item.PurchaseDealType,
                            PurchaseDealTypeName = GetDealTypeName(item.PurchaseDealType),
                            PurchaseDeliveryType = item.PurchaseDeliveryType,
                            PurchaseDeliveryTypeName = GetDeliveryTypeName(item.PurchaseDeliveryType),
                            PurchaseAppliedEarningsIncreaserId = item.PurchaseAppliedEarningsIncreaserId,
                            PurchaseTotalPayedAmount = item.PurchaseTotalPayedAmount,
                            PurchaseTotalTenantEarnings = item.PurchaseTotalTenantEarnings,
                            PurchaseTotalCashbackPercentage = item.PurchaseTotalCashbackPercentage,
                            PurchaseTotalCashbackAmount = item.PurchaseTotalCashbackTotalAmount,
                            PurchaseCreatedDate = item.PurchaseCreatedDate,
                            PurchaseUpdatedDate = item.PurchaseUpdatedDate
                        };

                        flattenedPurchaseItems.Add(flattenedPurchaseItem);
                    }

                    if (flattenedPurchaseItems?.Count > 0)
                    {
                        IEnumerable<IGrouping<Guid, FlattenedPurchaseItem>> groupedByPurchaseId = flattenedPurchaseItems.GroupBy(x => x.PurchaseId);

                        FlattenedPurchaseItem[] currentPurchaseItemsGroup = null;

                        purchaseWithItems = new List<PurchaseWithItems>();
                        PurchaseWithItems currentPurchaseWithItems;

                        int count;

                        foreach (IGrouping<Guid, FlattenedPurchaseItem> purchaseItemsGroup in groupedByPurchaseId)
                        {
                            currentPurchaseItemsGroup = purchaseItemsGroup.ToArray();

                            currentPurchaseWithItems = new PurchaseWithItems
                            {
                                Id = currentPurchaseItemsGroup[0].Id,
                                PurchaseCode = currentPurchaseItemsGroup[0].PurchaseCode,
                                PurchaseNumericCode = currentPurchaseItemsGroup[0].PurchaseNumericCode,
                                UserId = currentPurchaseItemsGroup[0].UserId,
                                TenantId = currentPurchaseItemsGroup[0].TenantId,
                                DispatchBranchId = currentPurchaseItemsGroup[0].DispatchBranchId,
                                DispatchValidationSourceId = currentPurchaseItemsGroup[0].DispatchValidationSourceId,
                                DispatchValidationSourceType = currentPurchaseItemsGroup[0].DispatchValidationSourceType,
                                DispatchValidationSourceTypeName = currentPurchaseItemsGroup[0].DispatchValidationSourceTypeName,
                                PaymentLogId = currentPurchaseItemsGroup[0].PaymentLogId,
                                Status = currentPurchaseItemsGroup[0].Status,
                                StatusName = currentPurchaseItemsGroup[0].StatusName,
                                DealType = currentPurchaseItemsGroup[0].DealType,
                                DealTypeName = currentPurchaseItemsGroup[0].DealTypeName,
                                DeliveryType = currentPurchaseItemsGroup[0].DeliveryType,
                                DeliveryTypeName = currentPurchaseItemsGroup[0].DeliveryTypeName,
                                AppliedEarningsIncreaserId = currentPurchaseItemsGroup[0].AppliedUserEarningsIncreaserId,
                                TotalPayedAmount = currentPurchaseItemsGroup[0].PurchaseTotalPayedAmount,
                                TotalTenantEarnings = currentPurchaseItemsGroup[0].PurchaseTotalTenantEarnings,
                                TotalCashbackPercentage = currentPurchaseItemsGroup[0].PurchaseTotalCashbackPercentage,
                                TotalCashbackAmount = currentPurchaseItemsGroup[0].PurchaseTotalCashbackAmount,
                                CreatedDate = currentPurchaseItemsGroup[0].CreatedDate,
                                UpdatedDate = currentPurchaseItemsGroup[0].UpdatedDate,
                                Items = new List<PurchaseItem>()
                            };

                            count = purchaseItemsGroup.Count();

                            for (int i = 0; i < count; ++i)
                            {
                                currentPurchaseWithItems.Items.Add(new PurchaseItem
                                {
                                    Id = currentPurchaseItemsGroup[i].Id,
                                    PurchaseId = currentPurchaseItemsGroup[i].PurchaseId,
                                    OfferId = currentPurchaseItemsGroup[i].OfferId,
                                    OfferMainCategoryId = currentPurchaseItemsGroup[i].OfferMainCategoryId,
                                    OfferMainHint = currentPurchaseItemsGroup[i].OfferMainHint,
                                    OfferComplementaryHint = currentPurchaseItemsGroup[i].OfferComplementaryHint,
                                    OfferKeywords = currentPurchaseItemsGroup[i].OfferKeywords,
                                    OfferPurchasedQuantity = currentPurchaseItemsGroup[i].OfferPurchasedQuantity,
                                    OfferPrice = currentPurchaseItemsGroup[i].OfferPrice,
                                    OfferRegularPrice = currentPurchaseItemsGroup[i].OfferRegularPrice,
                                    OfferImgId = currentPurchaseItemsGroup[i].OfferImgId,
                                    UserId = currentPurchaseItemsGroup[i].UserId,
                                    TenantId = currentPurchaseItemsGroup[i].TenantId,
                                    DispatchBranchId = currentPurchaseItemsGroup[i].DispatchBranchId,
                                    DispatchValidationSourceId = currentPurchaseItemsGroup[i].DispatchValidationSourceId,
                                    DispatchValidationSourceType = currentPurchaseItemsGroup[i].DispatchValidationSourceType,
                                    DispatchValidationSourceTypeName = currentPurchaseItemsGroup[i].DispatchValidationSourceTypeName,
                                    ShoppingCartItemId = currentPurchaseItemsGroup[i].ShoppingCartItemId,
                                    UserEarningsIncreserApplied = currentPurchaseItemsGroup[i].UserEarningsIncreserApplied,
                                    AppliedUserEarningsIncreaserId = currentPurchaseItemsGroup[i].AppliedUserEarningsIncreaserId,
                                    IncreasementAmount = currentPurchaseItemsGroup[i].IncreasementAmount,
                                    HasPreferences = currentPurchaseItemsGroup[i].HasPreferences,
                                    ChosenPreferences = currentPurchaseItemsGroup[i].ChosenPreferences,
                                    Status = currentPurchaseItemsGroup[i].Status,
                                    StatusName = currentPurchaseItemsGroup[i].StatusName,
                                    DeliveryType = currentPurchaseItemsGroup[i].DeliveryType,
                                    DeliveryTypeName = currentPurchaseItemsGroup[i].DeliveryTypeName,
                                    PayedAmount = currentPurchaseItemsGroup[i].PayedAmount,
                                    TenantEarnings = currentPurchaseItemsGroup[i].TenantEarnings,
                                    CashbackPercentage = currentPurchaseItemsGroup[i].CashbackPercentage,
                                    CashbackAmount = currentPurchaseItemsGroup[i].CashbackAmount,
                                    DealType = currentPurchaseItemsGroup[i].DealType,
                                    DealTypeName = currentPurchaseItemsGroup[i].DealTypeName,
                                    ClaimLocation = currentPurchaseItemsGroup[i].ClaimLocation,
                                    CreatedDate = currentPurchaseItemsGroup[i].CreatedDate,
                                    UpdatedDate = currentPurchaseItemsGroup[i].UpdatedDate
                                });
                            }

                            purchaseWithItems.Add(currentPurchaseWithItems);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                purchaseWithItems = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }


            return purchaseWithItems;
        }

        #endregion

        #region DELIVERYDETAILS

        #endregion


        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new PurchaseLogManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public PurchaseManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD PURCHASE LOG MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
