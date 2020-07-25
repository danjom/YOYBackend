using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOY.ValidationAPI.APIKeyAuth.Models.Authentication.ApiKeyGeneration
{
    public class ApiKeyValue
    {
        public string Value { set; get; }
        public string HashedValue { set; get; }
    }
}
