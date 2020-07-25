using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.DAO.Entities;
using YOY.UserAPI.Models.v1.VisitorInspector.POCO;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [ApiController]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class VisitorCheckerController : ControllerBase
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

        private readonly string[] AllowedCountryCodes = {"CR", "ESA", "CO"};

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
                _businessObjects = BusinessObjects.GetInstance(_tenant);
            }

            if (_businessObjects == null)
            {
                _businessObjects = BusinessObjects.GetInstance(_tenant);
            }
        }

        private async Task<IpAddressInspectionViewModel> GetCountryFromIP(string ipAddress)
        {
            IpAddressInspectionViewModel ipData = null;
            int callId = 2;

            var path = "http://api.ipstack.com/" + ipAddress + "?access_key=3cd7aafbd63006f869f3ed134265f6c0";
            var client = new HttpClient();
            var uri = new Uri(path);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ipData = Newtonsoft.Json.JsonConvert.DeserializeObject<IpAddressInspectionViewModel>(content);

            }
            else
            {
                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.NotFound, 0, ipAddress, 0, 0, false, null, HttpcallTypes.Post, response.Content.ToString());
            }

            return ipData;
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Post([FromBody]VisitorDeviceInfo model)
        {
            IActionResult result;
            int callId = 1;
            string parameters = model.ToString();

            if (ModelState.IsValid)
            {
                Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                IpAddressInspectionViewModel ipData = await GetCountryFromIP(HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString());//("190.171.102.40");// 

                if (ipData != null)
                {
                    bool allowedIp = false;

                    if (AllowedCountryCodes.Contains(ipData.CountryCode))
                    {
                        result = Ok(
                            new BasicResponse
                            {
                                StatusCode = Values.StatusCodes.Ok,
                                CustomAction = UserappErrorCustomActions.None,
                                DisplayMsgToUser = false,
                                DevError = "",
                                MsgContent = "",
                                MsgTitle = ""
                            });

                        allowedIp = true;
                    }
                    else
                    {
                        result = new UnauthorizedObjectResult(
                            new BasicResponse
                            {
                                StatusCode = Values.StatusCodes.Forbidden,
                                CustomAction = UserappErrorCustomActions.None,
                                DisplayMsgToUser = false,
                                DevError = _localizer["UserLocationIsRestricted"].Value,
                                MsgContent = "",
                                MsgTitle = ""
                            });
                    }

                    //Stores the correspondig log
                    this._businessObjects.VisitorLogs.Post(ipData.IpAddress, ipData.HostName, ipData.Type, ipData.ContinentName, ipData.CountryCode, ipData.CountryName,
                        ipData.RegionCode, ipData.RegionName, ipData.City, ipData.ZipCode, ipData.Latitude, ipData.Longitude, model.Latitude, model.Longitude, model.DeviceType,
                        model.DeviceModel, model.OsVersion, allowedIp);
                }
                else
                {
                    result = Ok(
                            new BasicResponse
                            {
                                StatusCode = Values.StatusCodes.Ok,
                                CustomAction = UserappErrorCustomActions.None,
                                DisplayMsgToUser = false,
                                DevError = "",
                                MsgContent = "",
                                MsgTitle = ""
                            });

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.NotFound, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, "Ocurrió un error y no se pudo recuperar los datos del ip de visitante");
                }
            }
            else
            {
                result = new BadRequestObjectResult(
                            new BasicResponse
                            {
                                StatusCode = Values.StatusCodes.BadRequest,
                                CustomAction = UserappErrorCustomActions.None,
                                DisplayMsgToUser = false,
                                DevError = _localizer["InvalidPayload"].Value,
                                MsgTitle = "",
                                MsgContent = ""
                            });

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.NotFound, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, "Parámetros recibidos son inválidos");
            }


            return result;
        }

        #endregion

        #region CONSTRUCTORS

        public VisitorCheckerController(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }

        #endregion
    }
}
