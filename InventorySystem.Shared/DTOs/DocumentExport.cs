using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs
{
    public class DocumentExport
    {
        [JsonPropertyName("Stream")]
        [JsonProperty("Stream")]
        public required MemoryStream Stream { get; set; }

        [JsonPropertyName("FileType")]
        [JsonProperty("FileType")]
        public required string FileType { get; set; }

        [JsonPropertyName("FileName")]
        [JsonProperty("FileName")]
        public required string FileName { get; set; }
    }
}
