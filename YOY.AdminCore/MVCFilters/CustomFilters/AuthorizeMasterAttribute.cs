using YOY.DAO.Entities;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using YOY.AdminCore.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace YOY.AdminCore.MVCFilters.CustomFilters
{
    /// <summary>
    /// Checks if the user trying to access a protected resource is 
    /// tenant's employee with the right roles
    /// </summary>
    public class AuthorizeMasterAttribute : TypeFilterAttribute
    {

        // passing params string[] here allows us to use multiple roles in the fitler argument
        public AuthorizeMasterAttribute(params string[] roles) : base(typeof(AuthorizeMasterFilter))
        {
            Arguments = new object[] { roles };
        }

    }

    public class AuthorizeMasterFilter : IAuthorizationFilter
    {
        readonly string[] _roles;

        public AuthorizeMasterFilter(string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            int allowedCount = 0;

            foreach (var role in _roles)
            {
                if (context.HttpContext.User.IsInRole(role))
                {
                    ++allowedCount;
                }
            }

            if (allowedCount == 0)
            {
                context.Result = new RedirectToRouteResult(new
               RouteValueDictionary(new { controller = "AccessDenied", action = "Index" }));
            }
        }
    }
}