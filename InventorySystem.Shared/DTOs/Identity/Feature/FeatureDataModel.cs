using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class FeatureDataModel : DataModel
    {
        [JsonPropertyName("ModuleId")]
        [JsonProperty("ModuleId")]
        public long? ModuleId { get; set; }

        [JsonPropertyName("Module")]
        [JsonProperty("Module")]
        public ModuleDataModel? Module { get; set; }

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
