using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieAPI.Common;

namespace MovieAPI.DAL.EntityConfigurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(x => x.Users)
               .WithMany(x => x.Roles)
               .UsingEntity<RoleUser>();

        builder.HasData(new()
        {
            Id = MyConst.Role.SystemId,
            Name = MyConst.Role.System,
            DisplayName = "系统",
            IsAdmin = true,
        }, new()
        {
            Id = MyConst.Role.AdminId,
            Name = MyConst.Role.Admin,
            DisplayName = "管理员",
            IsAdmin = true,
        }, new()
        {
            Id = MyConst.Role.UserId,
            Name = MyConst.Role.User,
            DisplayName = "用户",
            IsAdmin = false,
        }, new()
        {
            Id = MyConst.Role.AnonymousId,
            Name = MyConst.Role.Anonymous,
            DisplayName = "匿名",
            IsAdmin = false,
        });
    }
}
