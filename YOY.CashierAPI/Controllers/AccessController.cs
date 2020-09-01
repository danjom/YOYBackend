﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using YOY.CashierAPI.Models.v1.Access.POCO;
using YOY.CashierAPI.Models.v1.Access.SET;
using YOY.CashierAPI.Models.v1.IdentityModel;
using YOY.CashierAPI.Models.v1.Miscellaneous.POCO;
using YOY.DAO.Entities;
using YOY.DTO.Entities;
using YOY.DTO.Entities.Misc.TenantData;
using YOY.Values;

namespace YOY.CashierAPI.Controllers
{
    [RequireHttps]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class AccessController : ControllerBase
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
        private readonly UserManager<AppUser> userManager;
        private readonly IStringLocalizer<SharedResources> _localizer;

        private readonly int controllerVersion = 1;

        #endregion

        #region METHODS

        private string GetCurrencyTypeName(int currencyType)
        {
            string typeName = currencyType switch
            {
                CurrencyTypes.CostaRicanColon => _localizer["CostaRicaColons"].Value,
                CurrencyTypes.USDollar => _localizer["USDollars"].Value,
                CurrencyTypes.GuatemalanQuetzal => _localizer["GuatemalanQuetzal"].Value,
                CurrencyTypes.HonduranLempira => _localizer["HonduranLempira"].Value,
                CurrencyTypes.NicaraguanCordoba => _localizer["NicaraguanCordoba"].Value,
                CurrencyTypes.MexicanPeso => _localizer["MexicanPeso"].Value,
                CurrencyTypes.ColombianPeso => _localizer["ColombianPeso"].Value,
                _ => "--",
            };
            return typeName;

        }

