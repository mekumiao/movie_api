using Microsoft.EntityFrameworkCore;
using EFCoreExceptions = EntityFramework.Exceptions;

namespace MovieAPI.DAL;

/// <summary>
/// EFCore异常扩展
/// </summary>
public static class EFCoreExceptionsExtension
{
    public static DbContextOptionsBuilder UseExceptionProcessorMySql(this DbContextOptionsBuilder optionsBuilder)
    {
        return EFCoreExceptions.MySQL.Pomelo.ExceptionProcessorExtensions.UseExceptionProcessor(optionsBuilder);
    }

    public static DbContextOptionsBuilder UseExceptionProcessorSqlite(this DbContextOptionsBuilder optionsBuilder)
    {
        return EFCoreExceptions.Sqlite.ExceptionProcessorExtensions.UseExceptionProcessor(optionsBuilder);
    }

    public static DbContextOptionsBuilder UseExceptionProcessorSqlServer(this DbContextOptionsBuilder optionsBuilder)
    {
        return EFCoreExceptions.SqlServer.ExceptionProcessorExtensions.UseExceptionProcessor(optionsBuilder);
    }
}
