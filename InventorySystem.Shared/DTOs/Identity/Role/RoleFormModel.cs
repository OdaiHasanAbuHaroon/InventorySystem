using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class RoleFormModel : FormModel
    {
        [Required(ErrorMessage = "Role Name is required.")]
        [JsonPropertyName("RoleName")]
        [JsonProperty("RoleName")]
        [MaxLength(200, ErrorMessage = "Role Name cannot exceed 200 characters.")]
        public required string RoleName { get; set; }

        [JsonPropertyName("RoleDescription")]
        [JsonProperty("RoleDescription")]
        [MaxLength(500, ErrorMessage = "Role Description cannot exceed 500 characters.")]
        public string? RoleDescription { get; set; }
    }
}
