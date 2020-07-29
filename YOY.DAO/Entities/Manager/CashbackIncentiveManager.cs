using YOY.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using YOY.Values;
using YOY.Values.Strings;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities.Misc.ObjectState.POCO;
using YOY.DTO.Entities.Misc.CashbackIncentive;
using YOY.DTO.Entities.Misc.Branch;

namespace YOY.DAO.Entities.Manager
{
    public class CashbackIncentiveManager
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

        #region CASHBACKINCENTIVES

        private string GetMembershipLevelName(int level)
        {
            string levelName = level switch
            {
                MembershipLevels.Bronze => Resources.Bronze,
                MembershipLevels.Silver => Resources.Silver,
                MembershipLevels.Gold => Resources.Gold,
                MembershipLevels.Platinum => Resources.Platinum,
                MembershipLevels.Diamond => Resources.Diamond,
                _ => "--",
            };
            return levelName;
        }
        private string GetPublishState(DateTime releaseDate, DateTime? expirationDate, DateTime refDate)
        {
            string publishState;

            if (releaseDate > refDate)
            {
                publishState = Resources.NotReleased;
            }
            else
            {
                publishState = refDate > expirationDate ? Resources.Expired : Resources.Released;
            }

            return publishState;
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

        private string GetBenefitAmountTypeName(int earningType)
        {
            string earningTypeName = earningType switch
            {
                CashIncentiveBenefitAmountTypes.None => Resources.None,
                CashIncentiveBenefitAmountTypes.Percentage => Resources.Percentage,
                CashIncentiveBenefitAmountTypes.FixedAmount => Resources.FixedAmount,
                CashIncentiveBenefitAmountTypes.Points => Resources.Points,
                _ => "--",
            };
            return earningTypeName;
        }

        private string GetApplyTypeName(int combineType)
        {
            string combineTypeName = combineType switch
            {
                CashbackApplyTypes.WalletIncrease => Resources.WalletIncrease,
                CashbackApplyTypes.DirectDiscount => Resources.DirectDiscount,
                _ => "--",
            };
            return combineTypeName;
        }

        private string GetTypeName(int type)
        {
            string typeName = type switch
            {
                CashbackTypes.ByTotalAmount => Resources.ByTotalAmount,
                CashbackTypes.ByAmountBlock => Resources.ByAmountBlock,
                _ => "--",
            };
            return typeName;
        }

        private string GetDisplayTypeName(int displayType)
        {
            string typeName = displayType switch
            {
                DisplayTypes.ListingsOnly => Resources.ListingsOnly,
                DisplayTypes.BroadcastingAndListings => Resources.BroadcastingAndListings,
                DisplayTypes.BroadcastingOnly => Resources.BroadcastingOnly,
                DisplayTypes.UnlockCodeRequired => Resources.UnlockCodeRequired,
                _ => "--",
            };
            return typeName;
        }

        private string GetGeoSegmentationTypeName(int segmentationType)
        {
            string typeName = segmentationType switch
            {
                GeoSegmentationTypes.Country => Resources.CountrySegmentation,
                GeoSegmentationTypes.State => Resources.StateSegmentation,
                GeoSegmentationTypes.City => Resources.CitySegmentation,
                _ => "--",
            };
            return typeName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<CashbackIncentive> Gets(int expiredState, int activeState, int releaseState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<CashbackIncentive> cashbackIncentives = new List<CashbackIncentive>();

            yoyIj7qM58dCjContext context = new yoyIj7qM58dCjContext();//this context is created because this call is part of an async logic

            try
            {
                var query = (dynamic)null;

                switch (expiredState)
                {
                    case ExpiredStates.All:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate.Date > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                        }

                        break;
                    case ExpiredStates.Expired://If product is expired makes no sense evaluate release state, product was released before being expired
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                query = (from x in context.OltpcashbackIncentives
                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Active:
                                query = (from x in context.OltpcashbackIncentives
                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Inactive:
                                query = (from x in context.OltpcashbackIncentives
                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }

                        break;
                    case ExpiredStates.Valid:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate.Date > dateTime.Date
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate.Date >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                        }

                        break;
                }


                if (query != null)
                {
                    CashbackIncentive cashbackIncentive = null;
                    foreach (OltpcashbackIncentives item in query)
                    {

                        cashbackIncentive = new CashbackIncentive
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            ApplyType = item.ApplyType,
                            ApplyTypeName = GetApplyTypeName(item.ApplyType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            BenefitAmountType = item.BenefitAmountType,
                            BenefitAmountTypeName = GetBenefitAmountTypeName(item.BenefitAmountType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            MaxCombinedIncentives = item.MaxCombinedIncentives,
                            UnitValue = item.UnitValue,
                            PreviousUnitValue = item.PreviousUnitValue,
                            MinMembershipLevel = item.MinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            MinPurchasedAmount = item.MinPurchasedAmount,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            MaxValue = item.MaxValue,
                            AvailableQuantity = item.AvailableQuantity,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            IsActive = item.IsActive,
                            IsSponsored = item.IsSponsored,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidHours = item.ValidHours,
                            MaxUsagePerUser = item.MaxUsagesPerUser,
                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                            UsageCount = item.UsageCount,
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            RelevanceRate = item.RelevanceRate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            UpdatedDate = item.UpdatedDate,
                            CreatedDate = item.CreatedDate,
                        };

                        cashbackIncentive.PublishState = this.GetPublishState(cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, dateTime);

                        cashbackIncentives.Add(cashbackIncentive);

                    }
                }
                else
                {
                    cashbackIncentives = null;
                }

            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentives;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Get all cashback incentives of a specific type
        /// </summary>
        /// <param name="dealType"></param>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <returns></returns>
        public List<CashbackIncentive> Gets(int dealType, int expiredState, int activeState, int releaseState, DateTime dateTime, bool filterByTenant, int pageSize, int pageNumber)
        {
            List<CashbackIncentive> cashbackIncentives = new List<CashbackIncentive>();

            try
            {
                var query = (dynamic)null;

                switch (expiredState)
                {
                    case ExpiredStates.All:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }


                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }


                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }


                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }

                                break;
                        }

                        break;
                    case ExpiredStates.Expired://If product is expired makes no sense evaluate release state, product was released before being expired
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (filterByTenant)
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                break;
                            case ActiveStates.Active:
                                if (filterByTenant)
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }

                                break;
                            case ActiveStates.Inactive:
                                if (filterByTenant)
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }

                                break;
                        }

