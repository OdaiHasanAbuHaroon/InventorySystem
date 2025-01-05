using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class LocationDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Type")]
        [JsonProperty("Type")]
        public string? Type { get; set; }

        [JsonPropertyName("Address")]
        [JsonProperty("Address")]
        public string? Address { get; set; }

        [JsonPropertyName("ParentLocationId")]
        [JsonProperty("ParentLocationId")]
        public long? ParentLocationId { get; set; }
    }
}
