using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class GroupFeaturePermissionDataModel : DataModel
    {
        [JsonPropertyName("SecurityGroupId")]
        [JsonProperty("SecurityGroupId")]
        public long? SecurityGroupId { get; set; }

        [JsonPropertyName("SecurityGroup")]
        [JsonProperty("SecurityGroup")]
        public SecurityGroupDataModel? SecurityGroup { get; set; }

        [JsonPropertyName("FeaturePermissionId")]
        [JsonProperty("FeaturePermissionId")]
        public long? FeaturePermissionId { get; set; }

        [JsonPropertyName("FeaturePermission")]
        [JsonProperty("FeaturePermission")]
        public FeaturePermissionDataModel? FeaturePermission { get; set; }
    }
}
