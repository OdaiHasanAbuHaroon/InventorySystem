using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserModuleFormModel : FormModel
    {
        [Required(ErrorMessage = "User Id is required.")]
        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public required long UserId { get; set; }

        [Required(ErrorMessage = "Module Id is required.")]
        [JsonPropertyName("ModuleId")]
        [JsonProperty("ModuleId")]
        public required long ModuleId { get; set; }
    }
}
