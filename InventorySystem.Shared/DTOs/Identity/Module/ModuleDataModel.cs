using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class ModuleDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("Features")]
        [JsonProperty("Features")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<FeatureDataModel> Features { get; set; } = [];

        [JsonPropertyName("UserModules")]
        [JsonProperty("UserModules")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<UserModuleDataModel> UserModules { get; set; } = [];
    }
}
