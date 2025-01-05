using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Core
{
    public class CurrencyFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Currency name is required")]
        [StringLength(200, ErrorMessage = "Currency name cannot exceed 200 characters.")]
        public required string Name { get; set; }

        [JsonPropertyName("Code")]
        [JsonProperty("Code")]
        [Required(ErrorMessage = "Code is required")]
        [StringLength(3, ErrorMessage = "Code cannot exceed 3 characters.")]
        public required string Code { get; set; }

        [JsonPropertyName("Symbol")]
        [JsonProperty("Symbol")]
        [Required(ErrorMessage = "Symbol is required")]
        [StringLength(10, ErrorMessage = "Symbol cannot exceed 10 characters.")]
        public required string Symbol { get; set; }
    }
}
