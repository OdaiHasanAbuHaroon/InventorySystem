using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Tools
{
    public class ClaimJsonConverter : JsonConverter<Claim>
    {
        public override Claim Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            string? claimType = string.Empty;
            string? claimValue = string.Empty;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();

                    reader.Read();

                    if (propertyName == "Type")
                    {
                        claimType = reader.GetString();
                    }
                    else if (propertyName == "Value")
                    {
                        claimValue = reader.GetString();
                    }
                }
            }

            if (string.IsNullOrEmpty(claimType) || string.IsNullOrEmpty(claimValue))
            {
                throw new JsonException();
            }

            return new Claim(claimType, claimValue);
        }

        public override void Write(Utf8JsonWriter writer, Claim value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Type", value.Type);
            writer.WriteString("Value", value.Value);
            writer.WriteEndObject();
        }
    }
}
