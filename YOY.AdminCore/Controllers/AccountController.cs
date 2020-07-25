using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YOY.AdminCore.Models.Identity;
using YOY.DAO.Entities;
using YOY.Values;

namespace YOY.AdminCore.Controllers
{
    public class AccountController : Controller
    {

        #region PRIVATE_PROPERTIES

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger _logger;

        private static Tenant _tenant = null;
        private BusinessObjects _businessObjects = null;

        #endregion

        #region METHODS

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

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


        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.Clear();

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //Since for Control portal it's for general control, the tenantId will always be Guid.Empty
                    HttpContext.Session.SetString("TenantId", Guid.Empty.ToString());

                    this.Initialize();

                    DTO.Entities.UserData user = this._businessObjects.Users.Get(model.Email, UserKeys.Username);

                    if (user != null)
                    {
                        HttpContext.Session.SetInt32("UtcTimeDiff", user.UtcTimeDiff);
                    }

                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToAction(returnUrl);
                }


                /*
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                */

                if (result.IsLockedOut)
                {
                    _logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion

        #region ROLES

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Master", "SuperAdmin", "Admin", "Operator" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var _user = await UserManager.FindByEmailAsync("daniel@test.com");

            //here we tie the new user to the role
            await UserManager.AddToRoleAsync(_user, "Master");
        }

        #endregion
    }
}