                        break;
                    case ExpiredStates.Valid:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && x.IsActive && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.Released:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate.Date <= dateTime.Date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate.Date <= dateTime.Date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate.Date <= dateTime.Date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.ExpirationDate >= dateTime && x.ReleaseDate.Date <= dateTime.Date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                    case ReleaseStates.NotReleased:
                                        if (filterByTenant)
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                         where !x.Deleted && !x.IsActive && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }

                                break;
                        }

                        break;
                }


                if (query != null)
                {
                    CashbackIncentive cashbackIncentive = null;
                    foreach (OltpcashbackIncentives item in query)
                    {
                        cashbackIncentive = new CashbackIncentive
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            ApplyType = item.ApplyType,
                            ApplyTypeName = GetApplyTypeName(item.ApplyType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            BenefitAmountType = item.BenefitAmountType,
                            BenefitAmountTypeName = GetBenefitAmountTypeName(item.BenefitAmountType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            MaxCombinedIncentives = item.MaxCombinedIncentives,
                            UnitValue = item.UnitValue,
                            PreviousUnitValue = item.PreviousUnitValue,
                            MinMembershipLevel = item.MinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            MinPurchasedAmount = item.MinPurchasedAmount,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            MaxValue = item.MaxValue,
                            AvailableQuantity = item.AvailableQuantity,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            IsActive = item.IsActive,
                            IsSponsored = item.IsSponsored,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidHours = item.ValidHours,
                            MaxUsagePerUser = item.MaxUsagesPerUser,
                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                            UsageCount = item.UsageCount,
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            RelevanceRate = item.RelevanceRate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            UpdatedDate = item.UpdatedDate,
                            CreatedDate = item.CreatedDate,
                        };

                        cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, dateTime);

                        cashbackIncentives.Add(cashbackIncentive);

                    }
                }
                else
                {
                    cashbackIncentives = null;
                }

            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentives;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Get all offers from a tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <returns></returns>
        public List<CashbackIncentive> Gets(Guid tenantId, int expiredState, int activeState, int releaseState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<CashbackIncentive> cashbackIncentives = new List<CashbackIncentive>();

            try
            {
                var query = (dynamic)null;

                switch (expiredState)
                {
                    case ExpiredStates.All:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == tenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where x.TenantId == tenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == tenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == tenantId && x.IsActive
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == tenantId && x.IsActive && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == tenantId && x.IsActive && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == tenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == tenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == tenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                        }

                        break;
                    case ExpiredStates.Expired://If product is expired makes no sense evaluate release state, product was released before being expired
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                         where !x.Deleted && x.TenantId == tenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                         where !x.Deleted && x.IsActive && x.TenantId == tenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                         where !x.Deleted && !x.IsActive && x.TenantId == tenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }

                        break;
                    case ExpiredStates.Valid:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == tenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime && (DateTime)x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                        }

                        break;
                }


                if (query != null)
                {
                    CashbackIncentive cashbackIncentive = null;
                    foreach (OltpcashbackIncentives item in query)
                    {
                        cashbackIncentive = new CashbackIncentive
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            ApplyType = item.ApplyType,
                            ApplyTypeName = GetApplyTypeName(item.ApplyType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            BenefitAmountType = item.BenefitAmountType,
                            BenefitAmountTypeName = GetBenefitAmountTypeName(item.BenefitAmountType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            MaxCombinedIncentives = item.MaxCombinedIncentives,
                            UnitValue = item.UnitValue,
                            PreviousUnitValue = item.PreviousUnitValue,
                            MinMembershipLevel = item.MinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            MinPurchasedAmount = item.MinPurchasedAmount,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            MaxValue = item.MaxValue,
                            AvailableQuantity = item.AvailableQuantity,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            IsActive = item.IsActive,
                            IsSponsored = item.IsSponsored,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidHours = item.ValidHours,
                            MaxUsagePerUser = item.MaxUsagesPerUser,
                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                            UsageCount = item.UsageCount,
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            RelevanceRate = item.RelevanceRate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            UpdatedDate = item.UpdatedDate,
                            CreatedDate = item.CreatedDate,
                        };

                        cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, dateTime);

                        cashbackIncentives.Add(cashbackIncentive);

                    }
                }
                else
                {
                    cashbackIncentives = null;
                }

            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentives;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Returns all the cashback incentive with the max limit value, that haven't expired yet
        /// </summary>
        /// <param name="value"></param>
        /// <param name="activeState"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<CashbackIncentive> Gets(decimal value, int activeState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<CashbackIncentive> cashbackIncentives = new List<CashbackIncentive>();

            try
            {

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.UnitValue <= value && x.ExpirationDate >= dateTime.Date && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.UnitValue <= value && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.UnitValue <= value && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                if (query != null)
                {
                    CashbackIncentive cashbackIncentive = null;
                    foreach (OltpcashbackIncentives item in query)
                    {
                        cashbackIncentive = new CashbackIncentive
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            ApplyType = item.ApplyType,
                            ApplyTypeName = GetApplyTypeName(item.ApplyType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            BenefitAmountType = item.BenefitAmountType,
                            BenefitAmountTypeName = GetBenefitAmountTypeName(item.BenefitAmountType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            MaxCombinedIncentives = item.MaxCombinedIncentives,
                            UnitValue = item.UnitValue,
                            PreviousUnitValue = item.PreviousUnitValue,
                            MinMembershipLevel = item.MinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            MinPurchasedAmount = item.MinPurchasedAmount,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            MaxValue = item.MaxValue,
                            AvailableQuantity = item.AvailableQuantity,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            IsActive = item.IsActive,
                            IsSponsored = item.IsSponsored,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidHours = item.ValidHours,
                            MaxUsagePerUser = item.MaxUsagesPerUser,
                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                            UsageCount = item.UsageCount,
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            RelevanceRate = item.RelevanceRate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            UpdatedDate = item.UpdatedDate,
                            CreatedDate = item.CreatedDate,
                        };

                        cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, dateTime);


                        cashbackIncentives.Add(cashbackIncentive);
                    }
                }
            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentives;
        }


        /// <summary>
        /// Gets all cashback incentives with a given active state within a date
        /// </summary>
        /// <param name="activeState"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public List<CashbackIncentive> Gets(int activeState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<CashbackIncentive> cashbackIncentives = new List<CashbackIncentive>();

            try
            {

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                if (query != null)
                {
                    CashbackIncentive cashbackIncentive = null;
                    foreach (OltpcashbackIncentives item in query)
                    {
                        cashbackIncentive = new CashbackIncentive
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            ApplyType = item.ApplyType,
                            ApplyTypeName = GetApplyTypeName(item.ApplyType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            BenefitAmountType = item.BenefitAmountType,
                            BenefitAmountTypeName = GetBenefitAmountTypeName(item.BenefitAmountType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            MaxCombinedIncentives = item.MaxCombinedIncentives,
                            UnitValue = item.UnitValue,
                            PreviousUnitValue = item.PreviousUnitValue,
                            MinMembershipLevel = item.MinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            MinPurchasedAmount = item.MinPurchasedAmount,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            MaxValue = item.MaxValue,
                            AvailableQuantity = item.AvailableQuantity,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            IsActive = item.IsActive,
                            IsSponsored = item.IsSponsored,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidHours = item.ValidHours,
                            MaxUsagePerUser = item.MaxUsagesPerUser,
                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                            UsageCount = item.UsageCount,
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            RelevanceRate = item.RelevanceRate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            UpdatedDate = item.UpdatedDate,
                            CreatedDate = item.CreatedDate,
                        };

                        cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, dateTime);

                        cashbackIncentives.Add(cashbackIncentive);
                    }
                }
            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentives;
        }

        /// <summary>
        /// Get all rewards of a specific type
        /// </summary>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <returns></returns>
        public List<CashbackIncentive> Gets(int expiredState, int activeState, int releaseState, DateTime dateTime, int pageSize, int pageNumber, int nothing)
        {
            List<CashbackIncentive> cashbackIncentives = new List<CashbackIncentive>();

            try
            {
                var query = (dynamic)null;

                switch (expiredState)
                {
                    case ExpiredStates.All:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                        }

                        break;
                    case ExpiredStates.Expired://If product is expired makes no sense evaluate release state, product was released before being expired
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                         where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                         where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                         where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                        }

                        break;
                    case ExpiredStates.Valid:
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                 where !x.Deleted && !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                        }

                        break;
                }

                if (query != null)
                {
                    CashbackIncentive cashbackIncentive = null;
                    foreach (OltpcashbackIncentives item in query)
                    {
                        cashbackIncentive = new CashbackIncentive
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            Type = item.Type,
                            TypeName = GetTypeName(item.Type),
                            ApplyType = item.ApplyType,
                            ApplyTypeName = GetApplyTypeName(item.ApplyType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            BenefitAmountType = item.BenefitAmountType,
                            BenefitAmountTypeName = GetBenefitAmountTypeName(item.BenefitAmountType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            MaxCombinedIncentives = item.MaxCombinedIncentives,
                            UnitValue = item.UnitValue,
                            PreviousUnitValue = item.PreviousUnitValue,
                            MinMembershipLevel = item.MinMembershipLevel,
                            MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                            MinPurchasedAmount = item.MinPurchasedAmount,
                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                            MaxValue = item.MaxValue,
                            AvailableQuantity = item.AvailableQuantity,
                            Name = item.Name,
                            Description = item.Description,
                            Keywords = item.Keywords,
                            IsActive = item.IsActive,
                            IsSponsored = item.IsSponsored,
                            ValidWeekDays = item.ValidWeekDays,
                            ValidMonthDays = item.ValidMonthDays,
                            ValidHours = item.ValidHours,
                            MaxUsagePerUser = item.MaxUsagesPerUser,
                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                            UsageCount = item.UsageCount,
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            RelevanceRate = item.RelevanceRate,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            UpdatedDate = item.UpdatedDate,
                            CreatedDate = item.CreatedDate,
                        };

                        cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, dateTime);

                        cashbackIncentives.Add(cashbackIncentive);

                    }
                }
                else
                {
                    cashbackIncentives = null;
                }

            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentives;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        public CashbackIncentive Get(Guid id, int type, bool filterByTenant)
        {
            CashbackIncentive cashbackIncentive = null;

            try
            {

                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltpcashbackIncentives
                            where !x.Deleted && x.Type == type && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpcashbackIncentives
                            where !x.Deleted && x.Type == type && x.Id == id
                            select x;
                }


                foreach (OltpcashbackIncentives item in query)
                {
                    cashbackIncentive = new CashbackIncentive
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = GetTypeName(item.Type),
                        ApplyType = item.ApplyType,
                        ApplyTypeName = GetApplyTypeName(item.ApplyType),
                        DisplayType = item.DisplayType,
                        DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                        BenefitAmountType = item.BenefitAmountType,
                        BenefitAmountTypeName = GetBenefitAmountTypeName(item.BenefitAmountType),
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        MaxCombinedIncentives = item.MaxCombinedIncentives,
                        UnitValue = item.UnitValue,
                        PreviousUnitValue = item.PreviousUnitValue,
                        MinMembershipLevel = item.MinMembershipLevel,
                        MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                        MinPurchasedAmount = item.MinPurchasedAmount,
                        PurchasedAmountBlock = item.PurchasedAmountBlock,
                        MaxValue = item.MaxValue,
                        AvailableQuantity = item.AvailableQuantity,
                        Name = item.Name,
                        Description = item.Description,
                        Keywords = item.Keywords,
                        IsActive = item.IsActive,
                        IsSponsored = item.IsSponsored,
                        ValidWeekDays = item.ValidWeekDays,
                        ValidMonthDays = item.ValidMonthDays,
                        ValidHours = item.ValidHours,
                        MaxUsagePerUser = item.MaxUsagesPerUser,
                        MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                        UsageCount = item.UsageCount,
                        GeoSegmentationType = item.GeoSegmentationType,
                        GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                        Rules = item.Rules ?? Resources.NoRulesAvailable,
                        Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                        RelevanceRate = item.RelevanceRate,
                        ReleaseDate = item.ReleaseDate,
                        ExpirationDate = item.ExpirationDate,
                        UpdatedDate = item.UpdatedDate,
                        CreatedDate = item.CreatedDate,
                    };

                    DateTime now = DateTime.UtcNow;

                    cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, now);
                }
            }
            catch (Exception e)
            {
                cashbackIncentive = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentive;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //        

        public CashbackIncentive Get(Guid id, bool filterByTenant)
        {
            CashbackIncentive cashbackIncentive = null;

            try
            {

                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltpcashbackIncentives
                            where !x.Deleted && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpcashbackIncentives
                            where !x.Deleted && x.Id == id
                            select x;
                }


                foreach (OltpcashbackIncentives item in query)
                {
                    cashbackIncentive = new CashbackIncentive
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        Type = item.Type,
                        TypeName = GetTypeName(item.Type),
                        ApplyType = item.ApplyType,
                        ApplyTypeName = GetApplyTypeName(item.ApplyType),
                        DisplayType = item.DisplayType,
                        DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                        BenefitAmountType = item.BenefitAmountType,
                        BenefitAmountTypeName = GetBenefitAmountTypeName(item.BenefitAmountType),
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        MaxCombinedIncentives = item.MaxCombinedIncentives,
                        UnitValue = item.UnitValue,
                        PreviousUnitValue = item.PreviousUnitValue,
                        MinMembershipLevel = item.MinMembershipLevel,
                        MinMembershipLevelName = GetMembershipLevelName(item.MinMembershipLevel),
                        MinPurchasedAmount = item.MinPurchasedAmount,
                        PurchasedAmountBlock = item.PurchasedAmountBlock,
                        MaxValue = item.MaxValue,
                        AvailableQuantity = item.AvailableQuantity,
                        Name = item.Name,
                        Description = item.Description,
                        Keywords = item.Keywords,
                        IsActive = item.IsActive,
                        IsSponsored = item.IsSponsored,
                        ValidWeekDays = item.ValidWeekDays,
                        ValidMonthDays = item.ValidMonthDays,
                        ValidHours = item.ValidHours,
                        MaxUsagePerUser = item.MaxUsagesPerUser,
                        MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                        UsageCount = item.UsageCount,
                        GeoSegmentationType = item.GeoSegmentationType,
                        GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                        Rules = item.Rules ?? Resources.NoRulesAvailable,
                        Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                        RelevanceRate = item.RelevanceRate,
                        ReleaseDate = item.ReleaseDate,
                        ExpirationDate = item.ExpirationDate,
                        UpdatedDate = item.UpdatedDate,
                        CreatedDate = item.CreatedDate,
                    };

                    DateTime now = DateTime.UtcNow;

                    cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, now);
                }
            }
            catch (Exception e)
            {
                cashbackIncentive = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentive;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //        


        /// <summary>
        /// Creates new cashback incentive
        /// </summary>
        /// <param name="type"></param>
        /// <param name="displayType"></param>
        /// <param name="earningType"></param>
        /// <param name="dealType"></param>
        /// <param name="combineType"></param>
        /// <param name="unitValue"></param>
        /// <param name="previousUnitValue"></param>
        /// <param name="minMembershipLevel"></param>
        /// <param name="minPurchasedAmount"></param>
        /// <param name="purchasedAmountBlock"></param>
        /// <param name="maxValue"></param>
        /// <param name="availableQuantity"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="keywords"></param>
        /// <param name="isSponsored"></param>
        /// <param name="validWeekDays"></param>
        /// <param name="validMonthDays"></param>
        /// <param name="validHours"></param>
        /// <param name="maxUsagesPerUser"></param>
        /// <param name="minPurchasesCountToUse"></param>
        /// <param name="geoSegmentationType"></param>
        /// <param name="rules"></param>
        /// <param name="conditions"></param>
        /// <param name="relevanceRate"></param>
        /// <param name="releaseDate"></param>
        /// <param name="expirationDate"></param>
        /// <returns></returns>
        public CashbackIncentive Post(int type, int displayType, int applyType, int benefitAmountType, int dealType, int maxCombinedIncentives, decimal unitValue, decimal previousUnitValue, int minMembershipLevel, decimal minPurchasedAmount, 
            decimal purchasedAmountBlock, decimal maxValue, int availableQuantity, string name, string description, string keywords, bool isSponsored, string validWeekDays, 
            string validMonthDays, string validHours, int maxUsagesPerUser, int minPurchasesCountToUse, int geoSegmentationType, string rules, string conditions, double relevanceRate, DateTime releaseDate, DateTime expirationDate)
        {
            CashbackIncentive cashbackIncentive;
            OltpcashbackIncentives newCashbackIncentive = null;

            try
            {
                newCashbackIncentive = new OltpcashbackIncentives
                {
                    Id = Guid.NewGuid(),
                    TenantId = _businessObjects.Tenant.TenantId,
                    Type = type,
                    ApplyType = applyType,
                    DisplayType = displayType,
                    BenefitAmountType = benefitAmountType,
                    DealType = dealType,
                    MaxCombinedIncentives = maxCombinedIncentives,
                    UnitValue = unitValue,
                    PreviousUnitValue = previousUnitValue,
                    MinMembershipLevel = minMembershipLevel,
                    MinPurchasedAmount = minPurchasedAmount,
                    PurchasedAmountBlock = purchasedAmountBlock,
                    MaxValue = maxValue,
                    AvailableQuantity = availableQuantity,
                    Name = name,
                    Description = description,
                    Keywords = keywords,
                    IsActive = true,
                    IsSponsored = isSponsored,
                    ValidWeekDays = validWeekDays,
                    ValidMonthDays = validMonthDays,
                    ValidHours = validHours,
                    MaxUsagesPerUser = maxUsagesPerUser,
                    MinPurchasesCountToUse = minPurchasesCountToUse,
                    UsageCount = 0,
                    GeoSegmentationType = geoSegmentationType,
                    Rules = rules,
                    Conditions = conditions,
                    RelevanceRate = relevanceRate,
                    ReleaseDate = releaseDate,
                    ExpirationDate = expirationDate,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    Deleted = false
                };

                this._businessObjects.Context.OltpcashbackIncentives.Add(newCashbackIncentive);
                this._businessObjects.Context.SaveChanges();

                cashbackIncentive = new CashbackIncentive
                {
                    Id = newCashbackIncentive.Id,
                    TenantId = newCashbackIncentive.TenantId,
                    Type = newCashbackIncentive.Type,
                    TypeName = GetTypeName(newCashbackIncentive.Type),
                    ApplyType = newCashbackIncentive.ApplyType,
                    ApplyTypeName = GetApplyTypeName(newCashbackIncentive.ApplyType),
                    DisplayType = newCashbackIncentive.DisplayType,
                    DisplayTypeName = GetDisplayTypeName(newCashbackIncentive.DisplayType),
                    BenefitAmountType = newCashbackIncentive.BenefitAmountType,
                    BenefitAmountTypeName = GetBenefitAmountTypeName(newCashbackIncentive.BenefitAmountType),
                    DealType = newCashbackIncentive.DealType,
                    DealTypeName = GetDealTypeName(newCashbackIncentive.DealType),
                    MaxCombinedIncentives = newCashbackIncentive.MaxCombinedIncentives,
                    UnitValue = newCashbackIncentive.UnitValue,
                    PreviousUnitValue = newCashbackIncentive.PreviousUnitValue,
                    MinMembershipLevel = newCashbackIncentive.MinMembershipLevel,
                    MinMembershipLevelName = GetMembershipLevelName(newCashbackIncentive.MinMembershipLevel),
                    MinPurchasedAmount = newCashbackIncentive.MinPurchasedAmount,
                    PurchasedAmountBlock = newCashbackIncentive.PurchasedAmountBlock,
                    MaxValue = newCashbackIncentive.MaxValue,
                    AvailableQuantity = newCashbackIncentive.AvailableQuantity,
                    Name = newCashbackIncentive.Name,
                    Description = newCashbackIncentive.Description,
                    Keywords = newCashbackIncentive.Keywords,
                    IsActive = newCashbackIncentive.IsActive,
                    IsSponsored = newCashbackIncentive.IsSponsored,
                    ValidWeekDays = newCashbackIncentive.ValidWeekDays,
                    ValidMonthDays = newCashbackIncentive.ValidMonthDays,
                    ValidHours = newCashbackIncentive.ValidHours,
                    MaxUsagePerUser = newCashbackIncentive.MaxUsagesPerUser,
                    MinPurchasesCountToUse = newCashbackIncentive.MinPurchasesCountToUse,
                    UsageCount = newCashbackIncentive.UsageCount,
                    GeoSegmentationType = newCashbackIncentive.GeoSegmentationType,
                    GeoSegmentationTypeName = GetGeoSegmentationTypeName(newCashbackIncentive.GeoSegmentationType),
                    Rules = newCashbackIncentive.Rules ?? Resources.NoRulesAvailable,
                    Conditions = newCashbackIncentive.Conditions ?? Resources.NoConditionsAvailable,
                    RelevanceRate = newCashbackIncentive.RelevanceRate,
                    ReleaseDate = newCashbackIncentive.ReleaseDate,
                    ExpirationDate = newCashbackIncentive.ExpirationDate,
                    UpdatedDate = newCashbackIncentive.UpdatedDate,
                    CreatedDate = newCashbackIncentive.CreatedDate,
                };

                DateTime now = DateTime.UtcNow;


                cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, now);

            }
            catch (Exception e)
            {
                this._businessObjects.Context.OltpcashbackIncentives.Remove(newCashbackIncentive);
                this._businessObjects.Context.SaveChanges();

                cashbackIncentive = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentive;
        }//METHOD POST ENDS ----------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Updates a cashback incentive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="earningType"></param>
        /// <param name="type"></param>
        /// <param name="dealType"></param>
        /// <param name="unitValue"></param>
        /// <param name="minPurchasedAmount"></param>
        /// <param name="purchasedAmountBlock"></param>
        /// <param name="maxValue"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="keywords"></param>
        /// <param name="requiresReferenceCode"></param>
        /// <param name="releaseDate"></param>
        /// <param name="expirationDate"></param>
        /// <param name="validWeekDays"></param>
        /// <param name="validMonthDays"></param>
        /// <param name="validHours"></param>
        /// <param name="maxUsagesPerUser"></param>
        /// <param name="geoSegmentationType"></param>
        /// <param name="rules"></param>
        /// <param name="conditions"></param>
        /// <param name="oneTimeUsagePerUser"></param>
        /// <param name="minPurchasesCountToUse"></param>
        /// <param name="purchasesCountStartDate"></param>
        /// <param name="minPurchasesHoursTimeoutToUse"></param>
        /// <param name="maxPurchasesDaysTimeoutToUse"></param>
        /// <param name="commissionFeeType"></param>
        /// <param name="commissionFeeValue"></param>
        /// <param name="minCommissionFeeAmount"></param>
        /// <returns></returns>
        public CashbackIncentive Put(Guid id, int type, int displayType, int applyType, int benefitAmountType, int dealType, int maxCombinedIncentives, decimal unitValue, decimal previousUnitValue, int minMembershipLevel, decimal minPurchasedAmount, 
            decimal purchasedAmountBlock, decimal maxValue, int availableQuantity, string name, string description, string keywords, bool isSponsored, string validWeekDays, 
            string validMonthDays, string validHours, int maxUsagesPerUser, int minPurchasesCountToUse, int geoSegmentationType, string rules, string conditions, 
            double relevanceRate, DateTime releaseDate, DateTime expirationDate)
        {
            CashbackIncentive cashbackIncentive = null;

            try
            {
                OltpcashbackIncentives currentCashbackIncentive = (from x in _businessObjects.Context.OltpcashbackIncentives
                                                                    where x.Type == type && x.TenantId == _businessObjects.Tenant.TenantId && x.Id == id
                                                                    select x).FirstOrDefault();

                if (currentCashbackIncentive != null)
                {
                    currentCashbackIncentive.Type = type;
                    currentCashbackIncentive.DisplayType = displayType;
                    currentCashbackIncentive.ApplyType = applyType;
                    currentCashbackIncentive.BenefitAmountType = benefitAmountType;
                    currentCashbackIncentive.DealType = dealType;
                    currentCashbackIncentive.MaxCombinedIncentives = maxCombinedIncentives;
                    currentCashbackIncentive.UnitValue = unitValue;
                    currentCashbackIncentive.PreviousUnitValue = previousUnitValue;
                    currentCashbackIncentive.MinMembershipLevel = minMembershipLevel;
                    currentCashbackIncentive.MinPurchasedAmount = minPurchasedAmount;
                    currentCashbackIncentive.PurchasedAmountBlock = purchasedAmountBlock;
                    currentCashbackIncentive.MaxValue = maxValue;
                    currentCashbackIncentive.AvailableQuantity = availableQuantity;
                    currentCashbackIncentive.Name = name;
                    currentCashbackIncentive.Description = description;
                    currentCashbackIncentive.Keywords = keywords;
                    currentCashbackIncentive.IsSponsored = isSponsored;
                    currentCashbackIncentive.ValidWeekDays = validWeekDays;
                    currentCashbackIncentive.ValidMonthDays = validMonthDays;
                    currentCashbackIncentive.ValidHours = validHours;
                    currentCashbackIncentive.MaxUsagesPerUser = maxUsagesPerUser;
                    currentCashbackIncentive.MinPurchasesCountToUse = minPurchasesCountToUse;
                    currentCashbackIncentive.GeoSegmentationType = geoSegmentationType;
                    currentCashbackIncentive.Rules = rules;
                    currentCashbackIncentive.Conditions = conditions;
                    currentCashbackIncentive.RelevanceRate = relevanceRate;
                    currentCashbackIncentive.ReleaseDate = releaseDate;
                    currentCashbackIncentive.ExpirationDate = expirationDate;
                    currentCashbackIncentive.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    cashbackIncentive = new CashbackIncentive
                    {
                        Id = currentCashbackIncentive.Id,
                        TenantId = currentCashbackIncentive.TenantId,
                        Type = currentCashbackIncentive.Type,
                        TypeName = GetTypeName(currentCashbackIncentive.Type),
                        ApplyType = currentCashbackIncentive.ApplyType,
                        ApplyTypeName = GetApplyTypeName(currentCashbackIncentive.ApplyType),
                        DisplayType = currentCashbackIncentive.DisplayType,
                        DisplayTypeName = GetDisplayTypeName(currentCashbackIncentive.DisplayType),
                        BenefitAmountType = currentCashbackIncentive.BenefitAmountType,
                        BenefitAmountTypeName = GetBenefitAmountTypeName(currentCashbackIncentive.BenefitAmountType),
                        DealType = currentCashbackIncentive.DealType,
                        DealTypeName = GetDealTypeName(currentCashbackIncentive.DealType),
                        MaxCombinedIncentives = currentCashbackIncentive.MaxCombinedIncentives,
                        UnitValue = currentCashbackIncentive.UnitValue,
                        PreviousUnitValue = currentCashbackIncentive.PreviousUnitValue,
                        MinMembershipLevel = currentCashbackIncentive.MinMembershipLevel,
                        MinMembershipLevelName = GetMembershipLevelName(currentCashbackIncentive.MinMembershipLevel),
                        MinPurchasedAmount = currentCashbackIncentive.MinPurchasedAmount,
                        PurchasedAmountBlock = currentCashbackIncentive.PurchasedAmountBlock,
                        MaxValue = currentCashbackIncentive.MaxValue,
                        AvailableQuantity = currentCashbackIncentive.AvailableQuantity,
                        Name = currentCashbackIncentive.Name,
                        Description = currentCashbackIncentive.Description,
                        Keywords = currentCashbackIncentive.Keywords,
                        IsActive = currentCashbackIncentive.IsActive,
                        IsSponsored = currentCashbackIncentive.IsSponsored,
                        ValidWeekDays = currentCashbackIncentive.ValidWeekDays,
                        ValidMonthDays = currentCashbackIncentive.ValidMonthDays,
                        ValidHours = currentCashbackIncentive.ValidHours,
                        MaxUsagePerUser = currentCashbackIncentive.MaxUsagesPerUser,
                        MinPurchasesCountToUse = currentCashbackIncentive.MinPurchasesCountToUse,
                        UsageCount = currentCashbackIncentive.UsageCount,
                        GeoSegmentationType = currentCashbackIncentive.GeoSegmentationType,
                        GeoSegmentationTypeName = GetGeoSegmentationTypeName(currentCashbackIncentive.GeoSegmentationType),
                        Rules = currentCashbackIncentive.Rules ?? Resources.NoRulesAvailable,
                        Conditions = currentCashbackIncentive.Conditions ?? Resources.NoConditionsAvailable,
                        RelevanceRate = currentCashbackIncentive.RelevanceRate,
                        ReleaseDate = currentCashbackIncentive.ReleaseDate,
                        ExpirationDate = currentCashbackIncentive.ExpirationDate,
                        UpdatedDate = currentCashbackIncentive.UpdatedDate,
                        CreatedDate = currentCashbackIncentive.CreatedDate,
                    };

                    DateTime now = DateTime.UtcNow;

                    cashbackIncentive.PublishState = this.GetPublishState((DateTime)cashbackIncentive.ReleaseDate, cashbackIncentive.ExpirationDate, now);
                }

            }
            catch (Exception e)
            {
                cashbackIncentive = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentive;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Changes a state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObjectStateUpdate Put(Guid id, int type, int changeType)
        {
            ObjectStateUpdate result = new ObjectStateUpdate();

            try
            {
                OltpcashbackIncentives incentive = null;

                var query = from x in this._businessObjects.Context.OltpcashbackIncentives
                            where x.Type == type && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (OltpcashbackIncentives item in query)
                {
                    incentive = item;
                }

                if (incentive != null)
                {
                    switch (changeType)
                    {
                        case ChangeTypes.ActiveState:
                            incentive.IsActive = !incentive.IsActive;
                            incentive.UpdatedDate = DateTime.UtcNow;
                            this._businessObjects.Context.SaveChanges();

                            result.Success = true;
                            result.NewState = incentive.IsActive;
                            break;
                        case ChangeTypes.SponsoredState:
                            incentive.IsSponsored = !incentive.IsSponsored;
                            incentive.UpdatedDate = DateTime.UtcNow;
                            this._businessObjects.Context.SaveChanges();

                            result.Success = true;
                            result.NewState = incentive.IsSponsored;
                            break;
                    }

                }

            }
            catch (Exception e)
            {
                result.Success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return result;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// Changes a state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Put(Guid id, int newUsages)
        {
            bool success = false;

            try
            {
                OltpcashbackIncentives incentive = null;

                var query = from x in this._businessObjects.Context.OltpcashbackIncentives
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (OltpcashbackIncentives item in query)
                {
                    incentive = item;
                }

                if (incentive != null)
                {
                    incentive.UsageCount += newUsages;
                    incentive.UpdatedDate = DateTime.UtcNow;
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
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Deletes a cashback incentive
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id, int type)
        {
            bool success = false;

            try
            {

                OltpcashbackIncentives cashbackIncentive = (from x in this._businessObjects.Context.OltpcashbackIncentives
                                                            where x.Type == type && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                                                            select x).FirstOrDefault();


                if (cashbackIncentive != null)
                {
                    cashbackIncentive.Deleted = true;
                    cashbackIncentive.UpdatedDate = DateTime.UtcNow;

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
        }//METHOD DELETE ENDS --------------------------------------------------------------------------------------------------------------------------- //



        /// <summary>
        /// Modifies quantity attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public bool Put(Guid id, int quantity, int operation, int nothing)
        {
            bool success = false;

            try
            {

                int? availableQuantity = null;

                switch (operation)
                {
                    case ProductOperations.Increase:

                        availableQuantity = this._businessObjects.StoredProcsHandler.UpdateCashbackIncentiveQuantity(id, quantity);

                        if (availableQuantity != null)
                            success = true;


                        break;
                    case ProductOperations.Claim:

                        bool? result = this._businessObjects.StoredProcsHandler.UpdateCashbackIncentiveUsageCount(id, quantity);

                        if (result == true)
                        {
                            availableQuantity = this._businessObjects.StoredProcsHandler.UpdateCashbackIncentiveQuantity(id, -1 * quantity);

                            if (availableQuantity != null)
                                success = true;
                        }

                        break;

                }

            }
            catch (Exception e)
            {
                success = false;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return success;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        #endregion

        #region CASHBACKINCENTIVESFULLDATA
        /// <summary>
        /// Retrieve cashback incentive flattened data about all the offers that are potencially
        /// available in a radius from a given prosition. At this point there is no certainity that 
        /// the incentive actually is available for that position and radius, but that it belongs to a 
        /// tenant that has a branch within that radius from the reference point. 
        /// Also includes data to the corresponding preference
        /// It retrieve exclusively offers that are active, released, unexpired and not prizes
        /// for active and released tenants and active branches
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="radius"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private List<FlattenedCashbackIncentiveData> GetCashbackIncentivesDataByRegionWithLocation(Guid countryId, Guid stateId, int contentSegmentationType, string userId, decimal latitude, decimal longitude, double radius, DateTime dateTime, int selectorType)
        {
            List<FlattenedCashbackIncentiveData> cashbackIncentives = null;

            try
            {
                var query = (dynamic)null;

                switch (selectorType)
                {
                    case ContentFilters.Category:

                        //Nothing for now

                        break;
                    case ContentFilters.Tenant:

                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableCashbackIncentivesByCountryWithLocationTenantFocus(latitude, longitude, radius * DistanceLimits.MaxKMRangeForMainOffersByCountryFactor, countryId, userId, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableCashbackIncentivesByStateWithLocationTenantFocus(latitude, longitude, radius, stateId, countryId, userId, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedCashbackIncentiveData incentiveData;
                            cashbackIncentives = new List<FlattenedCashbackIncentiveData>();

                            foreach (TempcashbackIncentivesPreferenceBranches item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    incentiveData = new FlattenedCashbackIncentiveData
                                    {
                                        SelectorType = selectorType,
                                        CashbackIncentive = new CashbackIncentive
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            Type = item.Type,
                                            ApplyType = item.ApplyType,
                                            DisplayType = item.DisplayType,
                                            BenefitAmountType = item.BenefitAmountType,
                                            DealType = item.DealType,
                                            MaxCombinedIncentives = item.MaxCombinedIncentives,
                                            UnitValue = item.UnitValue,
                                            PreviousUnitValue = item.PreviousUnitValue,
                                            MinMembershipLevel = item.MinMembershipLevel,
                                            MinPurchasedAmount = item.MinPurchasedAmount,
                                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                                            MaxValue = item.MaxValue,
                                            AvailableQuantity = item.AvailableQuantity,
                                            Name = item.Name,
                                            Description = item.Description,
                                            Keywords = item.Keywords,
                                            IsActive = item.IsActive,
                                            IsSponsored = item.IsSponsored,
                                            ValidWeekDays = item.ValidWeekDays,
                                            ValidMonthDays = item.ValidMonthDays,
                                            ValidHours = item.ValidHours,
                                            MaxUsagePerUser = item.MaxUsagesPerUser,
                                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                                            UsageCount = item.UsageCount,
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            RelevanceRate = item.RelevanceRate,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            CreatedDate = item.CreatedDate,
                                        },
                                        Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                                        {
                                            Id = item.TenantId,
                                            Name = item.TenantName,
                                            Logo = item.TenantLogo,
                                            CountryId = item.TenantCountryId,
                                            CurrencySymbol = item.CurrencySymbol,
                                            CategoryId = item.TenantCategoryId,
                                            CategoryName = item.TenantCategoryName,
                                            Type = item.TenantType,
                                            RelevanceScore = item.TenantScore,
                                            NearestBranchId = Guid.Empty,
                                            NearestBranchName = "",
                                            NearestBranchLatitude = null,
                                            NearestBranchLongitude = null,
                                        },
                                        Branch = new BasicBranchData
                                        {
                                            Id = item.BranchId,
                                            Name = item.BranchName,
                                            DescriptiveAddress = item.BranchDescriptiveAddress,
                                            InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                            Latitude = item.BranchLatitude,
                                            Longitude = item.BranchLongitude,
                                            Distance = Math.Round(((double)item.Distance / 1000), 2, MidpointRounding.AwayFromZero),//Is originally in meters, it's passed to kilometers
                                            CityId = item.BranchCityId,
                                            StateId = item.BranchStateId,
                                            Enabled = false
                                        },
                                        //When its selector is tenant, there is no data about the preference
                                        Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                                        {
                                            Id = Guid.Empty,
                                            Name = "",
                                            Icon = "",
                                            RelevanceScore = -1
                                        },
                                        ExactLocationBased = true
                                    };

                                    cashbackIncentives.Add(incentiveData);
                                }

                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentives;
        }

        /// <summary>
        /// Retrieve offer flattened data about all the offers that are potencially
        /// available in a given state. At this point there is no certainity that 
        /// the offer actually is available in that state, but that it belongs to a 
        /// tenant that has a branch in that state. Also includes data to the corresponding preference
        /// It retrieve exclusively offers that are active, released, unexpired and not prizes
        /// for active and released tenants and active branches
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private List<FlattenedCashbackIncentiveData> GetCashbackIncentivesDataByRegion(Guid stateId, Guid countryId, int contentSegmentationType, string userId, DateTime dateTime, int selectorType)
        {
            List<FlattenedCashbackIncentiveData> cashbackIncentives = null;

            try
            {
                var query = (dynamic)null;

                switch (selectorType)
                {
                    case ContentFilters.Category:

                        //NOTHING FOR NOW

                        break;
                    case ContentFilters.Tenant:

                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableCashbackIncentivesByCountryTenantFocus(countryId, userId, dateTime)
                                        where (x.AvailableQuantity == -1 || x.AvailableQuantity > 0)
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableCashbackIncentivesByStateTenantFocus(stateId, countryId, userId, dateTime)
                                        where (x.AvailableQuantity == -1 || x.AvailableQuantity > 0)
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedCashbackIncentiveData incentiveData;
                            cashbackIncentives = new List<FlattenedCashbackIncentiveData>();

                            foreach (TempcashbackIncentivesPreferenceBranches item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    incentiveData = new FlattenedCashbackIncentiveData
                                    {
                                        SelectorType = selectorType,
                                        CashbackIncentive = new CashbackIncentive
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            Type = item.Type,
                                            ApplyType = item.ApplyType,
                                            DisplayType = item.DisplayType,
                                            BenefitAmountType = item.BenefitAmountType,
                                            DealType = item.DealType,
                                            MaxCombinedIncentives = item.MaxCombinedIncentives,
                                            UnitValue = item.UnitValue,
                                            PreviousUnitValue = item.PreviousUnitValue,
                                            MinMembershipLevel = item.MinMembershipLevel,
                                            MinPurchasedAmount = item.MinPurchasedAmount,
                                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                                            MaxValue = item.MaxValue,
                                            AvailableQuantity = item.AvailableQuantity,
                                            Name = item.Name,
                                            Description = item.Description,
                                            Keywords = item.Keywords,
                                            IsActive = item.IsActive,
                                            IsSponsored = item.IsSponsored,
                                            ValidWeekDays = item.ValidWeekDays,
                                            ValidMonthDays = item.ValidMonthDays,
                                            ValidHours = item.ValidHours,
                                            MaxUsagePerUser = item.MaxUsagesPerUser,
                                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                                            UsageCount = item.UsageCount,
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            RelevanceRate = item.RelevanceRate,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            CreatedDate = item.CreatedDate,
                                        },
                                        Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                                        {
                                            Id = item.TenantId,
                                            Name = item.TenantName,
                                            Logo = item.TenantLogo,
                                            CountryId = item.TenantCountryId,
                                            CurrencySymbol = item.CurrencySymbol,
                                            CategoryId = item.TenantCategoryId,
                                            CategoryName = item.TenantCategoryName,
                                            Type = item.TenantType,
                                            RelevanceScore = item.TenantScore,
                                            NearestBranchId = Guid.Empty,
                                            NearestBranchName = "",
                                            NearestBranchLatitude = null,
                                            NearestBranchLongitude = null,
                                        },
                                        Branch = new BasicBranchData
                                        {
                                            Id = item.BranchId,
                                            Name = item.BranchName,
                                            DescriptiveAddress = item.BranchDescriptiveAddress,
                                            InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                            Latitude = item.BranchLatitude,
                                            Longitude = item.BranchLongitude,
                                            Distance = null,//In this case there is no way to define distance
                                            CityId = item.BranchCityId,
                                            StateId = item.BranchStateId,
                                            Enabled = false
                                        },
                                        //When selector is tenant, there is no preference data
                                        Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                                        {
                                            Id = Guid.Empty,
                                            Name = "",
                                            Icon = "",
                                            RelevanceScore = -1
                                        },
                                        ExactLocationBased = false
                                    };

                                    cashbackIncentives.Add(incentiveData);
                                }

                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentives;
        }

        /*
        /// <summary>
        /// Retrieve cashback incentive flattened data about all the offers that are potencially
        /// available in a given state. At this point there is no certainity that 
        /// the incentive actually is available in that state, but that it belongs to a 
        /// tenant that has a branch in that state. Also includes data to the corresponding preference
        /// It retrieve exclusively incentives that are active, released, unexpired
        /// for active and released tenants and active branches
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private List<FlattenedCashbackIncentiveData> GetOffersDataByRegionForReference(Guid stateId, Guid countryId, int contentSegmentationType, string userId, DateTime dateTime, int referenceType, Guid referenceId)
        {
            List<FlattenedCashbackIncentiveData> cashbackIncentives = null;

            try
            {
                var query = (dynamic)null;

                switch (referenceType)
                {
                    case ContentFilters.Category:
                        
                        //NOTHING FOR NOW

                        break;
                    case ContentFilters.Tenant:
                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:
                                query = from x in this._businessObjects.Context.GetAvailableCashbackIncentivesByCountryForTenant(countryId, userId, referenceId, dateTime)
                                        where (x.AvailableQuantity == -1 || x.AvailableQuantity > 0)
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:
                                query = from x in this._businessObjects.Context.GetAvailableCashbackIncentivesByStateForTenant(stateId, countryId, userId, referenceId, dateTime)
                                        where (x.AvailableQuantity == -1 || x.AvailableQuantity > 0)
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedCashbackIncentiveData incentiveData;

                            cashbackIncentives = new List<FlattenedCashbackIncentiveData>();

                            foreach (TEMPCashbackIncentivesPreferenceBranch item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    incentiveData = new FlattenedCashbackIncentiveData
                                    {
                                        SelectorType = referenceType,
                                        CashbackIncentive = new CashbackIncentive
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            Type = item.Type,
                                            TypeName = GetTypeName(item.Type),
                                            EarningType = item.EarningType,
                                            EarningTypeName = GetEarningTypeName(item.EarningType),
                                            DealType = item.DealType,
                                            DealTypeName = GetDealTypeName(item.DealType),
                                            CombineType = item.CombineType,
                                            CombineTypeName = GetCombineTypeName(item.CombineType),
                                            UnitValue = item.UnitValue,
                                            PreviousUnitValue = item.PreviousUnitValue,
                                            MinPurchasedAmount = item.MinPurchasedAmount,
                                            PurchasedAmountBlock = item.PurchasedAmountBlock,
                                            MaxValue = item.MaxValue,
                                            AvailableQuantity = item.AvailableQuantity,
                                            Name = item.Name,
                                            Description = item.Description,
                                            RegexToMatchInReceipt = item.RegexToMatchInReceipt,
                                            Keywords = item.Keywords,
                                            RequiresReferenceCode = item.RequiresReferenceCode,
                                            IsActive = item.IsActive,
                                            IsSponsored = item.IsSponsored,
                                            CreationDate = item.CreationDate,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            ValidWeekDays = item.ValidWeekDays,
                                            ValidMonthDays = item.ValidMonthDays,
                                            ValidHours = item.ValidHours,
                                            MaxUsagePerUser = item.MaxUsagesPerUser,
                                            UsageCount = item.UsageCount,
                                            RelevanceRate = item.RelevanceRate,
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            ClaimInstructions = item.ClaimInstructions ?? Resources.DefaultIncentiveClaimInstructions,
                                            OneTimeUsagePerUser = item.OneTimeUsagePerUser,
                                            MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                                            MinPurchasesHoursTimeoutToUse = item.MinPurchasesHoursTimeoutToUse,
                                            MaxPurchasesDaysTimeoutToUse = item.MaxPurchasesDaysTimeoutToUse,
                                            CommissionFeeType = item.CommissionFeeType,
                                            CommissionFeeTypeName = GetCommissionTypeName(item.CommissionFeeType),
                                            CommissionFeeValue = item.CommissionFeeValue,
                                            MinCommissionFeeAmount = item.MinCommissionFeeAmount
                                        },
                                        Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                                        {
                                            Id = item.TenantId,
                                            Name = item.TenantName,
                                            Logo = item.TenantLogo,
                                            CountryId = item.TenantCountryId,
                                            CurrencySymbol = item.CurrencySymbol,
                                            CategoryId = item.TenantCategoryId,
                                            CategoryName = item.TenantCategoryName,
                                            Type = item.TenantType,
                                            RelevanceScore = item.TenantScore,
                                            NearestBranchId = Guid.Empty,
                                            NearestBranchName = "",
                                            NearestBranchLatitude = null,
                                            NearestBranchLongitude = null,
                                        },
                                        Branch = new BasicBranchData
                                        {
                                            Id = item.BranchId,
                                            Name = item.BranchName,
                                            DescriptiveAddress = item.BranchDescriptiveAddress,
                                            InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                            Latitude = item.BranchLatitude,
                                            Longitude = item.BranchLongitude,
                                            Distance = null,//In this case there is no way to define distance
                                            CityId = item.BranchCityId,
                                            StateId = item.BranchStateId,
                                            Enabled = false
                                        },
                                        //When selector is tenant, there is no preference data
                                        Preference = new DTO.Entities.Misc.Preference.BasicPreferenceData
                                        {
                                            Id = Guid.Empty,
                                            Name = "",
                                            Icon = "",
                                            RelevanceScore = -1
                                        },
                                        ExactLocationBased = false
                                    };

                                    cashbackIncentives.Add(incentiveData);
                                }
                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //TODO ERROR HANDLING
            }

            return cashbackIncentives;
        }*/

        /*
        /// <summary>
        /// Retrieve cashback incentive flattened data about all the offers that are potencially
        /// available in a given state. At this point there is no certainity that 
        /// the incentive actually is available in that state, but that it belongs to a 
        /// tenant that has a branch in that state. Also includes data to the corresponding preference
        /// It retrieve exclusively incentive that are active, released, unexpired and not prizes
        /// for active and released tenants and active branches
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private List<FlattenedCashbackIncentiveData> GetCashbackIncentiveDetails(Guid offerId, string userId, DateTime dateTime)
        {
            List<FlattenedCashbackIncentiveData> cashbackIncentives = null;

            try
            {
                var query = from x in this._businessObjects.Context.GetCashbackIncentiveDetailsWithLocations(offerId, userId, dateTime)
                            where (x.AvailableQuantity == -1 || x.AvailableQuantity > 0)
                            select x;

                if (query != null)
                {
                    FlattenedCashbackIncentiveData incentiveData;

                    cashbackIncentives = new List<FlattenedCashbackIncentiveData>();

                    foreach (TEMPCashbackIncentivesPreferenceBranch item in query)
                    {
                        if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                        {
                            incentiveData = new FlattenedCashbackIncentiveData
                            {
                                SelectorType = ContentFilters.Category,
                                CashbackIncentive = new CashbackIncentive
                                {
                                    Id = item.Id,
                                    TenantId = item.TenantId,
                                    Type = item.Type,
                                    TypeName = GetTypeName(item.Type),
                                    EarningType = item.EarningType,
                                    EarningTypeName = GetEarningTypeName(item.EarningType),
                                    DealType = item.DealType,
                                    DealTypeName = GetDealTypeName(item.DealType),
                                    CombineType = item.CombineType,
                                    CombineTypeName = GetCombineTypeName(item.CombineType),
                                    UnitValue = item.UnitValue,
                                    PreviousUnitValue = item.PreviousUnitValue,
                                    MinPurchasedAmount = item.MinPurchasedAmount,
                                    PurchasedAmountBlock = item.PurchasedAmountBlock,
                                    MaxValue = item.MaxValue,
                                    AvailableQuantity = item.AvailableQuantity,
                                    Name = item.Name,
                                    Description = item.Description,
                                    RegexToMatchInReceipt = item.RegexToMatchInReceipt,
                                    Keywords = item.Keywords,
                                    RequiresReferenceCode = item.RequiresReferenceCode,
                                    IsActive = item.IsActive,
                                    IsSponsored = item.IsSponsored,
                                    CreationDate = item.CreationDate,
                                    ReleaseDate = item.ReleaseDate,
                                    ExpirationDate = item.ExpirationDate,
                                    ValidWeekDays = item.ValidWeekDays,
                                    ValidMonthDays = item.ValidMonthDays,
                                    ValidHours = item.ValidHours,
                                    MaxUsagePerUser = item.MaxUsagesPerUser,
                                    UsageCount = item.UsageCount,
                                    RelevanceRate = item.RelevanceRate,
                                    GeoSegmentationType = item.GeoSegmentationType,
                                    GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                                    Rules = item.Rules ?? Resources.NoRulesAvailable,
                                    Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                    ClaimInstructions = item.ClaimInstructions ?? Resources.DefaultIncentiveClaimInstructions,
                                    OneTimeUsagePerUser = item.OneTimeUsagePerUser,
                                    MinPurchasesCountToUse = item.MinPurchasesCountToUse,
                                    MinPurchasesHoursTimeoutToUse = item.MinPurchasesHoursTimeoutToUse,
                                    MaxPurchasesDaysTimeoutToUse = item.MaxPurchasesDaysTimeoutToUse,
                                    CommissionFeeType = item.CommissionFeeType,
                                    CommissionFeeTypeName = GetCommissionTypeName(item.CommissionFeeType),
                                    CommissionFeeValue = item.CommissionFeeValue,
                                    MinCommissionFeeAmount = item.MinCommissionFeeAmount
                                },
                                Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                                {
                                    Id = item.TenantId,
                                    Name = item.TenantName,
                                    Logo = item.TenantLogo,
                                    CountryId = item.TenantCountryId,
                                    CurrencySymbol = item.CurrencySymbol,
                                    CategoryId = item.TenantCountryId,
                                    CategoryName = "",
                                    Type = item.TenantType,
                                    RelevanceScore = null,//When its selector is category, there is no tenant score
                                    NearestBranchId = Guid.Empty,
                                    NearestBranchName = "",
                                    NearestBranchLatitude = null,
                                    NearestBranchLongitude = null,
                                },
                                Branch = new BasicBranchData
                                {
                                    Id = item.BranchId,
                                    Name = item.BranchName,
                                    InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                    DescriptiveAddress = item.BranchDescriptiveAddress,
                                    Latitude = item.BranchLatitude,
                                    Longitude = item.BranchLongitude,
                                    Distance = null,//In this case there is no way to define distance
                                    CityId = item.BranchCityId,
                                    StateId = item.BranchStateId,
                                    Enabled = false
                                },
                                Preference = new DTO.Entities.Misc.Preference.BasicPreferenceData
                                {
                                    Id = item.PreferenceId,
                                    Name = item.PreferenceName,
                                    Icon = item.PreferenceIcon,
                                    RelevanceScore = item.PreferenceScore
                                },
                                ExactLocationBased = false
                            };

                            cashbackIncentives.Add(incentiveData);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                cashbackIncentives = null;
                //TODO ERROR HANDLING
            }

            return cashbackIncentives;
        }
        */

        private void BuildFullOfferList(ref List<FullCashbackIncentiveData> offersData, ref List<CashbackIncentiveDataWithBranches> enabledIncentives, bool includeBranchList, bool includeNearestBranch)
        {
            CashbackIncentiveDataWithBranches currentIncentive;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            BasicBranchData nearestBranch;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            int? branchGroupsCount;

            List<BasicBranchData> enabledBranches = null;

            for (int i = 0; i < enabledIncentives.Count; i++)
            {
                currentIncentive = enabledIncentives[i];

                if (currentIncentive.CashbackIncentive.GeoSegmentationType != GeoSegmentationTypes.Country)
                {
                    enabledLocations = this._businessObjects.ContentLocations.Gets(enabledIncentives[i].CashbackIncentive.Id);
                }
                else
                {
                    enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                }

                availableBranches = currentIncentive.Branches.GroupBy(x => x.Id)
                                                                       .Select(grp => grp.First())
                                                                       .ToList();
                enabledBranches = new List<BasicBranchData>();

                if (currentIncentive.CashbackIncentive.DealType == DealTypes.InStore || currentIncentive.CashbackIncentive.DealType == DealTypes.Catalog)
                {
                    //Depending on the geosegmentation the offer has, we will group the incentives by either state or city
                    switch (currentIncentive.CashbackIncentive.GeoSegmentationType)
                    {
                        case GeoSegmentationTypes.State:
                            //Will group by state
                            branchesGrouped = currentIncentive.Branches.GroupBy(x => x.StateId);
                            branchGroupsCount = branchesGrouped?.Count();


                            if (branchGroupsCount != null && branchGroupsCount > 0)
                            {
                                for (int j = 0; j < enabledLocations.Count; j++)
                                {
                                    for (int k = 0; k < branchGroupsCount; k++)
                                    {
                                        //If the location has the same geosegmentation and has the same location reference Id
                                        if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                        {
                                            enabledBranches.AddRange(availableBranches.Where(x => x.StateId == enabledLocations[j].Value).ToList());
                                        }
                                    }
                                }
                            }

                            break;
                        case GeoSegmentationTypes.City:
                            //Will group by state
                            branchesGrouped = currentIncentive.Branches.GroupBy(x => x.CityId);
                            branchGroupsCount = branchesGrouped?.Count();


                            if (branchGroupsCount != null && branchGroupsCount > 0)
                            {
                                for (int j = 0; j < enabledLocations.Count; j++)
                                {
                                    for (int k = 0; k < branchGroupsCount; k++)
                                    {
                                        //If the location has the same geosegmentation and has the same location reference Id
                                        if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                        {
                                            enabledBranches.AddRange(availableBranches.Where(x => x.CityId == enabledLocations[j].Value).ToList());
                                        }
                                    }
                                }
                            }

                            break;
                        case GeoSegmentationTypes.Country:
                            //This means the incentive is available to all the branches in the country
                            enabledBranches = availableBranches;

                            break;
                    }
                }


                if (enabledBranches?.Count > 0)
                {

                    enabledBranches = enabledBranches.OrderBy(x => x.Distance).ToList();

                    nearestBranch = enabledBranches.ElementAt(0);

                    if (includeNearestBranch && nearestBranch != null)
                    {
                        currentIncentive.Tenant.NearestBranchId = nearestBranch.Id;
                        currentIncentive.Tenant.NearestBranchName = nearestBranch.Name;
                        currentIncentive.Tenant.NearestBranchLatitude = nearestBranch.Latitude;
                        currentIncentive.Tenant.NearestBranchLongitude = nearestBranch.Longitude;
                        currentIncentive.Tenant.NearesBranchDistance = nearestBranch.Distance;
                    }

                    if (includeBranchList)
                    {
                        offersData.Add(new FullCashbackIncentiveData
                        {
                            SelectorType = currentIncentive.SelectorType,
                            CashbackIncentive = currentIncentive.CashbackIncentive,
                            Tenant = currentIncentive.Tenant,
                            Branches = enabledBranches,
                            Preference = currentIncentive.Preference
                        }
                        );
                    }
                    else
                    {
                        offersData.Add(new FullCashbackIncentiveData
                        {
                            SelectorType = currentIncentive.SelectorType,
                            CashbackIncentive = currentIncentive.CashbackIncentive,
                            Tenant = currentIncentive.Tenant,
                            Branches = new List<BasicBranchData>(),
                            Preference = currentIncentive.Preference
                        }
                        );
                    }

                }

            }
        }

        private void BuildFullOfferList(ref List<FullCashbackIncentiveData> offersData, ref List<CashbackIncentiveDataWithBranches> enabledIncentives, bool includeBranchList)
        {
            CashbackIncentiveDataWithBranches currentIncentive;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            int? branchGroupsCount;

            if (!includeBranchList)
            {
                bool locationMatch = false;

                for (int i = 0; i < enabledIncentives.Count; i++)
                {
                    currentIncentive = enabledIncentives[i];
                    locationMatch = false;

                    //This means it's enabled for the complete country, then it's enabled for all the potential branches
                    if (currentIncentive.CashbackIncentive.GeoSegmentationType != GeoSegmentationTypes.Country)
                    {
                        if (currentIncentive.CashbackIncentive.GeoSegmentationType != GeoSegmentationTypes.Country)
                        {
                            enabledLocations = this._businessObjects.ContentLocations.Gets(enabledIncentives[i].CashbackIncentive.Id);
                        }
                        else
                        {
                            enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                        }

                        availableBranches = currentIncentive.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();

                        if (enabledLocations?.Count > 0)
                        {

                            //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                            switch (currentIncentive.CashbackIncentive.GeoSegmentationType)
                            {
                                case GeoSegmentationTypes.State:
                                    //Will group by state
                                    branchesGrouped = currentIncentive.Branches.GroupBy(x => x.StateId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    locationMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        locationMatch = false;
                                    }

                                    break;
                                case GeoSegmentationTypes.City:
                                    //Will group by state
                                    branchesGrouped = currentIncentive.Branches.GroupBy(x => x.CityId);
                                    branchGroupsCount = branchesGrouped?.Count();


                                    if (branchGroupsCount != null && branchGroupsCount > 0)
                                    {
                                        for (int j = 0; j < enabledLocations.Count && !locationMatch; j++)
                                        {
                                            for (int k = 0; k < branchGroupsCount && !locationMatch; k++)
                                            {
                                                //If the location has the same geosegmentation and has the same location reference Id
                                                if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                                {
                                                    locationMatch = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        locationMatch = false;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            //If no enabled locations needs to be removed
                            locationMatch = false;
                        }
                    }
                    else
                    {
                        //If the offer is enabled to be avaiable to the complete country, then it's valid
                        locationMatch = true;
                    }

                    if (locationMatch)
                    {

                        offersData.Add(new FullCashbackIncentiveData
                        {
                            SelectorType = currentIncentive.SelectorType,
                            CashbackIncentive = currentIncentive.CashbackIncentive,
                            Preference = currentIncentive.Preference,
                            Tenant = currentIncentive.Tenant,
                            Branches = new List<BasicBranchData>()
                        }
                        );
                    }

                }
            }
            else
            {
                List<BasicBranchData> enabledBranches = null;

                for (int i = 0; i < enabledIncentives.Count; i++)
                {
                    currentIncentive = enabledIncentives[i];

                    if (currentIncentive.CashbackIncentive.GeoSegmentationType != GeoSegmentationTypes.Country)
                    {
                        enabledLocations = this._businessObjects.ContentLocations.Gets(enabledIncentives[i].CashbackIncentive.Id);
                    }
                    else
                    {
                        enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                    }

                    availableBranches = currentIncentive.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();
                    enabledBranches = new List<BasicBranchData>();

                    if (currentIncentive.CashbackIncentive.DealType == DealTypes.InStore || currentIncentive.CashbackIncentive.DealType == DealTypes.Catalog)
                    {
                        //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                        switch (currentIncentive.CashbackIncentive.GeoSegmentationType)
                        {
                            case GeoSegmentationTypes.State:
                                //Will group by state
                                branchesGrouped = currentIncentive.Branches.GroupBy(x => x.StateId);
                                branchGroupsCount = branchesGrouped?.Count();


                                if (branchGroupsCount != null && branchGroupsCount > 0)
                                {
                                    for (int j = 0; j < enabledLocations.Count; j++)
                                    {
                                        for (int k = 0; k < branchGroupsCount; k++)
                                        {
                                            //If the location has the same geosegmentation and has the same location reference Id
                                            if (enabledLocations[j].Key == GeoSegmentationTypes.State && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                            {
                                                enabledBranches.AddRange(availableBranches.Where(x => x.StateId == enabledLocations[j].Value).ToList());
                                            }
                                        }
                                    }
                                }

                                break;
                            case GeoSegmentationTypes.City:
                                //Will group by state
                                branchesGrouped = currentIncentive.Branches.GroupBy(x => x.CityId);
                                branchGroupsCount = branchesGrouped?.Count();


                                if (branchGroupsCount != null && branchGroupsCount > 0)
                                {
                                    for (int j = 0; j < enabledLocations.Count; j++)
                                    {
                                        for (int k = 0; k < branchGroupsCount; k++)
                                        {
                                            //If the location has the same geosegmentation and has the same location reference Id
                                            if (enabledLocations[j].Key == GeoSegmentationTypes.City && enabledLocations[j].Value == branchesGrouped.ElementAt(k).Key)
                                            {
                                                enabledBranches.AddRange(availableBranches.Where(x => x.CityId == enabledLocations[j].Value).ToList());
                                            }
                                        }
                                    }
                                }

                                break;
                            case GeoSegmentationTypes.Country:
                                //This means the offer is enabled to all the existing branches
                                enabledBranches = availableBranches;

                                break;
                        }
                    }


                    offersData.Add(new FullCashbackIncentiveData
                    {
                        SelectorType = currentIncentive.SelectorType,
                        CashbackIncentive = currentIncentive.CashbackIncentive,
                        Tenant = currentIncentive.Tenant,
                        Branches = enabledBranches,
                        Preference = currentIncentive.Preference
                    }
                    );

                }
            }
        }

        public List<FullCashbackIncentiveData> GetEnabledCashbackIncentivesByRegionWithLocation(Guid countryId, Guid stateId, int contentSegmentationType, string userId, decimal latitude, decimal longitude, double radius, DateTime dateTime, int selectorType, bool includeBranchList, bool includeNearestBranch)
        {
            List<FullCashbackIncentiveData> incentivesData = new List<FullCashbackIncentiveData>();

            try
            {
                //For large countries segmentation is by state, for small ones by country
                List<FlattenedCashbackIncentiveData> flattenedIncentives = this.GetCashbackIncentivesDataByRegionWithLocation(countryId, stateId, contentSegmentationType, userId, latitude, longitude, radius, dateTime, selectorType);

                if (flattenedIncentives?.Count > 0)
                {
                    CashbackIncentiveDataWithBranches currentIncentive;
                    IEnumerable<IGrouping<Guid, FlattenedCashbackIncentiveData>> groupedByIncentiveId = flattenedIncentives.GroupBy(x => x.CashbackIncentive.Id);
                    List<CashbackIncentiveDataWithBranches> enabledIncentives = new List<CashbackIncentiveDataWithBranches>();
                    FlattenedCashbackIncentiveData[] incentivesGroup = null;

                    foreach (IGrouping<Guid, FlattenedCashbackIncentiveData> incentiveDataGroup in groupedByIncentiveId)
                    {
                        incentivesGroup = incentiveDataGroup.ToArray();

                        currentIncentive = new CashbackIncentiveDataWithBranches
                        {
                            SelectorType = incentivesGroup[0].SelectorType,
                            CashbackIncentive = incentivesGroup[0].CashbackIncentive,
                            Preference = incentivesGroup[0].Preference,
                            Tenant = incentivesGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            ExactLocationBased = incentivesGroup[0].ExactLocationBased
                        };

                        for (int i = 0; i < incentivesGroup.Length; ++i)
                        {
                            currentIncentive.Branches.Add(incentivesGroup[i].Branch);
                        }

                        enabledIncentives.Add(currentIncentive);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled


                    this.BuildFullOfferList(ref incentivesData, ref enabledIncentives, includeBranchList, includeNearestBranch);


                }

            }
            catch (Exception e)
            {
                incentivesData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return incentivesData;

        }

        public List<FullCashbackIncentiveData> GetEnabledCashbackIncentivesByRegion(Guid stateId, Guid countryId, int contentSegmentationType, string userId, DateTime dateTime, int selectorType, bool includeBranchList)
        {
            List<FullCashbackIncentiveData> incentivesData = new List<FullCashbackIncentiveData>();

            try
            {
                //For large countries segmentation is by state, for small ones by country
                List<FlattenedCashbackIncentiveData> flattenedIncentives = this.GetCashbackIncentivesDataByRegion(stateId, countryId, contentSegmentationType, userId, dateTime, selectorType);

                if (flattenedIncentives?.Count > 0)
                {
                    CashbackIncentiveDataWithBranches currentIncentive;
                    IEnumerable<IGrouping<Guid, FlattenedCashbackIncentiveData>> groupedByIncentiveId = flattenedIncentives.GroupBy(x => x.CashbackIncentive.Id);
                    List<CashbackIncentiveDataWithBranches> enabledIncentives = new List<CashbackIncentiveDataWithBranches>();
                    FlattenedCashbackIncentiveData[] incentivesGroup = null;

                    foreach (IGrouping<Guid, FlattenedCashbackIncentiveData> incentiveDataGroup in groupedByIncentiveId)
                    {
                        incentivesGroup = incentiveDataGroup.ToArray();

                        currentIncentive = new CashbackIncentiveDataWithBranches
                        {
                            CashbackIncentive = incentivesGroup[0].CashbackIncentive,
                            Preference = incentivesGroup[0].Preference,
                            Tenant = incentivesGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            ExactLocationBased = incentivesGroup[0].ExactLocationBased
                        };

                        for (int i = 0; i < incentivesGroup.Length; ++i)
                        {
                            currentIncentive.Branches.Add(incentivesGroup[i].Branch);
                        }

                        enabledIncentives.Add(currentIncentive);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled

                    this.BuildFullOfferList(ref incentivesData, ref enabledIncentives, includeBranchList);

                }

            }
            catch (Exception e)
            {
                incentivesData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return incentivesData;

        }

        public List<FullCashbackIncentiveData> GetEnabledIncentivesByRegionForReference(Guid stateId, Guid countryId, int contentSegmentationType, string userId, DateTime dateTime, int referenceType, Guid referenceId, bool includeBranchList)
        {
            List<FullCashbackIncentiveData> offersData = new List<FullCashbackIncentiveData>();

            try
            {
                List<FlattenedCashbackIncentiveData> flattenedIncentives = null; // this.GetCashbackIncentivesDataByRegionForReference(stateId, countryId, contentSegmentationType, userId, dateTime, referenceType, referenceId);

                if (flattenedIncentives?.Count > 0)
                {
                    CashbackIncentiveDataWithBranches currentIncentive;
                    IEnumerable<IGrouping<Guid, FlattenedCashbackIncentiveData>> groupedByIncentiveId = flattenedIncentives.GroupBy(x => x.CashbackIncentive.Id);
                    List<CashbackIncentiveDataWithBranches> enabledIncentives = new List<CashbackIncentiveDataWithBranches>();
                    FlattenedCashbackIncentiveData[] incentivesGroup = null;

                    foreach (IGrouping<Guid, FlattenedCashbackIncentiveData> incentiveDataGroup in groupedByIncentiveId)
                    {
                        incentivesGroup = incentiveDataGroup.ToArray();

                        currentIncentive = new CashbackIncentiveDataWithBranches
                        {
                            CashbackIncentive = incentivesGroup[0].CashbackIncentive,
                            Preference = incentivesGroup[0].Preference,
                            Tenant = incentivesGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            ExactLocationBased = incentivesGroup[0].ExactLocationBased
                        };

                        for (int i = 0; i < incentivesGroup.Length; ++i)
                        {
                            currentIncentive.Branches.Add(incentivesGroup[i].Branch);
                        }

                        enabledIncentives.Add(currentIncentive);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled

                    this.BuildFullOfferList(ref offersData, ref enabledIncentives, includeBranchList);
                }

            }
            catch (Exception e)
            {
                offersData = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offersData;

        }

        public FullCashbackIncentiveData GetCashbackIncentiveData(Guid id, string userId, bool includeBranchList, int purposeType, DateTime dateTime)
        {
            FullCashbackIncentiveData cashbackIncentive = null;

            try
            {
                List<FlattenedCashbackIncentiveData> flattenedIncentives = null;// this.GetOfferDetails(id, userId, purposeType, dateTime);

                if (flattenedIncentives?.Count > 0)
                {
                    CashbackIncentiveDataWithBranches currentIncentive;
                    IEnumerable<IGrouping<Guid, FlattenedCashbackIncentiveData>> groupedByIncentiveId = flattenedIncentives.GroupBy(x => x.CashbackIncentive.Id);
                    List<CashbackIncentiveDataWithBranches> enabledIncentives = new List<CashbackIncentiveDataWithBranches>();
                    FlattenedCashbackIncentiveData[] incentivesGroup = null;

                    foreach (IGrouping<Guid, FlattenedCashbackIncentiveData> offerDataGroup in groupedByIncentiveId)
                    {
                        incentivesGroup = offerDataGroup.ToArray();

                        currentIncentive = new CashbackIncentiveDataWithBranches
                        {
                            CashbackIncentive = incentivesGroup[0].CashbackIncentive,
                            Preference = incentivesGroup[0].Preference,
                            Tenant = incentivesGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            ExactLocationBased = incentivesGroup[0].ExactLocationBased
                        };

                        for (int i = 0; i < incentivesGroup.Length; ++i)
                        {
                            currentIncentive.Branches.Add(incentivesGroup[i].Branch);
                        }

                        enabledIncentives.Add(currentIncentive);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled
                    List<FullCashbackIncentiveData> incentivesData = new List<FullCashbackIncentiveData>();

                    this.BuildFullOfferList(ref incentivesData, ref enabledIncentives, includeBranchList);

                    if (incentivesData?.Count > 0)
                    {
                        cashbackIncentive = incentivesData.ElementAt(0);
                    }
                }
            }
            catch (Exception e)
            {
                cashbackIncentive = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return cashbackIncentive;
        }


        #endregion

        #region INCENTIVE METRICS

        public int? GetCashbackIncentivesCountByDateRange(Guid refId, int refType, DateTime minDate, DateTime maxDate)
        {
            int? count = 0;

            try
            {

                switch (refType)
                {
                    case SearchableObjectTypes.Category:
                        //PENDING
                        break;
                    case SearchableObjectTypes.Commerce:
                        count = this._businessObjects.StoredProcsHandler.GetCashbackIncentivesCountForCommerceByDateRange(refId, minDate, maxDate);
                        break;
                }

            }
            catch (Exception e)
            {
                count = -1;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return count;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public CashbackIncentiveManager(BusinessObjects businessObjects)
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
