using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Manager.Misc.InterestPreference;
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
        private const double slideWidthProp = 1;
        private const double logoWithWidthProp = 1;
        private const double dealImgWidthProp = 1;
        private const double commerceCarrouselImgWidthProp = 2.5;

        private const int MaxFilterValueCellsOnCarrousel = 10;
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
                    CellsCount = 0,
                    Cells = new List<Cell>()
                };

                //1st slide
                Cell slideCell = new Cell
                {
                    Id = Guid.NewGuid(),
                    Type = CellTypes.Slide,
                    OnSelectAction = OnSelectCellActionTypes.DisplayPromotionalContent,
                    DetailedContent = new CellDetailContent()
                };

                Slide currentSlide = new Slide
                {
                    Id = slideCell.Id,
                    ImgUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596323484/dev/testing/slider2.png", imgHeight, (int)Math.Ceiling(imgHeight * slideWidthProp)),
                    ExpirationDate = DateTime.UtcNow.AddDays(3).ToString("yyyy-MM-dd HH':'mm':'ss"),
                    CommerceId = new Guid("2e0dc7af-791e-4f13-b9c7-9a2f59a4cd86"),
                    CountryId = new Guid("d7319a46-1389-488d-ba81-c60cd09be87a"),
                    StateId = new Guid("3b1de628-d6fd-45a9-b6c4-8523f3bd7677"),
                    Type = CellTypes.Slide
                };

                slideCell.DisplayData = currentSlide;

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
                    ImgUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596323484/dev/testing/slider3.png", imgHeight, (int)Math.Ceiling(imgHeight * slideWidthProp)),
                    ExpirationDate = DateTime.UtcNow.AddDays(5).ToString("yyyy-MM-dd HH':'mm':'ss"),
                    CommerceId = new Guid("2e0dc7af-791e-4f13-b9c7-9a2f59a4cd86"),
                    CountryId = new Guid("d7319a46-1389-488d-ba81-c60cd09be87a"),
                    StateId = new Guid("3b1de628-d6fd-45a9-b6c4-8523f3bd7677"),
                    Type = CellTypes.Slide
                };

                slideCell.DisplayData = currentSlide;

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
                    ImgUrl = ImageAdapter.TransformImg("https://res.cloudinary.com/yoyimgs/image/upload/v1596323484/dev/testing/slider1.png", imgHeight, (int)Math.Ceiling(imgHeight * slideWidthProp)),
                    ExpirationDate = DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd HH':'mm':'ss"),
                    CommerceId = new Guid("2e0dc7af-791e-4f13-b9c7-9a2f59a4cd86"),
                    CountryId = new Guid("d7319a46-1389-488d-ba81-c60cd09be87a"),
                    StateId = new Guid("3b1de628-d6fd-45a9-b6c4-8523f3bd7677"),
                    Type = CellTypes.Slide
                };

                slideCell.DisplayData = currentSlide;

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

        public ContentStructure BuildCategoryFilterOptions(string userId, int iconHeight)
        {
            ContentStructure categoryOptions;

            int callId = 3;
            string parameters = "UserId: " + userId + ", ImgHeight: " + iconHeight;

            try
            {

                categoryOptions = new ContentStructure
                {
                    HasOwner = false,
                    CellOwnerId = Guid.Empty,
                    DisplayStructureTitle = true,
                    StructureTitle = _localizer["ByCategory"].Value,
                    OnSelectMemberActionType = OnSelectCellActionTypes.UpdateDisplayedContent,
                    StructureType = ContentStructureTypes.Carrousel,
                    ViewAllAccessType = ViewAllCellContentAccess.CategoryList,
                    CellsCount = 0,
                    Cells = new List<Cell>()
                };

                //Retrieves preferences List
                List<UserPreferenceData> categoryPreferences = this._businessObjects.UserInterests.GetPreferences(userId);

                if (categoryPreferences != null && categoryPreferences?.Count > 0)
                {
                    //1st filter option
                    Cell filterTypeValue;
                    ContentFilterValueCell categoryCell;
                    string imgUrl;

                    foreach(UserPreferenceData item in categoryPreferences)
                    {
                        filterTypeValue = new Cell
                        {
                            Id = item.Id,
                            Type = CellTypes.Category,
                            OnSelectAction = OnSelectCellActionTypes.UpdateDisplayedContent,
                            DetailedContent = new CellDetailContent()
                        };

                        imgUrl = ImageAdapter.TransformImg(item.BaseImgUrl, iconHeight, (int)Math.Ceiling(iconHeight * logoWithWidthProp));

                        categoryCell = new ContentFilterValueCell
                        {
                            Id = item.Id,
                            CommerceId = Guid.Empty,
                            Type = CellTypes.Category,
                            Name = item.Name,
                            UnselectedImgUrl = imgUrl + CategoryUnSelectedAppend,
                            SelectedImgUrl = imgUrl + CategorySelectedAppend
                        };

                        filterTypeValue.DisplayData = categoryCell;

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

        /*public List<ContentStructure> BuildFiltersWithLocation(string userId, Guid stateId, Guid countryId, decimal latitude, decimal longitude)
        {

        }*/

        /*
        [Route("gets")]
        [HttpGet]
        public async Task<IActionResult> GetsAsync(string userId, string location, int sliderHeight, int dealImgHeight, int logoHeight, int commerceCarrouselHeight)
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

                    if (processedLocation.ValidLocation)
                    {
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
                        else
                        {
                            //candidateOffers = new List<FullOfferData>();
                        }
                    }
                    else
                    {

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

                    //Task to build slider
                    Task<ContentStructure> buildSlider = new Task<ContentStructure>(() => this.BuildSlider(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, sliderHeight));
                    buildSlider.Start();

                    //Filter type ids
                    Guid byCategoryFilterId = Guid.NewGuid();
                    Guid byCommerceFilterId = Guid.NewGuid();
                    Guid byShoppingMallId = Guid.NewGuid();

                    //Task to build filterTypes
                    Task<ContentStructure> buildFilterTypes = new Task<ContentStructure>(() => this.BuildFilterTypes(currentUser.Id, contentStateId, (Guid)currentUser.CountryId, byCategoryFilterId, byCommerceFilterId, byShoppingMallId));
                    buildSlider.Start();

                    //Task to build category filter options

        //-----------------------------------------------TASKS CONTENT STRUCTURES RETRIEVAL----------------------------------------------------------------

                    //Add the slider to the content feed
                    ContentStructure slider = await buildSlider;

                    if(slider != null && slider.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(slider);
                    }


                    //Add the filter types to the content feed
                    ContentStructure filterTypes = await buildFilterTypes;

                    if(filterTypes != null && filterTypes.Cells?.Count > 0)
                    {
                        contentFeed.ContentStructures.Add(filterTypes);
                    }


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

            }
        }
        */

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
