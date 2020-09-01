using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YOY.CashierAPI.Handlers.Authentication
{
    public interface ITokenHandler
    {
        object GenerateToken(IEnumerable<Claim> claims);
    }
}