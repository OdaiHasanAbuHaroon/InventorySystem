using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class BrandDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public long ManufacturerId { get; set; }

        [JsonPropertyName("User")]
        [JsonProperty("User")]
        [System.Text.Json.Serialization.JsonIgnore]
        public ManufacturerDataModel? Manufacturer { get; set; }
    }
}
