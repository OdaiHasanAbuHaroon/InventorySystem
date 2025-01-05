using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class CurrencyDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Code")]
        [JsonProperty("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("Symbol")]
        [JsonProperty("Symbol")]
        public string? Symbol { get; set; }

    }
}
