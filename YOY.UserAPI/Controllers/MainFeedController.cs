﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Manager.Misc.InterestPreference;
using YOY.DTO.Entities.Misc.Branch;
using YOY.DTO.Entities.Misc.Location;
using YOY.DTO.Entities.Misc.Offer;
using YOY.DTO.Entities.Misc.TenantData;
using YOY.DTO.Entities.Misc.User;
using YOY.UserAPI.Logic.Deal;
using YOY.UserAPI.Logic.Image;
using YOY.UserAPI.Logic.Location;
using YOY.UserAPI.Models.v1.Content.POCO;
using YOY.UserAPI.Models.v1.Content.SET;
using YOY.UserAPI.Models.v1.Deal.POCO;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.UserAPI.Models.v1.Miscellaneous.Location.POCO;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class MainFeedController : ControllerBase
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
        #endregion

        #region METHODS

        /// <summary>
        /// Initializes the business objects
        /// </summary>
        /// <param name="commerceId"></param>
        private void Initialize(Guid commerceId)
        {

            if (_tenant == null || _tenant.TenantId != commerceId)
            {
                _tenant = Tenant.GetInstance(commerceId);
            }

            if (_businessObjects == null)
            {
                _businessObjects = BusinessObjects.GetInstance(_tenant);
            }
        }


        private ContentStructure BuildSlider(string userId, Guid stateId, Guid countryId, int imgHeight)
        {
            ContentStructure slider;

            int callId = 1;
            string parameters = "UserId: " + userId + ", StateId: " + stateId + ", CountryId: " + countryId + ", ImgHeight: " + imgHeight;

            try
            {
                //Build the slider
                slider = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Header,
                    ContentLevel = FeedContentLevels.Level0,
                    HasOwner = false,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = false,
                    StructureTitle = "",
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayPromotionalContent,
                    StructureType = ContentStructureTypes.Slider,
                    RulingCriteriaType = ContentRulingCriterias.None,
                    ViewAllAccessType = ViewAllCellContentAccess.None,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = -1,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>(),
                    PageNumber = 0,
                    PageSize = 0
                };

                //1st slide
                Cell slideCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    Type = CellTypes.Slide,
                    OnSelectAction = OnSelectCellActionTypes.DisplayPromotionalContent,
                };

                Slide currentSlide = new Slide
                {
                    Id = slideCell.Id,
                    ImgUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596428098/dev/testing/slider2.png", imgHeight, (int)Math.Ceiling(imgHeight / slideWidthProp)),
                    ExpirationDate = DateTime.UtcNow.AddDays(3).ToString("yyyy-MM-dd HH':'mm':'ss"),
                    CommerceId = new Guid("2e0dc7af-791e-4f13-b9c7-9a2f59a4cd86"),
                    CountryId = new Guid("d7319a46-1389-488d-ba81-c60cd09be87a"),
                    StateId = new Guid("3b1de628-d6fd-45a9-b6c4-8523f3bd7677"),
                    Type = CellTypes.Slide
                };

                slideCell.DisplayData = currentSlide;

                SlideCellDetail slideCellDetailContent = new SlideCellDetail
                {
                    Id = slideCell.Id,
                    ContentType = CellDetailTypes.Slide,
                    RetrievePromotionalContent = true,
                    Title = "Noches de feria",
                    Description = "Todos los fines de semana de Agosto, iniciando el Jueves, a partir del as 7pm, aprovechá los mejores beneficios comprando con YOY"
                };

                slideCell.DetailedContent = slideCellDetailContent;

                //Add 1st slide
                slider.Cells.Add(slideCell);

                //2nd slide
                slideCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    Type = CellTypes.Slide,
                    OnSelectAction = OnSelectCellActionTypes.DisplayPromotionalContent,
                    DetailedContent = new CellContainedObject()
                };

                currentSlide = new Slide
                {
                    Id = slideCell.Id,
                    ImgUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596428094/dev/testing/slider3.png", imgHeight, (int)Math.Ceiling(imgHeight / slideWidthProp)),
                    ExpirationDate = DateTime.UtcNow.AddDays(5).ToString("yyyy-MM-dd HH':'mm':'ss"),
                    CommerceId = new Guid("2e0dc7af-791e-4f13-b9c7-9a2f59a4cd86"),
                    CountryId = new Guid("d7319a46-1389-488d-ba81-c60cd09be87a"),
                    StateId = new Guid("3b1de628-d6fd-45a9-b6c4-8523f3bd7677"),
                    Type = CellTypes.Slide
                };

                slideCell.DisplayData = currentSlide;

                slideCellDetailContent = new SlideCellDetail
                {
                    Id = slideCell.Id,
                    ContentType = CellDetailTypes.Slide,
                    RetrievePromotionalContent = true,
                    Title = "Pizza Month",
                    Description = "Agosto es el mes de la pizza, por ello te traemos las mejores promos de tus pizzerias favorita, no te lo perdás!!"
                };

                slideCell.DetailedContent = slideCellDetailContent;

                //Add 2nd slide
                slider.Cells.Add(slideCell);

                //3rd slide
                slideCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    Type = CellTypes.Slide,
                    OnSelectAction = OnSelectCellActionTypes.DisplayPromotionalContent,
                    DetailedContent = new CellContainedObject()
                };

                currentSlide = new Slide
                {
                    Id = slideCell.Id,
                    ImgUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596428094/dev/testing/slider1.png", imgHeight, (int)Math.Ceiling(imgHeight / slideWidthProp)),
                    ExpirationDate = DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd HH':'mm':'ss"),
                    CommerceId = new Guid("2e0dc7af-791e-4f13-b9c7-9a2f59a4cd86"),
                    CountryId = new Guid("d7319a46-1389-488d-ba81-c60cd09be87a"),
                    StateId = new Guid("3b1de628-d6fd-45a9-b6c4-8523f3bd7677"),
                    Type = CellTypes.Slide
                };

                slideCell.DisplayData = currentSlide;

                slideCellDetailContent = new SlideCellDetail
                {
                    Id = slideCell.Id,
                    ContentType = CellDetailTypes.Slide,
                    RetrievePromotionalContent = true,
                    Title = "Sopresas inéditas",
                    Description = "A partir de hoy y durante 10 días, podrás obtener promos de hasta el 85% con el código SORPRESASMAX. Ingresá a la sección de códigos y activalo"
                };

                slideCell.DetailedContent = slideCellDetailContent;

                //Add 3rd slide
                slider.Cells.Add(slideCell);

                slider.CellsCount = slider.Cells?.Count ?? 0;
                slider.PageSize = slider.Cells?.Count ?? 0;

                //In this case all cells need to be visible
                slider.MaxDisplayedCellsOnInitialStructure = slider.CellsCount;
            }
            catch(Exception e)
            {
                slider = null;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, e.InnerException != null ? e.InnerException.Message : e.Message);
            }


            return slider;
        }

        public ContentStructure BuildFilterTypes(string userId, Guid stateId, Guid countryId, Guid byCategoryFilterId, Guid byCommerceFilterId, Guid byShoppingMallId)
        {
            ContentStructure filterTypes;

            int callId = 2;
            string parameters = "UserId: " + userId + ", StateId: " + stateId + ", CountryId: " + countryId;


            try
            {
                //Build the filter types

                filterTypes = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Header,
                    ContentLevel = FeedContentLevels.Level0,
                    HasOwner = false,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = true,
                    StructureTitle = _localizer["Filter"].Value,
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateContentFilterOptions,
                    StructureType = ContentStructureTypes.Carrousel,
                    RulingCriteriaType = ContentRulingCriterias.None,
                    ViewAllAccessType = ViewAllCellContentAccess.None,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = -1,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>(),
                    PageNumber = 0,
                    PageSize = 0
                };

                //1st filter option
                Cell filterType = new Cell
                {
                    Id = byCategoryFilterId,
                    Type = CellTypes.FilterType,
                    OnSelectAction = OnSelectCellActionTypes.DisplayPromotionalContent,
                    DetailedContent = new CellContainedObject(),
                };

                ContentFilterTypeCell filterTypeCell = new ContentFilterTypeCell
                {
                    CommerceId = Guid.Empty,
                    Id = byCategoryFilterId,
                    Type = CellTypes.FilterType,
                    FilterType = ContentFilterTypes.Category,
                    FilterName = _localizer["ByCategory"].Value
                };

                filterType.DisplayData = filterTypeCell;

                //Add 1st filter type
                filterTypes.Cells.Add(filterType);

                //2nd filter option
                filterType = new Cell
                {
                    Id = byCommerceFilterId,
                    Type = CellTypes.FilterType,
                    OnSelectAction = OnSelectCellActionTypes.DisplayPromotionalContent,
                    DetailedContent = new CellContainedObject(),
                };

                filterTypeCell = new ContentFilterTypeCell
                {
                    CommerceId = Guid.Empty,
                    Id = byCommerceFilterId,
                    Type = CellTypes.FilterType,
                    FilterType = ContentFilterTypes.Commerce,
                    FilterName = _localizer["ByCommerce"].Value
                };

                filterType.DisplayData = filterTypeCell;

                //Add 2nd filter type
                filterTypes.Cells.Add(filterType);

                //3rd filter option
                filterType = new Cell
                {
                    Id = byShoppingMallId,
                    Type = CellTypes.FilterType,
                    OnSelectAction = OnSelectCellActionTypes.DisplayPromotionalContent,
                    DetailedContent = new CellContainedObject(),
                };

                filterTypeCell = new ContentFilterTypeCell
                {
                    CommerceId = Guid.Empty,
                    Id = byShoppingMallId,
                    Type = CellTypes.FilterType,
                    FilterType = ContentFilterTypes.ShoppingMall,
                    FilterName = _localizer["ByShoppingMall"].Value
                };

                filterType.DisplayData = filterTypeCell;

                //Add 3rd filter type
                filterTypes.Cells.Add(filterType);

                filterTypes.CellsCount = filterTypes.Cells?.Count ?? 0; 
                filterTypes.PageSize = filterTypes.Cells?.Count ?? 0;

                //In this case all cells need to be visible
                filterTypes.MaxDisplayedCellsOnInitialStructure = filterTypes.CellsCount;
            }
            catch(Exception e)
            {
                filterTypes = null;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, e.InnerException != null ? e.InnerException.Message : e.Message);
            }

            return filterTypes;
        }

        public ContentStructure BuildByCategoryFilterOptions(string userId, int iconHeight, Guid byCategoryFilterId)
        {
            ContentStructure categoryOptions;

            int callId = 3;
            string parameters = "UserId: " + userId + ", ImgHeight: " + iconHeight;

            try
            {

                categoryOptions = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Header,
                    ContentLevel = FeedContentLevels.Level1,
                    HasOwner = true,
                    CellOwnerId = byCategoryFilterId,
                    DisplayStructureTitle = false,
                    StructureTitle = _localizer["ByCategory"].Value,
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateDisplayedContent,
                    StructureType = ContentStructureTypes.Carrousel,
                    RulingCriteriaType = ContentRulingCriterias.None,
                    ViewAllAccessType = ViewAllCellContentAccess.CategoryList,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = MaxFilterValueCellsOnCarrousel,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>(),
                    PageNumber = 0,
                    PageSize = 0
                };

                //Retrieves preferences List
                List<UserPreferenceData> categoryPreferences = this._businessObjects.UserInterests.GetPreferences(userId);

                if (categoryPreferences != null && categoryPreferences?.Count > 0)
                {
                    
                    Cell filterTypeValue;
                    ContentFilterValueCell categoryCell;
                    CategoryCellDetail cellDetailContent;
                    string imgUrl;

                    foreach(UserPreferenceData item in categoryPreferences)
                    {
                        filterTypeValue = new Cell
                        {
                            Id = item.Id,
                            Type = CellTypes.Category,
                            OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent,
                        };

                        imgUrl = ImageAdapter.TransformImg(item.BaseImgUrl, iconHeight, (int)Math.Ceiling(iconHeight / logoWithWidthProp));

                        categoryCell = new ContentFilterValueCell
                        {
                            Id = item.Id,
                            CommerceId = Guid.Empty,
                            Type = CellTypes.Category,
                            FilterType = ContentFilterTypes.Category,
                            Name = item.Name,
                            UnselectedIcon = imgUrl + CategoryUnSelectedAppend,
                            SelectedIcon = imgUrl + CategorySelectedAppend,
                            UnselectedImgUrl = "",
                            SelectedImgUrl = ""
                        };

                        filterTypeValue.DisplayData = categoryCell;

                        cellDetailContent = new CategoryCellDetail
                        {
                            Id = item.Id,
                            ContentType = CellDetailTypes.Category,
                            Name = item.Name,
                            ImgUrl = imgUrl + CategoryUnSelectedAppend,
                            CommerceId = Guid.Empty
                        };

                        filterTypeValue.DetailedContent = cellDetailContent;

                        categoryOptions.Cells.Add(filterTypeValue);
                    }
                }

                categoryOptions.CellsCount = categoryOptions.Cells?.Count ?? 0;
                categoryOptions.PageSize = categoryOptions.Cells?.Count ?? 0;

            }
            catch(Exception e)
            {
                categoryOptions = null;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, e.InnerException != null ? e.InnerException.Message : e.Message);
            }

            return categoryOptions;
        }


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
                    PageSize = pageSize,
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
                                ContentType = CellDetailTypes.Commerce,
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
                                ContentType = CellDetailTypes.Commerce,
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
            catch(Exception e)
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
            int pageSize = 36;
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
                    PageSize = pageSize
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

                            displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(countryId, Guid.Empty, GeoSegmentationTypes.Country, (double)latitude, (double)longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, pageSize, pageNumber);

                            if (displayData?.Count == 0)//If no tenants nearby, then retrieve from the country
                            {

                                displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(countryId, Guid.Empty, GeoSegmentationTypes.Country, pageSize, pageNumber);
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

                                displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(userState.CountryId, stateId, GeoSegmentationTypes.State, (double)latitude, (double)longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, pageSize, pageNumber);
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

                                displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(userState.CountryId, stateId, GeoSegmentationTypes.State, pageSize, pageNumber);
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
                                ContentType = CellDetailTypes.ShoppingMall,
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

                            displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(countryId, Guid.Empty, GeoSegmentationTypes.Country, pageSize, pageNumber);

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

                            displayData = this._businessObjects.Branches.GetBranchHoldersDisplayData(userState.CountryId, stateId, GeoSegmentationTypes.State, pageSize, pageNumber);
                            break;
                    }

                    if (displayData?.Count > 0)
                    {
                        foreach (BranchHolderDisplayData item in displayData)
                        {
                            filterTypeValue = new Cell
                            {
                                Id = item.TenantId,
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
                                ContentType = CellDetailTypes.ShoppingMall,
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

        public List<ContentStructure> BuildMainContentDeals(string userId, bool validLocation, Guid countryId, Guid userStateId, decimal latitude, decimal longitude, int geoSegmentationType, int contentLogoHeight, int brandingLogoHeight, int dealImgHeight)
        {
            List<ContentStructure> contentStructures;

            int callId = 6;
            int offerSetToRetrieve = 400;
            int pageSize = 50;
            string parameters = "UserId: " + userId + ", ImgHeight: " + dealImgHeight + ", ValidLocation: " + validLocation + ", GeoSegmentationType:" + geoSegmentationType;


            try
            {
                contentStructures = new List<ContentStructure>();

                List<Cell> contentCells = new List<Cell>();
                Cell currentCell;
                ContentCellDisplayData displayData;
                DealContentCellDetail cellDetail;

                string logoUrl;
                string imgUrl;

                List<FlattenedOfferData> offers;

                if (validLocation)
                {
                    offers = this._businessObjects.Offers.GetOffersDataByRegionWithLocation(countryId, userStateId, geoSegmentationType, userId, latitude, longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, DateTime.UtcNow, ContentFilterTypes.Category, OfferPurposeTypes.Deal, offerSetToRetrieve, 0);
                }
                else
                {
                    offers = this._businessObjects.Offers.GetOffersDataByRegion(userStateId, countryId, geoSegmentationType, userId, DateTime.UtcNow, ContentFilterTypes.Category, OfferPurposeTypes.Deal, offerSetToRetrieve, 0);
                }

                if (offers?.Count > 0)
                {

                    List<DealDisplayData> dealDisplayData = DealDataConverter.ProccessDeals(offers, _localizer);

                    foreach(DealDisplayData item in dealDisplayData)
                    {
                        currentCell = new Cell
                        {
                            Id = item.Id,
                            OnSelectAction = OnSelectCellActionTypes.DisplayDealDetailScreen,
                            Type = CellTypes.Offer
                        };

                        logoUrl = ImageAdapter.TransformImg(item.CommerceWhiteLogoUrl, brandingLogoHeight, (int)Math.Ceiling(brandingLogoHeight / logoWithWidthProp));
                        imgUrl = ImageAdapter.TransformImg(item.DisplayImgUrl, dealImgHeight, (int)Math.Ceiling(dealImgHeight / dealImgWidthProp));

                        displayData = new ContentCellDisplayData
                        {
                            Id = currentCell.Id,
                            CommerceId = item.CommerceId,
                            Type = CellTypes.Offer,
                            DealType = item.DealType,
                            DealTypeName = item.DealTypeName,
                            DealTypeIcon = item.DealTypeIcon,
                            CommerceLogo = logoUrl,
                            ImgUrl = imgUrl,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            ExtraHint = item.ExtraHint,
                            DisplayExtraHint = item.DisplayExtraHint,
                            AvailableQuantity = item.AvailableQuantity,
                            AvailableQuantityHint = item.AvailableQuantityHint,
                            DisplayAvailableQuantityHint = item.DisplayAvailableQuantityHint,
                            Favorite = item.Favorite,
                            DisplayExpirationHint = item.DisplayExpirationHint,
                            ExpirationDate = item.ExpirationDate
                        };

                        currentCell.DisplayData = displayData;

                        logoUrl = ImageAdapter.TransformImg(item.CommerceLogoUrl, contentLogoHeight, (int)Math.Ceiling(contentLogoHeight / logoWithWidthProp));


                        cellDetail = new DealContentCellDetail
                        {
                            Id = item.Id,
                            CommerceId = item.CommerceId,
                            ContentType = CellDetailTypes.Offer,
                            DealType = item.DealType,
                            DealTypeName = item.DealTypeName,
                            DealTypeIcon = item.DealTypeIcon,
                            CommerceLogo = logoUrl,
                            ImgUrl = imgUrl,
                            CurrencySymbol = item.CurrencySymbol,
                            Price = item.Price,
                            PriceLiteral = item.PriceLiteral,
                            DisplayPrice = item.DisplayPrice,
                            RegularPrice = item.RegularPrice,
                            RegularPriceLiteral = item.RegularPriceLiteral,
                            DisplayRegularPrice = item.DisplayRegularPrice,
                            HasPreferences = item.HasPreferences,
                            CashbackHint = item.CashbackHint,
                            DisplayCashbackHint = item.DisplayCashbackHint,
                            ExtraHint = item.ExtraHint,
                            DisplayExtraHint = item.DisplayExtraHint,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            DealName = item.Name,
                            AvailableQuantity = item.AvailableQuantity,
                            AvailableQuantityHint = item.AvailableQuantityHint,
                            DisplayAvailableQuantityHint = item.DisplayAvailableQuantityHint,
                            Favorite = item.Favorite,
                            DisplayExpirationHint = item.DisplayExpirationHint,
                            ExpirationDate = item.ExpirationDate
                        };

                        currentCell.DetailedContent = cellDetail;

                        contentCells.Add(currentCell);

                    }
                }

                //Build featured deals

                ContentStructure currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Header,
                    ContentLevel = FeedContentLevels.Level0,
                    HasOwner = false,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = true,
                    StructureTitle = "Destacados",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnCarrousel,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    RulingCriteriaType = ContentRulingCriterias.Popular,
                    ViewAllAccessType = ViewAllCellContentAccess.DealContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Carrousel,
                    CellsCount = 26,//just for testing
                    PageNumber = 0,
                    PageSize = pageSize,
                    Cells = new List<Cell>()
                };

                Random random = new Random();

                for (int i=0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                }

                contentStructures.Add(currentStructure);

                //Now will add the 1st grid, owned by byFilterOwnerId
                currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = true,
                    StructureTitle = "Todo",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnGrid,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    RulingCriteriaType = ContentRulingCriterias.RelevantForUser,
                    ViewAllAccessType = ViewAllCellContentAccess.DealContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Grid,
                    CellsCount = 42,
                    PageNumber = 0,
                    PageSize = pageSize,
                    Cells = new List<Cell>()
                };

                random = new Random();

                for (int i = 0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                }

                contentStructures.Add(currentStructure);

                //Now will add the favorites carrousel
                currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = true,
                    StructureTitle = "Mis Favoritos",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnCarrousel,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    RulingCriteriaType = ContentRulingCriterias.Saved,
                    ViewAllAccessType = ViewAllCellContentAccess.FavoriteContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Carrousel,
                    CellsCount = 15,
                    PageNumber = 0,
                    PageSize = pageSize,
                    Cells = new List<Cell>()
                };

                random = new Random();

                for (int i = 0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                    ((ContentCellDisplayData)currentStructure.Cells[i].DisplayData).Favorite = true;

                }

                contentStructures.Add(currentStructure);

                //Now will add the cash incentives

                contentCells = new List<Cell>();

                CashIncentiveContentCellDetail cashCellDetail;

                //1st cash incentive cell

                currentCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    OnSelectAction = OnSelectCellActionTypes.DisplayCashIncentiveDetailScreen,
                    Type = CellTypes.CashIncentive
                };

                logoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596430629/dev/testing/logo5.png", contentLogoHeight, (int)Math.Ceiling(contentLogoHeight / logoWithWidthProp));
                imgUrl = "";

                displayData = new ContentCellDisplayData
                {
                    Id = currentCell.Id,
                    CommerceId = Guid.NewGuid(),
                    Type = CellTypes.CashIncentive,
                    DealType = DealTypes.Online,
                    DealTypeName = "En línea",
                    DealTypeIcon = "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/online-deal.png",
                    CommerceLogo = logoUrl,
                    ImgUrl = imgUrl,
                    MainHint = "7% Cashback",
                    ComplementaryHint = "Antes 3%",
                    ExtraHint = "Compras +₡10,000",
                    DisplayExtraHint = true,
                    AvailableQuantity = -1,
                    AvailableQuantityHint = "",
                    DisplayAvailableQuantityHint = false,
                    Favorite = true,
                    DisplayExpirationHint = false,
                    ExpirationDate = DateTime.UtcNow.AddDays(14).ToString("yyyy-MM-dd HH':'mm':'ss")
                };

                currentCell.DisplayData = displayData;

                cashCellDetail = new CashIncentiveContentCellDetail
                {
                    Id = currentCell.Id,
                    CommerceId = displayData.CommerceId,
                    ContentType = CellDetailTypes.CashIncentive
                };

                currentCell.DetailedContent = cashCellDetail;

                contentCells.Add(currentCell);

                //2nd deal cell

                currentCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    OnSelectAction = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    Type = CellTypes.CashIncentive
                };

                logoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596430629/dev/testing/logo2.png", contentLogoHeight, (int)Math.Ceiling(contentLogoHeight / logoWithWidthProp));
                imgUrl = "";

                displayData = new ContentCellDisplayData
                {
                    Id = currentCell.Id,
                    CommerceId = Guid.NewGuid(),
                    Type = CellTypes.CashIncentive,
                    DealType = DealTypes.InStore,
                    DealTypeName = "En tienda",
                    DealTypeIcon = "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/instore-deal.png",
                    CommerceLogo = logoUrl,
                    ImgUrl = imgUrl,
                    MainHint = "₡1000",
                    ComplementaryHint = "En cada ₡10,000",
                    ExtraHint = "10% cashback",
                    DisplayExtraHint = true,
                    AvailableQuantity = 45,
                    AvailableQuantityHint = "",
                    DisplayAvailableQuantityHint = false,
                    Favorite = true,
                    DisplayExpirationHint = true,
                    ExpirationDate = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd HH':'mm':'ss")
                };

                currentCell.DisplayData = displayData;

                cashCellDetail = new CashIncentiveContentCellDetail
                {
                    Id = currentCell.Id,
                    CommerceId = displayData.CommerceId,
                    ContentType = CellDetailTypes.CashIncentive
                };

                currentCell.DetailedContent = cashCellDetail;

                contentCells.Add(currentCell);

                //3rd deal cell

                currentCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    OnSelectAction = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    Type = CellTypes.CashIncentive
                };

                logoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596430629/dev/testing/logo2.png", contentLogoHeight, (int)Math.Ceiling(contentLogoHeight / logoWithWidthProp));
                imgUrl = "";

                displayData = new ContentCellDisplayData
                {
                    Id = currentCell.Id,
                    CommerceId = Guid.NewGuid(),
                    Type = CellTypes.CashIncentive,
                    DealType = DealTypes.Phone,
                    DealTypeName = "Telefónico",
                    DealTypeIcon = "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/phone-deal.png",
                    CommerceLogo = logoUrl,
                    ImgUrl = imgUrl,
                    MainHint = "Helado gratis",
                    ComplementaryHint = "Compras +₡3,000",
                    ExtraHint = "",
                    DisplayExtraHint = false,
                    AvailableQuantity = 10,
                    AvailableQuantityHint = "Quedan solo 10",
                    DisplayAvailableQuantityHint = true,
                    Favorite = true,
                    DisplayExpirationHint = true,
                    ExpirationDate = DateTime.UtcNow.AddHours(5).ToString("yyyy-MM-dd HH':'mm':'ss")
                };

                currentCell.DisplayData = displayData;

                cashCellDetail = new CashIncentiveContentCellDetail
                {
                    Id = currentCell.Id,
                    CommerceId = displayData.CommerceId,
                    ContentType = CellDetailTypes.CashIncentive
                };

                currentCell.DetailedContent = cashCellDetail;

                contentCells.Add(currentCell);

                //Build featured cash incentives carrousel

                currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = true,
                    StructureTitle = "Cashbacks Populares",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnCarrousel,
                    MaxMinsToKeepStored = MaxMinsToStoreBodyContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayCashIncentiveDetailScreen,
                    RulingCriteriaType = ContentRulingCriterias.Popular,
                    ViewAllAccessType = ViewAllCellContentAccess.CashIncentiveContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Carrousel,
                    CellsCount = 20,
                    PageNumber = 0,
                    PageSize = pageSize,
                    Cells = new List<Cell>()
                };

                random = new Random();

                for (int i = 0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                }

                contentStructures.Add(currentStructure);

                //Build cash incentives grid
                currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = true,
                    StructureTitle = "Gana cashback",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnGrid,
                    MaxMinsToKeepStored = MaxMinsToStoreBodyContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayCashIncentiveDetailScreen,
                    RulingCriteriaType = ContentRulingCriterias.Suggestions,
                    ViewAllAccessType = ViewAllCellContentAccess.CashIncentiveContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Grid,
                    CellsCount = 32,
                    PageNumber = 0,
                    PageSize = pageSize,
                    Cells = new List<Cell>()
                };

                random = new Random();

                for (int i = 0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                }

                contentStructures.Add(currentStructure);

            }
            catch (Exception e)
            {
                contentStructures = null;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, e.InnerException != null ? e.InnerException.Message : e.Message);
            }

            return contentStructures;
        }

        public List<ContentStructure> BuildContentByFilter(string userId, int filterType, Guid filterValue, bool validLocation, Guid countryId, Guid userStateId, decimal latitude, decimal longitude, int geoSegmentationType, int contentLogoHeight, int brandingLogoHeight, int dealImgHeight)
        {
            List<ContentStructure> contentStructures;

            int callId = 7;
            int offerSetToRetrieve = 400;
            int pageSize = 50;
            string LogoUrl = "";
            string parameters = "UserId: " + userId + ", ImgHeight: " + dealImgHeight + ", ValidLocation: " + validLocation + ", GeoSegmentationType:" + geoSegmentationType;


            try
            {
                List<FlattenedOfferData> offers = offers = this._businessObjects.Offers.GetOffersDataByRegionForReference(userStateId, countryId, geoSegmentationType, userId, DateTime.UtcNow, filterType, filterValue, OfferPurposeTypes.Deal, offerSetToRetrieve, 0);

                contentStructures = new List<ContentStructure>();

                List<Cell> contentCells = new List<Cell>();
                Cell currentCell;
                ContentCellDisplayData displayData;
                DealContentCellDetail cellDetail;

                string logoUrl;
                string imgUrl;

                if (offers?.Count > 0)
                {
                    List<DealDisplayData> dealDisplayData = DealDataConverter.ProccessDeals(offers, _localizer);

                    foreach (DealDisplayData item in dealDisplayData)
                    {
                        currentCell = new Cell
                        {
                            Id = item.Id,
                            OnSelectAction = OnSelectCellActionTypes.DisplayDealDetailScreen,
                            Type = CellTypes.Offer
                        };

                        logoUrl = ImageAdapter.TransformImg(item.CommerceWhiteLogoUrl, brandingLogoHeight, (int)Math.Ceiling(brandingLogoHeight / logoWithWidthProp));
                        imgUrl = ImageAdapter.TransformImg(item.DisplayImgUrl, dealImgHeight, (int)Math.Ceiling(dealImgHeight / dealImgWidthProp));

                        displayData = new ContentCellDisplayData
                        {
                            Id = currentCell.Id,
                            CommerceId = item.CommerceId,
                            Type = CellTypes.Offer,
                            DealType = item.DealType,
                            DealTypeName = item.DealTypeName,
                            DealTypeIcon = item.DealTypeIcon,
                            CommerceLogo = logoUrl,
                            ImgUrl = imgUrl,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            ExtraHint = item.ExtraHint,
                            DisplayExtraHint = item.DisplayExtraHint,
                            AvailableQuantity = item.AvailableQuantity,
                            AvailableQuantityHint = item.AvailableQuantityHint,
                            DisplayAvailableQuantityHint = item.DisplayAvailableQuantityHint,
                            Favorite = item.Favorite,
                            DisplayExpirationHint = item.DisplayExpirationHint,
                            ExpirationDate = item.ExpirationDate
                        };

                        currentCell.DisplayData = displayData;

                        logoUrl = ImageAdapter.TransformImg(item.CommerceLogoUrl, contentLogoHeight, (int)Math.Ceiling(contentLogoHeight / logoWithWidthProp));


                        cellDetail = new DealContentCellDetail
                        {
                            Id = item.Id,
                            CommerceId = item.CommerceId,
                            ContentType = CellDetailTypes.Offer,
                            DealType = item.DealType,
                            DealTypeName = item.DealTypeName,
                            DealTypeIcon = item.DealTypeIcon,
                            CommerceLogo = logoUrl,
                            ImgUrl = imgUrl,
                            CurrencySymbol = item.CurrencySymbol,
                            Price = item.Price,
                            PriceLiteral = item.PriceLiteral,
                            DisplayPrice = item.DisplayPrice,
                            RegularPrice = item.RegularPrice,
                            RegularPriceLiteral = item.RegularPriceLiteral,
                            DisplayRegularPrice = item.DisplayRegularPrice,
                            HasPreferences = item.HasPreferences,
                            CashbackHint = item.CashbackHint,
                            DisplayCashbackHint = item.DisplayCashbackHint,
                            ExtraHint = item.ExtraHint,
                            DisplayExtraHint = item.DisplayExtraHint,
                            MainHint = item.MainHint,
                            ComplementaryHint = item.ComplementaryHint,
                            DealName = item.Name,
                            AvailableQuantity = item.AvailableQuantity,
                            AvailableQuantityHint = item.AvailableQuantityHint,
                            DisplayAvailableQuantityHint = item.DisplayAvailableQuantityHint,
                            Favorite = item.Favorite,
                            DisplayExpirationHint = item.DisplayExpirationHint,
                            ExpirationDate = item.ExpirationDate
                        };

                        currentCell.DetailedContent = cellDetail;

                        contentCells.Add(currentCell);

                    }
                }

                ContentStructure currentStructure;

                //Now will add the 1st grid, owned by byFilterOwnerId
                currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = filterValue,
                    DisplayStructureTitle = true,
                    StructureTitle = "Todo",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnGrid,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    RulingCriteriaType = ContentRulingCriterias.RelevantForUser,
                    ViewAllAccessType = ViewAllCellContentAccess.DealContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Carrousel,
                    PageNumber = 0,
                    PageSize = pageSize,
                    CellsCount = 32,
                    Cells = new List<Cell>()
                };

                Random random = new Random();

                for (int i = 0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                }

                contentStructures.Add(currentStructure);

                //Now will add the favorites carrousel
                currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = filterValue,
                    DisplayStructureTitle = true,
                    StructureTitle = "Lo popular",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnCarrousel,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    RulingCriteriaType = ContentRulingCriterias.Popular,
                    ViewAllAccessType = ViewAllCellContentAccess.FavoriteContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Carrousel,
                    CellsCount = 32,
                    PageNumber = 0,
                    PageSize = pageSize,
                    Cells = new List<Cell>()
                };

                random = new Random();

                for (int i = 0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                    ((ContentCellDisplayData)currentStructure.Cells[i].DisplayData).Favorite = true;

                }

                contentStructures.Add(currentStructure);

                //Now will add the cash incentives

                contentCells = new List<Cell>();

                CashIncentiveContentCellDetail cashCellDetail;

                //1st cash incentive cell

                currentCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    OnSelectAction = OnSelectCellActionTypes.DisplayCashIncentiveDetailScreen,
                    Type = CellTypes.CashIncentive
                };

                logoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596430629/dev/testing/logo5.png", contentLogoHeight, (int)Math.Ceiling(contentLogoHeight / logoWithWidthProp));
                imgUrl = "";

                displayData = new ContentCellDisplayData
                {
                    Id = currentCell.Id,
                    CommerceId = Guid.NewGuid(),
                    Type = CellTypes.CashIncentive,
                    DealType = DealTypes.Online,
                    DealTypeName = "En línea",
                    DealTypeIcon = "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/online-deal.png",
                    CommerceLogo = logoUrl,
                    ImgUrl = imgUrl,
                    MainHint = "14% Cashback",
                    ComplementaryHint = "Antes 6%",
                    ExtraHint = "Compras +₡12,000",
                    DisplayExtraHint = true,
                    AvailableQuantity = -1,
                    AvailableQuantityHint = "",
                    DisplayAvailableQuantityHint = false,
                    Favorite = true,
                    DisplayExpirationHint = false,
                    ExpirationDate = DateTime.UtcNow.AddDays(14).ToString("yyyy-MM-dd HH':'mm':'ss")
                };

                currentCell.DisplayData = displayData;

                cashCellDetail = new CashIncentiveContentCellDetail
                {
                    Id = currentCell.Id,
                    CommerceId = displayData.CommerceId,
                    ContentType = CellDetailTypes.CashIncentive
                };

                currentCell.DetailedContent = cashCellDetail;

                contentCells.Add(currentCell);

                //2nd deal cell

                currentCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    OnSelectAction = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    Type = CellTypes.CashIncentive
                };

                logoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596430629/dev/testing/logo2.png", contentLogoHeight, (int)Math.Ceiling(contentLogoHeight / logoWithWidthProp));
                imgUrl = "";

                displayData = new ContentCellDisplayData
                {
                    Id = currentCell.Id,
                    CommerceId = Guid.NewGuid(),
                    Type = CellTypes.CashIncentive,
                    DealType = DealTypes.InStore,
                    DealTypeIcon = "En tienda",
                    DealTypeName = "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/instore-deal.png",
                    CommerceLogo = logoUrl,
                    ImgUrl = imgUrl,
                    MainHint = "₡1000",
                    ComplementaryHint = "En cada ₡10,000",
                    ExtraHint = "10% cashback",
                    DisplayExtraHint = true,
                    AvailableQuantity = 45,
                    AvailableQuantityHint = "",
                    DisplayAvailableQuantityHint = false,
                    Favorite = true,
                    DisplayExpirationHint = true,
                    ExpirationDate = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd HH':'mm':'ss")
                };

                currentCell.DisplayData = displayData;

                cashCellDetail = new CashIncentiveContentCellDetail
                {
                    Id = currentCell.Id,
                    CommerceId = displayData.CommerceId,
                    ContentType = CellDetailTypes.CashIncentive,
                };

                currentCell.DetailedContent = cashCellDetail;

                contentCells.Add(currentCell);

                //3rd deal cell

                currentCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    OnSelectAction = OnSelectCellActionTypes.DisplayDealDetailScreen,
                    Type = CellTypes.CashIncentive
                };

                logoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596430629/dev/testing/logo2.png", contentLogoHeight, (int)Math.Ceiling(contentLogoHeight / logoWithWidthProp));
                imgUrl = "";

                displayData = new ContentCellDisplayData
                {
                    Id = currentCell.Id,
                    CommerceId = Guid.NewGuid(),
                    Type = CellTypes.CashIncentive,
                    DealType = DealTypes.Phone,
                    DealTypeName = "Telefónico",
                    DealTypeIcon = "https://res.cloudinary.com/yoyimgs/image/upload/v1597767894/global/deal_icons/phone-deal.png",
                    CommerceLogo = logoUrl,
                    ImgUrl = imgUrl,
                    MainHint = "Helado gratis",
                    ComplementaryHint = "Compras +₡4,500",
                    ExtraHint = "",
                    DisplayExtraHint = true,
                    AvailableQuantity = 15,
                    AvailableQuantityHint = "Solo quedan 15",
                    DisplayAvailableQuantityHint = true,
                    Favorite = true,
                    DisplayExpirationHint = true,
                    ExpirationDate = DateTime.UtcNow.AddHours(5).ToString("yyyy-MM-dd HH':'mm':'ss")
                };

                currentCell.DisplayData = displayData;

                cashCellDetail = new CashIncentiveContentCellDetail
                {
                    Id = currentCell.Id,
                    CommerceId = displayData.CommerceId,
                    ContentType = CellDetailTypes.CashIncentive
                };

                currentCell.DetailedContent = cashCellDetail;

                contentCells.Add(currentCell);

                //Build featured cash incentives carrousel

                currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = filterValue,
                    DisplayStructureTitle = true,
                    StructureTitle = "Paga con YOY",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnCarrousel,
                    MaxMinsToKeepStored = MaxMinsToStoreBodyContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayCashIncentiveDetailScreen,
                    RulingCriteriaType = ContentRulingCriterias.Suggestions,
                    ViewAllAccessType = ViewAllCellContentAccess.CashIncentiveContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Carrousel,
                    PageNumber = 0,
                    PageSize = pageSize,
                    CellsCount = 16,
                    Cells = new List<Cell>()
                };

                random = new Random();

                for (int i = 0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                }

                contentStructures.Add(currentStructure);

                //Build cash incentives grid
                currentStructure = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = filterValue,
                    DisplayStructureTitle = true,
                    StructureTitle = "Gana cashback",
                    MaxDisplayedCellsOnInitialStructure = MaxContentCellsOnGrid,
                    MaxMinsToKeepStored = MaxMinsToStoreBodyContent,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayCashIncentiveDetailScreen,
                    ViewAllAccessType = ViewAllCellContentAccess.CashIncentiveContentList,
                    StoreLocally = true,
                    StructureType = ContentStructureTypes.Grid,
                    PageNumber = 0,
                    PageSize = pageSize,
                    CellsCount = 32,
                    Cells = new List<Cell>()
                };

                random = new Random();

                for (int i = 0; i < currentStructure.CellsCount; ++i)
                {
                    currentStructure.Cells.Add(contentCells[random.Next(0, 1000) % contentCells.Count]);
                }

                contentStructures.Add(currentStructure);

            }
            catch(Exception e)
            {
                contentStructures = null;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, e.InnerException != null ? e.InnerException.Message : e.Message);
            }

            return contentStructures;
        }

        public ContentStructure BuildByCommerceCarrouselByFilter(string userId, int filterType, Guid filterValue, bool validLocation, Guid countryId, Guid userStateId, decimal latitude, decimal longitude, int geoSegmentationType, int logoHeight, int carrouselLogoHeight, int carrouselHeight, int pageNumber)
        {
            ContentStructure commerceOptions;

            int pageSize = 36;
            int callId = 4;
            string parameters = "UserId: " + userId + ", ImgHeight: " + carrouselHeight + ", ValidLocation: " + validLocation + ", GeoSegmentationType:" + geoSegmentationType;

            try
            {

                commerceOptions = new ContentStructure
                {
                    FeedSection = ContentFeedSectionTypes.Content,
                    ContentLevel = FeedContentLevels.Level2,
                    HasOwner = true,
                    CellOwnerId = filterValue,
                    DisplayStructureTitle = false,
                    StructureTitle = "Comercios populares",
                    OnSelectMemberActionType = OnSelectCellActionTypes.GoToDealsListForCommerce,
                    StructureType = ContentStructureTypes.Carrousel,
                    RulingCriteriaType = ContentRulingCriterias.Popular,
                    ViewAllAccessType = ViewAllCellContentAccess.CommerceList,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = MaxFilterValueCellsOnCarrousel,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>()
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
                            logoUrl = ImageAdapter.TransformImg(item.WhiteLogoUrl, logoHeight, (int)Math.Ceiling(logoHeight / logoWithWidthProp));

                            commerceFilterValueCell = new ContentFilterValueCell
                            {
                                Id = item.TenantId,
                                CommerceId = item.TenantId,
                                Type = CellTypes.Commerce,
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
                            logoUrl = ImageAdapter.TransformImg(item.WhiteLogoUrl, logoHeight, (int)Math.Ceiling(logoHeight / logoWithWidthProp));
                            thumbnailUrl = "";

                            commerceFilterValueCell = new ContentFilterValueCell
                            {
                                Id = item.TenantId,
                                CommerceId = item.TenantId,
                                Type = CellTypes.Commerce,
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
                                ContentType = CellDetailTypes.Commerce,
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

                        commerceOptions.Cells.Take(commerceOptions.Cells.Count - 7);
                    }
                }
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


        [AllowAnonymous]
        [Route("gets")]
        [HttpGet]
        public async Task<IActionResult> GetsAsync(string userId, string location, int sliderHeight, int dealImgHeight, int contentLogoHeight, int brandingLogoHeight, int cardLogoHeight, int cardImgHeight, int thumbnailHeight)
        {
            IActionResult result;
            string errorMsg;
            string parameters = "UserId: " + userId + ", Location: " + location;

            int callId = 1;


            try
            {
                Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                UserWithLocationAndMembershipData currentUser = this._businessObjects.Users.Get(userId, true);

                if (currentUser != null)
                {
                    ProcessedLocation processedLocation = LocationProcessor.ProcessLocation(location);

                    Guid contentStateId = Guid.Empty;

                    if (currentUser.StateId != null)
                    {
                        contentStateId = (Guid)currentUser.StateId;

                        if (!(bool)currentUser.StateOperationState && currentUser.StateNearestNeighbor != null)
                        {
                            contentStateId = (Guid)currentUser.StateNearestNeighbor;
                        }

                        //Needs to retrieve the offers by location
                        //candidateOffers = this._businessObjects.Offers.GetEnabledOffersByRegionWithLocation((Guid)currentUser.CountryId, contentStateId, (int)currentUser.ContentSegmentationType, currentUser.Id, (double)processedLocation.Latitude, (double)processedLocation.Longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, DateTime.UtcNow, selectorType, true, false, OfferPurposeTypes.Deal);
                    }

                    ContentFeed contentFeed = new ContentFeed
                    {
                        ReferenceType = ContentFeedReferenceTypes.None,
                        ReferenceId = Guid.Empty,
                        UserId = userId,
                        FeedType = ContentFeedTypes.MainFeed,
                        ContentRetrieveType = ContentRetrieveTypes.CompleRetreive,
                        ContentStructures = new List<ContentStructure>(),
                        StructresCount = 0
                    };

                    //-----------------------------------------------TASKS TO BUILD FEED CONTENT STRUCTURS---------------------------------------------------

                    //Filter type ids
                    Guid byCategoryFilterId = Guid.NewGuid();
                    Guid byCommerceFilterId = Guid.NewGuid();
                    Guid byShoppingMallId = Guid.NewGuid();

                    
                    ////Task to build slider
                    Task<ContentStructure> buildSlider = new Task<ContentStructure>(() => this.BuildSlider(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, sliderHeight));
                    buildSlider.Start();

                    
                    ////Task to build filterTypes
                    Task<ContentStructure> buildFilterTypes = new Task<ContentStructure>(() => this.BuildFilterTypes(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, byCategoryFilterId, byCommerceFilterId, byShoppingMallId));
                    buildFilterTypes.Start();

                    ////Task to build category filter options
                    Task<ContentStructure> buildCategoryCarrousel = new Task<ContentStructure>(() => this.BuildByCategoryFilterOptions(currentUser.Id, cardLogoHeight, byCategoryFilterId));
                    buildCategoryCarrousel.Start();

                    ////Task to build commerce filter options
                    Task<ContentStructure> buildCommerceCarrousel = new Task<ContentStructure>(() => this.BuildByCommerceFilterOptions(currentUser.Id,byCommerceFilterId, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                        processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, cardLogoHeight, cardImgHeight, thumbnailHeight, 0));
                    buildCommerceCarrousel.Start();

                    ////Task to build shopping mall filter options
                    Task<ContentStructure> buildShoppingMallCarrousel = new Task<ContentStructure>(() => this.BuildShoppingMallFilterOptions(currentUser.Id, byShoppingMallId, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                        processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, cardLogoHeight, 0));
                    buildShoppingMallCarrousel.Start();


                    ////Task to build shopping mall filter options
                    Task<List<ContentStructure>> buildBuildContentStructures = new Task<List<ContentStructure>>(() => this.BuildMainContentDeals(currentUser.Id, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                        processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, contentLogoHeight , brandingLogoHeight, dealImgHeight));
                    buildBuildContentStructures.Start();

                    //-----------------------------------------------TASKS CONTENT STRUCTURES RETRIEVAL----------------------------------------------------------------

                    //Add the slider to the content feed
                    ContentStructure slider =  await buildSlider; //this.BuildSlider(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, sliderHeight);//


                    //Add the filter types to the content feed
                    ContentStructure filterTypes = await buildFilterTypes; // this.BuildFilterTypes(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, byCategoryFilterId, byCommerceFilterId, byShoppingMallId);//


                    //Add the category carrousel to the content feed
                    ContentStructure categoryCarrousel = await buildCategoryCarrousel; // this.BuildByCategoryFilterOptions(currentUser.Id, logoHeight, byCategoryFilterId);//


                    //Add the commerce carrousel to the content feed
                    ContentStructure commerceCarrousel = await buildCommerceCarrousel;// this.BuildByCommerceFilterOptions(currentUser.Id, byCommerceFilterId, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                    //processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, logoHeight, carrouselLogoHeight, featuredImgHeight, thumbnailHeight, 0);//


                    //Add the commerce carrousel to the content feed
                    ContentStructure shoppingMallCarrousel = await buildShoppingMallCarrousel;// this.BuildShoppingMallFilterOptions(currentUser.Id, byShoppingMallId, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                    //processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, logoHeight, 0);//

                    List<ContentStructure> contentStructures = await buildBuildContentStructures; //this.BuildMainContentDeals(currentUser.Id, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                        //processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, logoHeight, dealLogoHeight, dealImgHeight); //


                    if (slider != null && slider.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(slider);
                    }
                    
                    //In here goes the header content structures
                    foreach(ContentStructure item in contentStructures)
                    {
                        if(item.FeedSection == ContentFeedSectionTypes.Header)
                        {
                            contentFeed.ContentStructures.Add(item);
                        }
                    }

                    contentStructures = contentStructures.Where(x => x.FeedSection == ContentFeedSectionTypes.Content).ToList();

                    if (filterTypes != null && filterTypes.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(filterTypes);
                    }

                    if (categoryCarrousel != null && categoryCarrousel.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(categoryCarrousel);
                    }

                    if (commerceCarrousel != null && commerceCarrousel.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(commerceCarrousel);
                    }

                    if (shoppingMallCarrousel != null && shoppingMallCarrousel.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(shoppingMallCarrousel);
                    }

                    contentFeed.ContentStructures.AddRange(contentStructures);



                    //NOW WILL SEND THE RESPONDE
                    contentFeed.StructresCount = contentFeed.ContentStructures?.Count ?? 0;

                    result = Ok(contentFeed);
                }
                else
                {
                    errorMsg = "Error: Invalid user or location";

                    result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidUserOrLocation"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
                }
            }
            catch (Exception e)
            {
                errorMsg = "Error: An unexpected issue at main feed retrieving: " + e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;
        }
        
        [AllowAnonymous]
        [Route("getsByFilter")]
        [HttpGet]
        public async Task<IActionResult> GetsAsync(string userId, string location, int filterType, Guid filterValue, int dealImgHeight, int contentLogoHeight, int brandingLogoHeight, int cardLogoHeight, int cardImgHeight, int thumbnailHeight)
        {
            IActionResult result;
            string errorMsg;
            string parameters = "UserId: " + userId + ", Location: " + location + ", FilterType: " + filterType + ", FilterValue: "+ filterValue;

            int callId = 2;

            try
            {
                Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                UserWithLocationAndMembershipData currentUser = this._businessObjects.Users.Get(userId, true);

                if (currentUser != null)
                {
                    ProcessedLocation processedLocation = LocationProcessor.ProcessLocation(location);

                    Guid contentStateId = Guid.Empty;

                    if (currentUser.StateId != null)
                    {
                        contentStateId = (Guid)currentUser.StateId;

                        if (!(bool)currentUser.StateOperationState && currentUser.StateNearestNeighbor != null)
                        {
                            contentStateId = (Guid)currentUser.StateNearestNeighbor;
                        }

                    }

                    ContentFeed contentFeed = new ContentFeed
                    {
                        ReferenceType = ContentFeedReferenceTypes.Category,
                        ReferenceId = filterValue,
                        UserId = userId,
                        FeedType = ContentFeedTypes.None,
                        ContentRetrieveType = ContentRetrieveTypes.ContentSection,
                        ContentStructures = new List<ContentStructure>(),
                        StructresCount = 0
                    };

                    switch (filterType)
                    {
                        case ContentFilterTypes.Category:

                            contentFeed.FeedType = ContentFeedTypes.ByCategoryContent;

                            break;
                        case ContentFilterTypes.Commerce:

                            contentFeed.FeedType = ContentFeedTypes.ByCommerceContent;

                            break;
                        case ContentFilterTypes.ShoppingMall:

                            contentFeed.FeedType = ContentFeedTypes.ByShoppingMallContent;

                            break;
                    }

                    //Task to build shopping mall filter options
                    Task<List<ContentStructure>> buildBuildContentStructures = new Task<List<ContentStructure>>(() => this.BuildContentByFilter(currentUser.Id, filterType, filterValue, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                        processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, contentLogoHeight, brandingLogoHeight, dealImgHeight));
                    buildBuildContentStructures.Start();

                    //-----------------------------------------------TASKS CONTENT STRUCTURES RETRIEVAL----------------------------------------------------------------

                                                                         //processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, logoImgHeight, featuredImgHeight, thumbnailHeight, 0);//

                    List<ContentStructure> contentStructures = await buildBuildContentStructures;

                    if (contentStructures?.Count > 0)
                    {
                        int middel = (int)Math.Floor((double)(contentStructures.Count / 2));

                        for (int i = 0; i < middel; ++i)
                        {
                            contentFeed.ContentStructures.Add(contentStructures[i]);
                        }

                        //contentFeed.ContentStructures.Add(commerceCarrousel);

                        for (int i = middel; i < contentStructures.Count; ++i)
                        {
                            contentFeed.ContentStructures.Add(contentStructures[i]);
                        }

                    }


                    contentFeed.StructresCount = contentFeed.ContentStructures?.Count ?? 0;

                    result = Ok(contentFeed);
                }
                else
                {
                    errorMsg = "Error: Invalid user or location";

                    result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidUserOrLocation"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
                }
            }
            catch(Exception e)
            {
                errorMsg = "Error: An unexpected issue at main feed retrieving: " + e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;
        }

        #endregion

        #region CONSTRUCTORS
        public MainFeedController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion
    }
}
