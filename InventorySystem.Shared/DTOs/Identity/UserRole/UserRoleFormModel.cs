using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserRoleFormModel : FormModel
    {
        [Required(ErrorMessage = "Role Id is required.")]
        [JsonPropertyName("RoleId")]
        [JsonProperty("RoleId")]
        public required long RoleId { get; set; }

        [Required(ErrorMessage = "User Id is required.")]
        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public required long UserId { get; set; }
    }
}
