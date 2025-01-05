using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class FeatureFormModel : FormModel
    {
        [Required(ErrorMessage = "Module Id is required.")]
        [JsonPropertyName("ModuleId")]
        [JsonProperty("ModuleId")]
        public required long ModuleId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public required string Name { get; set; }

        [JsonPropertyName("Description")]
        [JsonProperty("Description")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
    }
}