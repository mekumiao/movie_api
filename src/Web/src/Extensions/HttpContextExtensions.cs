using System.ComponentModel;
using System.Reflection;

namespace MovieAPI.Web;

public static class HttpContextExtensions
{
    private readonly static Lazy<Dictionary<int, string>> errorMessages = new(() =>
    {
        var valuePairs = typeof(ErrorCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
                                   .Select(x => new KeyValuePair<int, string>(
                                       (x.GetValue(null) as int?) ?? 0, x.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty));
        return new Dictionary<int, string>(valuePairs);
    });

    public static IReadOnlyDictionary<int, string> ErrorMessages => errorMessages.Value;

    public static void AddErrorCode(this HttpContext httpContext, int code, string? errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
        {
            if (ErrorMessages.TryGetValue(code, out var value))
            {
                errorMessage = value;
            }
        }
        var item = new ErrorCode { Code = code, Error = errorMessage };
        if (httpContext.Items.TryGetValue("errorcode", out var list))
        {
            if (list is List<ErrorCode> codes)
            {
                codes.Add(item);
            }
            else
            {
                httpContext.Items.TryAdd("errorcode", new List<ErrorCode> { item });
            }
        }
        else
        {
            httpContext.Items.TryAdd("errorcode", new List<ErrorCode> { item });
        }
    }

    public static void AddErrorCode(this HttpContext httpContext, int code) => AddErrorCode(httpContext, code, null);

    public static ErrorCode? GetLastErrorCode(this HttpContext httpContext)
    {
        if (httpContext.Items.TryGetValue("errorcode", out var value) && value is List<ErrorCode> codes and { Count: > 0 })
        {
            return codes[^1];
        }
        return null;
    }

    public static void ClearErrorCode(this HttpContext httpContext)
    {
        if (httpContext.Items.TryGetValue("errorcode", out var list))
        {
            if (list is List<ErrorCode> codes)
            {
                codes.Clear();
            }
        }
    }
}
