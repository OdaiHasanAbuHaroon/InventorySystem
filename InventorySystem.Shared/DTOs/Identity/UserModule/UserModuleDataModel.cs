using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserModuleDataModel : DataModel
    {
        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public long? UserId { get; set; }

        [JsonPropertyName("User")]
        [JsonProperty("User")]
        public UserDataModel? User { get; set; }

        [JsonPropertyName("ModuleId")]
        [JsonProperty("ModuleId")]
        public long? ModuleId { get; set; }

        [JsonPropertyName("Module")]
        [JsonProperty("Module")]
        public ModuleDataModel? Module { get; set; }
    }
}
