using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserSecurityGroupFormModel : FormModel
    {
        [Required(ErrorMessage = "Security Group Id is required.")]
        [JsonPropertyName("SecurityGroupId")]
        [JsonProperty("SecurityGroupId")]
        public required long SecurityGroupId { get; set; }

        [Required(ErrorMessage = "User Id is required.")]
        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public required long UserId { get; set; }
    }
}
