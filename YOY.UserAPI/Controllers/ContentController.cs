using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Manager.Misc.InterestPreference;
using YOY.DTO.Entities.Misc.Location;
using YOY.DTO.Entities.Misc.TenantData;
using YOY.DTO.Entities.Misc.User;
using YOY.UserAPI.Logic.Image;
using YOY.UserAPI.Logic.Location;
using YOY.UserAPI.Models.v1.Content.POCO;
using YOY.UserAPI.Models.v1.Content.SET;
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
    public class ContentController : ControllerBase
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
        private const string CategoryUnSelectedAppend = "b";
        private const double slideWidthProp = 0.66666666666666667;
        private const double logoWithWidthProp = 1;
        private const double dealImgWidthProp = 1;
        private const double commerceCarrouselImgWidthProp = 0.375;

        private const int MaxMinsToStoreHeaderContent = 30;
        public const int MaxMinsToStoreBodyContent = 10;

        public const int MaxMetersToStoreContent = 1000;

        private const int MaxFilterValueCellsOnCarrousel = 8;
        private const int MaxContentCellsOnCarrousel = 8;
        private const int MacContentCellsOnGrid = 16;

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
                    HasOwner = false,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = false,
                    StructureTitle = "",
                    OnSelectMemberActionType = OnSelectCellActionTypes.DisplayPromotionalContent,
                    StructureType = ContentStructureTypes.Slider,
                    ViewAllAccessType = ViewAllCellContentAccess.None,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = -1,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>()
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

                SlideCellDetailContent slideCellDetailContent = new SlideCellDetailContent
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
                    DetailedContent = new CellDetailContent()
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

                slideCellDetailContent = new SlideCellDetailContent
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
                    DetailedContent = new CellDetailContent()
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

                slideCellDetailContent = new SlideCellDetailContent
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
                    HasOwner = false,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = true,
                    StructureTitle = _localizer["Filter"].Value,
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateContentFilterOptions,
                    StructureType = ContentStructureTypes.Carrousel,
                    ViewAllAccessType = ViewAllCellContentAccess.None,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = -1,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>()
                };

                //1st filter option
                Cell filterType = new Cell
                {
                    Id = byCategoryFilterId,
                    Type = CellTypes.FilterType,
                    OnSelectAction = OnSelectCellActionTypes.DisplayPromotionalContent,
                    DetailedContent = new CellDetailContent(),
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
                    DetailedContent = new CellDetailContent(),
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
                    DetailedContent = new CellDetailContent(),
                };

                filterTypeCell = new ContentFilterTypeCell
                {
                    CommerceId = Guid.Empty,
                    Id = byShoppingMallId,
                    Type = CellTypes.FilterType,
                    FilterType = ContentFilterTypes.Commerce,
                    FilterName = _localizer["ByCommerce"].Value
                };

                filterType.DisplayData = filterTypeCell;

                //Add 3rd filter type
                filterTypes.Cells.Add(filterType);

                filterTypes.CellsCount = filterTypes.Cells?.Count ?? 0;

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
                    HasOwner = true,
                    CellOwnerId = byCategoryFilterId,
                    DisplayStructureTitle = false,
                    StructureTitle = _localizer["ByCategory"].Value,
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateDisplayedContent,
                    StructureType = ContentStructureTypes.Carrousel,
                    ViewAllAccessType = ViewAllCellContentAccess.CategoryList,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = MaxFilterValueCellsOnCarrousel,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>()
                };

                //Retrieves preferences List
                List<UserPreferenceData> categoryPreferences = this._businessObjects.UserInterests.GetPreferences(userId);

                if (categoryPreferences != null && categoryPreferences?.Count > 0)
                {
                    
                    Cell filterTypeValue;
                    ContentFilterValueCell categoryCell;
                    CategoryCellDetailContent cellDetailContent;
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
                            Name = item.Name,
                            UnselectedIcon = imgUrl + CategoryUnSelectedAppend,
                            SelectedIcon = imgUrl + CategorySelectedAppend,
                            UnselectedImgUrl = "",
                            SelectedImgUrl = ""
                        };

                        filterTypeValue.DisplayData = categoryCell;

                        cellDetailContent = new CategoryCellDetailContent
                        {
                            Id = item.Id,
                            ContentType = CellDetailTypes.Category,
                            Name = item.Name,
                            ImgUrl = imgUrl + CategoryUnSelectedAppend
                        };

                        filterTypeValue.DetailedContent = cellDetailContent;

                        categoryOptions.Cells.Add(filterTypeValue);
                    }
                }

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


        public ContentStructure BuildByCommerceFilterOptions(string userId, Guid byCommerceFilterId, bool validLocation, Guid countryId, Guid userStateId, double latitude, double longitude, int geoSegmentationType, int logoHeight, int carrouselHeight, int thumbnailHeight, int pageNumber)
        {
            ContentStructure commerceOptions;

            int pageSize = 36;
            int callId = 4;
            string parameters = "UserId: " + userId + ", ImgHeight: " + carrouselHeight + ", ValidLocation: " + validLocation + ", GeoSegmentationType:" + geoSegmentationType;

            try
            {

                commerceOptions = new ContentStructure
                {
                    HasOwner = true,
                    CellOwnerId = byCommerceFilterId,
                    DisplayStructureTitle = false,
                    StructureTitle = _localizer["ByCommerce"].Value,
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateDisplayedContent,
                    StructureType = ContentStructureTypes.Carrousel,
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
                CommerceCellDetailContent commerceDetailContent;
                string carrouselImgUrl;
                string thumbnailUrl;
                string logoUrl;

                //If we can determine user location and based on it show him contextual commerces
                if (validLocation)
                {

                    switch (geoSegmentationType)
                    {
                        case GeoSegmentationTypes.Country:

                            displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, countryId, Guid.Empty, GeoSegmentationTypes.Country, latitude, longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, pageSize, pageNumber);

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

                                displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, userState.CountryId, stateId, GeoSegmentationTypes.State, latitude, longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, pageSize, pageNumber);
                            }
                            else
                            {//If state isn't in operation, retrieve preferences based in country and geolocation
                                displayData = this._businessObjects.Commerces.GetTenantsDisplayData(userId, userState.CountryId, latitude, longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, pageSize, pageNumber);
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
                            commerceDetailContent = new CommerceCellDetailContent
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
                            thumbnailUrl = ImageAdapter.TransformImg(item.ThumbnailUrl, thumbnailHeight, (int)Math.Ceiling(thumbnailHeight / logoWithWidthProp));

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
                            commerceDetailContent = new CommerceCellDetailContent
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
                    }
                }
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

        private ContentStructure BuildShoppingMallFilterOptions(string userId, Guid byShoppingFilterId, bool validLocation, Guid countryId, Guid userStateId, double latitude, double longitude, int geoSegmentationType, int logoHeight, int carrouselHeight, int thumbnailHeight, int pageNumber)
        {
            ContentStructure shoppingMallOptions;

            int callId = 5;
            int pageSize = 36;
            string LogoUrl = "";
            string parameters = "UserId: " + userId + ", ImgHeight: " + carrouselHeight + ", ValidLocation: " + validLocation + ", GeoSegmentationType:" + geoSegmentationType;

            try
            {
                //Build the slider
                shoppingMallOptions = new ContentStructure
                {
                    HasOwner = true,
                    CellOwnerId = byShoppingFilterId,
                    DisplayStructureTitle = false,
                    StructureTitle = "",
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateDisplayedContent,
                    StructureType = ContentStructureTypes.Slider,
                    ViewAllAccessType = ViewAllCellContentAccess.ShoppingMallList,
                    StoreLocally = true,
                    MaxMinsToKeepStored = MaxMinsToStoreHeaderContent,
                    MaxDisplayedCellsOnInitialStructure = MaxFilterValueCellsOnCarrousel,
                    MaxMetersToKeepStored = MaxMetersToStoreContent,
                    CellsCount = 0,
                    Cells = new List<Cell>()
                };

                //1st slide
                Cell filterTypeValue = new Cell
                {
                    Id = Guid.NewGuid(),
                    Type = CellTypes.ShoppingMall,
                    OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent
                };

                LogoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596433873/dev/testing/logoSM1.png", logoHeight, (int)Math.Ceiling(logoHeight / logoWithWidthProp));

                ContentFilterValueCell shoppingMallFilterValueCell = new ContentFilterValueCell
                {
                    Id = filterTypeValue.Id,
                    Type = CellTypes.ShoppingMall,
                    CommerceId = Guid.NewGuid(),
                    Name = "Express",
                    SelectedIcon = LogoUrl,
                    UnselectedIcon = LogoUrl,
                    SelectedImgUrl = "",
                    UnselectedImgUrl = ""
                };

                filterTypeValue.DisplayData = shoppingMallFilterValueCell;

                ShoppingMallCellDetailContent shoppingMallDetailContent = new ShoppingMallCellDetailContent
                {
                    ContentType = CellDetailTypes.ShoppingMall,
                    Id = shoppingMallFilterValueCell.Id,
                    ImgUrl = LogoUrl,
                    MainName = "Alamada",
                    BranchName = "Express"
                };

                filterTypeValue.DetailedContent = shoppingMallDetailContent;

                //Add 1st shopping mall
                shoppingMallOptions.Cells.Add(filterTypeValue);

                //2nd shoppingMall
                filterTypeValue = new Cell
                {
                    Id = Guid.NewGuid(),
                    Type = CellTypes.ShoppingMall,
                    OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent
                };

                LogoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596433873/dev/testing/logoSM2.png", logoHeight, (int)Math.Ceiling(logoHeight / logoWithWidthProp));

                shoppingMallFilterValueCell = new ContentFilterValueCell
                {
                    Id = filterTypeValue.Id,
                    Type = CellTypes.ShoppingMall,
                    CommerceId = Guid.NewGuid(),
                    Name = "Plaza Norte",
                    SelectedIcon = LogoUrl,
                    UnselectedIcon = LogoUrl,
                    SelectedImgUrl = "",
                    UnselectedImgUrl = ""
                };

                filterTypeValue.DisplayData = shoppingMallFilterValueCell;

                shoppingMallDetailContent = new ShoppingMallCellDetailContent
                {
                    ContentType = CellDetailTypes.ShoppingMall,
                    Id = shoppingMallFilterValueCell.Id,
                    ImgUrl = LogoUrl,
                    MainName = "Hyatt",
                    BranchName = "Plaza Norte"
                };

                filterTypeValue.DetailedContent = shoppingMallDetailContent;

                //Add 2nd slide
                shoppingMallOptions.Cells.Add(filterTypeValue);

                //3rd shopping mall
                filterTypeValue = new Cell
                {
                    Id = Guid.NewGuid(),
                    Type = CellTypes.ShoppingMall,
                    OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent
                };

                LogoUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596433873/dev/testing/logoSM3.png", logoHeight, (int)Math.Ceiling(logoHeight / logoWithWidthProp));

                shoppingMallFilterValueCell = new ContentFilterValueCell
                {
                    Id = filterTypeValue.Id,
                    Type = CellTypes.ShoppingMall,
                    CommerceId = Guid.NewGuid(),
                    Name = "Metropoli",
                    SelectedIcon = LogoUrl,
                    UnselectedIcon = LogoUrl,
                    SelectedImgUrl = "",
                    UnselectedImgUrl = ""
                };

                filterTypeValue.DisplayData = shoppingMallFilterValueCell;

                shoppingMallDetailContent = new ShoppingMallCellDetailContent
                {
                    ContentType = CellDetailTypes.ShoppingMall,
                    Id = shoppingMallFilterValueCell.Id,
                    ImgUrl = LogoUrl,
                    MainName = "AEON",
                    BranchName = "Metropoli"
                };

                filterTypeValue.DetailedContent = shoppingMallDetailContent;

                //Add 3rd slide
                shoppingMallOptions.Cells.Add(filterTypeValue);

                for(int i = 0; i< 10; ++i)
                {
                    shoppingMallOptions.Cells.Add(shoppingMallOptions.Cells[i % 3]);
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


        [Route("gets")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetsAsync(string userId, string location, int sliderHeight, int dealImgHeight, int logoImgHeight, int featuredImgHeight, int thumbnailHeight)
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

                    
                    //Task to build slider
                    Task<ContentStructure> buildSlider = new Task<ContentStructure>(() => this.BuildSlider(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, sliderHeight));
                    buildSlider.Start();

                    
                    //Task to build filterTypes
                    Task<ContentStructure> buildFilterTypes = new Task<ContentStructure>(() => this.BuildFilterTypes(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, byCategoryFilterId, byCommerceFilterId, byShoppingMallId));
                    buildFilterTypes.Start();

                    //Task to build category filter options
                    Task<ContentStructure> buildCategoryCarrousel = new Task<ContentStructure>(() => this.BuildByCategoryFilterOptions(currentUser.Id, logoImgHeight, byCategoryFilterId));
                    buildCategoryCarrousel.Start();

                    //Task to build commerce filter options
                    Task<ContentStructure> buildCommerceCarrousel = new Task<ContentStructure>(() => this.BuildByCommerceFilterOptions(currentUser.Id,byCommerceFilterId, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                        processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, logoImgHeight, featuredImgHeight, thumbnailHeight, 0));
                    buildCommerceCarrousel.Start();

                    //Task to build shopping mall filter options
                    Task<ContentStructure> buildShoppingMallCarrousel = new Task<ContentStructure>(() => this.BuildShoppingMallFilterOptions(currentUser.Id, byCommerceFilterId, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                        processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, logoImgHeight, featuredImgHeight, thumbnailHeight, 0));
                    buildShoppingMallCarrousel.Start();


                    //-----------------------------------------------TASKS CONTENT STRUCTURES RETRIEVAL----------------------------------------------------------------

                    //Add the slider to the content feed
                    ContentStructure slider =  await buildSlider; //this.BuildSlider(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, sliderHeight);//

                    if (slider != null && slider.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(slider);
                    }


                    //Add the filter types to the content feed
                    ContentStructure filterTypes = await buildFilterTypes; // this.BuildFilterTypes(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, byCategoryFilterId, byCommerceFilterId, byShoppingMallId);//

                    if(filterTypes != null && filterTypes.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(filterTypes);
                    }


                    //Add the category carrousel to the content feed
                    ContentStructure categoryCarrousel = await buildCategoryCarrousel; // this.BuildByCategoryFilterOptions(currentUser.Id, logoImgHeight, byCategoryFilterId);//

                    if (categoryCarrousel != null && categoryCarrousel.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(categoryCarrousel);
                    }

                    //Add the commerce carrousel to the content feed
                    ContentStructure commerceCarrousel = await buildCommerceCarrousel;// this.BuildByCommerceFilterOptions(currentUser.Id, byCommerceFilterId, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                        //processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, logoImgHeight, featuredImgHeight, thumbnailHeight, 0);//

                    if (commerceCarrousel != null && commerceCarrousel.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(commerceCarrousel);
                    }

                    //Add the commerce carrousel to the content feed
                    ContentStructure shoppingMallCarrousel = await buildShoppingMallCarrousel;// this.BuildByCommerceFilterOptions(currentUser.Id, byCommerceFilterId, processedLocation.ValidLocation, (Guid)currentUser.CountryId, (Guid)currentUser.StateId,
                                                                                      //processedLocation.Latitude ?? 0, processedLocation.Longitude ?? 0, currentUser.ContentSegmentationType ?? GeoSegmentationTypes.Country, logoImgHeight, featuredImgHeight, thumbnailHeight, 0);//

                    if (shoppingMallCarrousel != null && shoppingMallCarrousel.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(shoppingMallCarrousel);
                    }

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
                errorMsg = "Error: An unexpected issue at preferences retrieving: " + e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;
        }
        

        /*
        [Route("gets")]
        [HttpGet]
        public IActionResult Gets(string userId, string location, int filterType, Guid filterValue)
        {
            try
            {
                Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                UserWithLocationAndMembershipData currentUser = this._businessObjects.Users.Get(userId, true);

                if (currentUser != null)
                {
                    ProcessedLocation processedLocation = LocationProcessor.ProcessLocation(location);


                }
            }
            catch(Exception e)
            {

            }
        }*/

        #endregion

        #region CONSTRUCTORS
        public ContentController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion
    }
}
