using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.ValidationAPI.APIKeyAuth.Models.v1.ApiKey.POCO
{
    public class NewApiKeyData
    {
        public Guid TenantId { set; get; }
        public string ClientId { set; get; }
        public string Discriminator { set; get; }
        public Guid? RequesterReferenceId { set; get; }
        public int RequesterReferenceType { set; get; }
        public int ExpirationDays { set; get; }
    }
}
