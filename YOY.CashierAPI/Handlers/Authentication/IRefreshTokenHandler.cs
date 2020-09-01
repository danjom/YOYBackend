using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.CashierAPI.Models.Authentication;

namespace YOY.CashierAPI.Handlers.Authentication
{
    public interface IRefreshTokenHandler
    {
        RefreshToken GenerateRefreshToken(string username, string clientId, DateTime? expirationTime);
        RefreshToken RetrieveRefreshToken(string username, string clientId, string value, DateTime date);
        bool RevokeToken(string username, string value);

    }
}