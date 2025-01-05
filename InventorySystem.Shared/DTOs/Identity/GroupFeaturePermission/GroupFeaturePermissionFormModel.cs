using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class GroupFeaturePermissionFormModel : FormModel
    {
        [Required(ErrorMessage = "Security Group Id is required.")]
        [JsonPropertyName("SecurityGroupId")]
        [JsonProperty("SecurityGroupId")]
        public required long SecurityGroupId { get; set; }

        [Required(ErrorMessage = "Feature Permission Id is required.")]
        [JsonPropertyName("FeaturePermissionId")]
        [JsonProperty("FeaturePermissionId")]
        public required long FeaturePermissionId { get; set; }
    }
}
