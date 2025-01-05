using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.DTOs.Identity;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class ThemeDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Color")]
        [JsonProperty("Color")]
        public string? Color { get; set; }

        [JsonPropertyName("FontSize")]
        [JsonProperty("FontSize")]
        public int? FontSize { get; set; }

        [JsonPropertyName("IsDefault")]
        [JsonProperty("IsDefault")]
        public bool? IsDefault { get; set; }

        [JsonPropertyName("Users")]
        [JsonProperty("Users")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserDataModel> Users { get; set; } = [];
    }
}
