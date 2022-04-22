using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieAPI.Common;

namespace MovieAPI.DAL.EntityConfigurations;

internal class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
{
    public void Configure(EntityTypeBuilder<RoleUser> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.UserId });

        builder.HasOne(x => x.Role)
               .WithMany(x => x.RoleUsers)
               .HasForeignKey(x => x.RoleId)
               .IsRequired(true);

        builder.HasOne(x => x.User)
               .WithMany(x => x.RoleUsers)
               .HasForeignKey(x => x.UserId)
               .IsRequired(true);

        builder.HasData(new()
        {
            RoleId = MyConst.Role.SystemId,
            UserId = MyConst.User.SystemId,
        }, new()
        {
            RoleId = MyConst.Role.AdminId,
            UserId = MyConst.User.AdminId,
        }, new()
        {
            RoleId = MyConst.Role.AnonymousId,
            UserId = MyConst.User.AnonymousId,
        }, new()
        {
            RoleId = MyConst.Role.UserId,
            UserId = MyConst.User.MyUserId,
        });
    }
}
