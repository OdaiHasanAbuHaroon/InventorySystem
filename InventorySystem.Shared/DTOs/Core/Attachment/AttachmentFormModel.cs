using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class AttachmentFormModel : FormModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        [Required(ErrorMessage = "Attachment name is required")]
        [StringLength(200, ErrorMessage = "Attachment name cannot exceed 200 characters.")]
        public required string Name { get; set; }

        [JsonPropertyName("Extention")]
        [JsonProperty("Extention")]
        [Required(ErrorMessage = "Extention is required")]
        [StringLength(10, ErrorMessage = "Extention cannot exceed 10 characters.")]
        public required string Extention { get; set; }

        [JsonPropertyName("Path")]
        [JsonProperty("Path")]
        [Required(ErrorMessage = "Path is required")]
        [StringLength(256, ErrorMessage = "Path cannot exceed 256 characters.")]
        public required string Path { get; set; }

        [JsonPropertyName("FileContent")]
        [JsonProperty("FileContent")]
        [Required(ErrorMessage = "File Content is required.")]
        public required string FileContent { get; set; }
    }
}
