using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.ObjectState.POCO;
using YOY.DTO.Entities.Misc.Offer;
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
    public class OfferManager
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


        #region OFFER

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

        private string GetPurposeTypeName(int purposeType)
        {
            string typeName = purposeType switch
            {
                OfferPurposeTypes.Reward => Resources.Reward,
                OfferPurposeTypes.Deal => Resources.Deal,
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

        private string GetExtraBonusTypeName(int extraBonusType)
        {
            string extraBonusTypeName = extraBonusType switch
            {
                ExtraBonusTypes.None => Resources.None,
                ExtraBonusTypes.Percentage => Resources.Percentage,
                ExtraBonusTypes.FixedAmount => Resources.FixedAmount,
                _ => "--",
            };
            return extraBonusTypeName;
        }

        private string GetRewardTypeName(int rewardType)
        {
            string typeName = rewardType switch
            {
                RewardTypes.Deal => Resources.Deal,
                RewardTypes.Prize => Resources.Prize,
                RewardTypes.Game => Resources.Game,
                RewardTypes.Gift => Resources.Gift,
                _ => "--",
            };
            return typeName;
        }

        private string GetOfferTypeName(int offerType)
        {
            string typeName = offerType switch
            {
                OfferTypes.Reward => Resources.Reward,
                OfferTypes.Offer => Resources.Offer,
                OfferTypes.Coupon => Resources.Coupon,
                OfferTypes.CashbackIncentive => Resources.CashbackIncentive,
                OfferTypes.Special => Resources.SpecialProduct,
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

        private string GetBroadcastingScheduleTypeName(int scheduleType)
        {
            string typeName = scheduleType switch
            {
                ScheduleTypes.NoSchedule => Resources.None,
                ScheduleTypes.Continously => Resources.Continously,
                ScheduleTypes.Segmented => Resources.Segmented,
                _ => "--",
            };
            return typeName;
        }

        private string GetBroadcastingTimerTypeName(int timerType)
        {
            string typeName = timerType switch
            {
                TimerTypes.NoTimer => Resources.None,
                TimerTypes.CountDown => Resources.Countdown,
                TimerTypes.FastCountDown => Resources.FastCountdown,
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
                _ => "--",
            };
            return typeName;
        }


        /// <summary>
        /// Get all offers of a specific type
        /// </summary>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <returns></returns>
        public List<Offer> Gets(int expiredState, int activeState, int releaseState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<Offer> offers = new List<Offer>();

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
                                        query = (from x in context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpoffersView
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
                                        query = (from x in context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
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
                                query = (from x in context.OltpoffersView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Active:
                                query = (from x in context.OltpoffersView
                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Inactive:
                                query = (from x in context.OltpoffersView
                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
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
                                        query = (from x in context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate.Date > dateTime.Date
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate.Date >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                    Offer offer = null;
                    foreach (OltpoffersView item in query)
                    {

                        offer = new Offer
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MainCategoryId = item.MainCategoryId,
                            MainCategoryName = item.CategoryName,
                            OfferType = item.OfferType,
                            OfferTypeName = GetOfferTypeName(item.OfferType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            RewardType = item.RewardType,
                            RewardTypeName = GetRewardTypeName(item.RewardType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            Name = item.Name,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            Keywords = item.Keywords,
                            Description = item.Description,
                            Code = item.Code ?? "",
                            CodeImg = item.CodeImg,
                            MinsToUnlock = item.MinsToUnlock,
                            IsActive = item.IsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.HasPreferences,
                            AvailableQuantity = item.AvailableQuantity,
                            OneTimeRedemption = item.OneTimeRedemption,
                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                            ClaimLocation = item.ClaimLocation,
                            Value = item.Value,
                            RegularValue = item.RegularValue,
                            ExtraBonus = item.ExtraBonus,
                            ExtraBonusType = item.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                            MinIncentive = item.MinIncentive,
                            MaxIncentive = item.MaxIncentive,
                            IncentiveVariationType = item.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = item.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = item.RedeemCount,
                            ClaimCount = item.ClaimCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            TargettingParams = item.TargettingParams,
                            DisplayImgId = item.DisplayImageId,
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = item.LastBroadcastingUsage,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                            BroadcastingScheduleType = item.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            BroadcastingTitle = item.BroadcastingTitle,
                            BroadcastingMsg = item.BroadcastingMsg,
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        if (offer.HasUniqueCodes)
                        {
                            offer.Code = Resources.UniqueCodes;
                        }

                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, dateTime);

                        offers.Add(offer);

                    }
                }
                else
                {
                    offers = null;
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Returns all the promos in a range range
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<Offer> Gets(decimal minPrice, decimal maxPrice, int activeState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<Offer> offers = new List<Offer>();

            try
            {

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.Value >= minPrice && x.Value <= maxPrice && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.Value >= minPrice && x.Value <= maxPrice && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.Value >= minPrice && x.Value <= maxPrice && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                if (query != null)
                {
                    Offer offer = null;
                    foreach (OltpoffersView item in query)
                    {
                        offer = new Offer
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MainCategoryId = item.MainCategoryId,
                            MainCategoryName = item.CategoryName,
                            OfferType = item.OfferType,
                            OfferTypeName = GetOfferTypeName(item.OfferType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            RewardType = item.RewardType,
                            RewardTypeName = GetRewardTypeName(item.RewardType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            Name = item.Name,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            Keywords = item.Keywords,
                            Description = item.Description,
                            Code = item.Code ?? "",
                            CodeImg = item.CodeImg,
                            MinsToUnlock = item.MinsToUnlock,
                            IsActive = item.IsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.HasPreferences,
                            AvailableQuantity = item.AvailableQuantity,
                            OneTimeRedemption = item.OneTimeRedemption,
                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                            ClaimLocation = item.ClaimLocation,
                            Value = item.Value,
                            RegularValue = item.RegularValue,
                            ExtraBonus = item.ExtraBonus,
                            ExtraBonusType = item.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                            MinIncentive = item.MinIncentive,
                            MaxIncentive = item.MaxIncentive,
                            IncentiveVariationType = item.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = item.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = item.RedeemCount,
                            ClaimCount = item.ClaimCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            TargettingParams = item.TargettingParams,
                            DisplayImgId = item.DisplayImageId,
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = item.LastBroadcastingUsage,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                            BroadcastingScheduleType = item.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            BroadcastingTitle = item.BroadcastingTitle,
                            BroadcastingMsg = item.BroadcastingMsg,
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, dateTime);

                        offers.Add(offer);
                    }
                }
            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }


        /// <summary>
        /// Get all promotions of a specific type
        /// </summary>
        /// <param name="dealType"></param>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <returns></returns>
        public List<Offer> Gets(int dealType, int expiredState, int activeState, int releaseState, DateTime dateTime, bool filterByTenant, int pageSize, int pageNumber)
        {
            List<Offer> offers = new List<Offer>();

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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.ReleaseDate > dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.ReleaseDate > dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.ReleaseDate > dateTime
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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.ExpirationDate < dateTime
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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.ExpirationDate < dateTime
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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.ExpirationDate < dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.ExpirationDate >= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.ExpirationDate >= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.ExpirationDate >= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate.Date <= dateTime.Date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate.Date <= dateTime.Date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate.Date <= dateTime.Date
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.ExpirationDate >= dateTime && x.ReleaseDate.Date <= dateTime.Date
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                    Offer offer = null;
                    foreach (OltpoffersView item in query)
                    {
                        offer = new Offer
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MainCategoryId = item.MainCategoryId,
                            MainCategoryName = item.CategoryName,
                            OfferType = item.OfferType,
                            OfferTypeName = GetOfferTypeName(item.OfferType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            RewardType = item.RewardType,
                            RewardTypeName = GetRewardTypeName(item.RewardType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            Name = item.Name,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            Keywords = item.Keywords,
                            Description = item.Description,
                            Code = item.Code ?? "",
                            CodeImg = item.CodeImg,
                            MinsToUnlock = item.MinsToUnlock,
                            IsActive = item.IsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.HasPreferences,
                            AvailableQuantity = item.AvailableQuantity,
                            OneTimeRedemption = item.OneTimeRedemption,
                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                            ClaimLocation = item.ClaimLocation,
                            Value = item.Value,
                            RegularValue = item.RegularValue,
                            ExtraBonus = item.ExtraBonus,
                            ExtraBonusType = item.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                            MinIncentive = item.MinIncentive,
                            MaxIncentive = item.MaxIncentive,
                            IncentiveVariationType = item.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = item.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = item.RedeemCount,
                            ClaimCount = item.ClaimCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            TargettingParams = item.TargettingParams,
                            DisplayImgId = item.DisplayImageId,
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = item.LastBroadcastingUsage,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                            BroadcastingScheduleType = item.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            BroadcastingTitle = item.BroadcastingTitle,
                            BroadcastingMsg = item.BroadcastingMsg,
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, dateTime);

                        offers.Add(offer);

                    }
                }
                else
                {
                    offers = null;
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Get all promotions of a specific deal type, purpose type and  for specific category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="dealType"></param>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <returns></returns>
        public List<Offer> Gets(Guid categoryId, int dealType, int expiredState, int activeState, int releaseState, DateTime dateTime, bool filterByTenant, int pageSize, int pageNumber)
        {
            List<Offer> offers = new List<Offer>();

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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.ReleaseDate > dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.ReleaseDate > dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }

                                        break;
                                }

                                break;
                        }

                        break;
                    case ExpiredStates.Expired://If product is expired makes no sense to evaluate releaseState
                        switch (activeState)
                        {
                            case ActiveStates.All:
                                if (filterByTenant)
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.MainCategoryId == categoryId && x.ExpirationDate < dateTime
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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.MainCategoryId == categoryId && x.ExpirationDate < dateTime
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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                }
                                else
                                {
                                    if (dealType != DealTypes.All)
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate < dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    }
                                    else
                                    {
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.MainCategoryId == categoryId && x.ExpirationDate < dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where x.IsActive && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
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
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                        }
                                        else
                                        {
                                            if (dealType != DealTypes.All)
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.DealType == dealType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                         orderby x.CreatedDate descending
                                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            }
                                            else
                                            {
                                                query = (from x in this._businessObjects.Context.OltpoffersView
                                                         where !x.IsActive && x.MainCategoryId == categoryId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                    Offer offer = null;
                    foreach (OltpoffersView item in query)
                    {
                        offer = new Offer
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MainCategoryId = item.MainCategoryId,
                            MainCategoryName = item.CategoryName,
                            OfferType = item.OfferType,
                            OfferTypeName = GetOfferTypeName(item.OfferType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            RewardType = item.RewardType,
                            RewardTypeName = GetRewardTypeName(item.RewardType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            Name = item.Name,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            Keywords = item.Keywords,
                            Description = item.Description,
                            Code = item.Code ?? "",
                            CodeImg = item.CodeImg,
                            MinsToUnlock = item.MinsToUnlock,
                            IsActive = item.IsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.HasPreferences,
                            AvailableQuantity = item.AvailableQuantity,
                            OneTimeRedemption = item.OneTimeRedemption,
                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                            ClaimLocation = item.ClaimLocation,
                            Value = item.Value,
                            RegularValue = item.RegularValue,
                            ExtraBonus = item.ExtraBonus,
                            ExtraBonusType = item.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                            MinIncentive = item.MinIncentive,
                            MaxIncentive = item.MaxIncentive,
                            IncentiveVariationType = item.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = item.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = item.RedeemCount,
                            ClaimCount = item.ClaimCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            TargettingParams = item.TargettingParams,
                            DisplayImgId = item.DisplayImageId,
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = item.LastBroadcastingUsage,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                            BroadcastingScheduleType = item.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            BroadcastingTitle = item.BroadcastingTitle,
                            BroadcastingMsg = item.BroadcastingMsg,
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, dateTime);

                        offers.Add(offer);

                    }
                }
                else
                {
                    offers = null;
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Get all offers from a tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <returns></returns>
        public List<Offer> Gets(Guid tenantId, int expiredState, int activeState, int releaseState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<Offer> offers = new List<Offer>();

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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId && x.IsActive
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId && x.IsActive && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId && x.IsActive && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == tenantId
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == tenantId && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == tenantId && x.ReleaseDate > dateTime
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
                                query = (from x in this._businessObjects.Context.OltpoffersView
                                         where x.TenantId == tenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpoffersView
                                         where x.IsActive && x.TenantId == tenantId && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpoffersView
                                         where !x.IsActive && x.TenantId == tenantId && x.ExpirationDate < dateTime
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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime && (DateTime)x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == tenantId && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                    Offer offer = null;
                    foreach (OltpoffersView item in query)
                    {
                        offer = new Offer
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MainCategoryId = item.MainCategoryId,
                            MainCategoryName = item.CategoryName,
                            OfferType = item.OfferType,
                            OfferTypeName = GetOfferTypeName(item.OfferType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            RewardType = item.RewardType,
                            RewardTypeName = GetRewardTypeName(item.RewardType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            Name = item.Name,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            Keywords = item.Keywords,
                            Description = item.Description,
                            Code = item.Code ?? "",
                            CodeImg = item.CodeImg,
                            MinsToUnlock = item.MinsToUnlock,
                            IsActive = item.IsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.HasPreferences,
                            AvailableQuantity = item.AvailableQuantity,
                            OneTimeRedemption = item.OneTimeRedemption,
                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                            ClaimLocation = item.ClaimLocation,
                            Value = item.Value,
                            RegularValue = item.RegularValue,
                            ExtraBonus = item.ExtraBonus,
                            ExtraBonusType = item.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                            MinIncentive = item.MinIncentive,
                            MaxIncentive = item.MaxIncentive,
                            IncentiveVariationType = item.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = item.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = item.RedeemCount,
                            ClaimCount = item.ClaimCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            TargettingParams = item.TargettingParams,
                            DisplayImgId = item.DisplayImageId,
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = item.LastBroadcastingUsage,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                            BroadcastingScheduleType = item.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            BroadcastingTitle = item.BroadcastingTitle,
                            BroadcastingMsg = item.BroadcastingMsg,
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, dateTime);

                        offers.Add(offer);

                    }
                }
                else
                {
                    offers = null;
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Returns all the offers with the max limit value, that haven't expired yet
        /// </summary>
        /// <param name="loyaltyPoints"></param>
        /// <param name="minMembershipLevel"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<Offer> Gets(decimal value, int activeState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<Offer> offers = new List<Offer>();

            try
            {

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.Value <= value && x.ExpirationDate >= dateTime.Date && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.Value <= value && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.Value <= value && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                if (query != null)
                {
                    Offer offer = null;
                    foreach (OltpoffersView item in query)
                    {
                        offer = new Offer
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MainCategoryId = item.MainCategoryId,
                            MainCategoryName = item.CategoryName,
                            OfferType = item.OfferType,
                            OfferTypeName = GetOfferTypeName(item.OfferType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            RewardType = item.RewardType,
                            RewardTypeName = GetRewardTypeName(item.RewardType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            Name = item.Name,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            Keywords = item.Keywords,
                            Description = item.Description,
                            Code = item.Code ?? "",
                            CodeImg = item.CodeImg,
                            MinsToUnlock = item.MinsToUnlock,
                            IsActive = item.IsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.HasPreferences,
                            AvailableQuantity = item.AvailableQuantity,
                            OneTimeRedemption = item.OneTimeRedemption,
                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                            ClaimLocation = item.ClaimLocation,
                            Value = item.Value,
                            RegularValue = item.RegularValue,
                            ExtraBonus = item.ExtraBonus,
                            ExtraBonusType = item.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                            MinIncentive = item.MinIncentive,
                            MaxIncentive = item.MaxIncentive,
                            IncentiveVariationType = item.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = item.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = item.RedeemCount,
                            ClaimCount = item.ClaimCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            TargettingParams = item.TargettingParams,
                            DisplayImgId = item.DisplayImageId,
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = item.LastBroadcastingUsage,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                            BroadcastingScheduleType = item.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            BroadcastingTitle = item.BroadcastingTitle,
                            BroadcastingMsg = item.BroadcastingMsg,
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, dateTime);


                        offers.Add(offer);
                    }
                }
            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }


        /// <summary>
        /// Returns all the rewards that requires for the membershiplevel and that requires
        ///membership level less or equal than the minMembershipLevel param, that haven't expired yet
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="activeState"></param>
        /// <returns></returns>
        public List<Offer> Gets(int activeState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<Offer> offers = new List<Offer>();

            try
            {

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Active:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                    case ActiveStates.Inactive:
                        query = (from x in this._businessObjects.Context.OltpoffersView
                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                 orderby x.CreatedDate descending
                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                        break;
                }


                if (query != null)
                {
                    Offer offer = null;
                    foreach (OltpoffersView item in query)
                    {
                        offer = new Offer
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MainCategoryId = item.MainCategoryId,
                            MainCategoryName = item.CategoryName,
                            OfferType = item.OfferType,
                            OfferTypeName = GetOfferTypeName(item.OfferType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            RewardType = item.RewardType,
                            RewardTypeName = GetRewardTypeName(item.RewardType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            Name = item.Name,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            Keywords = item.Keywords,
                            Description = item.Description,
                            Code = item.Code ?? "",
                            CodeImg = item.CodeImg,
                            MinsToUnlock = item.MinsToUnlock,
                            IsActive = item.IsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.HasPreferences,
                            AvailableQuantity = item.AvailableQuantity,
                            OneTimeRedemption = item.OneTimeRedemption,
                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                            ClaimLocation = item.ClaimLocation,
                            Value = item.Value,
                            RegularValue = item.RegularValue,
                            ExtraBonus = item.ExtraBonus,
                            ExtraBonusType = item.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                            MinIncentive = item.MinIncentive,
                            MaxIncentive = item.MaxIncentive,
                            IncentiveVariationType = item.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = item.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = item.RedeemCount,
                            ClaimCount = item.ClaimCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            TargettingParams = item.TargettingParams,
                            DisplayImgId = item.DisplayImageId,
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = item.LastBroadcastingUsage,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                            BroadcastingScheduleType = item.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            BroadcastingTitle = item.BroadcastingTitle,
                            BroadcastingMsg = item.BroadcastingMsg,
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, dateTime);

                        offers.Add(offer);
                    }
                }
            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }

        /// <summary>
        /// Get all rewards of a specific type
        /// </summary>
        /// <param name="rewardType"></param>
        /// <param name="expiredState"></param>
        /// <param name="activeState"></param>
        /// <param name="releaseState"></param>
        /// <returns></returns>
        public List<Offer> Gets(int rewardType, int purposeType, int expiredState, int activeState, int releaseState, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<Offer> offers = new List<Offer>();

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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ReleaseDate > dateTime
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
                                query = (from x in this._businessObjects.Context.OltpoffersView
                                         where x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Active:
                                query = (from x in this._businessObjects.Context.OltpoffersView
                                         where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate < dateTime
                                         orderby x.CreatedDate descending
                                         select x).Skip(pageSize * pageNumber).Take(pageSize);
                                break;
                            case ActiveStates.Inactive:
                                query = (from x in this._businessObjects.Context.OltpoffersView
                                         where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate < dateTime
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
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Active:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                }

                                break;
                            case ActiveStates.Inactive:
                                switch (releaseState)
                                {
                                    case ReleaseStates.All:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.Released:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime && x.ReleaseDate <= dateTime
                                                 orderby x.CreatedDate descending
                                                 select x).Skip(pageSize * pageNumber).Take(pageSize);
                                        break;
                                    case ReleaseStates.NotReleased:
                                        query = (from x in this._businessObjects.Context.OltpoffersView
                                                 where !x.IsActive && x.TenantId == this._businessObjects.Tenant.TenantId && x.RewardType == rewardType && x.PurposeType == purposeType && x.ExpirationDate >= dateTime && x.ReleaseDate > dateTime
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
                    Offer offer = null;
                    foreach (OltpoffersView item in query)
                    {
                        offer = new Offer
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            MainCategoryId = item.MainCategoryId,
                            MainCategoryName = item.CategoryName,
                            OfferType = item.OfferType,
                            OfferTypeName = GetOfferTypeName(item.OfferType),
                            DealType = item.DealType,
                            DealTypeName = GetDealTypeName(item.DealType),
                            RewardType = item.RewardType,
                            RewardTypeName = GetRewardTypeName(item.RewardType),
                            PurposeType = item.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                            GeoSegmentationType = item.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                            DisplayType = item.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                            Name = item.Name,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            Keywords = item.Keywords,
                            Description = item.Description,
                            Code = item.Code ?? "",
                            CodeImg = item.CodeImg,
                            MinsToUnlock = item.MinsToUnlock,
                            IsActive = item.IsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.HasPreferences,
                            AvailableQuantity = item.AvailableQuantity,
                            OneTimeRedemption = item.OneTimeRedemption,
                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                            ClaimLocation = item.ClaimLocation,
                            Value = item.Value,
                            RegularValue = item.RegularValue,
                            ExtraBonus = item.ExtraBonus,
                            ExtraBonusType = item.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                            MinIncentive = item.MinIncentive,
                            MaxIncentive = item.MaxIncentive,
                            IncentiveVariationType = item.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = item.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = item.RedeemCount,
                            ClaimCount = item.ClaimCount,
                            ReleaseDate = item.ReleaseDate,
                            ExpirationDate = item.ExpirationDate,
                            TargettingParams = item.TargettingParams,
                            DisplayImgId = item.DisplayImageId,
                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = item.LastBroadcastingUsage,
                            BroadcastingTimerType = item.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                            BroadcastingScheduleType = item.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                            BroadcastingTitle = item.BroadcastingTitle,
                            BroadcastingMsg = item.BroadcastingMsg,
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, dateTime);

                        offers.Add(offer);

                    }
                }
                else
                {
                    offers = null;
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }//GETS METHOD ENDS ----------------------------------------------------------------------------------------------------------------------------- //




        public Offer Get(Guid id, int offerType, bool filterByTenant)
        {
            Offer offer = null;

            try
            {

                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltpoffersView
                            where x.OfferType == offerType && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpoffersView
                            where x.OfferType == offerType && x.Id == id
                            select x;
                }


                foreach (OltpoffersView item in query)
                {
                    offer = new Offer
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        MainCategoryId = item.MainCategoryId,
                        MainCategoryName = item.CategoryName,
                        OfferType = item.OfferType,
                        OfferTypeName = GetOfferTypeName(item.OfferType),
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        RewardType = item.RewardType,
                        RewardTypeName = GetRewardTypeName(item.RewardType),
                        PurposeType = item.PurposeType,
                        PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                        GeoSegmentationType = item.GeoSegmentationType,
                        GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                        DisplayType = item.DisplayType,
                        DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                        Name = item.Name,
                        MainHint = item.MainHint,
                        ComplementaryHint = item.ComplementaryHint,
                        Keywords = item.Keywords,
                        Description = item.Description,
                        Code = item.Code ?? "",
                        CodeImg = item.CodeImg,
                        MinsToUnlock = item.MinsToUnlock,
                        IsActive = item.IsActive,
                        IsExclusive = item.IsExclusive,
                        IsSponsored = item.IsSponsored,
                        HasUniqueCodes = item.HasUniqueCodes,
                        HasPreferences = item.HasPreferences,
                        AvailableQuantity = item.AvailableQuantity,
                        OneTimeRedemption = item.OneTimeRedemption,
                        MaxClaimsPerUser = item.MaxClaimsPerUser,
                        MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                        PurchasesCountStartDate = item.PurchasesCountStartDate,
                        ClaimLocation = item.ClaimLocation,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        ExtraBonus = item.ExtraBonus,
                        ExtraBonusType = item.ExtraBonusType,
                        ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                        MinIncentive = item.MinIncentive,
                        MaxIncentive = item.MaxIncentive,
                        IncentiveVariationType = item.IncentiveVariationType,
                        //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                        IncentiveVariation = item.IncentiveVariation,
                        //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                        RedeemCount = item.RedeemCount,
                        ClaimCount = item.ClaimCount,
                        ReleaseDate = item.ReleaseDate,
                        ExpirationDate = item.ExpirationDate,
                        TargettingParams = item.TargettingParams,
                        DisplayImgId = item.DisplayImageId,
                        Rules = item.Rules ?? Resources.NoRulesAvailable,
                        Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                        ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                        LastBroadcastingUsage = item.LastBroadcastingUsage,
                        BroadcastingTimerType = item.BroadcastingTimerType,
                        BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                        BroadcastingScheduleType = item.BroadcastingScheduleType,
                        BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                        BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                        BroadcastingTitle = item.BroadcastingTitle,
                        BroadcastingMsg = item.BroadcastingMsg,
                        RelevanceRate = item.RelevanceRate,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    DateTime now = DateTime.UtcNow;

                    offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, now);
                }
            }
            catch (Exception e)
            {
                offer = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offer;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //        

        public Offer Get(Guid id, bool filterByTenant)
        {
            Offer offer = null;

            try
            {

                var query = (dynamic)null;

                if (filterByTenant)
                {
                    query = from x in this._businessObjects.Context.OltpoffersView
                            where x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;
                }
                else
                {
                    query = from x in this._businessObjects.Context.OltpoffersView
                            where x.Id == id
                            select x;
                }


                foreach (OltpoffersView item in query)
                {
                    offer = new Offer
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        MainCategoryId = item.MainCategoryId,
                        MainCategoryName = item.CategoryName,
                        OfferType = item.OfferType,
                        OfferTypeName = GetOfferTypeName(item.OfferType),
                        DealType = item.DealType,
                        DealTypeName = GetDealTypeName(item.DealType),
                        RewardType = item.RewardType,
                        RewardTypeName = GetRewardTypeName(item.RewardType),
                        PurposeType = item.PurposeType,
                        PurposeTypeName = this.GetPurposeTypeName(item.PurposeType),
                        GeoSegmentationType = item.GeoSegmentationType,
                        GeoSegmentationTypeName = GetGeoSegmentationTypeName(item.GeoSegmentationType),
                        DisplayType = item.DisplayType,
                        DisplayTypeName = GetDisplayTypeName(item.DisplayType),
                        Name = item.Name,
                        MainHint = item.MainHint,
                        ComplementaryHint = item.ComplementaryHint,
                        Keywords = item.Keywords,
                        Description = item.Description,
                        Code = item.Code ?? "",
                        CodeImg = item.CodeImg,
                        MinsToUnlock = item.MinsToUnlock,
                        IsActive = item.IsActive,
                        IsExclusive = item.IsExclusive,
                        IsSponsored = item.IsSponsored,
                        HasUniqueCodes = item.HasUniqueCodes,
                        HasPreferences = item.HasPreferences,
                        AvailableQuantity = item.AvailableQuantity,
                        OneTimeRedemption = item.OneTimeRedemption,
                        MaxClaimsPerUser = item.MaxClaimsPerUser,
                        MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                        PurchasesCountStartDate = item.PurchasesCountStartDate,
                        ClaimLocation = item.ClaimLocation,
                        Value = item.Value,
                        RegularValue = item.RegularValue,
                        ExtraBonus = item.ExtraBonus,
                        ExtraBonusType = item.ExtraBonusType,
                        ExtraBonusTypeName = this.GetExtraBonusTypeName(item.ExtraBonusType),
                        MinIncentive = item.MinIncentive,
                        MaxIncentive = item.MaxIncentive,
                        IncentiveVariationType = item.IncentiveVariationType,
                        //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                        IncentiveVariation = item.IncentiveVariation,
                        //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                        RedeemCount = item.RedeemCount,
                        ClaimCount = item.ClaimCount,
                        ReleaseDate = item.ReleaseDate,
                        ExpirationDate = item.ExpirationDate,
                        TargettingParams = item.TargettingParams,
                        DisplayImgId = item.DisplayImageId,
                        Rules = item.Rules ?? Resources.NoRulesAvailable,
                        Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                        ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                        LastBroadcastingUsage = item.LastBroadcastingUsage,
                        BroadcastingTimerType = item.BroadcastingTimerType,
                        BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(item.BroadcastingTimerType),
                        BroadcastingScheduleType = item.BroadcastingScheduleType,
                        BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(item.BroadcastingScheduleType),
                        BroadcastingMinsToRedeem = item.BroadcastingMinsToRedeem,
                        BroadcastingTitle = item.BroadcastingTitle,
                        BroadcastingMsg = item.BroadcastingMsg,
                        RelevanceRate = item.RelevanceRate,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate
                    };

                    DateTime now = DateTime.UtcNow;

                    offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, now);
                }
            }
            catch (Exception e)
            {
                offer = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offer;
        }//METHOD GET ENDS ------------------------------------------------------------------------------------------------------------------------------ //        




        /// <summary>
        /// Creates a new offer
        /// </summary>
        /// <param name="mainCategoryId"></param>
        /// <param name="offerType"></param>
        /// <param name="dealType"></param>
        /// <param name="rewardType"></param>
        /// <param name="purposeType"></param>
        /// <param name="geoSegmentationType"></param>
        /// <param name="displayType"></param>
        /// <param name="name"></param>
        /// <param name="mainHint"></param>
        /// <param name="complementaryHint"></param>
        /// <param name="keywords"></param>
        /// <param name="code"></param>
        /// <param name="codeImg"></param>
        /// <param name="description"></param>
        /// <param name="minsToUnlock"></param>
        /// <param name="isExclusive"></param>
        /// <param name="isSponsored"></param>
        /// <param name="hasUniqueCodes"></param>
        /// <param name="hasPreferences"></param>
        /// <param name="availableQuantity"></param>
        /// <param name="oneTimeRedemption"></param>
        /// <param name="maxClaimsPerUser"></param>
        /// <param name="minPurchasesCountToRedeem"></param>
        /// <param name="purchasesCountStartDate"></param>
        /// <param name="claimLocation"></param>
        /// <param name="value"></param>
        /// <param name="regularValue"></param>
        /// <param name="extraBonus"></param>
        /// <param name="extraBonusType"></param>
        /// <param name="minIncentive"></param>
        /// <param name="maxIncentive"></param>
        /// <param name="incentiveVariationType"></param>
        /// <param name="incentiveVariation"></param>
        /// <param name="secondsIncentiveVariationFrame"></param>
        /// <param name="imgId"></param>
        /// <param name="targettingParams"></param>
        /// <param name="releaseDate"></param>
        /// <param name="expirationDate"></param>
        /// <param name="rules"></param>
        /// <param name="conditions"></param>
        /// <param name="claimInstructions"></param>
        /// <param name="broadcastingScheduleType"></param>
        /// <param name="broadcastingTimerType"></param>
        /// <param name="broadcastingMinsToRedeem"></param>
        /// <param name="broadcastingTitle"></param>
        /// <param name="broadcastingMsg"></param>
        /// <param name="relevanceRate"></param>
        /// <returns></returns>
        public Offer Post(Guid mainCategoryId, int offerType, int dealType, int rewardType, int purposeType, int geoSegmentationType, int displayType, string name, 
            string mainHint, string complementaryHint, string keywords, string code, Guid? codeImg, string description, int minsToUnlock, bool isExclusive, bool isSponsored, bool hasUniqueCodes,
            int availableQuantity, bool? oneTimeRedemption, int maxClaimsPerUser, int minPurchasesCountToRedeem, DateTime? purchasesCountStartDate,
            string claimLocation, decimal value, decimal? regularValue, double extraBonus, int extraBonusType, decimal minIncentive, decimal maxIncentive, int incentiveVariationType,
            decimal incentiveVariation, int secondsIncentiveVariationFrame, Guid? imgId, string targettingParams,  DateTime releaseDate, DateTime expirationDate,
            string rules, string conditions, string claimInstructions, int broadcastingScheduleType, int broadcastingTimerType, int broadcastingMinsToRedeem, string broadcastingTitle,
            string broadcastingMsg, double relevanceRate)
        {
            Oltpoffers newOffer = null;

            Offer offer = null;
            try
            {
                newOffer = new Oltpoffers
                {
                    Id = Guid.NewGuid(),
                    TenantId = this._businessObjects.Tenant.TenantId,
                    MainCategoryId = mainCategoryId,
                    OfferType = offerType,
                    DealType = dealType,
                    RewardType = rewardType,
                    PurposeType = purposeType,
                    GeoSegmentationType = geoSegmentationType,
                    DisplayType = displayType,
                    Name = name,
                    MainHint = mainHint,
                    ComplementaryHint = complementaryHint,
                    Keywords = keywords,
                    Description = description,
                    Code = code ?? "",
                    CodeImg = codeImg,
                    MinsToUnlock = minsToUnlock,
                    IsActive = true,
                    IsExclusive = isExclusive,
                    IsSponsored = isSponsored,
                    HasUniqueCodes = hasUniqueCodes,
                    HasPreferences = false,
                    AvailableQuantity = availableQuantity,
                    OneTimeRedemption = false,
                    MaxClaimsPerUser = maxClaimsPerUser,
                    MinPurchasesCountToRedeem = minPurchasesCountToRedeem,
                    PurchasesCountStartDate = purchasesCountStartDate,
                    ClaimLocation = claimLocation,
                    Value = value,
                    RegularValue = regularValue,
                    ExtraBonus = extraBonus,
                    ExtraBonusType = extraBonusType,
                    MinIncentive = minIncentive,
                    MaxIncentive = maxIncentive,
                    IncentiveVariationType = incentiveVariationType,
                    IncentiveVariation = incentiveVariation,
                    SecondsIncentiveVariationFrame = secondsIncentiveVariationFrame,
                    RedeemCount = 0,
                    ClaimCount = 0,
                    ReleaseDate = releaseDate,
                    ExpirationDate = expirationDate,
                    TargettingParams = targettingParams,
                    DisplayImageId = imgId,
                    Rules = rules ?? Resources.NoRulesAvailable,
                    Conditions = conditions ?? Resources.NoConditionsAvailable,
                    ClaimInstructions = claimInstructions ?? Resources.NoClaimInstructionsAvailable,
                    LastBroadcastingUsage = null,
                    BroadcastingTimerType = broadcastingTimerType,
                    BroadcastingScheduleType = broadcastingScheduleType,
                    BroadcastingMinsToRedeem = broadcastingMinsToRedeem,
                    BroadcastingTitle = broadcastingTitle,
                    BroadcastingMsg = broadcastingMsg,
                    RelevanceRate = relevanceRate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                if(oneTimeRedemption == null)
                {

                    switch (purposeType)
                    {
                        case OfferPurposeTypes.Reward:
                            newOffer.OneTimeRedemption = true;
                            break;
                        case OfferPurposeTypes.Deal:
                            newOffer.OneTimeRedemption = false;
                            break;
                    }
                }
                else
                {
                    newOffer.OneTimeRedemption = (bool)oneTimeRedemption;
                }

                this._businessObjects.Context.Oltpoffers.Add(newOffer);
                this._businessObjects.Context.SaveChanges();

                OltpoffersView newOfferView = (from x in this._businessObjects.Context.OltpoffersView
                                           where x.Id == newOffer.Id
                                           select x).FirstOrDefault();

                if(newOfferView != null)
                {
                    offer = new Offer
                    {
                        Id = newOfferView.Id,
                        TenantId = newOfferView.TenantId,
                        MainCategoryId = newOfferView.MainCategoryId,
                        MainCategoryName = newOfferView.CategoryName,
                        OfferType = newOfferView.OfferType,
                        OfferTypeName = GetOfferTypeName(newOfferView.OfferType),
                        DealType = newOfferView.DealType,
                        DealTypeName = GetDealTypeName(newOfferView.DealType),
                        RewardType = newOfferView.RewardType,
                        RewardTypeName = GetRewardTypeName(newOfferView.RewardType),
                        PurposeType = newOfferView.PurposeType,
                        PurposeTypeName = this.GetPurposeTypeName(newOfferView.PurposeType),
                        GeoSegmentationType = newOfferView.GeoSegmentationType,
                        GeoSegmentationTypeName = GetGeoSegmentationTypeName(newOfferView.GeoSegmentationType),
                        DisplayType = newOfferView.DisplayType,
                        DisplayTypeName = GetDisplayTypeName(newOfferView.DisplayType),
                        Name = newOfferView.Name,
                        MainHint = newOffer.MainHint,
                        ComplementaryHint = newOffer.ComplementaryHint,
                        Keywords = newOfferView.Keywords,
                        Description = newOfferView.Description,
                        Code = newOfferView.Code ?? "",
                        CodeImg = newOfferView.CodeImg,
                        MinsToUnlock = newOfferView.MinsToUnlock,
                        IsActive = newOfferView.IsActive,
                        IsExclusive = newOfferView.IsExclusive,
                        IsSponsored = newOfferView.IsSponsored,
                        HasUniqueCodes = newOfferView.HasUniqueCodes,
                        HasPreferences = newOfferView.HasPreferences,
                        AvailableQuantity = newOfferView.AvailableQuantity,
                        OneTimeRedemption = newOfferView.OneTimeRedemption,
                        MaxClaimsPerUser = newOfferView.MaxClaimsPerUser,
                        MinPurchasesCountToRedeem = newOfferView.MinPurchasesCountToRedeem,
                        PurchasesCountStartDate = newOfferView.PurchasesCountStartDate,
                        ClaimLocation = newOfferView.ClaimLocation,
                        Value = newOfferView.Value,
                        RegularValue = newOfferView.RegularValue,
                        ExtraBonus = newOfferView.ExtraBonus,
                        ExtraBonusType = newOfferView.ExtraBonusType,
                        ExtraBonusTypeName = this.GetExtraBonusTypeName(newOfferView.ExtraBonusType),
                        MinIncentive = newOfferView.MinIncentive,
                        MaxIncentive = newOfferView.MaxIncentive,
                        IncentiveVariationType = newOfferView.IncentiveVariationType,
                        //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                        IncentiveVariation = newOfferView.IncentiveVariation,
                        //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                        RedeemCount = newOfferView.RedeemCount,
                        ClaimCount = newOfferView.ClaimCount,
                        ReleaseDate = newOfferView.ReleaseDate,
                        ExpirationDate = newOfferView.ExpirationDate,
                        TargettingParams = newOfferView.TargettingParams,
                        DisplayImgId = newOfferView.DisplayImageId,
                        Rules = newOfferView.Rules ?? Resources.NoRulesAvailable,
                        Conditions = newOfferView.Conditions ?? Resources.NoConditionsAvailable,
                        ClaimInstructions = newOfferView.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                        LastBroadcastingUsage = newOfferView.LastBroadcastingUsage,
                        BroadcastingTimerType = newOfferView.BroadcastingTimerType,
                        BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(newOfferView.BroadcastingTimerType),
                        BroadcastingScheduleType = newOfferView.BroadcastingScheduleType,
                        BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(newOfferView.BroadcastingScheduleType),
                        BroadcastingMinsToRedeem = newOfferView.BroadcastingMinsToRedeem,
                        BroadcastingTitle = newOfferView.BroadcastingTitle,
                        BroadcastingMsg = newOfferView.BroadcastingMsg,
                        RelevanceRate = newOfferView.RelevanceRate,
                        CreatedDate = newOfferView.CreatedDate,
                        UpdatedDate = newOfferView.UpdatedDate
                    };

                    DateTime now = DateTime.UtcNow;


                    offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, now);
                }

            }
            catch (Exception e)
            {
                this._businessObjects.Context.Oltpoffers.Remove(newOffer);
                this._businessObjects.Context.SaveChanges();

                offer = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offer;
        }//METHOD POST ENDS ----------------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// Modifies quantity attributes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public bool Put(Guid id, int offerType, int quantity, int operation)
        {
            bool success = false;

            try
            {

                bool? result = null;
                int? availableQuantity = null;

                switch (operation)
                {
                    case ProductOperations.Redemption:

                        result = this._businessObjects.StoredProcsHandler.UpdateOfferRedeemCount(id, quantity);

                        if (result == true)
                            success = true;

                        success = true;

                        break;
                    case ProductOperations.Increase:

                        availableQuantity = this._businessObjects.StoredProcsHandler.UpdateOfferAvailableQuantity(id, quantity);

                        if (availableQuantity != null)
                            success = true;


                        break;
                    case ProductOperations.Claim:

                        result = this._businessObjects.StoredProcsHandler.UpdateOfferClaimCount(id, quantity);

                        if (result == true)
                        {
                            availableQuantity = this._businessObjects.StoredProcsHandler.UpdateOfferAvailableQuantity(id, -1 * quantity);

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


        /// <summary>
        /// Updates an offer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offerType"></param>
        /// <param name="mainCategoryId"></param>
        /// <param name="name"></param>
        /// <param name="mainHint"></param>
        /// <param name="complementaryHint"></param>
        /// <param name="keywords"></param>
        /// <param name="code"></param>
        /// <param name="codeImg"></param>
        /// <param name="description"></param>
        /// <param name="isActive"></param>
        /// <param name="isExclusive"></param>
        /// <param name="isSponsored"></param>
        /// <param name="hasUniqueCodes"></param>
        /// <param name="hasPreferences"></param>
        /// <param name="availableQuantity"></param>
        /// <param name="maxClaimsPerUser"></param>
        /// <param name="minPurchasesCountToRedeem"></param>
        /// <param name="purchasesCountStartDate"></param>
        /// <param name="claimLocation"></param>
        /// <param name="value"></param>
        /// <param name="regularValue"></param>
        /// <param name="extraBonus"></param>
        /// <param name="extraBonusType"></param>
        /// <param name="minIncentive"></param>
        /// <param name="maxIncentive"></param>
        /// <param name="incentiveVariationType"></param>
        /// <param name="incentiveVariation"></param>
        /// <param name="secondsIncentiveVariationFrame"></param>
        /// <param name="imgId"></param>
        /// <param name="targettingParams"></param>
        /// <param name="releaseDate"></param>
        /// <param name="expirationDate"></param>
        /// <param name="relevanceRate"></param>
        /// <returns></returns>
        public Offer Put(Guid id, int offerType, Guid mainCategoryId, string name, string mainHint, string complementaryHint, string keywords, string code, Guid? codeImg, 
            string description, bool isActive, bool isExclusive, bool isSponsored, bool hasUniqueCodes, int availableQuantity, 
            int maxClaimsPerUser, int minPurchasesCountToRedeem, DateTime? purchasesCountStartDate, string claimLocation, decimal value, 
            decimal? regularValue, double extraBonus, int extraBonusType, decimal minIncentive, decimal maxIncentive, int incentiveVariationType, decimal incentiveVariation, 
            int secondsIncentiveVariationFrame, Guid? imgId, string targettingParams, DateTime releaseDate, DateTime expirationDate, double relevanceRate)
        {
            Offer offer = null;

            try
            {
                Oltpoffers currentOffer = null;

                var query = from x in this._businessObjects.Context.Oltpoffers
                            where x.OfferType == offerType && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (Oltpoffers item in query)
                {
                    currentOffer = item;
                }

                if (currentOffer != null)
                {
                    currentOffer.MainCategoryId = mainCategoryId;
                    currentOffer.Name = name;
                    currentOffer.MainHint = mainHint;
                    currentOffer.ComplementaryHint = complementaryHint;
                    currentOffer.Keywords = keywords;
                    currentOffer.Code = code;
                    currentOffer.CodeImg = codeImg;
                    currentOffer.Description = description;
                    currentOffer.IsActive = isActive;
                    currentOffer.IsExclusive = isExclusive;
                    currentOffer.IsSponsored = isSponsored;
                    currentOffer.HasUniqueCodes = hasUniqueCodes;
                    currentOffer.AvailableQuantity = availableQuantity;
                    currentOffer.MaxClaimsPerUser = maxClaimsPerUser;
                    currentOffer.MinPurchasesCountToRedeem = minPurchasesCountToRedeem;
                    currentOffer.PurchasesCountStartDate = purchasesCountStartDate;
                    currentOffer.ClaimLocation = claimLocation;
                    currentOffer.Value = value;
                    currentOffer.RegularValue = regularValue;
                    currentOffer.ExtraBonus = extraBonus;
                    currentOffer.ExtraBonusType = extraBonusType;
                    currentOffer.MinIncentive = minIncentive;
                    currentOffer.MaxIncentive = maxIncentive;
                    currentOffer.IncentiveVariation = incentiveVariation;
                    currentOffer.SecondsIncentiveVariationFrame = secondsIncentiveVariationFrame;
                    currentOffer.DisplayImageId = imgId;
                    currentOffer.TargettingParams = targettingParams;
                    currentOffer.ReleaseDate = releaseDate;
                    currentOffer.ExpirationDate = expirationDate;
                    currentOffer.RelevanceRate = relevanceRate;
                    currentOffer.UpdatedDate = DateTime.UtcNow;

                    this._businessObjects.Context.SaveChanges();

                    OltpoffersView currentOfferView = (from x in this._businessObjects.Context.OltpoffersView
                                                   where x.Id == currentOffer.Id
                                                   select x).FirstOrDefault();

                    if (currentOfferView != null)
                    {
                        offer = new Offer
                        {
                            Id = currentOfferView.Id,
                            TenantId = currentOfferView.TenantId,
                            MainCategoryId = currentOfferView.MainCategoryId,
                            MainCategoryName = currentOfferView.CategoryName,
                            OfferType = currentOfferView.OfferType,
                            OfferTypeName = GetOfferTypeName(currentOfferView.OfferType),
                            DealType = currentOfferView.DealType,
                            DealTypeName = GetDealTypeName(currentOfferView.DealType),
                            RewardType = currentOfferView.RewardType,
                            RewardTypeName = GetRewardTypeName(currentOfferView.RewardType),
                            PurposeType = currentOfferView.PurposeType,
                            PurposeTypeName = this.GetPurposeTypeName(currentOfferView.PurposeType),
                            GeoSegmentationType = currentOfferView.GeoSegmentationType,
                            GeoSegmentationTypeName = GetGeoSegmentationTypeName(currentOfferView.GeoSegmentationType),
                            DisplayType = currentOfferView.DisplayType,
                            DisplayTypeName = GetDisplayTypeName(currentOfferView.DisplayType),
                            Name = currentOfferView.Name,
                            MainHint = currentOfferView.MainHint,
                            ComplementaryHint = currentOffer.ComplementaryHint,
                            Keywords = currentOfferView.Keywords,
                            Description = currentOfferView.Description,
                            Code = currentOfferView.Code ?? "",
                            CodeImg = currentOfferView.CodeImg,
                            MinsToUnlock = currentOfferView.MinsToUnlock,
                            IsActive = currentOfferView.IsActive,
                            IsExclusive = currentOfferView.IsExclusive,
                            IsSponsored = currentOfferView.IsSponsored,
                            HasUniqueCodes = currentOfferView.HasUniqueCodes,
                            HasPreferences = currentOfferView.HasPreferences,
                            AvailableQuantity = currentOfferView.AvailableQuantity,
                            OneTimeRedemption = currentOfferView.OneTimeRedemption,
                            MaxClaimsPerUser = currentOfferView.MaxClaimsPerUser,
                            MinPurchasesCountToRedeem = currentOfferView.MinPurchasesCountToRedeem,
                            PurchasesCountStartDate = currentOfferView.PurchasesCountStartDate,
                            ClaimLocation = currentOfferView.ClaimLocation,
                            Value = currentOfferView.Value,
                            RegularValue = currentOfferView.RegularValue,
                            ExtraBonus = currentOfferView.ExtraBonus,
                            ExtraBonusType = currentOfferView.ExtraBonusType,
                            ExtraBonusTypeName = this.GetExtraBonusTypeName(currentOfferView.ExtraBonusType),
                            MinIncentive = currentOfferView.MinIncentive,
                            MaxIncentive = currentOfferView.MaxIncentive,
                            IncentiveVariationType = currentOfferView.IncentiveVariationType,
                            //IncentiveVarationTypeName = GetIncentiveVariationTypeName(item.IncentiveVariationType),
                            IncentiveVariation = currentOfferView.IncentiveVariation,
                            //SecondsIncentiveVariationFrame = item.SecondsIncentiveVariationFrame,
                            RedeemCount = currentOfferView.RedeemCount,
                            ClaimCount = currentOfferView.ClaimCount,
                            ReleaseDate = currentOfferView.ReleaseDate,
                            ExpirationDate = currentOfferView.ExpirationDate,
                            TargettingParams = currentOfferView.TargettingParams,
                            DisplayImgId = currentOfferView.DisplayImageId,
                            Rules = currentOfferView.Rules ?? Resources.NoRulesAvailable,
                            Conditions = currentOfferView.Conditions ?? Resources.NoConditionsAvailable,
                            ClaimInstructions = currentOfferView.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                            LastBroadcastingUsage = currentOfferView.LastBroadcastingUsage,
                            BroadcastingTimerType = currentOfferView.BroadcastingTimerType,
                            BroadcastingTimerTypeName = this.GetBroadcastingTimerTypeName(currentOfferView.BroadcastingTimerType),
                            BroadcastingScheduleType = currentOfferView.BroadcastingScheduleType,
                            BroadcastingScheduleTypeName = this.GetBroadcastingScheduleTypeName(currentOfferView.BroadcastingScheduleType),
                            BroadcastingMinsToRedeem = currentOfferView.BroadcastingMinsToRedeem,
                            BroadcastingTitle = currentOfferView.BroadcastingTitle,
                            BroadcastingMsg = currentOfferView.BroadcastingMsg,
                            RelevanceRate = currentOfferView.RelevanceRate,
                            CreatedDate = currentOfferView.CreatedDate,
                            UpdatedDate = currentOfferView.UpdatedDate
                        };

                        DateTime now = DateTime.UtcNow;


                        offer.PublishState = this.GetPublishState((DateTime)offer.ReleaseDate, offer.ExpirationDate, now);
                    }
                }

            }
            catch (Exception e)
            {
                offer = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offer;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Changes a state
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObjectStateUpdate Put(Guid id, int offerType, int changeType)
        {
            ObjectStateUpdate result = null;

            try
            {
                Oltpoffers offer = null;

                var query = from x in this._businessObjects.Context.Oltpoffers
                            where x.OfferType == offerType && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (Oltpoffers item in query)
                {
                    offer = item;
                }

                if (offer != null)
                {
                    result = new ObjectStateUpdate();

                    switch (changeType)
                    {
                        case ChangeTypes.ActiveState:
                            offer.IsActive = !offer.IsActive;
                            offer.UpdatedDate = DateTime.UtcNow;
                            this._businessObjects.Context.SaveChanges();

                            result.Success = true;
                            result.NewState = (bool)offer.IsActive;
                            break;
                        case ChangeTypes.ExclusiveState:
                            offer.IsExclusive = !offer.IsExclusive;
                            offer.UpdatedDate = DateTime.UtcNow;
                            this._businessObjects.Context.SaveChanges();

                            result.Success = true;
                            result.NewState = (bool)offer.IsExclusive;
                            break;
                        case ChangeTypes.SponsoredState:
                            offer.IsSponsored = !offer.IsSponsored;
                            offer.UpdatedDate = DateTime.UtcNow;
                            this._businessObjects.Context.SaveChanges();

                            result.Success = true;
                            result.NewState = offer.IsSponsored;
                            break;
                        case ChangeTypes.HasPreferences:
                            offer.HasPreferences = !offer.HasPreferences;
                            offer.UpdatedDate = DateTime.UtcNow;
                            this._businessObjects.Context.SaveChanges();

                            result.Success = true;
                            result.NewState = offer.HasPreferences;
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
        public bool Put(Guid id, int offerType, DateTime lastBroadcastingUsage)
        {
            bool success = false;

            try
            {
                Oltpoffers offer = null;

                var query = from x in this._businessObjects.Context.Oltpoffers
                            where x.OfferType == offerType && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                foreach (Oltpoffers item in query)
                {
                    offer = item;
                }

                if (offer != null)
                {
                    offer.LastBroadcastingUsage = lastBroadcastingUsage;
                    offer.UpdatedDate = DateTime.UtcNow;
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
        /// Changes offer display img
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imageId"></param>
        /// <param name="contentType"></param>
        /// <param name="imgType"></param>
        /// <returns></returns>
        public Guid? Put(Guid id, int offerType, Guid imageId, int imgType)
        {
            Guid? currentImg = null;

            try
            {
                var query = from x in this._businessObjects.Context.Oltpoffers
                            where x.OfferType == offerType && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                            select x;

                Oltpoffers offer = null;
                foreach (Oltpoffers item in query)
                {
                    offer = item;
                }


                if (offer != null)
                {
                    switch (imgType)
                    {
                        case ProductImgTypes.DisplayImg:
                            currentImg = offer.DisplayImageId;
                            offer.DisplayImageId = imageId;
                            offer.UpdatedDate = DateTime.UtcNow;
                            break;
                        case ProductImgTypes.Code:
                            currentImg = offer.CodeImg;
                            offer.CodeImg = imageId;
                            offer.UpdatedDate = DateTime.UtcNow;
                            break;

                    }

                    this._businessObjects.Context.SaveChanges();

                }

            }
            catch (Exception e)
            {
                currentImg = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return currentImg;
        }//METHOD PUT ENDS ------------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// Deletes an offer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id, int offerType)
        {
            bool success = false;

            try
            {

                Oltpoffers offer  = (from x in this._businessObjects.Context.Oltpoffers
                                        where x.OfferType == offerType && x.TenantId == this._businessObjects.Tenant.TenantId && x.Id == id
                                        select x).FirstOrDefault();


                if (offer != null)
                {
                    offer.Deleted = true;
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


        #endregion

        #region OFFERSFULLDATA

        /// <summary>
        /// Retrieve offer flattened data about all the offers that are potencially
        /// available in a radius from a given prosition. At this point there is no certainity that 
        /// the offer actually is available for that position and radius, but that it belongs to a 
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
        private List<FlattenedOfferData> GetOffersDataByRegionWithLocation(Guid countryId, Guid stateId, int contentSegmentationType, string userId, decimal latitude, decimal longitude, double radius, DateTime dateTime, int selectorType, int offerPurpose)
        {
            List<FlattenedOfferData> offers = null;

            try
            {
                var query = (dynamic)null;

                switch (selectorType)
                {
                    case ContentFilters.Category:

                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:

                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByCountryWithLocationPreferenceFocus(latitude, longitude, radius * DistanceLimits.MaxKMRangeForMainOffersByCountryFactor, countryId, userId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:

                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByStateWithLocationPreferenceFocus(latitude, longitude, radius, stateId, countryId, userId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedOfferData offer;
                            offers = new List<FlattenedOfferData>();

                            foreach (TempoffersPreferenceBranches item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    offer = new FlattenedOfferData
                                    {
                                        SelectorType = selectorType,
                                        Offer = new Offer
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            MainCategoryId = item.MainCategoryId,
                                            MainCategoryName = "",//Not needed
                                            OfferType = item.OfferType,
                                            OfferTypeName = "",//Not needed
                                            DealType = item.DealType,
                                            DealTypeName = "",//Not needed
                                            RewardType = item.RewardType,
                                            RewardTypeName = "",//Not needed
                                            PurposeType = item.PurposeType,
                                            PurposeTypeName = "",//Not needed
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            GeoSegmentationTypeName = "",//Not needed
                                            DisplayType = item.DisplayType,
                                            DisplayTypeName = "",//Not needed
                                            Name = item.Name,
                                            MainHint = item.MainHint,
                                            ComplementaryHint = item.ComplementaryHint,
                                            Keywords = item.Keywords,
                                            Description = item.Description,
                                            Code = "",//Not needed
                                            CodeImg = null,//Not needed
                                            MinsToUnlock = item.MinsToUnlock,
                                            IsActive = item.IsActive,
                                            IsExclusive = item.IsExclusive,
                                            IsSponsored = item.IsSponsored,
                                            HasUniqueCodes = false,//Not needed
                                            HasPreferences = item.HasPreferences,
                                            AvailableQuantity = item.AvailableQuantity,
                                            OneTimeRedemption = item.OneTimeRedemption,
                                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                                            ClaimLocation = item.ClaimLocation,
                                            Value = item.Value,
                                            RegularValue = item.RegularValue,
                                            ExtraBonus = item.ExtraBonus,
                                            ExtraBonusType = item.ExtraBonusType,
                                            ExtraBonusTypeName = "",//Not needed
                                            MinIncentive = -1,//Not needed
                                            MaxIncentive = -1,//Not needed
                                            IncentiveVariationType = -1,//Not needed
                                            IncentiveVarationTypeName = "",//Not needed
                                            IncentiveVariation = -1,//Not needed
                                            SecondsIncentiveVariationFrame = -1,//Not needed
                                            RedeemCount = item.RedeemCount,
                                            ClaimCount = item.ClaimCount,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            TargettingParams = item.TargettingParams,
                                            DisplayImgId = item.DisplayImageId,
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                            LastBroadcastingUsage = null,//Not needed
                                            BroadcastingTimerType = -1,//Not needed
                                            BroadcastingTimerTypeName = "",//Not needed
                                            BroadcastingScheduleType = -1,//Not needed
                                            BroadcastingScheduleTypeName = "",//Not needed
                                            BroadcastingMinsToRedeem = -1,//Not needed
                                            BroadcastingTitle = "",//Not needed
                                            BroadcastingMsg = "",//Not needed
                                            RelevanceRate = item.RelevanceRate,
                                            CreatedDate = item.CreatedDate,
                                            UpdatedDate = DateTime.UtcNow,//Not needed
                                            SatisfactionScore = item.SatisfactionScore,
                                            RelevanceScore = item.RelevanceScore
                                        },
                                        Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                                        {
                                            Id = item.TenantId,
                                            Name = item.TenantName,
                                            Logo = item.TenantLogo,
                                            CountryId = item.TenantCountryId,
                                            CurrencySymbol = item.CurrencySymbol,
                                            Type = item.TenantType,
                                            CategoryId = item.TenantCategoryId,
                                            CategoryName = "",
                                            RelevanceScore = null,//When its selector is category, there is no info about tenant relevance
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
                                        Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                                        {
                                            Id = item.PreferenceId,
                                            Name = item.PreferenceName,
                                            Icon = item.PreferenceIcon,
                                            RelevanceScore = item.PreferenceScore
                                        },
                                        ExactLocationBased = true
                                    };

                                    offers.Add(offer);
                                }
                            }
                        }

                        break;
                    case ContentFilters.Tenant:

                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByCountryWithLocationTenantFocus(latitude, longitude, radius * DistanceLimits.MaxKMRangeForMainOffersByCountryFactor, countryId, userId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByStateWithLocationTenantFocus(latitude, longitude, radius, stateId, countryId, userId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedOfferData offer;
                            offers = new List<FlattenedOfferData>();

                            foreach (TempoffersPreferenceBranches item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    offer = new FlattenedOfferData
                                    {
                                        SelectorType = selectorType,
                                        Offer = new Offer
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            MainCategoryId = item.MainCategoryId,
                                            MainCategoryName = "",//Not needed
                                            OfferType = item.OfferType,
                                            OfferTypeName = "",//Not needed
                                            DealType = item.DealType,
                                            DealTypeName = "",//Not needed
                                            RewardType = item.RewardType,
                                            RewardTypeName = "",//Not needed
                                            PurposeType = item.PurposeType,
                                            PurposeTypeName = "",//Not needed
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            GeoSegmentationTypeName = "",//Not needed
                                            DisplayType = item.DisplayType,
                                            DisplayTypeName = "",//Not needed
                                            Name = item.Name,
                                            MainHint = item.MainHint,
                                            ComplementaryHint = item.ComplementaryHint,
                                            Keywords = item.Keywords,
                                            Description = item.Description,
                                            Code = "",//Not needed
                                            CodeImg = null,//Not needed
                                            MinsToUnlock = item.MinsToUnlock,
                                            IsActive = item.IsActive,
                                            IsExclusive = item.IsExclusive,
                                            IsSponsored = item.IsSponsored,
                                            HasUniqueCodes = false,//Not needed
                                            HasPreferences = item.HasPreferences,
                                            AvailableQuantity = item.AvailableQuantity,
                                            OneTimeRedemption = item.OneTimeRedemption,
                                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                                            ClaimLocation = item.ClaimLocation,
                                            Value = item.Value,
                                            RegularValue = item.RegularValue,
                                            ExtraBonus = item.ExtraBonus,
                                            ExtraBonusType = item.ExtraBonusType,
                                            ExtraBonusTypeName = "",//Not needed
                                            MinIncentive = -1,//Not needed
                                            MaxIncentive = -1,//Not needed
                                            IncentiveVariationType = -1,//Not needed
                                            IncentiveVarationTypeName = "",//Not needed
                                            IncentiveVariation = -1,//Not needed
                                            SecondsIncentiveVariationFrame = -1,//Not needed
                                            RedeemCount = item.RedeemCount,
                                            ClaimCount = item.ClaimCount,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            TargettingParams = item.TargettingParams,
                                            DisplayImgId = item.DisplayImageId,
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                            LastBroadcastingUsage = null,//Not needed
                                            BroadcastingTimerType = -1,//Not needed
                                            BroadcastingTimerTypeName = "",//Not needed
                                            BroadcastingScheduleType = -1,//Not needed
                                            BroadcastingScheduleTypeName = "",//Not needed
                                            BroadcastingMinsToRedeem = -1,//Not needed
                                            BroadcastingTitle = "",//Not needed
                                            BroadcastingMsg = "",//Not needed
                                            RelevanceRate = item.RelevanceRate,
                                            CreatedDate = item.CreatedDate,
                                            UpdatedDate = DateTime.UtcNow,//Not needed
                                            SatisfactionScore = item.SatisfactionScore,
                                            RelevanceScore = item.RelevanceScore
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

                                    offers.Add(offer);
                                }

                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
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
        private List<FlattenedOfferData> GetOffersDataByRegion(Guid stateId, Guid countryId, int contentSegmentationType, string userId, DateTime dateTime, int selectorType, int offerPurpose)
        {
            List<FlattenedOfferData> offers = null;

            try
            {
                var query = (dynamic)null;

                switch (selectorType)
                {
                    case ContentFilters.Category:

                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByCountryPreferenceFocus(countryId, userId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByStatePreferenceFocus(stateId, countryId, userId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedOfferData offer;
                            offers = new List<FlattenedOfferData>();

                            foreach (TempoffersPreferenceBranches item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    offer = new FlattenedOfferData
                                    {
                                        SelectorType = selectorType,
                                        Offer = new Offer
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            MainCategoryId = item.MainCategoryId,
                                            MainCategoryName = "",//Not needed
                                            OfferType = item.OfferType,
                                            OfferTypeName = "",//Not needed
                                            DealType = item.DealType,
                                            DealTypeName = "",//Not needed
                                            RewardType = item.RewardType,
                                            RewardTypeName = "",//Not needed
                                            PurposeType = item.PurposeType,
                                            PurposeTypeName = "",//Not needed
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            GeoSegmentationTypeName = "",//Not needed
                                            DisplayType = item.DisplayType,
                                            DisplayTypeName = "",//Not needed
                                            Name = item.Name,
                                            MainHint = item.MainHint,
                                            ComplementaryHint = item.ComplementaryHint,
                                            Keywords = item.Keywords,
                                            Description = item.Description,
                                            Code = "",//Not needed
                                            CodeImg = null,//Not needed
                                            MinsToUnlock = item.MinsToUnlock,
                                            IsActive = item.IsActive,
                                            IsExclusive = item.IsExclusive,
                                            IsSponsored = item.IsSponsored,
                                            HasUniqueCodes = false,//Not needed
                                            HasPreferences = item.HasPreferences,
                                            AvailableQuantity = item.AvailableQuantity,
                                            OneTimeRedemption = item.OneTimeRedemption,
                                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                                            ClaimLocation = item.ClaimLocation,
                                            Value = item.Value,
                                            RegularValue = item.RegularValue,
                                            ExtraBonus = item.ExtraBonus,
                                            ExtraBonusType = item.ExtraBonusType,
                                            ExtraBonusTypeName = "",//Not needed
                                            MinIncentive = -1,//Not needed
                                            MaxIncentive = -1,//Not needed
                                            IncentiveVariationType = -1,//Not needed
                                            IncentiveVarationTypeName = "",//Not needed
                                            IncentiveVariation = -1,//Not needed
                                            SecondsIncentiveVariationFrame = -1,//Not needed
                                            RedeemCount = item.RedeemCount,
                                            ClaimCount = item.ClaimCount,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            TargettingParams = item.TargettingParams,
                                            DisplayImgId = item.DisplayImageId,
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                            LastBroadcastingUsage = null,//Not needed
                                            BroadcastingTimerType = -1,//Not needed
                                            BroadcastingTimerTypeName = "",//Not needed
                                            BroadcastingScheduleType = -1,//Not needed
                                            BroadcastingScheduleTypeName = "",//Not needed
                                            BroadcastingMinsToRedeem = -1,//Not needed
                                            BroadcastingTitle = "",//Not needed
                                            BroadcastingMsg = "",//Not needed
                                            RelevanceRate = item.RelevanceRate,
                                            CreatedDate = item.CreatedDate,
                                            UpdatedDate = DateTime.UtcNow,//Not needed
                                            SatisfactionScore = item.SatisfactionScore,
                                            RelevanceScore = item.RelevanceScore
                                        },
                                        Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                                        {
                                            Id = item.TenantId,
                                            Name = item.TenantName,
                                            Logo = item.TenantLogo,
                                            CountryId = item.TenantCountryId,
                                            CurrencySymbol = item.CurrencySymbol,
                                            CategoryId = item.TenantCategoryId,
                                            CategoryName = "",
                                            Type = item.TenantType,
                                            RelevanceScore = null,//When its selector is category, there is tenant score
                                            NearestBranchId = Guid.Empty,
                                            NearestBranchName = "",
                                            NearestBranchLatitude = null,
                                            NearestBranchLongitude = null,
                                        },
                                        Branch = new BasicBranchData
                                        {
                                            Id = item.BranchId,
                                            Name = item.BranchName,
                                            Latitude = item.BranchLatitude,
                                            Longitude = item.BranchLongitude,
                                            DescriptiveAddress = item.BranchDescriptiveAddress,
                                            InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                            Distance = null,//In this case there is no way to define distance
                                            CityId = item.BranchCityId,
                                            StateId = item.BranchStateId,
                                            Enabled = false
                                        },
                                        Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                                        {
                                            Id = item.PreferenceId,
                                            Name = item.PreferenceName,
                                            Icon = item.PreferenceIcon,
                                            RelevanceScore = item.PreferenceScore
                                        },
                                        ExactLocationBased = false
                                    };

                                    offers.Add(offer);
                                }
                            }
                        }

                        break;
                    case ContentFilters.Tenant:

                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByCountryTenantFocus(countryId, userId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByStateTenantFocus(stateId, countryId, userId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedOfferData offer;
                            offers = new List<FlattenedOfferData>();

                            foreach (TempoffersPreferenceBranches item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    offer = new FlattenedOfferData
                                    {
                                        SelectorType = selectorType,
                                        Offer = new Offer
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            MainCategoryId = item.MainCategoryId,
                                            MainCategoryName = "",//Not needed
                                            OfferType = item.OfferType,
                                            OfferTypeName = "",//Not needed
                                            DealType = item.DealType,
                                            DealTypeName = "",//Not needed
                                            RewardType = item.RewardType,
                                            RewardTypeName = "",//Not needed
                                            PurposeType = item.PurposeType,
                                            PurposeTypeName = "",//Not needed
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            GeoSegmentationTypeName = "",//Not needed
                                            DisplayType = item.DisplayType,
                                            DisplayTypeName = "",//Not needed
                                            Name = item.Name,
                                            MainHint = item.MainHint,
                                            ComplementaryHint = item.ComplementaryHint,
                                            Keywords = item.Keywords,
                                            Description = item.Description,
                                            Code = "",//Not needed
                                            CodeImg = null,//Not needed
                                            MinsToUnlock = item.MinsToUnlock,
                                            IsActive = item.IsActive,
                                            IsExclusive = item.IsExclusive,
                                            IsSponsored = item.IsSponsored,
                                            HasUniqueCodes = false,//Not needed
                                            HasPreferences = item.HasPreferences,
                                            AvailableQuantity = item.AvailableQuantity,
                                            OneTimeRedemption = item.OneTimeRedemption,
                                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                                            ClaimLocation = item.ClaimLocation,
                                            Value = item.Value,
                                            RegularValue = item.RegularValue,
                                            ExtraBonus = item.ExtraBonus,
                                            ExtraBonusType = item.ExtraBonusType,
                                            ExtraBonusTypeName = "",//Not needed
                                            MinIncentive = -1,//Not needed
                                            MaxIncentive = -1,//Not needed
                                            IncentiveVariationType = -1,//Not needed
                                            IncentiveVarationTypeName = "",//Not needed
                                            IncentiveVariation = -1,//Not needed
                                            SecondsIncentiveVariationFrame = -1,//Not needed
                                            RedeemCount = item.RedeemCount,
                                            ClaimCount = item.ClaimCount,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            TargettingParams = item.TargettingParams,
                                            DisplayImgId = item.DisplayImageId,
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                            LastBroadcastingUsage = null,//Not needed
                                            BroadcastingTimerType = -1,//Not needed
                                            BroadcastingTimerTypeName = "",//Not needed
                                            BroadcastingScheduleType = -1,//Not needed
                                            BroadcastingScheduleTypeName = "",//Not needed
                                            BroadcastingMinsToRedeem = -1,//Not needed
                                            BroadcastingTitle = "",//Not needed
                                            BroadcastingMsg = "",//Not needed
                                            RelevanceRate = item.RelevanceRate,
                                            CreatedDate = item.CreatedDate,
                                            UpdatedDate = DateTime.UtcNow,//Not needed
                                            SatisfactionScore = item.SatisfactionScore,
                                            RelevanceScore = item.RelevanceScore
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

                                    offers.Add(offer);
                                }

                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
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
        private List<FlattenedOfferData> GetOffersDataByRegionForReference(Guid stateId, Guid countryId, int contentSegmentationType, string userId, DateTime dateTime, int referenceType, Guid referenceId, int offerPurpose)
        {
            List<FlattenedOfferData> offers = null;

            try
            {
                var query = (dynamic)null;

                switch (referenceType)
                {
                    case ContentFilters.Category:
                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByCountryForPreference(countryId, userId, referenceId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByStateForPreference(stateId, countryId, userId, referenceId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedOfferData offer;

                            offers = new List<FlattenedOfferData>();

                            foreach (TempoffersPreferenceBranches item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    offer = new FlattenedOfferData
                                    {
                                        SelectorType = referenceType,
                                        Offer = new Offer
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            MainCategoryId = item.MainCategoryId,
                                            MainCategoryName = "",//Not needed
                                            OfferType = item.OfferType,
                                            OfferTypeName = "",//Not needed
                                            DealType = item.DealType,
                                            DealTypeName = "",//Not needed
                                            RewardType = item.RewardType,
                                            RewardTypeName = "",//Not needed
                                            PurposeType = item.PurposeType,
                                            PurposeTypeName = "",//Not needed
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            GeoSegmentationTypeName = "",//Not needed
                                            DisplayType = item.DisplayType,
                                            DisplayTypeName = "",//Not needed
                                            Name = item.Name,
                                            MainHint = item.MainHint,
                                            ComplementaryHint = item.ComplementaryHint,
                                            Keywords = item.Keywords,
                                            Description = item.Description,
                                            Code = "",//Not needed
                                            CodeImg = null,//Not needed
                                            MinsToUnlock = item.MinsToUnlock,
                                            IsActive = item.IsActive,
                                            IsExclusive = item.IsExclusive,
                                            IsSponsored = item.IsSponsored,
                                            HasUniqueCodes = false,//Not needed
                                            HasPreferences = item.HasPreferences,
                                            AvailableQuantity = item.AvailableQuantity,
                                            OneTimeRedemption = item.OneTimeRedemption,
                                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                                            ClaimLocation = item.ClaimLocation,
                                            Value = item.Value,
                                            RegularValue = item.RegularValue,
                                            ExtraBonus = item.ExtraBonus,
                                            ExtraBonusType = item.ExtraBonusType,
                                            ExtraBonusTypeName = "",//Not needed
                                            MinIncentive = -1,//Not needed
                                            MaxIncentive = -1,//Not needed
                                            IncentiveVariationType = -1,//Not needed
                                            IncentiveVarationTypeName = "",//Not needed
                                            IncentiveVariation = -1,//Not needed
                                            SecondsIncentiveVariationFrame = -1,//Not needed
                                            RedeemCount = item.RedeemCount,
                                            ClaimCount = item.ClaimCount,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            TargettingParams = item.TargettingParams,
                                            DisplayImgId = item.DisplayImageId,
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                            LastBroadcastingUsage = null,//Not needed
                                            BroadcastingTimerType = -1,//Not needed
                                            BroadcastingTimerTypeName = "",//Not needed
                                            BroadcastingScheduleType = -1,//Not needed
                                            BroadcastingScheduleTypeName = "",//Not needed
                                            BroadcastingMinsToRedeem = -1,//Not needed
                                            BroadcastingTitle = "",//Not needed
                                            BroadcastingMsg = "",//Not needed
                                            RelevanceRate = item.RelevanceRate,
                                            CreatedDate = item.CreatedDate,
                                            UpdatedDate = DateTime.UtcNow,//Not needed
                                            SatisfactionScore = item.SatisfactionScore,
                                            RelevanceScore = item.RelevanceScore
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
                                            DescriptiveAddress = item.BranchDescriptiveAddress,
                                            InquiriesPhoneNumber = item.BranchInquiriesPhoneNumber,
                                            Latitude = item.BranchLatitude,
                                            Longitude = item.BranchLongitude,
                                            Distance = null,//In this case there is no way to define distance
                                            CityId = item.BranchCityId,
                                            StateId = item.BranchStateId,
                                            Enabled = false
                                        },
                                        Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                                        {
                                            Id = item.PreferenceId,
                                            Name = item.PreferenceName,
                                            Icon = item.PreferenceIcon,
                                            RelevanceScore = item.PreferenceScore
                                        },
                                        ExactLocationBased = false
                                    };

                                    offers.Add(offer);
                                }
                            }
                        }

                        break;
                    case ContentFilters.Tenant:
                        switch (contentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByCountryForTenant(countryId, userId, referenceId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                            case GeoSegmentationTypes.State:
                                query = from x in this._businessObjects.FuncsHandler.GetAvailableOffersByStateForTenant(stateId, countryId, userId, referenceId, offerPurpose, dateTime)
                                        where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                        select x;
                                break;
                        }

                        if (query != null)
                        {
                            FlattenedOfferData offer;

                            offers = new List<FlattenedOfferData>();

                            foreach (TempoffersPreferenceBranches item in query)
                            {
                                if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                                {
                                    offer = new FlattenedOfferData
                                    {
                                        SelectorType = referenceType,
                                        Offer = new Offer
                                        {
                                            Id = item.Id,
                                            TenantId = item.TenantId,
                                            MainCategoryId = item.MainCategoryId,
                                            MainCategoryName = "",//Not needed
                                            OfferType = item.OfferType,
                                            OfferTypeName = "",//Not needed
                                            DealType = item.DealType,
                                            DealTypeName = "",//Not needed
                                            RewardType = item.RewardType,
                                            RewardTypeName = "",//Not needed
                                            PurposeType = item.PurposeType,
                                            PurposeTypeName = "",//Not needed
                                            GeoSegmentationType = item.GeoSegmentationType,
                                            GeoSegmentationTypeName = "",//Not needed
                                            DisplayType = item.DisplayType,
                                            DisplayTypeName = "",//Not needed
                                            Name = item.Name,
                                            MainHint = item.MainHint,
                                            ComplementaryHint = item.ComplementaryHint,
                                            Keywords = item.Keywords,
                                            Description = item.Description,
                                            Code = "",//Not needed
                                            CodeImg = null,//Not needed
                                            MinsToUnlock = item.MinsToUnlock,
                                            IsActive = item.IsActive,
                                            IsExclusive = item.IsExclusive,
                                            IsSponsored = item.IsSponsored,
                                            HasUniqueCodes = false,//Not needed
                                            HasPreferences = item.HasPreferences,
                                            AvailableQuantity = item.AvailableQuantity,
                                            OneTimeRedemption = item.OneTimeRedemption,
                                            MaxClaimsPerUser = item.MaxClaimsPerUser,
                                            MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                            PurchasesCountStartDate = item.PurchasesCountStartDate,
                                            ClaimLocation = item.ClaimLocation,
                                            Value = item.Value,
                                            RegularValue = item.RegularValue,
                                            ExtraBonus = item.ExtraBonus,
                                            ExtraBonusType = item.ExtraBonusType,
                                            ExtraBonusTypeName = "",//Not needed
                                            MinIncentive = -1,//Not needed
                                            MaxIncentive = -1,//Not needed
                                            IncentiveVariationType = -1,//Not needed
                                            IncentiveVarationTypeName = "",//Not needed
                                            IncentiveVariation = -1,//Not needed
                                            SecondsIncentiveVariationFrame = -1,//Not needed
                                            RedeemCount = item.RedeemCount,
                                            ClaimCount = item.ClaimCount,
                                            ReleaseDate = item.ReleaseDate,
                                            ExpirationDate = item.ExpirationDate,
                                            TargettingParams = item.TargettingParams,
                                            DisplayImgId = item.DisplayImageId,
                                            Rules = item.Rules ?? Resources.NoRulesAvailable,
                                            Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                            ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                            LastBroadcastingUsage = null,//Not needed
                                            BroadcastingTimerType = -1,//Not needed
                                            BroadcastingTimerTypeName = "",//Not needed
                                            BroadcastingScheduleType = -1,//Not needed
                                            BroadcastingScheduleTypeName = "",//Not needed
                                            BroadcastingMinsToRedeem = -1,//Not needed
                                            BroadcastingTitle = "",//Not needed
                                            BroadcastingMsg = "",//Not needed
                                            RelevanceRate = item.RelevanceRate,
                                            CreatedDate = item.CreatedDate,
                                            UpdatedDate = DateTime.UtcNow,//Not needed
                                            SatisfactionScore = item.SatisfactionScore,
                                            RelevanceScore = item.RelevanceScore
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

                                    offers.Add(offer);
                                }
                            }
                        }

                        break;
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
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
        private List<FlattenedOfferData> GetOfferDetails(Guid offerId, string userId, int offerPurpose, DateTime dateTime)
        {
            List<FlattenedOfferData> offers = null;

            try
            {
                var query = from x in this._businessObjects.FuncsHandler.GetOfferDetailsWithLocations(offerId, userId, offerPurpose, dateTime)
                            where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                            select x;

                if (query != null)
                {
                    FlattenedOfferData offer;

                    offers = new List<FlattenedOfferData>();

                    foreach (TempoffersPreferenceBranches item in query)
                    {
                        if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                        {
                            offer = new FlattenedOfferData
                            {
                                SelectorType = ContentFilters.Category,
                                Offer = new Offer
                                {
                                    Id = item.Id,
                                    TenantId = item.TenantId,
                                    MainCategoryId = item.MainCategoryId,
                                    MainCategoryName = "",//Not needed
                                    OfferType = item.OfferType,
                                    OfferTypeName = "",//Not needed
                                    DealType = item.DealType,
                                    DealTypeName = "",//Not needed
                                    RewardType = item.RewardType,
                                    RewardTypeName = "",//Not needed
                                    PurposeType = item.PurposeType,
                                    PurposeTypeName = "",//Not needed
                                    GeoSegmentationType = item.GeoSegmentationType,
                                    GeoSegmentationTypeName = "",//Not needed
                                    DisplayType = item.DisplayType,
                                    DisplayTypeName = "",//Not needed
                                    Name = item.Name,
                                    MainHint = item.MainHint,
                                    ComplementaryHint = item.ComplementaryHint,
                                    Keywords = item.Keywords,
                                    Description = item.Description,
                                    Code = "",//Not needed
                                    CodeImg = null,//Not needed
                                    MinsToUnlock = item.MinsToUnlock,
                                    IsActive = item.IsActive,
                                    IsExclusive = item.IsExclusive,
                                    IsSponsored = item.IsSponsored,
                                    HasUniqueCodes = false,//Not needed
                                    HasPreferences = item.HasPreferences,
                                    AvailableQuantity = item.AvailableQuantity,
                                    OneTimeRedemption = item.OneTimeRedemption,
                                    MaxClaimsPerUser = item.MaxClaimsPerUser,
                                    MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                    PurchasesCountStartDate = item.PurchasesCountStartDate,
                                    ClaimLocation = item.ClaimLocation,
                                    Value = item.Value,
                                    RegularValue = item.RegularValue,
                                    ExtraBonus = item.ExtraBonus,
                                    ExtraBonusType = item.ExtraBonusType,
                                    ExtraBonusTypeName = "",//Not needed
                                    MinIncentive = -1,//Not needed
                                    MaxIncentive = -1,//Not needed
                                    IncentiveVariationType = -1,//Not needed
                                    IncentiveVarationTypeName = "",//Not needed
                                    IncentiveVariation = -1,//Not needed
                                    SecondsIncentiveVariationFrame = -1,//Not needed
                                    RedeemCount = item.RedeemCount,
                                    ClaimCount = item.ClaimCount,
                                    ReleaseDate = item.ReleaseDate,
                                    ExpirationDate = item.ExpirationDate,
                                    TargettingParams = item.TargettingParams,
                                    DisplayImgId = item.DisplayImageId,
                                    Rules = item.Rules ?? Resources.NoRulesAvailable,
                                    Conditions = item.Conditions ?? Resources.NoConditionsAvailable,
                                    ClaimInstructions = item.ClaimInstructions ?? Resources.NoClaimInstructionsAvailable,
                                    LastBroadcastingUsage = null,//Not needed
                                    BroadcastingTimerType = -1,//Not needed
                                    BroadcastingTimerTypeName = "",//Not needed
                                    BroadcastingScheduleType = -1,//Not needed
                                    BroadcastingScheduleTypeName = "",//Not needed
                                    BroadcastingMinsToRedeem = -1,//Not needed
                                    BroadcastingTitle = "",//Not needed
                                    BroadcastingMsg = "",//Not needed
                                    RelevanceRate = item.RelevanceRate,
                                    CreatedDate = item.CreatedDate,
                                    UpdatedDate = DateTime.UtcNow,//Not needed
                                    SatisfactionScore = item.SatisfactionScore,
                                    RelevanceScore = item.RelevanceScore
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
                                Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                                {
                                    Id = item.PreferenceId,
                                    Name = item.PreferenceName,
                                    Icon = item.PreferenceIcon,
                                    RelevanceScore = item.PreferenceScore
                                },
                                ExactLocationBased = false
                            };

                            offers.Add(offer);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
        }

        private void BuildFullOfferList(ref List<FullOfferData> offersData, ref List<OfferDataWithBranches> enabledOffers, bool includeBranchList, bool includeNearestBranch)
        {
            OfferDataWithBranches currentOffer;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            BasicBranchData nearestBranch;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            int? branchGroupsCount;

            List<BasicBranchData> enabledBranches = null;

            for (int i = 0; i < enabledOffers.Count; i++)
            {
                currentOffer = enabledOffers[i];

                if (currentOffer.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                {
                    enabledLocations = this._businessObjects.ContentLocations.Gets(enabledOffers[i].Offer.Id);
                }
                else
                {
                    enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                }

                availableBranches = currentOffer.Branches.GroupBy(x => x.Id)
                                                                       .Select(grp => grp.First())
                                                                       .ToList();
                enabledBranches = new List<BasicBranchData>();

                if (currentOffer.Offer.DealType == DealTypes.InStore || currentOffer.Offer.DealType == DealTypes.Catalog)
                {
                    //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                    switch (currentOffer.Offer.GeoSegmentationType)
                    {
                        case GeoSegmentationTypes.State:
                            //Will group by state
                            branchesGrouped = currentOffer.Branches.GroupBy(x => x.StateId);
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
                            branchesGrouped = currentOffer.Branches.GroupBy(x => x.CityId);
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
                            //This means the offer is available to all the branches in the country
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
                        currentOffer.Tenant.NearestBranchId = nearestBranch.Id;
                        currentOffer.Tenant.NearestBranchName = nearestBranch.Name;
                        currentOffer.Tenant.NearestBranchLatitude = nearestBranch.Latitude;
                        currentOffer.Tenant.NearestBranchLongitude = nearestBranch.Longitude;
                        currentOffer.Tenant.NearesBranchDistance = nearestBranch.Distance;
                    }

                    if (includeBranchList)
                    {
                        offersData.Add(new FullOfferData
                        {
                            SelectorType = currentOffer.SelectorType,
                            Offer = currentOffer.Offer,
                            Tenant = currentOffer.Tenant,
                            Branches = enabledBranches,
                            Preference = currentOffer.Preference
                        }
                        );
                    }
                    else
                    {
                        offersData.Add(new FullOfferData
                        {
                            SelectorType = currentOffer.SelectorType,
                            Offer = currentOffer.Offer,
                            Tenant = currentOffer.Tenant,
                            Branches = new List<BasicBranchData>(),
                            Preference = currentOffer.Preference
                        }
                        );
                    }

                }

            }
        }

        private void BuildFullOfferList(ref List<FullOfferData> offersData, ref List<OfferDataWithBranches> enabledOffers, bool includeBranchList)
        {
            OfferDataWithBranches currentOffer;
            List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>> enabledLocations = null;
            List<BasicBranchData> availableBranches;
            IEnumerable<IGrouping<Guid, BasicBranchData>> branchesGrouped;
            int? branchGroupsCount;

            if (!includeBranchList)
            {
                bool locationMatch = false;

                for (int i = 0; i < enabledOffers.Count; i++)
                {
                    currentOffer = enabledOffers[i];
                    locationMatch = false;

                    //This means it's enabled for the complete country, then it's enabled for all the potential branches
                    if (currentOffer.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                    {
                        if (currentOffer.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                        {
                            enabledLocations = this._businessObjects.ContentLocations.Gets(enabledOffers[i].Offer.Id);
                        }
                        else
                        {
                            enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                        }

                        availableBranches = currentOffer.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();

                        if (enabledLocations?.Count > 0)
                        {

                            //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                            switch (currentOffer.Offer.GeoSegmentationType)
                            {
                                case GeoSegmentationTypes.State:
                                    //Will group by state
                                    branchesGrouped = currentOffer.Branches.GroupBy(x => x.StateId);
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
                                    branchesGrouped = currentOffer.Branches.GroupBy(x => x.CityId);
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

                        offersData.Add(new FullOfferData
                        {
                            SelectorType = currentOffer.SelectorType,
                            Offer = currentOffer.Offer,
                            Preference = currentOffer.Preference,
                            Tenant = currentOffer.Tenant,
                            Branches = new List<BasicBranchData>()
                        }
                        );
                    }

                }
            }
            else
            {
                List<BasicBranchData> enabledBranches = null;

                for (int i = 0; i < enabledOffers.Count; i++)
                {
                    currentOffer = enabledOffers[i];

                    if (currentOffer.Offer.GeoSegmentationType != GeoSegmentationTypes.Country)
                    {
                        enabledLocations = this._businessObjects.ContentLocations.Gets(enabledOffers[i].Offer.Id);
                    }
                    else
                    {
                        enabledLocations = new List<DTO.Entities.Misc.Structure.POCO.Pair<int, Guid>>();
                    }

                    availableBranches = currentOffer.Branches.GroupBy(x => x.Id)
                                                                           .Select(grp => grp.First())
                                                                           .ToList();
                    enabledBranches = new List<BasicBranchData>();

                    if (currentOffer.Offer.DealType == DealTypes.InStore || currentOffer.Offer.DealType == DealTypes.Catalog)
                    {
                        //Depending on the geosegmentation the offer has, we will group the offers by either state or city
                        switch (currentOffer.Offer.GeoSegmentationType)
                        {
                            case GeoSegmentationTypes.State:
                                //Will group by state
                                branchesGrouped = currentOffer.Branches.GroupBy(x => x.StateId);
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
                                branchesGrouped = currentOffer.Branches.GroupBy(x => x.CityId);
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


                    offersData.Add(new FullOfferData
                    {
                        SelectorType = currentOffer.SelectorType,
                        Offer = currentOffer.Offer,
                        Tenant = currentOffer.Tenant,
                        Branches = enabledBranches,
                        Preference = currentOffer.Preference
                    }
                    );

                }
            }
        }

        public List<FullOfferData> GetEnabledOffersByRegionWithLocation(Guid countryId, Guid stateId, int contentSegmentationType, string userId, decimal latitude, decimal longitude, double radius, DateTime dateTime, int selectorType, bool includeBranchList, bool includeNearestBranch, int offerPurpose)
        {
            List<FullOfferData> offersData = new List<FullOfferData>();

            try
            {
                //For large countries segmentation is by state, for small ones by country
                List<FlattenedOfferData> flattenedOffers = this.GetOffersDataByRegionWithLocation(countryId, stateId, contentSegmentationType, userId, latitude, longitude, radius, dateTime, selectorType, offerPurpose);

                if (flattenedOffers?.Count > 0)
                {
                    OfferDataWithBranches currentOffer;
                    IEnumerable<IGrouping<Guid, FlattenedOfferData>> groupedByOfferId = flattenedOffers.GroupBy(x => x.Offer.Id);
                    List<OfferDataWithBranches> enabledOffers = new List<OfferDataWithBranches>();
                    FlattenedOfferData[] offersGroup = null;

                    foreach (IGrouping<Guid, FlattenedOfferData> offerDataGroup in groupedByOfferId)
                    {
                        offersGroup = offerDataGroup.ToArray();

                        currentOffer = new OfferDataWithBranches
                        {
                            SelectorType = offersGroup[0].SelectorType,
                            Offer = offersGroup[0].Offer,
                            Preference = offersGroup[0].Preference,
                            Tenant = offersGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            ExactLocationBased = offersGroup[0].ExactLocationBased
                        };

                        for (int i = 0; i < offersGroup.Length; ++i)
                        {
                            currentOffer.Branches.Add(offersGroup[i].Branch);
                        }

                        enabledOffers.Add(currentOffer);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled


                    this.BuildFullOfferList(ref offersData, ref enabledOffers, includeBranchList, includeNearestBranch);


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

        public List<FullOfferData> GetEnabledOffersByRegion(Guid stateId, Guid countryId, int contentSegmentationType, string userId, DateTime dateTime, int selectorType, bool includeBranchList, int offerPurpose)
        {
            List<FullOfferData> offersData = new List<FullOfferData>();

            try
            {
                //For large countries segmentation is by state, for small ones by country
                List<FlattenedOfferData> flattenedOffers = this.GetOffersDataByRegion(stateId, countryId, contentSegmentationType, userId, dateTime, selectorType, offerPurpose);

                if (flattenedOffers?.Count > 0)
                {
                    OfferDataWithBranches currentOffer;
                    IEnumerable<IGrouping<Guid, FlattenedOfferData>> groupedByOfferId = flattenedOffers.GroupBy(x => x.Offer.Id);
                    List<OfferDataWithBranches> enabledOffers = new List<OfferDataWithBranches>();
                    FlattenedOfferData[] offersGroup = null;

                    foreach (IGrouping<Guid, FlattenedOfferData> offerDataGroup in groupedByOfferId)
                    {
                        offersGroup = offerDataGroup.ToArray();

                        currentOffer = new OfferDataWithBranches
                        {
                            Offer = offersGroup[0].Offer,
                            Preference = offersGroup[0].Preference,
                            Tenant = offersGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            ExactLocationBased = offersGroup[0].ExactLocationBased
                        };

                        for (int i = 0; i < offersGroup.Length; ++i)
                        {
                            currentOffer.Branches.Add(offersGroup[i].Branch);
                        }

                        enabledOffers.Add(currentOffer);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled

                    this.BuildFullOfferList(ref offersData, ref enabledOffers, includeBranchList);

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

        public List<FullOfferData> GetEnabledOffersByRegionForReference(Guid stateId, Guid countryId, int contentSegmentationType, string userId, DateTime dateTime, int referenceType, Guid referenceId, bool includeBranchList, int offerPurpose)
        {
            List<FullOfferData> offersData = new List<FullOfferData>();

            try
            {
                List<FlattenedOfferData> flattenedOffers = this.GetOffersDataByRegionForReference(stateId, countryId, contentSegmentationType, userId, dateTime, referenceType, referenceId, offerPurpose);

                if (flattenedOffers?.Count > 0)
                {
                    OfferDataWithBranches currentOffer;
                    IEnumerable<IGrouping<Guid, FlattenedOfferData>> groupedByOfferId = flattenedOffers.GroupBy(x => x.Offer.Id);
                    List<OfferDataWithBranches> enabledOffers = new List<OfferDataWithBranches>();
                    FlattenedOfferData[] offersGroup = null;

                    foreach (IGrouping<Guid, FlattenedOfferData> offerDataGroup in groupedByOfferId)
                    {
                        offersGroup = offerDataGroup.ToArray();

                        currentOffer = new OfferDataWithBranches
                        {
                            Offer = offersGroup[0].Offer,
                            Preference = offersGroup[0].Preference,
                            Tenant = offersGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            ExactLocationBased = offersGroup[0].ExactLocationBased
                        };

                        for (int i = 0; i < offersGroup.Length; ++i)
                        {
                            currentOffer.Branches.Add(offersGroup[i].Branch);
                        }

                        enabledOffers.Add(currentOffer);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled

                    this.BuildFullOfferList(ref offersData, ref enabledOffers, includeBranchList);
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

        public FullOfferData GetOfferData(Guid id, string userId, bool includeBranchList, int purposeType, DateTime dateTime)
        {
            FullOfferData offer = null;

            try
            {
                List<FlattenedOfferData> flattenedOffers = this.GetOfferDetails(id, userId, purposeType, dateTime);

                if (flattenedOffers?.Count > 0)
                {
                    OfferDataWithBranches currentOffer;
                    IEnumerable<IGrouping<Guid, FlattenedOfferData>> groupedByOfferId = flattenedOffers.GroupBy(x => x.Offer.Id);
                    List<OfferDataWithBranches> enabledOffers = new List<OfferDataWithBranches>();
                    FlattenedOfferData[] offersGroup = null;

                    foreach (IGrouping<Guid, FlattenedOfferData> offerDataGroup in groupedByOfferId)
                    {
                        offersGroup = offerDataGroup.ToArray();

                        currentOffer = new OfferDataWithBranches
                        {
                            Offer = offersGroup[0].Offer,
                            Preference = offersGroup[0].Preference,
                            Tenant = offersGroup[0].Tenant,
                            Branches = new List<BasicBranchData>(),
                            ExactLocationBased = offersGroup[0].ExactLocationBased
                        };

                        for (int i = 0; i < offersGroup.Length; ++i)
                        {
                            currentOffer.Branches.Add(offersGroup[i].Branch);
                        }

                        enabledOffers.Add(currentOffer);
                    }

                    //At this point the offers have all the branches where it can be enabled, now it's time to verify in which branches
                    //each offer can be actually enabled
                    List<FullOfferData> offersData = new List<FullOfferData>();

                    this.BuildFullOfferList(ref offersData, ref enabledOffers, includeBranchList);

                    if (offersData?.Count > 0)
                    {
                        offer = offersData.ElementAt(0);
                    }
                }
            }
            catch (Exception e)
            {
                offer = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offer;
        }

        #endregion

        #region OFFERS METRICS


        public int? GetOffersCount(Guid refId, int refType, DateTime dateTime, int offerPurpose)
        {
            int? count = 0;

            try
            {

                switch (refType)
                {
                    case SearchableObjectTypes.Category:
                        count = this._businessObjects.StoredProcsHandler.GetOffersCountForPreference(refId, dateTime, offerPurpose);
                        break;
                    case SearchableObjectTypes.Commerce:
                        count = this._businessObjects.StoredProcsHandler.GetOffersCountForCommerce(refId, dateTime, offerPurpose);
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
        public OfferManager(BusinessObjects businessObjects)
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
