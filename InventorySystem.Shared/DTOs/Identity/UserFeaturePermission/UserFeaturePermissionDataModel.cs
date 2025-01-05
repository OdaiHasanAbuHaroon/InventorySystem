using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserFeaturePermissionDataModel : DataModel
    {
        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public long? UserId { get; set; }

        [JsonPropertyName("User")]
        [JsonProperty("User")]
        public UserDataModel? User { get; set; }

        [JsonPropertyName("FeaturePermissionId")]
        [JsonProperty("FeaturePermissionId")]
        public long? FeaturePermissionId { get; set; }

        [JsonPropertyName("FeaturePermission")]
        [JsonProperty("FeaturePermission")]
        public FeaturePermissionDataModel? FeaturePermission { get; set; }
    }
}
