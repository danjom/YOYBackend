using System;
using System.Collections.Generic;
using System.Linq;
using YOY.DAO.Entities.DB;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.Offer;
using YOY.Values;
using YOY.Values.Strings;

namespace YOY.DAO.Entities.Manager
{
    public class SavedItemManager
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

        public string GetReferenceTypeName(int referenceType)
        {
            string referenceTypeName = referenceType switch
            {
                SavedItemReferenceTypes.Offer => Resources.Offer,
                SavedItemReferenceTypes.CashbackIncentive => Resources.CashbackIncentive,
                SavedItemReferenceTypes.ProductItem => Resources.Product,
                _ => "--"
            };

            return referenceTypeName;
        }

        public List<SavedItem> Gets(string userId, int referenceType, Guid? tenantId, Guid? tenantHolderId, int activeState, int pageSize, int pageNumber)
        {
            List<SavedItem> savedItems = null;

            try
            {
                var query = (dynamic)null;

                if(referenceType != SavedItemReferenceTypes.All)
                {
                    if(tenantId != null)
                    {
                        if(tenantHolderId != null)
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.UserId == userId && x.TenantId == tenantId && x.TenantHolderId == tenantHolderId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Active:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.IsActive && x.UserId == userId && x.TenantId == tenantId && x.TenantHolderId == tenantHolderId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Inactive:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where !x.IsActive && x.UserId == userId && x.TenantId == tenantId && x.TenantHolderId == tenantHolderId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                        }
                        else
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.UserId == userId && x.TenantId == tenantId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Active:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.IsActive && x.UserId == userId && x.TenantId == tenantId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Inactive:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where !x.IsActive && x.UserId == userId && x.TenantId == tenantId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }

                        }
                    }
                    else
                    {
                        if (tenantHolderId != null)
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.UserId == userId && x.TenantHolderId == tenantHolderId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Active:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.IsActive && x.UserId == userId && x.TenantHolderId == tenantHolderId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Inactive:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where !x.IsActive && x.UserId == userId && x.TenantHolderId == tenantHolderId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                        }
                        else
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.UserId == userId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Active:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.IsActive && x.UserId == userId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Inactive:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where !x.IsActive && x.UserId == userId && x.ReferenceType == referenceType
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }

                        }

                    }
                }
                else
                {
                    if (tenantId != null)
                    {
                        if (tenantHolderId != null)
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.UserId == userId && x.TenantId == tenantId && x.TenantHolderId == tenantHolderId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Active:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.IsActive && x.UserId == userId && x.TenantId == tenantId && x.TenantHolderId == tenantHolderId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Inactive:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where !x.IsActive && x.UserId == userId && x.TenantId == tenantId && x.TenantHolderId == tenantHolderId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                        }
                        else
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.UserId == userId && x.TenantId == tenantId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Active:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.IsActive && x.UserId == userId && x.TenantId == tenantId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Inactive:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where !x.IsActive && x.UserId == userId && x.TenantId == tenantId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }

                        }
                    }
                    else
                    {
                        if (tenantHolderId != null)
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.UserId == userId && x.TenantHolderId == tenantHolderId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Active:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.IsActive && x.UserId == userId && x.TenantHolderId == tenantHolderId 
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Inactive:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where !x.IsActive && x.UserId == userId && x.TenantHolderId == tenantHolderId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }
                        }
                        else
                        {
                            switch (activeState)
                            {
                                case ActiveStates.All:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.UserId == userId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Active:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where x.IsActive && x.UserId == userId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                                case ActiveStates.Inactive:
                                    query = (from x in this._businessObjects.Context.OltpsavedItems
                                             where !x.IsActive && x.UserId == userId
                                             select x).Skip(pageSize * pageNumber).Take(pageSize);
                                    break;
                            }

                        }

                    }
                }
                
                if(query != null)
                {
                    savedItems = new List<SavedItem>();
                    SavedItem savedItem;

                    foreach(OltpsavedItems item in query)
                    {
                        savedItem = new SavedItem
                        {
                            Id = item.Id,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetReferenceTypeName(item.ReferenceType),
                            TenantHolderId = item.TenantHolderId,
                            TenantId = item.TenantId,
                            UserId = item.UserId,
                            IsActive = item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };

                        savedItems.Add(savedItem);
                    }
                }
            }
            catch(Exception e)
            {
                savedItems = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return savedItems;
        }

        public SavedItem Get(int referenceType, Guid referenceId)
        {
            SavedItem savedItem = null;

            try
            {
                var query = from x in this._businessObjects.Context.OltpsavedItems
                            where x.ReferenceType == referenceType && x.ReferenceId == referenceId
                            select x;

                if(query != null)
                {
                    foreach(OltpsavedItems item in query)
                    {
                        savedItem = new SavedItem
                        {
                            Id = item.Id,
                            ReferenceId = item.ReferenceId,
                            ReferenceType = item.ReferenceType,
                            ReferenceTypeName = GetReferenceTypeName(item.ReferenceType),
                            TenantHolderId = item.TenantHolderId,
                            TenantId = item.TenantId,
                            UserId = item.UserId,
                            IsActive = item.IsActive,
                            CreatedDate = item.CreatedDate,
                            UpdatedDate = item.UpdatedDate
                        };
                    }
                }
            }
            catch(Exception e)
            {
                savedItem = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return savedItem;
        }

        public SavedItem Post(Guid referenceId, int referenceType, Guid tenantId, Guid? tenantHolderId, string userId)
        {
            SavedItem savedItem;

            try
            {
                OltpsavedItems newSavedItem = new OltpsavedItems
                {
                    Id = Guid.NewGuid(),
                    ReferenceId = referenceId,
                    ReferenceType = referenceType,
                    TenantId = tenantId,
                    TenantHolderId = tenantId,
                    UserId = userId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                this._businessObjects.Context.OltpsavedItems.Add(newSavedItem);
                this._businessObjects.Context.SaveChanges();

                savedItem = new SavedItem
                {
                    Id = newSavedItem.Id,
                    ReferenceId = newSavedItem.ReferenceId,
                    ReferenceType = newSavedItem.ReferenceType,
                    ReferenceTypeName = GetReferenceTypeName(newSavedItem.ReferenceType),
                    TenantHolderId = newSavedItem.TenantHolderId,
                    TenantId = newSavedItem.TenantId,
                    UserId = newSavedItem.UserId,
                    IsActive = newSavedItem.IsActive,
                    CreatedDate = newSavedItem.CreatedDate,
                    UpdatedDate = newSavedItem.UpdatedDate
                };
            }
            catch(Exception e)
            {
                savedItem = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return savedItem;
        }

        public bool Delete(Guid referenceId, int referenceType, string userId)
        {
            bool success = false;

            try
            {
                OltpsavedItems currentSavedItem = (from x in this._businessObjects.Context.OltpsavedItems
                                                   where x.UserId == userId && x.ReferenceType == referenceType && x.ReferenceId == referenceId
                                                   select x).FirstOrDefault();

                if (currentSavedItem != null)
                {
                    this._businessObjects.Context.OltpsavedItems.Remove(currentSavedItem);

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

        #region SAVEDCONTENTFULLDATA

        public List<FlattenedOfferData> GetSavedOffersForUser(string userId, Guid countryId, Guid stateId, int contentSegmentationType, int offerPurpose, DateTime dateTime, int pageSize, int pageNumber)
        {
            List<FlattenedOfferData> offers = null;

            try
            {
                var query = (dynamic)null;

                switch (contentSegmentationType)
                {
                    case GeoSegmentationTypes.Country:

                        query = from x in this._businessObjects.FuncsHandler.GetSavedOffersForUserByCountry(countryId, userId, offerPurpose, dateTime, pageSize, pageNumber)
                                where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                select x;
                        break;
                    case GeoSegmentationTypes.State:

                        query = from x in this._businessObjects.FuncsHandler.GetSavedOffersForUserByState(countryId, stateId, userId, offerPurpose, dateTime, pageSize, pageNumber)
                                where x.AvailableQuantity == -1 || x.AvailableQuantity > 0
                                select x;
                        break;
                }

                if (query != null)
                {
                    FlattenedOfferData offer;
                    offers = new List<FlattenedOfferData>();

                    foreach (TempofferDisplayContents item in query)
                    {
                        if (item.AvailableQuantity == -1 || item.AvailableQuantity > 0)
                        {
                            offer = new FlattenedOfferData
                            {
                                SelectorType = -1,
                                Offer = new DisplayOfferData
                                {
                                    Id = item.Id,
                                    TenantId = item.TenantId,
                                    MainCategoryId = item.MainCategoryId,
                                    OfferType = item.OfferType,
                                    DealType = item.DealType,
                                    PurposeType = item.PurposeType,
                                    RewardType = item.RewardType,
                                    GeoSegmentationType = item.GeoSegmentationType,
                                    DisplayType = item.DisplayType,
                                    MainHint = item.MainHint,
                                    ComplementaryHint = item.ComplementaryHint,
                                    ProductHint = item.ProductHint,
                                    Keywords = item.Keywords,
                                    Description = item.Description,
                                    MinsToUnlock = item.MinsToUnlock,
                                    IsActive = item.IsActive,
                                    IsSponsored = item.IsSponsored,
                                    IsExclusive = item.IsExclusive,
                                    HasPreferences = item.HasPreferences,
                                    Value = item.Value,
                                    RegularValue = item.RegularValue,
                                    ExtraBonus = item.ExtraBonus,
                                    ExtraBonusType = item.ExtraBonusType,
                                    AvailableQuantity = item.AvailableQuantity,
                                    OneTimeRedemption = item.OneTimeRedemption,
                                    MaxClaimsPerUser = item.MaxClaimsPerUser,
                                    MinPurchasesCountToRedeem = item.MinPurchasesCountToRedeem,
                                    PurchasesCountStartDate = item.PurchasesCountStartDate,
                                    ClaimCount = item.ClaimCount,
                                    ClaimLocation = item.ClaimLocation,
                                    DisplayImgUrl = item.DisplayImageUrl,
                                    Rules = item.Rules,
                                    Conditions = item.Conditions,
                                    ClaimInstructions = item.ClaimInstructions,
                                    RelevanceRate = item.RelevanceRate,
                                    RelevanceScoreForMainCategory = item.RelevanceScore,
                                    TargettingParams = item.TargettingParams,
                                    ReleaseDate = item.ReleaseDate,
                                    ExpirationDate = item.ExpirationDate,
                                    CreatedDate = item.CreatedDate
                                },
                                Tenant = new DTO.Entities.Misc.TenantData.BasicTenantData
                                {
                                    Id = item.TenantId,
                                    Name = item.TenantName,
                                    LogoUrl = item.TenantLogoUrl,
                                    WhiteLogoUrl = item.TenantWhiteLogoUrl,
                                    CountryId = item.TenantCountryId,
                                    CurrencySymbol = item.CurrencySymbol,
                                    CashbackPercentage = item.TenantCashbackPercentage,
                                    RelevanceStatus = item.TenantRelevanceStatus,
                                    Type = item.TenantType,
                                    CategoryId = item.TenantCategoryId,
                                    RelevanceScore = null,//When its selector is category, there is no info about tenant relevance

                                },
                                Preference = new DTO.Entities.Misc.InterestPreference.BasicUserPreferenceData
                                {
                                    Id = item.PreferenceId,
                                    Name = item.PreferenceName,
                                    Icon = item.PreferenceIcon,
                                    RelevanceScore = item.PreferenceScore
                                },
                                BranchHolder = new BasicBranchHolderData//In this case is irrelevant
                                {
                                    Id = Guid.Empty,
                                    Name = "",
                                    TenantName = "",
                                    RelevanceStatus = -1
                                },
                                ExactLocationBased = true
                            };

                            offers.Add(offer);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                offers = null;
                //ERROR HANDLING
                this._businessObjects.StoredProcsHandler.AddExceptionLogging(ExceptionLayers.DAO, this.GetType().Name, e.Message.ToString(), e.GetType().Name.ToString(), e.StackTrace.ToString(), "");

            }

            return offers;
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
        public SavedItemManager(BusinessObjects businessObjects)
        {
            if (businessObjects != null)
                this._businessObjects = businessObjects;
            else
            {
                throw new ArgumentNullException(nameof(businessObjects));
            } // ELSE ENDS
        } // METHOD PRODUCT PREFERENCE MANAGER ------------------------------------------------------------------------------------------------------------------------ //

        #endregion
    }
}
