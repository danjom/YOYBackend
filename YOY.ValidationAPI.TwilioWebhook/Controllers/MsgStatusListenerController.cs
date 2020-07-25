using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YOY.DAO.Entities;
using YOY.Values;

namespace YOY.ValidationAPI.TwilioWebhook.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class MsgStatusListenerController : ControllerBase
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
        private static int controllerVersion = 1;

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

        [HttpPost]
        [Route("post")]
        public IActionResult Post()
        {
            int callId = 1;
            string parameters = "";

            try
            {
                Initialize(new Guid(MembershipConfigValues.BaseCommerceId));

                // Retrieve the message id and status
                string smsSid = Request.Form["SmsSid"];
                string messageStatus = Request.Form["MessageStatus"];

                parameters = "SId: " + smsSid + ", MessageStatus: " + messageStatus;

                if(smsSid != null && messageStatus != null)
                {
                    this._businessObjects.TextMsgLogs.Put(smsSid, messageStatus);
                }
            }
            catch(Exception e)
            {
                //Registers the invalid call
                this._businessObjects.HttpcallInvokationLogs.Post("", this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Post, e.InnerException.ToString());
            }



            return Content("Handled");
        }

        #endregion
    }
}
