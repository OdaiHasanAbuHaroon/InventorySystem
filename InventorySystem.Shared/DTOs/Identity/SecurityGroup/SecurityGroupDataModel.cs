using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class SecurityGroupDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("UserSecurityGroups")]
        [JsonProperty("UserSecurityGroups")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<UserSecurityGroupDataModel> UserSecurityGroups { get; set; } = [];

        [JsonPropertyName("GroupFeaturePermissions")]
        [JsonProperty("GroupFeaturePermissions")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<GroupFeaturePermissionDataModel> GroupFeaturePermissions { get; set; } = [];
    }
}
