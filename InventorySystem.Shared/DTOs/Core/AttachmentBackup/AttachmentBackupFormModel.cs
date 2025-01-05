using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class AttachmentBackupFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Attachment backup name is required")]
        [StringLength(255, ErrorMessage = "Attachment backup name cannot exceed 255 characters.")]
        public required string Name { get; set; }

        [JsonPropertyName("Extention")]
        [JsonProperty("Extention")]
        [Required(ErrorMessage = "Extention is required")]
        [StringLength(10, ErrorMessage = "Extention cannot exceed 10 characters.")]
        public required string Extention { get; set; }

        [JsonPropertyName("Base64Content")]
        [JsonProperty("Base64Content")]
        [Required(ErrorMessage = "Base64Content is required")]
        public required string Base64Content { get; set; }

        [JsonPropertyName("AttachmentId")]
        [JsonProperty("AttachmentId")]
        [Required(ErrorMessage = "Attachment is required")]
        public required long AttachmentId { get; set; }
    }
}
