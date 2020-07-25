using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.ValidationAPI.APIKeyAuth.Models.Authentication
{
    public class ApiKey
    {
        public Guid Id { set; get; }
        public string Key { set; get; }
        public string Discriminator { set; get; }
        [JsonIgnore]
        public string HashedKey { set; get; }
        public string ClientId { set; get; }
        public Guid TenantId { set; get; }
        public Guid? RequesterReferenceId { set; get; }
        public int RequesterReferenceType { set; get; }
        public string RequesterReferenceTypeName { set; get; }
        public DateTime IssuedUTCDateTime { set; get; }
        public DateTime ExpiresUTCDateTime { set; get; }
        [JsonIgnore]
        public DateTime? LastUsageDate { set; get; }
        [JsonIgnore]
        public int UsageCount { set; get; }
        [JsonIgnore]
        public bool Revoked { set; get; }
    }
}
