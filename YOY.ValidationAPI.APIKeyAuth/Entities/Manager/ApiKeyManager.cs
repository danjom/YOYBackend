using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using YOY.Security.Cryptography;
using YOY.ValidationAPI.APIKeyAuth.Entities.DB;
using YOY.ValidationAPI.APIKeyAuth.Models.Authentication;
using YOY.ValidationAPI.APIKeyAuth.Models.Authentication.ApiKeyGeneration;
using YOY.Values;

namespace YOY.ValidationAPI.APIKeyAuth.Entities.Manager
{
    public static class ApiKeyManager
    {
        private static yoyIj7qM58dCjContext _context;

        public static List<ApiKey> Gets(Guid tenantId, Guid? requesterReferenceId, int requesterReferenceType, int activeState)
        {
            List<ApiKey> apiKeys = null;

            try
            {

                var query = (dynamic)null;

                if(requesterReferenceId != null)
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:
                            query = from x in _context.Apikeys
                                    where x.TenantId == tenantId && x.RequesterReferenceType == requesterReferenceType && x.RequesterReferenceId == requesterReferenceId
                                    select x;
                            break;
                        case ActiveStates.Active:
                            query = from x in _context.Apikeys
                                    where !x.Revoked && x.TenantId == tenantId && x.RequesterReferenceType == requesterReferenceType && x.RequesterReferenceId == requesterReferenceId
                                    select x;
                            break;
                        case ActiveStates.Inactive:
                            query = from x in _context.Apikeys
                                    where x.Revoked && x.TenantId == tenantId && x.RequesterReferenceType == requesterReferenceType && x.RequesterReferenceId == requesterReferenceId
                                    select x;
                            break;
                    }
                }
                else
                {
                    switch (activeState)
                    {
                        case ActiveStates.All:
                            query = from x in _context.Apikeys
                                    where x.TenantId == tenantId
                                    select x;
                            break;
                        case ActiveStates.Active:
                            query = from x in _context.Apikeys
                                    where !x.Revoked && x.TenantId == tenantId
                                    select x;
                            break;
                        case ActiveStates.Inactive:
                            query = from x in _context.Apikeys
                                    where x.Revoked && x.TenantId == tenantId
                                    select x;
                            break;
                    }
                }

                if(query != null)
                {
                    apiKeys = new List<ApiKey>();
                    ApiKey key;

                    foreach(Apikeys item in query)
                    {
                        key = new ApiKey
                        {
                            Id = item.Id,
                            TenantId = item.TenantId,
                            Discriminator = item.Discriminator,
                            HashedKey = item.HashedKey,
                            ClientId = item.ClientId,
                            RequesterReferenceId = item.RequesterReferenceId,
                            RequesterReferenceType = item.RequesterReferenceType,
                            IssuedUTCDateTime = item.IssuedUtcdate,
                            ExpiresUTCDateTime = item.ExpiresUtcdate,
                            LastUsageDate = item.LastUsageDate,
                            UsageCount = item.UsageCount,
                            Revoked = item.Revoked
                        };

                        apiKeys.Add(key);
                    }
                }
            }
            catch (Exception)
            {
                apiKeys = null;
            }

