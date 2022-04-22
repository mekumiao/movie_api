using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieAPI.Common.Converters;

/// <summary>
/// 解决序列化long过长,会被截断的问题
/// </summary>
public class LongJsonConverter : JsonConverter<long>
{
    public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt64();
        }
        else
        {
            var msg = reader.GetString();
            _ = msg ?? throw new ArgumentNullException("long", "无法将null值转换为long类型");
            return long.TryParse(msg, out var number)
                ? number : throw new ArgumentException($"无法将{msg}转换为long类型");
        }
    }

    public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
