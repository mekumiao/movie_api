using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieAPI.DAL;

internal class UserSearchHistoryConfiguration : IEntityTypeConfiguration<UserSearchHistory>
{
    public void Configure(EntityTypeBuilder<UserSearchHistory> builder)
    {
        builder.HasOne(x => x.User)
               .WithMany(x => x.UserSearchHistories)
               .HasForeignKey(x => x.UserId);
    }
}
