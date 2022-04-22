using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieAPI.Common;

namespace MovieAPI.DAL.EntityConfigurations;

/// <summary>
/// 用户信息
/// </summary>
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(p => p.Name).IsUnique();

        //配置性别枚举转换
        builder.Property(e => e.Gender)
            .HasConversion(new EnumToNumberConverter<Gender, byte>());

        builder.HasData(new()
        {
            Id = MyConst.User.AnonymousId,
            Name = MyConst.User.Anonymous,
            FullName = MyConst.User.Anonymous,
            NickName = "匿名",
        }, new()
        {
            Id = MyConst.User.SystemId,
            Name = MyConst.User.System,
            FullName = MyConst.User.System,
            NickName = "系统",
        }, new()
        {
            Id = MyConst.User.AdminId,
            Name = MyConst.User.Admin,
            FullName = MyConst.User.Admin,
            NickName = "管理员",
        }, new()
        {
            Id = MyConst.User.MyUserId,
            Name = MyConst.User.MyUser,
            FullName = MyConst.User.MyUser,
            NickName = "用户",
        });
    }
}
