using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventorySystem.Shared.Tools
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _dateFormat;

        public DateOnlyJsonConverter(string dateFormat = "yyyy-MM-dd")
        {
            _dateFormat = dateFormat;
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out var dateTime))
                {
                    return DateOnly.FromDateTime(dateTime);
                }
            }

            throw new JsonException($"Unable to convert {reader.GetString()} to DateOnly");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat));
        }
    }
}
