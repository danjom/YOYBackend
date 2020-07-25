using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.UserAPI.Models.Authentication;

namespace YOY.UserAPI.Handlers.Authentication
{
    public interface IRefreshTokenHandler
    {
        RefreshToken GenerateRefreshToken(string username, string clientId, DateTime? expirationTime);
        RefreshToken RetrieveRefreshToken(string username, string clientId, string value, DateTime date);
        bool RevokeToken(string username, string value);

    }
}