        private void Initialize(Guid commerceId, long accountNumber)
        {
            //1st initialize in order to get tenant data
            _tenant = Tenant.GetInstance(Guid.Empty);
            _businessObjects = BusinessObjects.GetInstance(_tenant);

            if (commerceId != Guid.Empty)
            {

                int utcTimeDiff = int.MinValue;

                TenantInfo currentTenant = this._businessObjects.Commerces.Get(commerceId, CommerceKeys.TenantKey);

                if (accountNumber != -1)
                {
                    UserData user = this._businessObjects.Users.Get(accountNumber);
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

        private int GetAccessLevel(Guid? branchId, string roleName)
        {
            int accessLevel;

            switch (roleName)
            {
                case RoleNames.Operator:
                    accessLevel = BusinessPortalAccessLevels.ViewOnly;
                    break;
                case RoleNames.Admin:
                    if (branchId == null)
                    {
                        accessLevel = BusinessPortalAccessLevels.ActiveStatusUpdater;
                    }
                    else
                    {
                        accessLevel = BusinessPortalAccessLevels.ViewOnly;
                    }
                    break;
                case RoleNames.SuperAdmin:
                    if (branchId == null)
                    {
                        accessLevel = BusinessPortalAccessLevels.CreatorUpdater;
                    }
                    else
                    {
                        accessLevel = BusinessPortalAccessLevels.ActiveStatusUpdater;
                    }
                    break;
                case RoleNames.Master:
                    accessLevel = BusinessPortalAccessLevels.FullAccess;
                    break;
                default:
                    accessLevel = BusinessPortalAccessLevels.NoAccess;
                    break;
            }

            return accessLevel;
        }

        /// <summary>
        /// Retrieves all the accesses a given user has
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("gets")]
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Gets(string userId)
        {
            IActionResult result;
            AccessData enabledAccesses = new AccessData();

            int callId = 1;
            string errorMsg;
            string parameters = "UserId: " + userId;

            if (!string.IsNullOrEmpty(userId))
            {
                if (_businessObjects == null)
                {
                    Initialize(Guid.Empty, -1);
                }


                try
                {

                    List<string> userRoles = null;
                    AppUser user = null;

                    using (userManager)
                    {

                        user = await userManager.FindByIdAsync(userId);

                        if (user == null)
                        {
                            return new NotFoundObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["UserNotFound"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
                        }
                        else
                        {
                            userRoles = (await userManager.GetRolesAsync(user)).ToList();
                        }
                    }

                    bool isMaster = false;
                    bool validCommerce = false;
                    int accessLevel = -1;
                    string roleName = "";
                    Guid employeeId = Guid.Empty;

                    //Retrieve all the employee assignments user has
                    List<Employee> employeeRoles = this._businessObjects.Employees.Gets(userId, UserKeys.UserId, ActiveStates.Active);

                    //Check if this is a Master user
                    for (int i = 0; i < userRoles.Count && !isMaster; ++i)
                    {
                        if (userRoles[i].CompareTo(RoleNames.Master) == 0)
                            isMaster = true;

                        //Remove all the unpriviledge roles
                        if (userRoles[i].CompareTo(RoleNames.Master) != 0 && userRoles[i].CompareTo(RoleNames.SuperAdmin) != 0 && userRoles[i].CompareTo(RoleNames.Admin) != 0 && userRoles[i].CompareTo(RoleNames.Operator) != 0)
                            userRoles.RemoveAt(i--);
                    }

                    List<TenantData> enabledTenants = new List<TenantData>();

                    if (isMaster)
                    {
                        UserData userData = this._businessObjects.Users.Get(userId, UserKeys.UserId);

                        roleName = RoleNames.Master;
                        employeeId = Guid.Empty;
                        accessLevel = BusinessPortalAccessLevels.FullAccess;

                        if (userData != null)
                        {
                            //Since it's a master, all the tenants are accessible
                            List<MinTenantInfo> allTenants = this._businessObjects.Commerces.Gets((Guid)userData.CountryId, ActiveStates.Active, ReleaseStates.Released, PaginationDefaults.PageSize, PaginationDefaults.PageNumber);

                            if (allTenants?.Count > 0)
                            {
                                foreach (MinTenantInfo item in allTenants)
                                {
                                    enabledTenants.Add(new TenantData
                                    {
                                        TenantId = item.TenantId,
                                        Name = item.Name,
                                        LogoUrl = item.LogoUrl,
                                        CurrencySymbol = item.CurrencySymbol,
                                        CurrencyType = item.CurrencyType,
                                        CurrencyTypeName = GetCurrencyTypeName(item.CurrencyType)
                                    });
                                }
                            }
                        }

                    }
                    else
                    {
                        //If isn't a master, have employee roles assigned and 
                        if (employeeRoles?.Count > 0 && userRoles?.Count > 0)
                        {
                            //Now it's time to discard the employee roles that doesn't meet the criteria
                            for (int i = 0; i < employeeRoles.Count; ++i)
                            {
                                if (employeeRoles[i].RoleName.CompareTo(RoleNames.Master) != 0 && employeeRoles[i].RoleName.CompareTo(RoleNames.SuperAdmin) != 0 && employeeRoles[i].RoleName.CompareTo(RoleNames.Admin) != 0)
                                {
                                    employeeRoles.RemoveAt(i--);
                                }
                                else
                                {
                                    if (string.IsNullOrWhiteSpace(roleName) || roleName.CompareTo(RoleNames.Operator) == 0)
                                    {
                                        roleName = employeeRoles[i].RoleName;
                                        employeeId = employeeRoles[i].Id;
                                        accessLevel = GetAccessLevel(employeeRoles[i].BranchId, roleName);
                                    }
                                    else
                                    {
                                        if (roleName.CompareTo(RoleNames.SuperAdmin) != 0 && employeeRoles[i].RoleName.CompareTo(RoleNames.SuperAdmin) == 0)
                                        {
                                            roleName = employeeRoles[i].RoleName;
                                            employeeId = employeeRoles[i].Id;

                                            if (accessLevel < BusinessPortalAccessLevels.CreatorUpdater)
                                                accessLevel = GetAccessLevel(employeeRoles[i].BranchId, roleName);
                                            else
                                            {
                                                if (employeeRoles[i].BranchId == null)
                                                    accessLevel = GetAccessLevel(employeeRoles[i].BranchId, roleName);
                                            }
                                        }
                                    }

                                    enabledTenants.Add(new TenantData
                                    {
                                        TenantId = employeeRoles[i].TenantId,
                                        Name = employeeRoles[i].Name,
                                        CurrencySymbol = employeeRoles[i].CurrencySymbol,
                                        CurrencyType = employeeRoles[i].CurrencyType,
                                        CurrencyTypeName = GetCurrencyTypeName(employeeRoles[i].CurrencyType)
                                    });
                                }
                            }
                        }
                        else
                        {
                            accessLevel = BusinessPortalAccessLevels.NoAccess;
                        }
                    }

                    //At this points we have all the enabled tenants 
                    enabledAccesses.Username = user.Email;
                    enabledAccesses.HasAccess = false;
                    enabledAccesses.Commerces = new List<CommerceData>();

                    TenantData currentTenantData = null;
                    List<Branch> branches = null;
                    CommerceData currentCommerceData = null;
                    Models.v1.Access.POCO.BranchData currentBranchData = null;
                    List<Models.v1.Access.POCO.BranchData> branchesData = null;

                    for (int t = 0; t < enabledTenants.Count; ++t)
                    {
                        validCommerce = false;
                        currentTenantData = enabledTenants[t];

                        //Creates a commerce data
                        currentCommerceData = new CommerceData
                        {
                            Id = currentTenantData.TenantId,
                            EmployeeId = employeeId,
                            Logo = currentTenantData.LogoUrl ?? "",
                            Name = currentTenantData.Name,
                            CurrencySymbol = currentTenantData.CurrencySymbol,
                            CurrencyType = currentTenantData.CurrencyType,
                            CurrencyName = GetCurrencyTypeName(currentTenantData.CurrencyType),
                            Branches = new List<Models.v1.Access.POCO.BranchData>(),
                            RoleName = roleName,
                            AccessLevel = accessLevel
                        };

                        //If is Master have access to every brach of every commerce
                        if (isMaster)
                        {
                            branches = this._businessObjects.Branches.Gets(currentTenantData.TenantId, ActiveStates.Active, OpenStates.All, DateTime.UtcNow, PaginationDefaults.PageSize, PaginationDefaults.PageNumber);

                            if (branches?.Count > 0)
                            {
                                validCommerce = true;

                                foreach (Branch itemBranch in branches)
                                {
                                    currentBranchData = new Models.v1.Access.POCO.BranchData
                                    {
                                        Id = itemBranch.Id,
                                        AccessKey = "M",//There is no access key for Master, "M" will be the distinguisher
                                        Name = itemBranch.Name,
                                    };

                                    currentCommerceData.Branches.Add(currentBranchData);
                                }
                            }
                            else
                            {
                                validCommerce = false;
                            }

                        }
                        else
                        {
                            //If isn't a master, user only has access to the branches he has been assigned

                            branchesData = new List<Models.v1.Access.POCO.BranchData>();

                            validCommerce = false;

                            for (int i = 0; i < employeeRoles.Count; ++i)
                            {
                                //If it's a employee role on current tenant
                                if (employeeRoles[i].TenantId == currentTenantData.TenantId)
                                {
                                    validCommerce = true;

                                    if (employeeRoles[i].BranchId != null)
                                    {
                                        currentBranchData = new Models.v1.Access.POCO.BranchData
                                        {
                                            Id = (Guid)employeeRoles[i].BranchId,
                                            Name = employeeRoles[i].BranchName,
                                            AccessKey = employeeRoles[i].AccessKey
                                        };

                                        currentCommerceData.Branches.Add(currentBranchData);
                                    }
                                    else
                                    {

                                        branches = this._businessObjects.Branches.Gets(currentTenantData.TenantId, ActiveStates.Active, OpenStates.All, DateTime.UtcNow, PaginationDefaults.PageSize, PaginationDefaults.PageNumber);

                                        if (branches?.Count > 0)
                                        {
                                            validCommerce = true;

                                            foreach (Branch itemBranch in branches)
                                            {
                                                currentBranchData = new Models.v1.Access.POCO.BranchData
                                                {
                                                    Id = itemBranch.Id,
                                                    AccessKey = employeeRoles[i].AccessKey,
                                                    Name = itemBranch.Name,
                                                };

                                                currentCommerceData.Branches.Add(currentBranchData);
                                            }
                                        }
                                        else
                                        {
                                            validCommerce = false;
                                        }
                                    }


                                    //Now that the data has been retrieved, needs to be supresses
                                    employeeRoles.RemoveAt(i--);

                                }
                            }

                            //At this point we have all the branches with access for this user

                        }

                        if (validCommerce)
                        {
                            enabledAccesses.HasAccess = true;
                            enabledAccesses.Commerces.Add(currentCommerceData);
                        }
                        else
                        {
                            enabledTenants.RemoveAt(t--);
                        }
                    }

                    result = Ok(enabledAccesses);

                }
                catch (Exception e)
                {
                    errorMsg = "Error: An error ocurred while data was being retrieved, " + (e.InnerException != null ? e.InnerException.Message : e.Message);
                    result = new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);

                    //Registers the invalid call
                    this._businessObjects.HttpcallInvokationLogs.Post(userId, this.GetType().Name, callId, controllerVersion,
                                        Values.StatusCodes.InternalServerError, 0, parameters, 0, 0, false, null, HttpcallTypes.Get, errorMsg);
                }
            }
            else
            {
                return new BadRequestObjectResult(
                                new BasicResponse
                                {
                                    StatusCode = Values.StatusCodes.BadRequest,
                                    CustomAction = UserappErrorCustomActions.None,
                                    DisplayMsgToUser = false,
                                    DevError = _localizer["InvalidPayload"].Value,
                                    MsgContent = "",
                                    MsgTitle = ""
                                });
            }


            return result;
        }

        #endregion

        #region CONSTRUCTORS


        public AccessController(UserManager<AppUser> userManager, IStringLocalizer<SharedResources> localizer)
        {
            this.userManager = userManager;
            this._localizer = localizer;
        }

        #endregion
    }
}
