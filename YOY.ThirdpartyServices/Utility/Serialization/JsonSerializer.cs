using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace YOY.ThirdpartyServices.Utility.Serialization
{
    public static class JsonSerializer
    {
        public static string ToJson(this object value)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            };
            return JsonConvert.SerializeObject(value, settings);
        }
    }
}