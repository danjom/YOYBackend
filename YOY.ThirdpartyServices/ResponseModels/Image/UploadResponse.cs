using Newtonsoft.Json;

namespace YOY.ThirdpartyServices.ResponseModels.Image
{
    public class UploadResponse
    {
        [JsonProperty("public_id")]
        public string PublicId { get; set; }
        [JsonProperty("signature")]
        public string Signature { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
