﻿using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YOY.CashierAPI.Models.Authentication.RefreshTokenGeneration;
using System.Text;
using YOY.Security.Cryptography;
using YOY.CashierAPI.Models.Authentication;
using YOY.CashierAPI.Entities.Manager;

namespace YOY.CashierAPI.Handlers.Authentication
{
    public class OwnRefreshTokenHandler : IRefreshTokenHandler
    {
        private string _salt;
        private string _conn;

        private RefreshTokenValue GenerateRefreshToken()
        {
            byte[] randomNumber;
            string value;
            string hashedCode;

            do
            {
                randomNumber = new byte[16];

                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);

                value = Convert.ToBase64String(randomNumber).Trim('=').Replace('\\', 'L').Replace('/', '1');
                hashedCode = SecurityHash.ComputeHash(value.ToString(), "SHA512", Encoding.UTF8.GetBytes(_salt));
            }
            while (!SecurityHash.VerifyHash(value, "SHA512", hashedCode));


            RefreshTokenValue keyValue = new RefreshTokenValue
            {
                Value = value,
                HashedValue = hashedCode
            };

            return keyValue;
        }

        public RefreshToken GenerateRefreshToken(string username, string clientId, DateTime? expirationTime)
        {
            RefreshToken refreshToken;

            try
            {

                RefreshTokenValue tokenData = GenerateRefreshToken();

                RefreshTokenManager.SetConnection(_conn);
                refreshToken = RefreshTokenManager.Post(username, clientId, tokenData, expirationTime);

            }
            catch (Exception)
            {
                refreshToken = null;
            }

            return refreshToken;
        }

        public RefreshToken RetrieveRefreshToken(string username, string clientId, string value, DateTime date)
        {
            RefreshToken refreshToken;
            try
            {
                RefreshTokenManager.SetConnection(_conn);
                refreshToken = RefreshTokenManager.Get(username, clientId, value, date);

            }
            catch (Exception)
            {
                refreshToken = null;
            }

            return refreshToken;
        }

        public bool RevokeToken(string username, string value)
        {
            bool success;

            try
            {
                RefreshTokenManager.SetConnection(_conn);
                success = RefreshTokenManager.Put(username, value);
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public OwnRefreshTokenHandler(string salt, string connString)
        {
            _salt = salt;
            _conn = connString;
        }
    }
}
