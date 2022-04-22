using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieAPI.Common.Converters;

/// <summary>
/// 解决序列化ulong过长,会被截断的问题
/// </summary>
public class ULongJsonConverter : JsonConverter<ulong>
{
    public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var msg = reader.GetString();
        _ = msg ?? throw new ArgumentNullException("long", "无法将null值转换为ulong类型");
        return ulong.TryParse(msg, out var number)
            ? number : throw new ArgumentException($"无法将{msg}转换为ulong类型");
    }

    public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
