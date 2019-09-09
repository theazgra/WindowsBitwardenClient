using Newtonsoft.Json;
using BitwardenNET.CustomJsonConverters;

namespace BitwardenNET.VaultTypes.ItemTypes
{
    public class ItemCard
    {
        [JsonProperty("cardholderName")]
        public string CardholderName { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("number")]
        [JsonConverter(typeof(CardNumberJsonConverter))]
        public CardNumber Number { get; set; }

        [JsonProperty("expMonth")]
        [JsonConverter(typeof(StringToIntJsonConverter))]
        public int ExpirationMonth { get; set; }

        [JsonProperty("expYear")]
        [JsonConverter(typeof(StringToIntJsonConverter))]
        public int ExpirationYear { get; set; }

        [JsonProperty("code")]
        [JsonConverter(typeof(StringToIntJsonConverter))]
        public int SecurityCode { get; set; }
    }
}
