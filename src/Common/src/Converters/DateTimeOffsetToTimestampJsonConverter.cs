using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieAPI.Common.Converters;

public class DateTimeOffsetToTimestampJsonConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (long.TryParse(value, out var longValue))
        {
            return DateTimeOffset.FromUnixTimeSeconds(longValue).ToLocalTime();
        }
        throw new ArgumentException($"不能将{value}反序列化为DateTimeOffset类型");
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUnixTimeMilliseconds().ToString());
    }
}
