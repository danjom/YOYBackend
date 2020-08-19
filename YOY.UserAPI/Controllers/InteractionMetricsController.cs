using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YOY.DAO.Entities;
using YOY.UserAPI.Models.v1.InteractionMetric;
using YOY.UserAPI.Models.v1.Miscellaneous.BasicResponse.POCO;
using YOY.Values;

namespace YOY.UserAPI.Controllers
{
    [RequireHttps]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class InteractionMetricsController : ControllerBase
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

        [HttpPost]
        [Route("post")]
        public IActionResult Post([FromBody] NewInteractionMetricRecord model)
        {
            IActionResult result;
            int callId = 1;
            string parameters = model.ToString();
            string errorMsg;

            try
            {
                Initialize(Guid.Empty);

                if (ModelState.IsValid)
                {

                    bool success = this._businessObjects.UserInteractionMetrics.Post(model.UserId, model.ReferenceType, model.ReferenceId, model.Location, model.DwellTimeInSeconds);


                    if (success)
                    {

                        result = Ok(new BasicResponse
                        {
                            StatusCode = Values.StatusCodes.Ok,
                            CustomAction = UserappErrorCustomActions.None,
                            DisplayMsgToUser = false,
                            DevError = "",
                            MsgContent = "",
                            MsgTitle = ""
                        });
                    }
                    else
                    {
                        errorMsg = "Interaction metric add failed";
                        result = new BadRequestObjectResult(
                                    new BasicResponse
                                    {
                                        StatusCode = Values.StatusCodes.BadRequest,
                                        CustomAction = UserappErrorCustomActions.None,
                                        DisplayMsgToUser = false,
                                        DevError = "Metric not logged",
                                        MsgContent = "",
                                        MsgTitle = ""
                                    });

                        //Registers the invalid call
                        this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                            Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                    }

                }
                else
                {
                    errorMsg = "Invalid payload";
                    result = new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = "Metric not logged",
                                    MsgContent = "",
                                    MsgTitle = ""
                                });

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.BadRequest, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
                }

            }
            catch (Exception e)
            {
                errorMsg = "Error: An exception has occured, " + e.InnerException != null ? e.InnerException.Message : e.Message;
                result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post(model.UserId, this.GetType().Name, callId, controllerVersion,
                                    Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, errorMsg);
            }

            return result;
        }

        #endregion
    }
}
