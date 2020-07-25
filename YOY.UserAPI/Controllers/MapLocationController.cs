using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.Location;
using YOY.UserAPI.Models.v1.MapLocation.POCO;
using YOY.UserAPI.Models.v1.MapLocation.SET;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class MapLocationController : ControllerBase
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

        #endregion

        #region METHODS 

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

        /// <summary>
        /// Retrieve user location data and enabled states in order to let user to selected a new location
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Route("gets")]
        [HttpGet]
        public IActionResult Gets(string userId)
        {
            IActionResult result;
            string errorMsg;
            int callId = 1;
            string parameters = "User id: " + userId;

            try
            {
                Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                UserData currentUser = this._businessObjects.Users.Get(userId, UserKeys.UserId);
                State userCurrentState = null;

                AvailableStates userLocationData = new AvailableStates
                {
                    Locations = new List<StatesInCountry>()
                };

                if (currentUser != null)
                {
                    List<StatesByCountry> availableStates = this._businessObjects.States.GetStatesByCountry();


                    userLocationData.Count = availableStates?.Count ?? 0;

                    if (currentUser.StateId != null && currentUser.StateId != Guid.Empty)
                    {
                        userCurrentState = this._businessObjects.States.Get((Guid)currentUser.StateId);

                        userLocationData.CurrentCountryId = userCurrentState.CountryId;
                        userLocationData.CurrentCountryName = userCurrentState.CountryName;
                        userLocationData.CurrentCountryCode = userCurrentState.CountryCode;
                        userLocationData.CurrentStateId = userCurrentState.Id;
                        userLocationData.CurrentStateName = userCurrentState.Name;
                        userLocationData.SelectedLocationExists = true;
                    }
                    else
                    {
                        userLocationData.CurrentCountryId = Guid.Empty;
                        userLocationData.CurrentCountryName = "";
                        userLocationData.CurrentCountryCode = "";
                        userLocationData.CurrentStateId = Guid.Empty;
                        userLocationData.CurrentStateName = "";
                        userLocationData.SelectedLocationExists = false;
                    }

                    //In case for some reason the app crashed without selecting preferences, to correct that issue and fix app functionality
                    userLocationData.ShowPreferences = this._businessObjects.UserInterests.Gets(currentUser.AccountNumber, ActiveStates.Active) > 0;

                    StatesInCountry currentCountry = null;

                    foreach (StatesByCountry item in availableStates)
                    {
                        currentCountry = new StatesInCountry
                        {
                            Country = item.Country,
                            CountryFlagUrl = item.ContryFlag,
                            States = new List<StateData>()
                        };

                        foreach (StateBaseData stateItem in item.States)
                        {
                            currentCountry.States.Add(new StateData
                            {
                                Id = stateItem.Id,
                                Name = stateItem.Name,
                                Latitude = Math.Round(stateItem.Latitude,5),
                                Longitude = Math.Round(stateItem.Longitude, 5)

                            });
                        }

                        userLocationData.Locations.Add(currentCountry);

                    }

                    result = Ok(userLocationData);
                }
                else
                {
                    errorMsg = "Error: User not found";

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
                    this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
                }
            }
            catch (Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;

        }//GETS ENDS -------------------------------------------------------------------------------------------------------//


        #endregion

        #region CONSTRUCTORS

        public MapLocationController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }

        #endregion
    }
}
