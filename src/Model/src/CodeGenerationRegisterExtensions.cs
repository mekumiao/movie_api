using System.Runtime.Serialization;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public static class CodeGenerationRegisterExtensions
{
    public static AdaptAttributeBuilder IgnoreDataMemberAttribute(this AdaptAttributeBuilder builder)
    {
        return builder.IgnoreAttributes(typeof(IgnoreDataMemberAttribute));
    }

    public static PropertySettingBuilder<T> IgnoreOperator<T>(this PropertySettingBuilder<T> propertySetting) where T : TableEntity
    {
        return propertySetting.Ignore(x => x.CreateUser)
                              .Ignore(x => x.CreateUserId)
                              .Ignore(x => x.UpdateUser)
                              .Ignore(x => x.UpdateUserId);
    }

    public static PropertySettingBuilder<T> IgnoreOperatingTime<T>(this PropertySettingBuilder<T> propertySetting) where T : TableEntity
    {
        return propertySetting.Ignore(x => x.CreateTime)
                              .Ignore(x => x.UpdateTime);
    }
}
