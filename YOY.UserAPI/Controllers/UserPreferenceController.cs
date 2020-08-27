using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Manager.Misc.InterestPreference;
using YOY.DTO.Entities.Misc.Location;
using YOY.DTO.Entities.Misc.User;
using YOY.UserAPI.Logic.Image;
using YOY.UserAPI.Logic.Location;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.UserAPI.Models.v1.Miscellaneous.Location.POCO;
using YOY.UserAPI.Models.v1.UserPreference.POCO;
using YOY.UserAPI.Models.v1.UserPreference.SET;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class UserPreferenceController : ControllerBase
    {
        #region PROPERTIES_AND_RESOURCES

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


        private static int controllerVersion = 1;
        private readonly int PageSize = 48;
        private readonly string CategorySelectedAppend = "w";
        private readonly string CategoryUnSelectedAppend = "b";
        private const double ImgWidthProp = 1;

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
                _businessObjects = BusinessObjects.GetInstance(_tenant);
            }

            if (_businessObjects == null)
            {
                _businessObjects = BusinessObjects.GetInstance(_tenant);
            }
        }

        [AllowAnonymous]
        [Route("gets")]
        [HttpGet]
        public IActionResult Gets(string userId, string location, int imgHeight)//latitude*longitude
        {
            IActionResult result;
            int callId = 1;
            string errorMsg;
            string parameters = "UserId: " + userId + ", Location: " + location;

            try
            {
                Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                UserWithLocationAndMembershipData currentUser = this._businessObjects.Users.Get(userId, true);

                if (currentUser != null)
                {

                    ProcessedLocation processedLocation = LocationProcessor.ProcessLocation(location);


                    EnabledPreferences preferences = new EnabledPreferences
                    {
                        Categories = new List<UserPreferenceData>(),
                        Commerces = new List<UserPreferenceData>(),
                        ContentType = UserPreferenceResponseContentType.FullPreferenceSet
                    };

                    //First retrieve all the existing preferences

                    //CATEGORY PREFERENCES
                    List<UserPreferenceData> categoryPreferences = this._businessObjects.UserInterests.GetPreferences(currentUser.Id);

                    if (categoryPreferences?.Count > 0)
                    {
                        foreach (UserPreferenceData item in categoryPreferences)
                        {

                            item.BaseImgUrl = ImageAdapter.TransformImg(item.BaseImgUrl, imgHeight, (int)Math.Ceiling(imgHeight * ImgWidthProp));


                            item.UnSeletedImgUrl = item.BaseImgUrl + CategoryUnSelectedAppend;
                            item.SelectedImgUrl = item.BaseImgUrl + CategorySelectedAppend;

                            item.BaseImgUrl = null;//to avoid it to go in the payload
                        }

                        preferences.Categories = categoryPreferences;
                    }


                    string imgUrl;
                    List<UserPreferenceData> tenantPreferences = null;
                    bool currentStateAlreadyTried = false;

                    //If we can determine user location and based on it show him contextual commerces
                    if (processedLocation.ValidLocation)
                    {
                        //COMMERCE PREFERENCES

                        switch (currentUser.ContentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:

                                tenantPreferences = this._businessObjects.UserInterests.GetPreferences(currentUser.Id, (Guid)currentUser.CountryId, Guid.Empty, GeoSegmentationTypes.Country, (double)processedLocation.Latitude, (double)processedLocation.Longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, PageSize, 0);

                                if (tenantPreferences?.Count == 0)//If no tenants nearby, then retrieve from the country
                                {

                                    tenantPreferences = this._businessObjects.UserInterests.GetPreferences(currentUser.Id, (Guid)currentUser.CountryId, Guid.Empty, GeoSegmentationTypes.Country, PageSize, 0);
                                }

                                break;
                            case GeoSegmentationTypes.State:

                                State userState = this._businessObjects.States.Get((Guid)currentUser.StateId);
                                Guid stateId = Guid.Empty;

                                //1st will try to use state and location
                                if (userState.InOperation)
                                {
                                    stateId = userState.Id;
                                    currentStateAlreadyTried = true;

                                    tenantPreferences = this._businessObjects.UserInterests.GetPreferences(currentUser.Id, userState.CountryId, stateId, GeoSegmentationTypes.State, (double)processedLocation.Latitude, (double)processedLocation.Longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, PageSize, 0);
                                }
                                else
                                {//If state isn't in operation, retrieve preferences based in country and geolocation
                                    tenantPreferences = this._businessObjects.UserInterests.GetPreferences(currentUser.Id, userState.CountryId, (double)processedLocation.Latitude, (double)processedLocation.Longitude, DistanceLimits.MaxKMRangeToShowOffers * 1000, PageSize, 0);
                                }

                                if (tenantPreferences?.Count == 0)
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

                                    tenantPreferences = this._businessObjects.UserInterests.GetPreferences(currentUser.Id, userState.CountryId, stateId, GeoSegmentationTypes.State, PageSize, 0);
                                }

                                break;
                        }


                        if (tenantPreferences?.Count > 0)
                        {
                            preferences.Commerces = tenantPreferences;

                            foreach (UserPreferenceData item in preferences.Commerces)
                            {
                                if (!string.IsNullOrWhiteSpace(item.BaseImgUrl))
                                {

                                    imgUrl = item.BaseImgUrl;
                                }
                                else
                                {
                                    imgUrl = "";
                                }

                                item.BaseImgUrl = ImageAdapter.TransformImg(item.BaseImgUrl, imgHeight, (int)Math.Ceiling(imgHeight * ImgWidthProp));


                                item.UnSeletedImgUrl = imgUrl;
                                item.SelectedImgUrl = imgUrl;

                                item.BaseImgUrl = null;//to avoid it to go in the payload
                            }
                        }
                    }
                    else
                    {
                        switch (currentUser.ContentSegmentationType)
                        {
                            case GeoSegmentationTypes.Country:

                                tenantPreferences = this._businessObjects.UserInterests.GetPreferences(currentUser.Id, (Guid)currentUser.CountryId, Guid.Empty, GeoSegmentationTypes.Country, PageSize, 0);

                                break;
                            case GeoSegmentationTypes.State:

                                State userState = this._businessObjects.States.Get((Guid)currentUser.StateId);
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

                                tenantPreferences = this._businessObjects.UserInterests.GetPreferences(currentUser.Id, userState.CountryId, stateId, GeoSegmentationTypes.State, PageSize, 0);
                                break;
                        }

                        if (tenantPreferences?.Count > 0)
                        {
                            preferences.Commerces = tenantPreferences;

                            foreach (UserPreferenceData item in preferences.Commerces)
                            {
                                if (!string.IsNullOrWhiteSpace(item.BaseImgUrl))
                                {

                                    imgUrl = item.BaseImgUrl;
                                }
                                else
                                {
                                    imgUrl = "";
                                }

                                item.SelectedImgUrl = imgUrl;
                                item.UnSeletedImgUrl = imgUrl;
                                item.BaseImgUrl = null;
                            }
                        }
                    }

                    //If it doesn't have commerces to display will display only 100
                    if (preferences.Commerces.Count == 0)
                    {
                        if (preferences.Categories.Count > 0)
                            preferences.ContentType = UserPreferenceResponseContentType.FullPreferenceSet;
                        else
                            preferences.ContentType = UserPreferenceResponseContentType.OnlyCategories;
                    }
                    else
                    {
                        if (preferences.Categories.Count > 0)
                            preferences.ContentType = UserPreferenceResponseContentType.OnlyCategories;
                        else
                            preferences.ContentType = UserPreferenceResponseContentType.None;
                    }



                    result = Ok(preferences);
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

        /// <summary>
        /// Let user update the preferences
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("post")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserChosenPreferenceSet model)
        {
            IActionResult result = null;
            int callId = 2;
            string errorMsg;
            string parameters = model.ToString();

            try
            {

                Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                if (ModelState.IsValid && model.Categories?.Count > 0 && model.Commerces?.Count > 0)
                {
                    UserData currentUser = this._businessObjects.Users.Get(model.UserId, UserKeys.UserId);

                    if (currentUser != null)
                    {


                        //Retrieves user current interests
                        List<UserInterest> userInterests = this._businessObjects.UserInterests.GetPreferencesForUser(currentUser.Id, InterestTypes.All, ActiveStates.All);


                        /*
                        UserInterest currentInterest = null;
                        bool exists = true;

                        Category matchCategory;

                        //Creates categories interests
                        foreach (ChosenPreference item in model.Categories)
                        {
                            exists = false;

                            //Check if that is an already existent interest
                            for (int i = 0; i < userInterests.Count && !exists; ++i)
                            {
                                currentInterest = userInterests.ElementAt(i);

                                //Check if current existent interest match with the current one sent from backend
                                if (currentInterest.InterestType == InterestTypes.Category && currentInterest.InterestId == item.Id)
                                {
                                    //Remove the existent interest from the interest list
                                    userInterests.RemoveAt(i);
                                    //Mark that the interest alredy exists
                                    exists = true;
                                }
                            }

                            //If it's a new interest
                            if (!exists)
                            {
                                matchCategory = this._businessObjects.Categories.Get(item.Id);

                                //Creates a new interest
                                currentInterest = this._businessObjects.UserInterests.Post(currentUser.Id, item.Id, InterestTypes.Category, InterestOrigintTypes.UserSelection, (int)matchCategory.Herarchy);

                                if (currentInterest == null)
                                {
                                    new NullReferenceException("Preference creation failed");
                                }
                            }
                            else
                            {
                                //If interest exists and it's inactive
                                if (!currentInterest.IsActive)
                                {
                                    //Just need to update the active state
                                    this._businessObjects.UserInterests.Put(currentUser.Id, item.Id);
                                }

                            }

                        }


                        //Creates commerces interest
                        foreach (ChosenPreference item in model.Commerces)
                        {
                            exists = false;

                            //Check if that is an already existent interest
                            for (int i = 0; i < userInterests.Count && !exists; ++i)
                            {
                                currentInterest = userInterests.ElementAt(i);

                                //Check if current existent interest match with the current one sent from backend
                                if (currentInterest.InterestType == InterestTypes.Tenant && currentInterest.InterestId == item.Id)
                                {
                                    //Remove the existent interest from the interest list
                                    userInterests.RemoveAt(i);
                                    //Mark that the interest alredy exists
                                    exists = true;
                                }
                            }

                            //If it's a new interest
                            if (!exists)
                            {
                                //Creates a new interest
                                currentInterest = this._businessObjects.UserInterests.Post(currentUser.Id, item.Id, InterestTypes.Tenant, InterestOrigintTypes.UserSelection, 0);

                                if (currentInterest == null)
                                {
                                    new NullReferenceException("Preference creation failed");
                                }
                            }
                            else
                            {
                                //If interest exists and it's inactive
                                if (!currentInterest.IsActive)
                                {
                                    //Just need to update the active state
                                    this._businessObjects.UserInterests.Put(currentUser.Id, item.Id);
                                }

                            }
                        }

                        //If there are interests that previously user had but this time were unselected
                        foreach (UserInterest item in userInterests)
                        {
                            if (item.IsActive)
                            {
                                //Just need to update the deactive state
                                this._businessObjects.UserInterests.Put(currentUser.Id, item.InterestId);
                            }
                        }
                        */


                        Task<bool> updateCategoryPreferencesTask = new Task<bool>(() => this.UpdateCategoryPreferences(currentUser.Id, userInterests.Where(x => x.InterestType == InterestTypes.Category).ToList(), model.Categories));
                        updateCategoryPreferencesTask.Start();

                        Task<bool> updateCommercePreferencesTask = new Task<bool>(() => this.UpdateCommercePreferences(currentUser.Id, userInterests.Where(x => x.InterestType == InterestTypes.Tenant).ToList(), model.Commerces));
                        updateCommercePreferencesTask.Start();


                        //bool updateCategoryPreferencesResult = await updateCategoryPreferencesTask;

                        if(await updateCategoryPreferencesTask && await updateCommercePreferencesTask)
                        {

                            result = Ok(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = "",
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                        }
                        else
                        {
                            result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                        }

                    }
                    else
                    {
                        errorMsg = "Error: Invalid User";
                        result = new NotFoundObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["UserNotFound"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

                        //Registers the invalid call
                        this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                            Values.StatusCodes.NotFound, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                    }

                }
                else
                {
                    errorMsg = "Error: Data received is invalid or have wrong format";
                    result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.NotFound,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }
            }
            catch (Exception e)
            {
                errorMsg = "Error: An error ocurred while completing the operation " + e.InnerException;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
            }

            return result;
        }

        private bool UpdateCategoryPreferences(string userId, List<UserInterest> userInterests, List<ChosenPreference> categoryPreferences)
        {
            bool success;
            int callId = 3;

            try
            {
                UserInterest currentInterest = null;
                bool exists = true;


                Category matchCategory;

                //Creates categories interests
                foreach (ChosenPreference item in categoryPreferences)
                {
                    exists = false;

                    //Check if that is an already existent interest
                    for (int i = 0; i < userInterests.Count && !exists; ++i)
                    {
                        currentInterest = userInterests.ElementAt(i);

                        //Check if current existent interest match with the current one sent from backend
                        if (currentInterest.InterestType == InterestTypes.Category && currentInterest.InterestId == item.Id)
                        {
                            //Remove the existent interest from the interest list
                            userInterests.RemoveAt(i);
                            //Mark that the interest alredy exists
                            exists = true;
                        }
                    }

                    //If it's a new interest
                    if (!exists)
                    {
                        matchCategory = this._businessObjects.Categories.Get(item.Id);

                        //Creates a new interest
                        currentInterest = this._businessObjects.UserInterests.Post(userId, item.Id, InterestTypes.Category, InterestOrigintTypes.UserSelection, (int)matchCategory.Herarchy);

                        if (currentInterest == null)
                        {
                            new NullReferenceException("Preference creation failed");
                        }
                    }
                    else
                    {
                        //If interest exists and it's inactive
                        if (!currentInterest.IsActive)
                        {
                            //Just need to update the active state
                            this._businessObjects.UserInterests.Put(userId, item.Id);
                        }

                    }

                }

                //If there are interests that previously user had but this time were unselected
                foreach (UserInterest item in userInterests)
                {
                    if (item.IsActive)
                    {
                        //Just need to update the deactive state
                        this._businessObjects.UserInterests.Put(userId, item.InterestId);
                    }
                }

                success = true;
            }
            catch(Exception e)
            {
                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, "Cantidad preferences: " + categoryPreferences?.Count, 0, 0, false, null, HttpcallTypes.Post, e.InnerException != null ? e.InnerException.Message : e.Message);

                success = false;
            }

            return success;
        }

        private bool UpdateCommercePreferences(string userId, List<UserInterest> userInterests, List<ChosenPreference> commercePreferences)
        {
            bool success;
            int callId = 4;

            try
            {
                bool exists;
                UserInterest currentInterest = null;

                //Creates commerces interest
                foreach (ChosenPreference item in commercePreferences)
                {
                    exists = false;

                    //Check if that is an already existent interest
                    for (int i = 0; i < userInterests.Count && !exists; ++i)
                    {
                        currentInterest = userInterests.ElementAt(i);

                        //Check if current existent interest match with the current one sent from backend
                        if (currentInterest.InterestType == InterestTypes.Tenant && currentInterest.InterestId == item.Id)
                        {
                            //Remove the existent interest from the interest list
                            userInterests.RemoveAt(i);
                            //Mark that the interest alredy exists
                            exists = true;
                        }
                    }

                    //If it's a new interest
                    if (!exists)
                    {
                        //Creates a new interest
                        currentInterest = this._businessObjects.UserInterests.Post(userId, item.Id, InterestTypes.Tenant, InterestOrigintTypes.UserSelection, 0);

                        if (currentInterest == null)
                        {
                            new NullReferenceException("Preference creation failed");
                        }
                    }
                    else
                    {
                        //If interest exists and it's inactive
                        if (!currentInterest.IsActive)
                        {
                            //Just need to update the active state
                            this._businessObjects.UserInterests.Put(userId, item.Id);
                        }

                    }
                }

                //If there are interests that previously user had but this time were unselected
                foreach (UserInterest item in userInterests)
                {
                    if (item.IsActive)
                    {
                        //Just need to update the deactive state
                        this._businessObjects.UserInterests.Put(userId, item.InterestId);
                    }
                }

                success = true;
            }
            catch (Exception e)
            {
                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, "Cantidad preferences: "+ commercePreferences?.Count, 0, 0, false, null, HttpcallTypes.Post, e.InnerException != null ? e.InnerException.Message : e.Message);

                success = false;
            }

            return success;
        }

        #endregion


        #region CONSTRUCTORS

        public UserPreferenceController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }

        #endregion
    }
}
