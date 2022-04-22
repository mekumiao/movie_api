using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieAPI.DAL;

internal class MovieFileConfiguration : IEntityTypeConfiguration<MovieFile>
{
    public void Configure(EntityTypeBuilder<MovieFile> builder)
    {
    }
}
