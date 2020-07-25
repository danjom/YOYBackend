using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using YOY.Security.Cryptography;
using YOY.ValidationAPI.APIKeyAuth.Entities.Manager;
using YOY.ValidationAPI.APIKeyAuth.Models.Authentication;
using YOY.ValidationAPI.APIKeyAuth.Models.Authentication.ApiKeyGeneration;

namespace YOY.ValidationAPI.APIKeyAuth.Handlers.Authentication
{
    public static class ApiKeyHandler
    {

        private static string _conn;
        private static string _salt;

        private static ApiKeyValue GenerateApiKey()
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
        

            ApiKeyValue keyValue = new ApiKeyValue
            {
                Value = value,
                HashedValue = hashedCode
            };

            return keyValue;
        }

        public static ApiKey Get(string value, string discriminator, int activeState, int expiredState, DateTime date)
        {
            ApiKey apiKey;

            try
            {
                ApiKeyManager.SetConnection(_conn);
                apiKey = ApiKeyManager.Get(value, discriminator, activeState, expiredState, date);
            }
            catch (Exception)
            {
                apiKey = null;
            }

            return apiKey;
        }

        public static ApiKey GenerateApiKey(string clientId, string discriminator, Guid tenantId, Guid? requestedReferenceId, int requesterReferenceType, int expirationDays)
        {
            ApiKey apiKey;

            try
            {

                ApiKeyValue keyData = GenerateApiKey();

                apiKey = ApiKeyManager.Post(keyData, discriminator, clientId, tenantId, requestedReferenceId, requesterReferenceType, expirationDays);

            }
            catch (Exception)
            {
                apiKey = null;
            }

            return apiKey;
        }

        public static void SetParams(string salt, string connString)
        {
            _salt = salt;
            _conn = connString;
        }
    }
}
