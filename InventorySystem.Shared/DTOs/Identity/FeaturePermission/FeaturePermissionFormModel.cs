using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class FeaturePermissionFormModel : FormModel
    {
        [Required(ErrorMessage = "Feature Id is required.")]
        [JsonPropertyName("FeatureId")]
        [JsonProperty("FeatureId")]
        public required long FeatureId { get; set; }

        [Required(ErrorMessage = "Permission Id is required.")]
        [JsonPropertyName("PermissionId")]
        [JsonProperty("PermissionId")]
        public required long PermissionId { get; set; }
    }
}
