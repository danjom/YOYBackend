using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YOY.UserAPI.Config;

namespace YOY.UserAPI.Handlers.Authentication
{
    public class JWTTokenHandler : ITokenHandler
    {
        private JwtBearerTokenSettings _jwtBearerTokenSettings;
        private readonly string _salt;

        public JWTTokenHandler(JwtBearerTokenSettings jwtBearerTokenSettings, string salt)
        {
            _jwtBearerTokenSettings = jwtBearerTokenSettings;
            _salt = salt;
        }

        public object GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_salt);

            var claimsDictionary = new Dictionary<string, object>();

            foreach (var claim in claims)
            {
                claimsDictionary.Add(claim.Type, claim.Value);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Claims = claimsDictionary,
                Expires = DateTime.UtcNow.AddMinutes(_jwtBearerTokenSettings.ExpiryTimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSettings.Audience,
                Issuer = _jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
