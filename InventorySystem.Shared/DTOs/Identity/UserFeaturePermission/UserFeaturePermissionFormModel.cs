using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class UserFeaturePermissionFormModel : FormModel
    {
        [Required(ErrorMessage = "User Id is required.")]
        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public required long UserId { get; set; }

        [Required(ErrorMessage = "Feature Permission Id is required.")]
        [JsonPropertyName("FeaturePermissionId")]
        [JsonProperty("FeaturePermissionId")]
        public required long FeaturePermissionId { get; set; }
    }
}
