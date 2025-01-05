using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class AttachmentBackupDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Extention")]
        [JsonProperty("Extention")]
        public string? Extention { get; set; }

        [JsonPropertyName("Base64Content")]
        [JsonProperty("Base64Content")]
        public string? Base64Content { get; set; }

        [JsonPropertyName("AttachmentId")]
        [JsonProperty("AttachmentId")]
        public long AttachmentId { get; set; }

        [JsonPropertyName("Attachment")]
        [JsonProperty("Attachment")]
        [System.Text.Json.Serialization.JsonIgnore]
        public AttachmentDataModel? Attachment { get; set; }
    }
}
