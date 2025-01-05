using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.DTOs.Identity;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class LanguageDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("NativeName")]
        [JsonProperty("NativeName")]
        public string? NativeName { get; set; }

        [JsonPropertyName("Code")]
        [JsonProperty("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("NameAr")]
        [JsonProperty("NameAr")]
        public string? NameAr { get; set; }

        [JsonPropertyName("Users")]
        [JsonProperty("Users")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserDataModel> Users { get; set; } = [];
    }
}
