using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using YOY.BusinessAPI.Models.v1.DealPreference.POCO;
using YOY.BusinessAPI.Models.v1.DealPreference.SET;
using YOY.BusinessAPI.Models.v1.Misc.BasicResponse.POCO;
using YOY.DAO.Entities;
using YOY.DAO.Entities.Manager.Misc.Image;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.OfferPreference;
using YOY.Values;

namespace YOY.BusinessAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class DealPreferenceController : ControllerBase
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
        private ImageHandler _imgHandler;

        private const int controllerVersion = 1;

        private readonly string[] types = { "Radio-Button", "Check-Box", "Dropdown", "Color-Picker", "Tags-Picker" };

        #endregion

        #region METHODS

        private void Initialize(Guid commerceId, string userId)
        {
            //1st initialize in order to get tenant data
            _tenant = Tenant.GetInstance(Guid.Empty);
            _businessObjects = BusinessObjects.GetInstance(_tenant);

            if (_imgHandler == null)
                _imgHandler = new ImageHandler();

            if (commerceId != Guid.Empty)
            {

                int utcTimeDiff = int.MinValue;

                TenantInfo currentTenant = this._businessObjects.Commerces.Get(commerceId, CommerceKeys.TenantKey);

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    UserData user = this._businessObjects.Users.Get(userId, UserKeys.UserId);
                    utcTimeDiff = user.UtcTimeDiff;
                }

                _tenant = Tenant.GetInstance(currentTenant.TenantId, currentTenant.CategoryId, currentTenant.CurrencySymbol, utcTimeDiff, currentTenant.DefaultCommissionFeePercentage);
            }
            else
            {
                _tenant = Tenant.GetInstance(commerceId, Guid.Empty, "", int.MinValue, 0);
            }


            _businessObjects = BusinessObjects.GetInstance(_tenant);
        }

        [Route("getTypes")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult Gets()
        {

            DealPreferenceTypes preferenceTypes = new DealPreferenceTypes
            {
                Count = 0,
                Types = new List<DealPreferenceType>()
            };
            DealPreferenceType type;

            for(int i = 0; i< types.Length; ++i)
            {
                type = new DealPreferenceType
                {
                    Type = i + 1,
                    TypeName = types[i]
                };

                preferenceTypes.Types.Add(type);
            }

            preferenceTypes.Count = preferenceTypes.Types.Count;

            return Ok(preferenceTypes);

        }


        [Route("gets")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult Gets(Guid dealId, Guid employeeId, Guid tenantId, string userId)
        {
            IActionResult result;

            PreferencesForDeal preferences = new PreferencesForDeal
            {
                PreferencesData = new List<DealPreferenceData>(),
                DealId = dealId,
                Count = 0
            };

            string errorMsg;
            int callId = 1;
            string parameters = "EmployeeId: " + employeeId + " - UserId: " + userId + " - OfferId: " + dealId;

            try
            {
                if (_businessObjects == null)
                {
                    Initialize(tenantId, userId);
                }

                List<OfferPreferenceWithOptions> preferenceWithOptions = this._businessObjects.OfferPreferences.Gets(dealId, null);
                DealPreferenceData currentDealPreference;

                if (preferenceWithOptions?.Count > 0)
                {
                    foreach(OfferPreferenceWithOptions item in preferenceWithOptions)
                    {
                        currentDealPreference = new DealPreferenceData
                        {
                            Id = item.Id,
                            DealId = item.OfferId,
                            TenantId = tenantId,
                            Name = item.Name,
                            Hint = item.Hint,
                            InputType = item.InputType,
                            InputTypeName = item.InputTypeName,
                            IsMandatory = item.Mandatory,
                            MinRequiredSelectedOptions = item.MinOptionsSelected,
                            MaxRequiredOptions = item.MaxOptionsSelected,
                            Options = new List<DealPreferenceOption>()
                        };

                        if (item.Options?.Count > 0)
                        {
                            foreach (PreferenceOptionJoinView optionItem in item.Options)
                            {
                                if(optionItem.Id != null)
                                {
                                    currentDealPreference.Options.Add(new DealPreferenceOption
                                    {
                                        Id = (Guid)optionItem.Id,
                                        PreferenceId = optionItem.PreferenceId,
                                        DealId = optionItem.OfferId,
                                        Value = optionItem.Value,
                                        Price = optionItem.Price,
                                        RegularPrice = optionItem.RegularPrice ?? -1,
                                        DisplayImgUrl = ""//CHANGE THIS!!!!
                                    });
                                }
                                
                            }
                        }

                        preferences.PreferencesData.Add(currentDealPreference);

                    }
                }

                result = Ok(preferences);
            }
            catch(Exception e)
            {
                errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
            }

            return result;

        }
        
        /// <summary>
        /// This call creates a new preference, and in case options are contained in the object, proceeds to validate them and create
        /// them as well
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("post")]
        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] NewDealPreference model)
        {
            int callId = 2;
            string parameters = model.ToString();
            string errorMsg;

            Initialize(model.TenantId, "");
            IActionResult result;

            if (!ModelState.IsValid || model.DealId == Guid.Empty || string.IsNullOrWhiteSpace(model.UserId) || model.InputType < PreferenceInputTypes.RadioButton || model.InputType > PreferenceInputTypes.TagsPicker)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    ErrorCode = Values.StatusCodes.BadRequest,
                                    ShowErrorToUser = false,
                                    InnerError = "Invalid Payload",
                                    PublicError = ""
                                });

            }
            else
            {

                try
                {
                    //1st creates the preference
                    OfferPreference newPreference = this._businessObjects.OfferPreferences.Post(model.DealId, model.Name, model.Hint, model.InputType, model.MinOptionsSelected, model.MaxOptionsSelected, model.IsMandatory);

                    if (newPreference != null)
                    {
                        

                        DealPreferenceData preferenceData = new DealPreferenceData
                        {
                            Id = newPreference.Id,
                            DealId = newPreference.OfferId,
                            TenantId = model.TenantId,
                            Name = newPreference.Name,
                            Hint = newPreference.Hint,
                            InputType = newPreference.InputType,
                            InputTypeName = newPreference.InputTypeName,
                            IsMandatory = newPreference.Mandatory,
                            MinRequiredSelectedOptions = newPreference.MinOptionsSelected,
                            MaxRequiredOptions = newPreference.MaxOptionsSelected,
                            Options = new List<DealPreferenceOption>()
                        };

                        Offer offer = this._businessObjects.Offers.Get(model.DealId, false);

                        if(offer != null && !offer.HasPreferences)//in case doesn't have preferences yet
                        {
                            this._businessObjects.Offers.Put(offer.Id, offer.OfferType, ChangeTypes.HasPreferences);
                        }


                        result = Ok(preferenceData);
                    }
                    else
                    {
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    }
                }
                catch(Exception e)
                {
                    errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }
                
            }

            return result;
        }


        /// <summary>
        /// This call updates the preference and if contained, also the options
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("postOption")]
        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult PostOption([FromBody] NewDealPrefOption model)
        {
            int callId = 3;
            string parameters = model.ToString();
            string errorMsg;

            Initialize(model.TenantId, "");
            IActionResult result;

            if (!ModelState.IsValid && model.PreferenceId != Guid.Empty && !string.IsNullOrWhiteSpace(model.UserId) && model.TenantId != Guid.Empty && model.PreferenceId != Guid.Empty)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    ErrorCode = Values.StatusCodes.BadRequest,
                                    ShowErrorToUser = false,
                                    InnerError = "Invalid Payload",
                                    PublicError = ""
                                });

            }
            else
            {
                try
                {
                    Initialize(model.TenantId, model.UserId);

                    OfferPreference preference = this._businessObjects.OfferPreferences.Get(model.PreferenceId);

                    if (preference != null && model.Value.Length > 2 && model.Value.Length < 100 && model.Price >= 0 && model.RegularPrice >= model.Price)
                    {

                        OfferPreferenceOption preferenceOption = this._businessObjects.OfferPreferences.Post(preference.Id, preference.OfferId, null, model.Value, model.Price, model.RegularPrice, false);

                        if (preferenceOption != null)
                        {
                            List<OfferPreferenceWithOptions> offerPreferenceWithOptions = this._businessObjects.OfferPreferences.Gets(preference.OfferId, preference.Id);

                            if (offerPreferenceWithOptions?.Count == 1)
                            {
                                DealPreferenceData preferenceData = new DealPreferenceData
                                {
                                    Id = offerPreferenceWithOptions[0].Id,
                                    DealId = offerPreferenceWithOptions[0].OfferId,
                                    TenantId = model.TenantId,
                                    Name = offerPreferenceWithOptions[0].Name,
                                    Hint = offerPreferenceWithOptions[0].Hint,
                                    InputType = offerPreferenceWithOptions[0].InputType,
                                    InputTypeName = offerPreferenceWithOptions[0].InputTypeName,
                                    IsMandatory = offerPreferenceWithOptions[0].Mandatory,
                                    MinRequiredSelectedOptions = offerPreferenceWithOptions[0].MinOptionsSelected,
                                    MaxRequiredOptions = offerPreferenceWithOptions[0].MaxOptionsSelected,
                                    Options = new List<DealPreferenceOption>()
                                };

                                if (offerPreferenceWithOptions[0].Options?.Count > 0)
                                {


                                    foreach (PreferenceOptionJoinView optionItem in offerPreferenceWithOptions[0].Options)
                                    {
                                        if (optionItem.Id != null)
                                        {
                                            preferenceData.Options.Add(new DealPreferenceOption
                                            {
                                                Id = (Guid)optionItem.Id,
                                                PreferenceId = optionItem.PreferenceId,
                                                DealId = optionItem.OfferId,
                                                Value = optionItem.Value,
                                                Price = optionItem.Price,
                                                RegularPrice = optionItem.RegularPrice ?? -1,
                                                DisplayImgUrl = ""//CHANGE THIS!!!!
                                            });
                                        }
                                    }
                                }

                                result = Ok(preferenceData);
                            }
                            else
                            {
                                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                            }

                        }
                        else
                        {
                            result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                        }

                    }
                    else
                    {
                        result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    ErrorCode = Values.StatusCodes.BadRequest,
                                    ShowErrorToUser = false,
                                    InnerError = "Invalid Payload",
                                    PublicError = ""
                                });
                    }
                }
                catch (Exception e)
                {
                    errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                }
            }

            return result;
        }

        /// <summary>
        /// This call updates the preference and if contained, also the options
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("put")]
        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult Put([FromBody] UpdatedDealPreference model)
        {
            int callId = 3;
            string parameters = model.ToString();
            string errorMsg;

            Initialize(model.TenantId, "");
            IActionResult result;

            if (!ModelState.IsValid && model.DealId != Guid.Empty && !string.IsNullOrWhiteSpace(model.UserId) && model.TenantId != Guid.Empty && model.Id != Guid.Empty)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    ErrorCode = Values.StatusCodes.BadRequest,
                                    ShowErrorToUser = false,
                                    InnerError = "Invalid Payload",
                                    PublicError = ""
                                });

            }
            else
            {
                try
                {
                    Initialize(model.TenantId, model.UserId);

                    OfferPreference updatedPreference = this._businessObjects.OfferPreferences.Put(model.Id, model.Name, model.Hint, model.InputType, model.MinRequiredSelectedOptions, model.MaxRequiredRequiredOptions, model.IsMandatory);

                    if (updatedPreference != null)
                    {
                        

                        List<OfferPreferenceWithOptions> offerPreferenceWithOptions = this._businessObjects.OfferPreferences.Gets(updatedPreference.OfferId, updatedPreference.Id);

                        if (offerPreferenceWithOptions?.Count == 1)
                        {
                            DealPreferenceData preferenceData = new DealPreferenceData
                            {
                                Id = offerPreferenceWithOptions[0].Id,
                                DealId = offerPreferenceWithOptions[0].OfferId,
                                TenantId = model.TenantId,
                                Name = offerPreferenceWithOptions[0].Name,
                                Hint = offerPreferenceWithOptions[0].Hint,
                                InputType = offerPreferenceWithOptions[0].InputType,
                                InputTypeName = offerPreferenceWithOptions[0].InputTypeName,
                                IsMandatory = offerPreferenceWithOptions[0].Mandatory,
                                MinRequiredSelectedOptions = offerPreferenceWithOptions[0].MinOptionsSelected,
                                MaxRequiredOptions = offerPreferenceWithOptions[0].MaxOptionsSelected,
                                Options = new List<DealPreferenceOption>()
                            };

                            if (offerPreferenceWithOptions[0].Options?.Count > 0)
                            {


                                foreach (PreferenceOptionJoinView optionItem in offerPreferenceWithOptions[0].Options)
                                {
                                    if (optionItem.Id != null)
                                    {
                                        preferenceData.Options.Add(new DealPreferenceOption
                                        {
                                            Id = (Guid)optionItem.Id,
                                            PreferenceId = optionItem.PreferenceId,
                                            DealId = optionItem.OfferId,
                                            Value = optionItem.Value,
                                            Price = optionItem.Price,
                                            RegularPrice = optionItem.RegularPrice ?? -1,
                                            DisplayImgUrl = ""//CHANGE THIS!!!!
                                        });
                                    }
                                }
                            }

                            result = Ok(preferenceData);
                        }
                        else
                        {
                            result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                        }

                        /*DealPreferenceData preferenceData = new DealPreferenceData
                        {
                            Id = updatedPreference.Id,
                            DealId = updatedPreference.OfferId,
                            TenantId = model.TenantId,
                            InputType = updatedPreference.InputType,
                            InputTypeName = updatedPreference.InputTypeName,
                            Hint = updatedPreference.Hint,
                            Name = updatedPreference.Name,
                            IsMandatory = updatedPreference.Mandatory,
                            MinRequiredSelectedOptions = updatedPreference.MinOptionsSelected,
                            MaxRequiredRequiredOptions = updatedPreference.MaxOptionsSelected,
                            Options = dealPreferenceOptions ?? new List<DealPreferenceOption>(),
                            AddedOptions = addedOptions,
                            UpdatedOptions = updatedOptions,
                            DeletedOptions = deletedOptions,
                            InvalidOptions = invalidOptions
                        };*/

                        
                    }
                    else
                    {
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    }
                }
                catch(Exception e)
                {
                    errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Put, errorMsg);
                }
            }

            return result;
        }

        [Route("delete")]
        [HttpDelete]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromBody] DealPreferenceToDelete model)
        {
            int callId = 4;
            string parameters = model.ToString();
            string errorMsg;

            IActionResult result;

            Initialize(model.TenantId, model.UserId);

            if (!ModelState.IsValid)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Delete, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    ErrorCode = Values.StatusCodes.BadRequest,
                                    ShowErrorToUser = false,
                                    InnerError = "Invalid Payload",
                                    PublicError = ""
                                });

            }
            else
            {
                try
                {
                    bool success = this._businessObjects.OfferPreferences.Delete(model.Id);

                    if (success)
                    {
                        int prefCount = this._businessObjects.OfferPreferences.Gets(model.DealId, ActiveStates.Active).Count;

                        if(prefCount == 0)
                        {
                            Offer offer = this._businessObjects.Offers.Get(model.DealId, false);

                            if (offer != null && offer.HasPreferences)
                                this._businessObjects.Offers.Put(offer.Id, offer.OfferType, ChangeTypes.HasPreferences);
                        }

                        result = Ok(new SuccessResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            ShowMsgToUser = true,
                            MessageToDisplay = "Preference eliminada éxitosamente"
                        });
                    }
                    else
                    {
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    }
                }
                catch(Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Delete, e.InnerException != null ? e.InnerException.Message : e.Message);
                }
            }

            return result;
        }

        [Route("deleteOption")]
        [HttpDelete]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public IActionResult DeleteOption([FromBody] DealPrefOptionToDelete model)
        {
            int callId = 4;
            string parameters = model.ToString();
            string errorMsg;

            IActionResult result;

            Initialize(model.TenantId, model.UserId);

            if (!ModelState.IsValid)
            {
                errorMsg = "ERROR: Invalid data received, " + parameters;

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Delete, errorMsg);

                result = new BadRequestObjectResult(
                                new ErrorResponse
                                {
                                    ErrorCode = Values.StatusCodes.BadRequest,
                                    ShowErrorToUser = false,
                                    InnerError = "Invalid Payload",
                                    PublicError = ""
                                });

            }
            else
            {
                try
                {
                    bool success = this._businessObjects.OfferPreferences.Delete(model.Id, model.PreferenceId);

                    if (success)
                    {
                        result = Ok(new SuccessResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            ShowMsgToUser = true,
                            MessageToDisplay = "Preference eliminada éxitosamente"
                        });
                    }
                    else
                    {
                        result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
                    }
                }
                catch (Exception e)
                {
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(model.EmployeeId.ToString(), this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Delete, e.InnerException != null ? e.InnerException.Message : e.Message);
                }
            }

            return result;
        }

        #endregion
    }
}