            return apiKeys;
        }

        public static ApiKey Get(string value, string discriminator, int activeState, int expiredState, DateTime date)
        {
            ApiKey key = null;

            try
            {

                var query = (dynamic)null;

                switch (activeState)
                {
                    case ActiveStates.All:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                query = from x in _context.Apikeys
                                        where x.Discriminator == discriminator
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in _context.Apikeys
                                        where x.Discriminator == discriminator && x.ExpiresUtcdate > date
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in _context.Apikeys
                                        where x.Discriminator == discriminator && x.ExpiresUtcdate <= date
                                        select x;
                                break;

                        }
                        break;
                    case ActiveStates.Active:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                query = from x in _context.Apikeys
                                        where !x.Revoked && x.Discriminator == discriminator
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in _context.Apikeys
                                        where !x.Revoked && x.Discriminator == discriminator && x.ExpiresUtcdate > date
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in _context.Apikeys
                                        where !x.Revoked && x.Discriminator == discriminator && x.ExpiresUtcdate <= date
                                        select x;
                                break;

                        }
                        break;
                    case ActiveStates.Inactive:
                        switch (expiredState)
                        {
                            case ExpiredStates.All:
                                query = from x in _context.Apikeys
                                        where x.Revoked && x.Discriminator == discriminator
                                        select x;
                                break;
                            case ExpiredStates.Valid:
                                query = from x in _context.Apikeys
                                        where x.Revoked && x.Discriminator == discriminator && x.ExpiresUtcdate > date
                                        select x;
                                break;
                            case ExpiredStates.Expired:
                                query = from x in _context.Apikeys
                                        where x.Revoked && x.Discriminator == discriminator && x.ExpiresUtcdate <= date
                                        select x;
                                break;

                        }
                        break;
                }

                if(query != null)
                {
                    Apikeys apikey = null;

                    foreach(Apikeys item in query)
                    {
                        if (SecurityHash.VerifyHash(value, "SHA512", item.HashedKey))
                            apikey = item;
                    }

                    if(apikey != null)
                    {
                        key = new ApiKey
                        {
                            Id = apikey.Id,
                            TenantId = apikey.TenantId,
                            Discriminator = apikey.Discriminator,
                            Key = value,
                            HashedKey = apikey.HashedKey,
                            ClientId = apikey.ClientId,
                            RequesterReferenceId = apikey.RequesterReferenceId,
                            RequesterReferenceType = apikey.RequesterReferenceType,
                            IssuedUTCDateTime = apikey.IssuedUtcdate,
                            ExpiresUTCDateTime = apikey.ExpiresUtcdate,
                            LastUsageDate = apikey.LastUsageDate,
                            UsageCount = apikey.UsageCount,
                            Revoked = apikey.Revoked
                        };

                        ++apikey.UsageCount;
                        apikey.LastUsageDate = DateTime.UtcNow;

                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                key = null;
            }

            return key;
        }

        public static ApiKey Post(ApiKeyValue apiKeyValue, string discriminator, string clientId, Guid tenantId, Guid? requesterReferenceId, int requesterReferenceType, int expirationDays)
        {
            ApiKey key;
            try
            {

                Apikeys newKey = new Apikeys
                {
                    Id = Guid.NewGuid(),
                    Discriminator = discriminator,
                    HashedKey = apiKeyValue.HashedValue,
                    ClientId = clientId,
                    TenantId = tenantId,
                    RequesterReferenceId = requesterReferenceId,
                    RequesterReferenceType = requesterReferenceType,
                    IssuedUtcdate = DateTime.UtcNow,
                    ExpiresUtcdate = DateTime.UtcNow.AddDays(expirationDays),
                    LastUsageDate = null,
                    UsageCount = 0,
                    Revoked = false
                };

                _context.Apikeys.Add(newKey);
                _context.SaveChanges();

                key = new ApiKey
                {
                    Id = newKey.Id,
                    TenantId = newKey.TenantId,
                    Key = apiKeyValue.Value,
                    HashedKey = newKey.HashedKey,
                    ClientId = newKey.ClientId,
                    RequesterReferenceId = newKey.RequesterReferenceId,
                    RequesterReferenceType = newKey.RequesterReferenceType,
                    IssuedUTCDateTime = newKey.IssuedUtcdate,
                    ExpiresUTCDateTime = newKey.ExpiresUtcdate,
                    LastUsageDate = newKey.LastUsageDate,
                    UsageCount = newKey.UsageCount,
                    Revoked = newKey.Revoked
                };
            }
            catch(Exception)
            {
                key = null;
            }

            return key;
        }

        public static bool Put(string value)
        {
            bool success = false;

            try
            {

                var query = from x in _context.Apikeys
                            select x;

                if (query != null)
                {
                    Apikeys apikeys = null;

                    foreach (Apikeys item in query)
                    {
                        if (SecurityHash.VerifyHash(value, "SHA512", item.HashedKey))
                            apikeys = item;
                    }

                    if (apikeys != null)
                    {
                        apikeys.Revoked = !apikeys.Revoked;
                        _context.SaveChanges();

                        success = true;
                    }
                }
            }
            catch(Exception)
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
