using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class ThemeFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Theme name is required")]
        [StringLength(100, ErrorMessage = "Theme name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        [JsonPropertyName("Color")]
        [JsonProperty("Color")]
        [Required(ErrorMessage = "Color is required")]
        [StringLength(50, ErrorMessage = "Color cannot exceed 50 characters.")]
        public required string Color { get; set; }

        [JsonPropertyName("FontSize")]
        [JsonProperty("FontSize")]
        public int? FontSize { get; set; }

        [JsonPropertyName("IsDefault")]
        [JsonProperty("IsDefault")]
        [Required(ErrorMessage = "Is default value is required")]
        public required bool IsDefault { get; set; } = false;
    }
}
