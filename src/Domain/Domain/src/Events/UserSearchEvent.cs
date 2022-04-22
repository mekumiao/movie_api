namespace MovieAPI.Domain.Events;

public class UserSearchEvent : EventArgs
{
    /// <summary>
    /// 搜索标签
    /// </summary>
    public string Tag { get; set; }
    public string Value { get; set; }

    public UserSearchEvent(string tag, string value)
    {
        Tag = tag;
        Value = value;
    }
}
