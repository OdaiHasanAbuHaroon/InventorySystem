using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class PermissionDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("FeaturePermissions")]
        [JsonProperty("FeaturePermissions")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<FeaturePermissionDataModel> FeaturePermissions { get; set; } = [];
    }
}
