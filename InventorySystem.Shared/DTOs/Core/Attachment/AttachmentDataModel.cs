using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Business
{
    public class AttachmentDataModel : DataModel
    {
        [JsonPropertyName("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Extention")]
        [JsonProperty("Extention")]
        public string? Extention { get; set; }

        [JsonPropertyName("Path")]
        [JsonProperty("Path")]
        public string? Path { get; set; }

        [JsonPropertyName("FileContent")]
        [JsonProperty("FileContent")]
        public string? FileContent { get; set; }

    }
}
