using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using MovieAPI.Common;

namespace MovieAPI.DAL.ValueGenerators;

/// <summary>
/// 雪花算法ID生成
/// </summary>
internal class SnowflakeIdValueGenerator : ValueGenerator<long>
{
    private readonly static Snowflake _snowflake = new(5, 5);
    public override bool GeneratesTemporaryValues => false;

    public SnowflakeIdValueGenerator() { }

    public override long Next([NotNull] EntityEntry entry!!) => _snowflake.NextId();
}
