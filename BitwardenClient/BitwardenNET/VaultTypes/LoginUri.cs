
using Newtonsoft.Json;

namespace BitwardenNET.VaultTypes
{
    public class LoginUri
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
        public UriMatchDetection MatchDetection { get; set; } = UriMatchDetection.Default;
    }
}
