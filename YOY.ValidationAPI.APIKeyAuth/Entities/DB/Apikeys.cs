using System;
using System.Collections.Generic;

namespace YOY.ValidationAPI.APIKeyAuth.Entities.DB
{
    public partial class Apikeys
    {
        public Guid Id { get; set; }
        public string HashedKey { get; set; }
        public string ClientId { get; set; }
        public string Discriminator { get; set; }
        public Guid TenantId { get; set; }
        public Guid? RequesterReferenceId { get; set; }
        public int RequesterReferenceType { get; set; }
        public DateTime IssuedUtcdate { get; set; }
        public DateTime ExpiresUtcdate { get; set; }
        public DateTime? LastUsageDate { get; set; }
        public int UsageCount { get; set; }
        public bool Revoked { get; set; }
    }
}
