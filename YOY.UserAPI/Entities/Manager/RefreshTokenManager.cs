using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.UserAPI.Entities.DB;
using YOY.UserAPI.Models.Authentication;
using YOY.UserAPI.Models.Authentication.RefreshTokenGeneration;
using YOY.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace YOY.UserAPI.Entities.Manager
{
    public static class RefreshTokenManager
    {
        private static yoyIj7qM58dCjContext _context;


        public static RefreshToken Get(string username, string clientId, string value, DateTime date)
        {
            RefreshToken refreshToken = null;

            try
            {

                var query = from x in _context.RefreshTokens
                            where x.Username == username && x.ClientId == clientId && !x.Revoked && x.ExpiresUtc >= date
                            select x;

                if (query != null)
                {
                    RefreshTokens token = null;

                    foreach (RefreshTokens item in query)
                    {
                        if (SecurityHash.VerifyHash(value, "SHA512", item.HashedValue))
                            token = item;
                    }

                    if (token != null)
                    {
                        token.Revoked = true;
                        _context.SaveChanges();

                        refreshToken = new RefreshToken
                        {
                            Id = token.Id,
                            Username = token.Username,
                            ClientId = token.ClientId,
                            IssuedUTC = token.IssuedUtc,
                            ExpiresUTC = token.ExpiresUtc,
                            Revoked = token.Revoked,
                            Value = value
                        };

                        _context.RefreshTokens.Remove(token);
                        _context.SaveChanges();
                    }
                }
            }
            catch(Exception)
            {
                refreshToken = null;
            }

            return refreshToken;
        }
        public static RefreshToken Post(string username, string clientId, RefreshTokenValue tokenData, DateTime? expirationTime)
        {
            RefreshToken refreshToken;

            try
            {


                RefreshTokens newToken = new RefreshTokens
                {
                    Id = Guid.NewGuid(),
                    Username = username,
                    ClientId = clientId,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = expirationTime,
                    Revoked = false,
                    HashedValue = tokenData.HashedValue
                };

                _context.RefreshTokens.Add(newToken);
                _context.SaveChanges();

                refreshToken = new RefreshToken
                {
                    Id = newToken.Id,
                    Username = newToken.Username,
                    ClientId = newToken.ClientId,
                    IssuedUTC = newToken.IssuedUtc,
                    ExpiresUTC = newToken.ExpiresUtc,
                    Revoked = newToken.Revoked,
                    Value = tokenData.Value
                };

            }
            catch (Exception)
            {
                refreshToken = null;
            }

            return refreshToken;
        }

        public static bool Put(string username, string value)
        {
            bool success = false;

            try
            {
                var query = from x in _context.RefreshTokens
                            where x.Username == username && !x.Revoked
                            select x;

                if (query != null)
                {
                    RefreshTokens token = null;

                    foreach (RefreshTokens item in query)
                    {
                        if (SecurityHash.VerifyHash(value, "SHA512", item.HashedValue))
                            token = item;
                    }

                    if (token != null)
                    {
                        token.Revoked = true;
                        _context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public static void SetConnection(string connString)
        {
            _context = new yoyIj7qM58dCjContext(connString);
        }
    }
}
