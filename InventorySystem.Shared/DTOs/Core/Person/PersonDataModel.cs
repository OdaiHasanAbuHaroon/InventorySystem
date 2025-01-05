using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.DTOs.Identity;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class PersonDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Reference")]
        [JsonProperty("Reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("Type")]
        [JsonProperty("Type")]
        public string? Type { get; set; }

        [JsonPropertyName("Cost")]
        [JsonProperty("Cost")]
        public decimal Cost { get; set; }

        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public long UserId { get; set; }

        [JsonPropertyName("User")]
        [JsonProperty("User")]
        [System.Text.Json.Serialization.JsonIgnore]
        public UserDataModel? User { get; set; }
    }
}
