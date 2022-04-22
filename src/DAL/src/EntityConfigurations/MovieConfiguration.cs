using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieAPI.DAL;

internal class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasIndex(p => p.DetailRelativeURL).IsUnique();

        builder.HasOne(x => x.MovieFile)
               .WithMany(x => x.Movies)
               .HasForeignKey(x => x.MovieFileId)
               .IsRequired(false);
    }
}
