using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieAPI.DAL;

internal class UserMovieConfiguration : IEntityTypeConfiguration<UserMovie>
{
    public void Configure(EntityTypeBuilder<UserMovie> builder)
    {
        builder.HasOne(x => x.User)
               .WithMany(x => x.UserMovies)
               .HasForeignKey(x => x.UserId)
               .IsRequired(false);

        builder.HasOne(x => x.Movie)
               .WithMany(x => x.UserMovies)
               .HasForeignKey(x => x.MovieId)
               .IsRequired(false);
    }
}
