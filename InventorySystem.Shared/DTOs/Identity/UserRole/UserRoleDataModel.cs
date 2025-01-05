using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserRoleDataModel : DataModel
    {
        [JsonPropertyName("RoleId")]
        [JsonProperty("RoleId")]
        public long? RoleId { get; set; }

        [JsonPropertyName("Role")]
        [JsonProperty("Role")]
        public RoleDataModel? Role { get; set; }

        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public long? UserId { get; set; }

        [JsonPropertyName("User")]
        [JsonProperty("User")]
        public UserDataModel? User { get; set; }
    }
}
