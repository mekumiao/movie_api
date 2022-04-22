using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieAPI.Common.Converters;

/// <summary>
/// 精确到秒的转化器
/// </summary>
public class SecondDateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == default)
        {
            return default;
        }
        return DateTime.Parse(value, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));
    }
}
