using YOY.AdminCore.MVCFilters.CustomFilters;
using YOY.DAO.Entities;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace YOY.AdminCore.Controllers
{
    [Authorize]
    [AuthorizeMaster("Master","SuperAdmin")]
    [RequireHttps]
    public class HomeController : Controller
    {

        #region PROPERTIES_AND_RESOURCES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // PARENT BUSINESS OBJECTS ---------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// Parent business objects 
        /// </summary>
        private static Tenant _tenant = null;
        private BusinessObjects _businessObjects = null;

        #endregion

        private bool Initialize()
        {
            bool success = false;

            if (HttpContext.Session.GetString("TenantId") != null)
            {
                Guid sessionTenantId = new Guid(HttpContext.Session.GetString("TenantId"));

                if (_tenant == null || _tenant.TenantId != sessionTenantId)
                {

                    _tenant = Tenant.GetInstance(sessionTenantId);

                    _businessObjects = BusinessObjects.GetInstance(_tenant);
                }
                else
                {
                    if (_businessObjects == null)
                    {
                        _businessObjects = BusinessObjects.GetInstance(_tenant);
                    }
                }

                success = true;
            }

            return success;

        }

        [HttpGet]
        public ActionResult Index()
        {
            ActionResult result = RedirectToAction("Login", "Account");

            if (Initialize())
            {
                result = View();
            }
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}