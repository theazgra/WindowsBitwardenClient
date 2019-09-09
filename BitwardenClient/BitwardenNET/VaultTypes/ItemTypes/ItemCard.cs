using Newtonsoft.Json;

namespace BitwardenNET.VaultTypes.ItemTypes
{
    public class ItemCard
    {
        [JsonProperty("cardholderName")]
        public string CardholderName { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("expMonth")]
        public int ExpirationMonth { get; set; }

        [JsonProperty("expYear")]
        public int ExpirationYear { get; set; }

        [JsonProperty("code")]
        public int SecurityCode { get; set; }
    }
}
