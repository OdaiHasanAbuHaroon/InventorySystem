using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class FeaturePermissionDataModel
    {
        [JsonPropertyName("Id")]
        [JsonProperty("Id")]
        public long? Id { get; set; }

        [JsonPropertyName("FeatureId")]
        [JsonProperty("FeatureId")]
        public long? FeatureId { get; set; }

        [JsonPropertyName("Feature")]
        [JsonProperty("Feature")]
        public FeatureDataModel? Feature { get; set; }

        [JsonPropertyName("PermissionId")]
        [JsonProperty("PermissionId")]
        public long? PermissionId { get; set; }

        [JsonPropertyName("Permission")]
        [JsonProperty("Permission")]
        public PermissionDataModel? Permission { get; set; }

        [JsonPropertyName("UserFeaturePermissions")]
        [JsonProperty("UserFeaturePermissions")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserFeaturePermissionDataModel> UserFeaturePermissions { get; set; } = [];

        [JsonPropertyName("GroupFeaturePermissions")]
        [JsonProperty("GroupFeaturePermissions")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<GroupFeaturePermissionDataModel> GroupFeaturePermissions { get; set; } = [];
    }
}
