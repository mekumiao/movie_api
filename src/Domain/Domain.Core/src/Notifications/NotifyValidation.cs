namespace MovieAPI.Domain.Core;

/// <summary>
/// 领域通知类
/// </summary>
public class NotifyValidation : EventArgs
{
    public NotifyValidation(string value)
    {
        Value = value;
    }
    public string Value { get; private set; }
}
