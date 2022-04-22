using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieAPI.Common;

namespace MovieAPI.DAL.EntityConfigurations;

internal class UserSecretConfiguration : IEntityTypeConfiguration<UserSecret>
{
    public void Configure(EntityTypeBuilder<UserSecret> builder)
    {
        builder.HasIndex(x => x.Username).IsUnique();

        builder.HasData(new()
        {
            Id = 101,
            UserId = MyConst.User.AdminId,
            Username = MyConst.User.Admin,
        }, new()
        {
            Id = 102,
            UserId = MyConst.User.MyUserId,
            Username = MyConst.User.MyUser,
        });
    }
}
