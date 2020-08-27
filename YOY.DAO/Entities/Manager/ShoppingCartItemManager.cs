using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class ShoppingCartItemManager
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

        public List<ShoppingCartItem> Gets(string userId, Guid? tenantId, Guid? offerId, int activeState, int expiredState, DateTime date, int pageSize, int pageNumber)
        {
            List<ShoppingCartItem> shoppingCartItems;

            try
            {
                var query = (dynamic)null;

                if(!string.IsNullOrWhiteSpace(userId))
                {
                    if(tenantId != null)
                    {
                        if (offerId != null)
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.TenantId == tenantId && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.TenantId == tenantId && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.TenantId == tenantId && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Active:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.TenantId == tenantId && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.TenantId == tenantId && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.TenantId == tenantId && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Inactive:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.TenantId == tenantId && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.TenantId == tenantId && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.TenantId == tenantId && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.TenantId == tenantId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.TenantId == tenantId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.TenantId == tenantId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Active:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.TenantId == tenantId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.TenantId == tenantId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.TenantId == tenantId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Inactive:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.TenantId == tenantId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.TenantId == tenantId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.TenantId == tenantId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (offerId != null)
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Active:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Inactive:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Active:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && x.IsActive && x.OfferIsActive && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Inactive:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive)
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.UserId == userId && (!x.IsActive || !x.OfferIsActive) && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    if (tenantId != null)
                    {
                        if (offerId != null)
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Active:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.IsActive && x.OfferIsActive && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.IsActive && x.OfferIsActive && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.IsActive && x.OfferIsActive && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Inactive:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && (!x.IsActive || !x.OfferIsActive) && x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && (!x.IsActive || !x.OfferIsActive) && x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && (!x.IsActive || !x.OfferIsActive) && x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;

                            }
                        }
                        else
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Active:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.IsActive && x.OfferIsActive
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.IsActive && x.OfferIsActive && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && x.IsActive && x.OfferIsActive && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Inactive:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && (!x.IsActive || !x.OfferIsActive)
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && (!x.IsActive || !x.OfferIsActive) && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.TenantId == tenantId && (!x.IsActive || !x.OfferIsActive) && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (offerId != null)
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Active:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId && x.IsActive && x.OfferIsActive
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId && x.IsActive && x.OfferIsActive && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId && x.IsActive && x.OfferIsActive && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Inactive:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId && (!x.IsActive || !x.OfferIsActive)
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId && (!x.IsActive || !x.OfferIsActive) && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.OfferId == offerId && (!x.IsActive || !x.OfferIsActive) && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Active:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.IsActive && x.OfferIsActive
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.IsActive && x.OfferIsActive && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where x.IsActive && x.OfferIsActive && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                                case ActiveStates.Inactive:
                                    switch (expiredState)
                                    {
                                        case ExpiredStates.All:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where !x.IsActive || !x.OfferIsActive
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Valid:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where (!x.IsActive || !x.OfferIsActive) && x.ExpirationDate > date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                        case ExpiredStates.Expired:
                                            query = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                     where (!x.IsActive || !x.OfferIsActive) && x.ExpirationDate <= date
                                                     orderby x.CreatedDate descending
                                                     select x).Skip(pageSize * pageNumber).Take(pageSize);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }

                if(query != null)
                {
                    shoppingCartItems = new List<ShoppingCartItem>();
                    ShoppingCartItem cartItem;

                    foreach(OltpshoppingCartItemsView item in query)
                    {
                        cartItem = new ShoppingCartItem
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            UserId = item.UserId,
                            OfferId = item.OfferId,
                            Quantity = item.Quantity,
                            HasPreferences = item.HasPreferences,
                            ChosenPreferences = XElement.Parse(item.ChosenPreferences),
                            AdditionalNotes = item.AdditionalNotes,
                            IsActive = (bool)item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate,
                            Offer = new Offer
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
                                IsActive = item.OfferIsActive,
                                IsExclusive = item.IsExclusive,
                                IsSponsored = item.IsSponsored,
                                HasUniqueCodes = item.HasUniqueCodes,
                                HasPreferences = item.OfferHasPreferences,
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
                                LastBroadcastingUsage = null,
                                BroadcastingTimerType = -1,
                                BroadcastingTimerTypeName = "",
                                BroadcastingScheduleType = -1,
                                BroadcastingScheduleTypeName = "",
                                BroadcastingMinsToRedeem = -1,
                                BroadcastingTitle = "",
                                BroadcastingMsg = "",
                                RelevanceRate = item.RelevanceRate,
                                CreatedDate = item.OfferCreatedDate,
                                UpdatedDate = item.OfferUpdatedDate
                            }
                        };
                    }
                }
                else
                {
                    shoppingCartItems = new List<ShoppingCartItem>();
                }
            }
            catch(Exception e)
            {
                shoppingCartItems = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return shoppingCartItems;
        }

        public ShoppingCartItem Get(Guid id)
        {
            ShoppingCartItem shoppingCartItem;

            try
            {
                OltpshoppingCartItemsView item = (from x in this._businessObjects.Context.OltpshoppingCartItemsView
                                                                       where x.Id == id
                                                                       select x).FirstOrDefault();

                if(item != null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        Id = item.Id,
                        TenantId = item.TenantId,
                        UserId = item.UserId,
                        OfferId = item.OfferId,
                        Quantity = item.Quantity,
                        HasPreferences = item.HasPreferences,
                        ChosenPreferences = XElement.Parse(item.ChosenPreferences),
                        AdditionalNotes = item.AdditionalNotes,
                        IsActive = (bool)item.IsActive,
                        CreatedDate = item.CreatedDate,
                        UpdatedDate = item.UpdatedDate,
                        Offer = new Offer
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
                            IsActive = item.OfferIsActive,
                            IsExclusive = item.IsExclusive,
                            IsSponsored = item.IsSponsored,
                            HasUniqueCodes = item.HasUniqueCodes,
                            HasPreferences = item.OfferHasPreferences,
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
                            LastBroadcastingUsage = null,
                            BroadcastingTimerType = -1,
                            BroadcastingTimerTypeName = "",
                            BroadcastingScheduleType = -1,
                            BroadcastingScheduleTypeName = "",
                            BroadcastingMinsToRedeem = -1,
                            BroadcastingTitle = "",
                            BroadcastingMsg = "",
                            RelevanceRate = item.RelevanceRate,
                            CreatedDate = item.OfferCreatedDate,
                            UpdatedDate = item.OfferUpdatedDate
                        }
                    };
                }
                else
                {
                    shoppingCartItem = new ShoppingCartItem();
                }
            }
            catch(Exception e)
            {
                shoppingCartItem = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return shoppingCartItem;
        }

        public ShoppingCartItem Post(Guid tenantId, string userId, Guid offerId, int quantity, bool hasPreferences, XElement chosenPreferences, string additionalNotes)
        {
            ShoppingCartItem shoppingCartItem;

            try
            {
                OltpshoppingCartItems newCartItem = new OltpshoppingCartItems
                {
                    Id = Guid.NewGuid(),
                    TenantId = tenantId,
                    UserId = userId,
                    OfferId = offerId,
                    Quantity = quantity,
                    HasPreferences = hasPreferences,
                    ChosenPreferences = chosenPreferences != null ? chosenPreferences.ToString() : null,
                    AdditionalNotes = additionalNotes,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpshoppingCartItems.Add(newCartItem);
                this._businessObjects.Context.SaveChanges();

                shoppingCartItem = new ShoppingCartItem
                {
                    Id = newCartItem.Id,
                    TenantId = newCartItem.TenantId,
                    UserId = newCartItem.UserId,
                    OfferId = newCartItem.OfferId,
                    Quantity = newCartItem.Quantity,
                    HasPreferences = newCartItem.HasPreferences,
                    ChosenPreferences = XElement.Parse(newCartItem.ChosenPreferences),
                    AdditionalNotes = newCartItem.AdditionalNotes,
                    IsActive = (bool)newCartItem.IsActive,
                    CreatedDate = newCartItem.CreatedDate,
                    UpdatedDate = newCartItem.UpdatedDate
                };
            }
            catch(Exception e)
            {
                shoppingCartItem = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return shoppingCartItem;
        }

        public ShoppingCartItem Put(Guid id, int? quantity, string additionalNotes, XElement chosenPreferences)
        {
            ShoppingCartItem shoppingCartItem;

            try
            {
                OltpshoppingCartItems currentCartItem = (from x in this._businessObjects.Context.OltpshoppingCartItems
                                                            where x.Id == id
                                                            select x).FirstOrDefault();

                if(currentCartItem != null)
                {
                    if(chosenPreferences == null && string.IsNullOrEmpty(additionalNotes))
                    {
                        //In this case is necessary justo to update quantity
                        if (quantity != null)
                        {
                            currentCartItem.Quantity = (int)quantity;
                        }
                    }
                    else
                    {
                        if (quantity != null)
                        {
                            currentCartItem.Quantity = (int)quantity;
                        }

                        if (chosenPreferences != null)
                        {
                            currentCartItem.ChosenPreferences = chosenPreferences.ToString();
                        }

                        if (additionalNotes != null)
                        {
                            currentCartItem.AdditionalNotes = additionalNotes;
                        }
                    }
                    

                    currentCartItem.UpdatedDate = DateTime.UtcNow;

                    if(quantity == 0)//Needs to be deleted
                    {
                        this._businessObjects.Context.OltpshoppingCartItems.Remove(currentCartItem);
                    }

                    this._businessObjects.Context.SaveChanges();

                    shoppingCartItem = new ShoppingCartItem
                    {
                        Id = currentCartItem.Id,
                        TenantId = currentCartItem.TenantId,
                        UserId = currentCartItem.UserId,
                        OfferId = currentCartItem.OfferId,
                        Quantity = currentCartItem.Quantity,
                        HasPreferences = currentCartItem.HasPreferences,
                        ChosenPreferences = XElement.Parse(currentCartItem.ChosenPreferences),
                        AdditionalNotes = currentCartItem.AdditionalNotes,
                        IsActive = (bool)currentCartItem.IsActive,
                        CreatedDate = currentCartItem.CreatedDate,
                        UpdatedDate = currentCartItem.UpdatedDate
                    };
                }
                else
                {
                    shoppingCartItem = null;
                }
            }
            catch(Exception e)
            {
                shoppingCartItem = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return shoppingCartItem;
        }

        public ShoppingCartItem Delete(Guid id)
        {
            ShoppingCartItem shoppingCartItem;

            try
            {
                OltpshoppingCartItems currentCartItem = (from x in this._businessObjects.Context.OltpshoppingCartItems
                                                             where x.Id == id
                                                             select x).FirstOrDefault();

                if (currentCartItem != null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        Id = currentCartItem.Id,
                        TenantId = currentCartItem.TenantId,
                        UserId = currentCartItem.UserId,
                        OfferId = currentCartItem.OfferId,
                        Quantity = currentCartItem.Quantity,
                        HasPreferences = currentCartItem.HasPreferences,
                        ChosenPreferences = XElement.Parse(currentCartItem.ChosenPreferences),
                        AdditionalNotes = currentCartItem.AdditionalNotes,
                        IsActive = (bool)currentCartItem.IsActive,
                        CreatedDate = currentCartItem.CreatedDate,
                        UpdatedDate = currentCartItem.UpdatedDate
                    };

                    this._businessObjects.Context.OltpshoppingCartItems.Remove(currentCartItem);

                    this._businessObjects.Context.SaveChanges();

                    
                }
                else
                {
                    shoppingCartItem = null;
                }
            }
            catch (Exception e)
            {
                shoppingCartItem = null;

                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return shoppingCartItem;
        }

        #endregion

        #region CONSTRUCTORS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTOR                                                                                                                              //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Creates a new ShoppingCartItemManager with its specific businessObject
        /// </summary>
        /// <param name="businessObjects"></param>
        /// <exception cref="ArgumentNullException">businessObjects is not set to an instance of an object</exception>
        public ShoppingCartItemManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD SHOPPING CART ITEM MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
