using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Responses
{
    public class ResponseMessage
    {
        public ResponseMessage()
        {

        }

        public ResponseMessage(string type, string message)
        {
            Type = type;
            Message = message;
        }

        public ResponseMessage(string type, string message, string code)
        {
            Type = type;
            Message = message;
            Code = code;
        }

        [JsonPropertyName("Message")]
        [JsonProperty("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("Code")]
        [JsonProperty("Code")]
        public string? Code { get; set; }

        [JsonPropertyName("Type")]
        [JsonProperty("Type")]
        public string? Type { get; set; }
    }
}
