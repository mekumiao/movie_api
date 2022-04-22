using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieAPI.Common.Converters;

/// <summary>
/// 短时间转换器
/// </summary>
public class ShortTimeJsonConverter : JsonConverter<ShortTime>
{
    public override ShortTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == default)
        {
            return default;
        }
        return new ShortTime
        {
            Time = DateTime.Parse(value, CultureInfo.InvariantCulture)
        };
    }

    public override void Write(Utf8JsonWriter writer, ShortTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Time.ToShortTimeString() ?? string.Empty);
    }
}
