using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.DTO.Entities.Misc.OfferPreference;
using YOY.UserAPI.Models.v1.DealPreference.POCO;
using YOY.UserAPI.Models.v1.DealPreference.SET;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class DealPreferenceController : ControllerBase
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

        [Route("gets")]
        [HttpGet]
        public IActionResult Gets(Guid dealId, Guid commerceId, string userId)
        {
            IActionResult result;

            PreferencesForDeal preferences;

            string errorMsg;
            int callId = 1;
            string parameters = "TenantId: " + commerceId + " - UserId: " + userId + " - OfferId: " + dealId;

            try
            {
                if (_businessObjects == null)
                {
                    Initialize(commerceId);
                }

                preferences = new PreferencesForDeal
                {
                    Preferences = new List<DealPreferenceData>(),
                    DealId = dealId,
                    Count = 0
                };

                List<OfferPreferenceWithOptions> preferenceWithOptions = this._businessObjects.OfferPreferences.Gets(dealId, null);
                DealPreferenceData currentDealPreference;

                if (preferenceWithOptions?.Count > 0)
                {
                    foreach (OfferPreferenceWithOptions item in preferenceWithOptions)
                    {
                        currentDealPreference = new DealPreferenceData
                        {
                            Id = item.Id,
                            DealId = item.OfferId,
                            Name = item.Name,
                            Hint = item.Hint,
                            InputType = item.InputType,
                            IsMandatory = item.Mandatory,
                            MinRequiredOptions = item.MinOptionsSelected,
                            MaxRequiredOptions = item.MaxOptionsSelected,
                            Options = new List<DealPreferenceOption>()
                        };

                        if (item.Options?.Count > 0)
                        {
                            foreach (PreferenceOptionJoinView optionItem in item.Options)
                            {
                                if (optionItem.Id != null)
                                {
                                    currentDealPreference.Options.Add(new DealPreferenceOption
                                    {
                                        Id = (Guid)optionItem.Id,
                                        PreferenceId = optionItem.PreferenceId,
                                        DealId = optionItem.OfferId,
                                        Value = optionItem.Value,
                                        Price = optionItem.Price,
                                        RegularPrice = optionItem.RegularPrice ?? -1
                                    });
                                }

                            }
                        }

                        preferences.Preferences.Add(currentDealPreference);

                    }
                }

                result = Ok(preferences);
            }
            catch (Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;

        }


        #endregion

        #region CONSTRUCTORS
        public DealPreferenceController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }
        #endregion
    }
}
