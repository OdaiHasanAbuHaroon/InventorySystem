using InventorySystem.Shared.DTOs.BaseDTOs;
using Newtonsoft.Json;
using Riok.Mapperly.Abstractions;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.DTOs.Identity
{
    public class RoleDataModel : DataModel
    {
        [JsonPropertyName("RoleName")]
        [JsonProperty("RoleName")]
        public string? RoleName { get; set; }

        [JsonPropertyName("RoleDescription")]
        [JsonProperty("RoleDescription")]
        public string? RoleDescription { get; set; }

        [JsonPropertyName("UserRoles")]
        [JsonProperty("UserRoles")]
        [MapperIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<UserRoleDataModel> UserRoles { get; set; } = [];
    }
}
