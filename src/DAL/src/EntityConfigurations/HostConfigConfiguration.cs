using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieAPI.DAL.EntityConfigurations;

internal class HostConfigConfiguration : IEntityTypeConfiguration<HostConfig>
{
    public void Configure(EntityTypeBuilder<HostConfig> builder)
    {
        builder.HasData(new()
        {
            Id = 101,
            Name = "http://localhost:8100",
            Host = "http://localhost:8100",
            Remark = "http://localhost:8100",
        }, new()
        {
            Id = 102,
            Name = "http://192.168.0.101:8100",
            Host = "http://192.168.0.101:8100",
            Remark = "http://192.168.0.101:8100",
        }, new()
        {
            Id = 103,
            Name = "http://192.168.43.189:8100",
            Host = "http://192.168.43.189:8100",
            Remark = "http://192.168.43.189:8100",
        });
    }
}
