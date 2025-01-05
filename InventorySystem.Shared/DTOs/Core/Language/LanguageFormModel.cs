using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class LanguageFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Device manufacturer name is required")]
        [StringLength(100, ErrorMessage = "Device manufacturer name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        [JsonPropertyName("NativeName")]
        [JsonProperty("NativeName")]
        [Required(ErrorMessage = "Native name is required")]
        [StringLength(100, ErrorMessage = "Native name cannot exceed 100 characters.")]
        public required string NativeName { get; set; }

        [JsonPropertyName("Code")]
        [JsonProperty("Code")]
        [Required(ErrorMessage = "Code is required")]
        [StringLength(10, ErrorMessage = "Code cannot exceed 10 characters.")]
        public required string Code { get; set; }

        [JsonPropertyName("NameAr")]
        [JsonProperty("NameAr")]
        [StringLength(100, ErrorMessage = "Arabic name cannot exceed 100 characters.")]
        public string? NameAr { get; set; }
    }
}
