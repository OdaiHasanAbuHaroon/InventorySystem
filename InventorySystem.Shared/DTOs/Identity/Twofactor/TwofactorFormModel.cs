using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class TwofactorFormModel : FormModel
    {
        [Required(ErrorMessage = "User Id is required.")]
        [JsonPropertyName("UserId")]
        [JsonProperty("UserId")]
        public required long UserId { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [JsonPropertyName("Code")]
        [JsonProperty("Code")]
        [MaxLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
        public required string Code { get; set; }

        [Required(ErrorMessage = "Expiration Date is required.")]
        [JsonPropertyName("ExpirationDate")]
        [JsonProperty("ExpirationDate")]
        public required DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Is Used is required.")]
        [JsonPropertyName("IsUsed")]
        [JsonProperty("IsUsed")]
        public required bool IsUsed { get; set; } = false;

        [Required(ErrorMessage = "Is Sent is required.")]
        [JsonPropertyName("IsSent")]
        [JsonProperty("IsSent")]
        public required bool IsSent { get; set; } = false;

        [Required(ErrorMessage = "Stamp is required.")]
        [JsonPropertyName("Stamp")]
        [JsonProperty("Stamp")]
        public required string Stamp { get; set; }

        [Required(ErrorMessage = "Request Type is required.")]
        [JsonPropertyName("RequestType")]
        [JsonProperty("RequestType")]
        public required int RequestType { get; set; }
    }
}
