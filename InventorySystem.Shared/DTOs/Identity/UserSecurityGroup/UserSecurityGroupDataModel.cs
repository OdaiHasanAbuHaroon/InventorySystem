using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserSecurityGroupDataModel : DataModel
    {
        [JsonPropertyName("SecurityGroupId")]
        [JsonProperty("SecurityGroupId")]
        public long? SecurityGroupId { get; set; }

        [JsonPropertyName("SecurityGroup")]
        [JsonProperty("SecurityGroup")]
        public SecurityGroupDataModel? SecurityGroup { get; set; }

        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public long? UserId { get; set; }

        [JsonPropertyName("User")]
        [JsonProperty("User")]
        public UserDataModel? User { get; set; }
    }
}
