using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YOY.UserAPI.Handlers.Authentication
{
    public interface ITokenHandler
    {
        object GenerateToken(IEnumerable<Claim> claims);
    }
}
