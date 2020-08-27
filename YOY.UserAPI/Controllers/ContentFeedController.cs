using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.Location;
using YOY.DTO.Entities.Misc.TenantData;
using YOY.UserAPI.Logic.Image;
using YOY.UserAPI.Models.v1.Content.POCO;
using YOY.UserAPI.Models.v1.Content.SET;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class ContentFeedController : ControllerBase
    {
        #region PROPERTIES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private static Tenant _tenant;
        private BusinessObjects _businessObjects;
        private readonly IStringLocalizer<SharedResources> _localizer;


        private const string CategorySelectedAppend = "w";
        private const string CategoryUnSelectedAppend = "g";
        private const double slideWidthProp = 0.66666666666666667;
        private const double logoWithWidthProp = 1;
        private const double dealImgWidthProp = 1;
        private const double commerceCarrouselImgWidthProp = 0.375;

        private const int MaxMinsToStoreHeaderContent = 30;
        public const int MaxMinsToStoreBodyContent = 10;

        public const int MaxMetersToStoreContent = 1000;

        private const int MaxFilterValueCellsOnCarrousel = 12;
        private const int MaxContentCellsOnCarrousel = 12;
        private const int MaxContentCellsOnGrid = 16;

        private const int controllerVersion = 1;

        private const int filterOptionsPageSize = 36;
        private const int contentPageSize = 36;
        #endregion

        #region METHODS

        public ContentStructure BuildByCommerceFilterOptions(string userId, Guid byCommerceFilterId, bool validLocation, Guid countryId, Guid userStateId, decimal latitude, decimal longitude, int geoSegmentationType, int carrouselLogoHeight, int carrouselHeight, int thumbnailHeight, int pageNumber)
        {
            ContentStructure commerceOptions;

            int pageSize = 36;
            int callId = 4;
            string parameters = "UserId: " + userId + ", ImgHeight: " + carrouselHeight + ", ValidLocation: " + validLocation + ", GeoSegmentationType:" + geoSegmentationType;

            try
            {

                commerceOptions = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Header,
                    ContentLevel = FeedContentLevels.Level1,
                    HasOwner = true,
                    CellOwnerId = byCommerceFilterId,
                    DisplayStructureTitle = false,
                    StructureTitle = _localizer["ByCommerce"].Value,
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateDisplayedContent,
                    StructureType = ContentStructureTypes.Carrousel,
                    RulingCriteriaType = ContentRulingCriterias.None,
                    ViewAllAccessType = ViewAllCellContentAccess.CommerceList,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = MaxFilterValueCellsOnCarrousel,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>(),
                    PageNumber = 0
                };

                //COMMERCE OPTIONS
                List<TenantDisplayData> displayData = new List<TenantDisplayData>();

                Cell filterTypeValue;
                ContentFilterValueCell commerceFilterValueCell;
                CommerceCellDetail commerceDetailContent;
                string carrouselImgUrl;
                string thumbnailUrl;
                string logoUrl;

                //If we can determine user location and based on it show him contextual commerces
                if (validLocation)
                {

                    switch (geoSegmentationType)
                    {
                        case GeoSegmentationTypes.Country:

                            displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, countryId, Guid.Empty, GeoSegmentationTypes.Country, (double)latitude, (double)longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, pageSize, pageNumber);

                            if (displayData?.Count == 0)//If no tenants nearby, then retrieve from the country
                            {

                                displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, countryId, Guid.Empty, GeoSegmentationTypes.Country, pageSize, pageNumber);
                            }

                            break;
                        case GeoSegmentationTypes.State:

                            State userState = this._businessObjects.States.Get(userStateId);
                            Guid stateId = Guid.Empty;
                            bool currentStateAlreadyTried = false;

                            //1st will try to use state and location
                            if (userState.InOperation)
                            {
                                stateId = userState.Id;
                                currentStateAlreadyTried = true;

                                displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, userState.CountryId, stateId, GeoSegmentationTypes.State, (double)latitude, (double)longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, pageSize, pageNumber);
                            }
                            else
                            {//If state isn't in operation, retrieve preferences based in country and geolocation
                                displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, userState.CountryId, (double)latitude, (double)longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, pageSize, pageNumber);
                            }

                            if (displayData?.Count == 0)
                            {

                                if (!currentStateAlreadyTried && userState.InOperation)
                                {
                                    stateId = userState.Id;
                                }
                                else
                                {
                                    if (userState.NearestStateId != null)
                                        stateId = (Guid)userState.NearestStateId;
                                }

                                displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, userState.CountryId, stateId, GeoSegmentationTypes.State, pageSize, pageNumber);
                            }

                            break;
                    }


                    if (displayData?.Count > 0)
                    {

                        foreach (TenantDisplayData item in displayData)
                        {
                            filterTypeValue = new Cell
                            {
                                Id = item.TenantId,
                                Type = CellTypes.Commerce,
                                OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent,
                            };

                            //1st crates the filter value cell
                            carrouselImgUrl = ImageAdapter.TransformImg(item.CarrouselImgUrl, carrouselHeight, (int)Math.Ceiling(carrouselHeight / commerceCarrouselImgWidthProp));
                            logoUrl = ImageAdapter.TransformImg(item.WhiteLogoUrl, carrouselLogoHeight, (int)Math.Ceiling(carrouselLogoHeight / logoWithWidthProp));
                            thumbnailUrl = ImageAdapter.TransformImg(item.ThumbnailUrl, thumbnailHeight, (int)Math.Ceiling(thumbnailHeight / logoWithWidthProp));

                            commerceFilterValueCell = new ContentFilterValueCell
                            {
                                Id = item.TenantId,
                                CommerceId = item.TenantId,
                                Type = CellTypes.Commerce,
                                FilterType = ContentFilterTypes.Commerce,
                                Name = item.Name,
                                UnselectedImgUrl = carrouselImgUrl,
                                SelectedImgUrl = carrouselImgUrl,
                                UnselectedIcon = logoUrl,
                                SelectedIcon = logoUrl
                            };

                            filterTypeValue.DisplayData = commerceFilterValueCell;

                            //Now creates the commerce detail cell
                            commerceDetailContent = new CommerceCellDetail
                            {
                                Id = item.TenantId,
                                Name = item.Name,
                                ContentType = CellTypes.Commerce,
                                CommerceId = item.TenantId,
                                ImgUrl = thumbnailUrl,
                                CategoryName = item.CategoryName,
                                DiscountHint = item.DiscountHint,
                                CashbackHint = item.CashbackHint,
                                ShowRate = true,
                                Rate = 5
                            };

                            filterTypeValue.DetailedContent = commerceDetailContent;

                            commerceOptions.Cells.Add(filterTypeValue);
                        }
                    }
                }
                else
                {
                    switch (geoSegmentationType)
                    {
                        case GeoSegmentationTypes.Country:

                            displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, countryId, Guid.Empty, GeoSegmentationTypes.Country, pageSize, pageNumber);

                            break;
                        case GeoSegmentationTypes.State:

                            State userState = this._businessObjects.States.Get(userStateId);
                            Guid stateId = Guid.Empty;

                            if (userState.InOperation)
                            {
                                stateId = userState.Id;
                            }
                            else
                            {
                                if (userState.NearestStateId != null)
                                    stateId = (Guid)userState.NearestStateId;
                            }

                            displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, userState.CountryId, stateId, GeoSegmentationTypes.State, pageSize, pageNumber);
                            break;
                    }

                    if (displayData?.Count > 0)
                    {
                        foreach (TenantDisplayData item in displayData)
                        {
                            filterTypeValue = new Cell
                            {
                                Id = item.TenantId,
                                Type = CellTypes.Commerce,
                                OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent,
                            };

                            //1st crates the filter value cell
                            carrouselImgUrl = ImageAdapter.TransformImg(item.CarrouselImgUrl, carrouselHeight, (int)Math.Ceiling(carrouselHeight / commerceCarrouselImgWidthProp));
                            logoUrl = ImageAdapter.TransformImg(item.WhiteLogoUrl, carrouselLogoHeight, (int)Math.Ceiling(carrouselLogoHeight / logoWithWidthProp));
                            thumbnailUrl = ImageAdapter.TransformImg(item.ThumbnailUrl, thumbnailHeight, (int)Math.Ceiling(thumbnailHeight / logoWithWidthProp));

                            commerceFilterValueCell = new ContentFilterValueCell
                            {
                                Id = item.TenantId,
                                CommerceId = item.TenantId,
                                Type = CellTypes.Commerce,
                                FilterType = ContentFilterTypes.Commerce,
                                Name = item.Name,
                                UnselectedImgUrl = carrouselImgUrl,
                                SelectedImgUrl = carrouselImgUrl,
                                UnselectedIcon = logoUrl,
                                SelectedIcon = logoUrl
                            };

                            filterTypeValue.DisplayData = commerceFilterValueCell;

                            //Now creates the commerce detail cell
                            commerceDetailContent = new CommerceCellDetail
                            {
                                Id = item.TenantId,
                                ContentType = CellTypes.Commerce,
                                CommerceId = item.TenantId,
                                Name = item.Name,
                                ImgUrl = thumbnailUrl,
                                CategoryName = item.CategoryName,
                                DiscountHint = item.DiscountHint,
                                CashbackHint = item.CashbackHint,
                                ShowRate = true,
                                Rate = 5
                            };

                            filterTypeValue.DetailedContent = commerceDetailContent;

                            commerceOptions.Cells.Add(filterTypeValue);
                        }
                    }
                }

                commerceOptions.CellsCount = commerceOptions.Cells?.Count ?? 0;
            }
            catch (Exception e)
            {
                commerceOptions = null;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, e.InnerException != null ? e.InnerException.Message : e.Message);
            }

            return commerceOptions;
        }

        private ContentStructure BuildShoppingMallFilterOptions(string userId, Guid byShoppingFilterId, bool validLocation, Guid countryId, Guid userStateId, decimal latitude, decimal longitude, int geoSegmentationType, int logoHeight, int pageNumber)
        {
            ContentStructure shoppingMallOptions;

            int callId = 5;
            string parameters = "UserId: " + userId + ", LogoHeight: " + logoHeight + ", ValidLocation: " + validLocation + ", GeoSegmentationType:" + geoSegmentationType;

            try
            {
                //Build the slider
                shoppingMallOptions = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Header,
                    ContentLevel = FeedContentLevels.Level1,
                    HasOwner = true,
                    CellOwnerId = byShoppingFilterId,
                    DisplayStructureTitle = false,
                    StructureTitle = "",
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateDisplayedContent,
                    StructureType = ContentStructureTypes.Carrousel,
                    RulingCriteriaType = ContentRulingCriterias.None,
                    ViewAllAccessType = ViewAllCellContentAccess.ShoppingMallList,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = MaxFilterValueCellsOnCarrousel,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>(),
                    PageNumber = 0,
                    PageSize = filterOptionsPageSize
                };

                //COMMERCE OPTIONS
                List<BranchHolderDisplayData> displayData = new List<BranchHolderDisplayData>();

                Cell filterTypeValue;
                ContentFilterValueCell shoppingMallFilterValueCell;
                ShoppingMallCellDetail shoppingMallDetailContent;
                string logoUrl;

                //If we can determine user location and based on it show him contextual commerces
                if (validLocation)
                {

                    switch (geoSegmentationType)
                    {
                        case GeoSegmentationTypes.Country:

                            displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(countryId, Guid.Empty, GeoSegmentationTypes.Country, (double)latitude, (double)longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, filterOptionsPageSize, pageNumber);

                            if (displayData?.Count == 0)//If no tenants nearby, then retrieve from the country
                            {

                                displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(countryId, Guid.Empty, GeoSegmentationTypes.Country, filterOptionsPageSize, pageNumber);
                            }

                            break;
                        case GeoSegmentationTypes.State:

                            State userState = this._businessObjects.States.Get(userStateId);
                            Guid stateId = Guid.Empty;
                            bool currentStateAlreadyTried = false;

                            //1st will try to use state and location
                            if (userState.InOperation)
                            {
                                stateId = userState.Id;
                                currentStateAlreadyTried = true;

                                displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(userState.CountryId, stateId, GeoSegmentationTypes.State, (double)latitude, (double)longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, filterOptionsPageSize, pageNumber);
                            }

                            if (displayData?.Count == 0)
                            {

                                if (!currentStateAlreadyTried && userState.InOperation)
                                {
                                    stateId = userState.Id;
                                }
                                else
                                {
                                    if (userState.NearestStateId != null)
                                        stateId = (Guid)userState.NearestStateId;
                                }

                                displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(userState.CountryId, stateId, GeoSegmentationTypes.State, filterOptionsPageSize, pageNumber);
                            }

                            break;
                    }


                    if (displayData?.Count > 0)
                    {

                        foreach (BranchHolderDisplayData item in displayData)
                        {
                            filterTypeValue = new Cell
                            {
                                Id = item.Id,
                                Type = CellTypes.ShoppingMall,
                                OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent,
                            };

                            //1st crates the filter value cell
                            logoUrl = ImageAdapter.TransformImg(item.LogoUrl, logoHeight, (int)Math.Ceiling(logoHeight / logoWithWidthProp));

                            shoppingMallFilterValueCell = new ContentFilterValueCell
                            {
                                Id = item.Id,
                                CommerceId = item.TenantId,
                                Type = CellTypes.ShoppingMall,
                                FilterType = ContentFilterTypes.ShoppingMall,
                                Name = item.Name,
                                UnselectedImgUrl = "",
                                SelectedImgUrl = "",
                                UnselectedIcon = logoUrl,
                                SelectedIcon = logoUrl
                            };

                            filterTypeValue.DisplayData = shoppingMallFilterValueCell;

                            //Now creates the commerce detail cell
                            shoppingMallDetailContent = new ShoppingMallCellDetail
                            {
                                Id = item.Id,
                                BranchName = item.Name,
                                ShoppingMallName = item.TenantName,
                                CommerceId = item.TenantId,
                                ContentType = CellTypes.ShoppingMall,
                                ImgUrl = logoUrl
                            };

                            filterTypeValue.DetailedContent = shoppingMallDetailContent;

                            shoppingMallOptions.Cells.Add(filterTypeValue);
                        }
                    }
                }
                else
                {
                    switch (geoSegmentationType)
                    {
                        case GeoSegmentationTypes.Country:

                            displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(countryId, Guid.Empty, GeoSegmentationTypes.Country, filterOptionsPageSize, pageNumber);

                            break;
                        case GeoSegmentationTypes.State:

                            State userState = this._businessObjects.States.Get(userStateId);
                            Guid stateId = Guid.Empty;

                            if (userState.InOperation)
                            {
                                stateId = userState.Id;
                            }
                            else
                            {
                                if (userState.NearestStateId != null)
                                    stateId = (Guid)userState.NearestStateId;
                            }

                            displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(userState.CountryId, stateId, GeoSegmentationTypes.State, filterOptionsPageSize, pageNumber);
                            break;
                    }

                    if (displayData?.Count > 0)
                    {
                        foreach (BranchHolderDisplayData item in displayData)
                        {
                            filterTypeValue = new Cell
                            {
                                Id = item.Id,
                                Type = CellTypes.ShoppingMall,
                                OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent,
                            };

                            //1st crates the filter value cell
                            logoUrl = ImageAdapter.TransformImg(item.LogoUrl, logoHeight, (int)Math.Ceiling(logoHeight / logoWithWidthProp));

                            shoppingMallFilterValueCell = new ContentFilterValueCell
                            {
                                Id = item.Id,
                                CommerceId = item.TenantId,
                                Type = CellTypes.ShoppingMall,
                                FilterType = ContentFilterTypes.ShoppingMall,
                                Name = item.Name,
                                UnselectedImgUrl = "",
                                SelectedImgUrl = "",
                                UnselectedIcon = logoUrl,
                                SelectedIcon = logoUrl
                            };

                            filterTypeValue.DisplayData = shoppingMallFilterValueCell;

                            //Now creates the commerce detail cell
                            shoppingMallDetailContent = new ShoppingMallCellDetail
                            {
                                Id = item.Id,
                                CommerceId = item.TenantId,
                                ContentType = CellTypes.ShoppingMall,
                                BranchName = item.Name,
                                ShoppingMallName = item.TenantName,
                                ImgUrl = logoUrl,

                            };

                            filterTypeValue.DetailedContent = shoppingMallDetailContent;

                            shoppingMallOptions.Cells.Add(filterTypeValue);
                        }
                    }
                }

                shoppingMallOptions.CellsCount = shoppingMallOptions.Cells?.Count ?? 0;
            }
            catch (Exception e)
            {
                shoppingMallOptions = null;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, e.InnerException != null ? e.InnerException.Message : e.Message);
            }

            return shoppingMallOptions;
        }

        #endregion

        #region CONSTRUCTORS
        public ContentFeedController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion
    }
}
