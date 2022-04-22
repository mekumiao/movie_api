using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieAPI.DAL.ValueGenerators;

namespace MovieAPI.DAL;

/// <summary>
/// 数据库上下文
/// </summary>
public class MovieDbContext : DbContext
{
#nullable disable
    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<UserMovie> UserMovies { get; set; }
    public DbSet<MovieFile> MovieFiles { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<UserSearchHistory> UserSearchHistories { get; set; }
    public DbSet<MovieType> MovieTypes { get; set; }
    public DbSet<HostConfig> HostConfigs { get; set; }
    public DbSet<ApkVersion> ApkVersions { get; set; }
    public DbSet<UploadFile> UploadFiles { get; set; }
    public DbSet<UserSecret> UserSecrets { get; set; }
#nullable enable

    private readonly IUser _user;

    public MovieDbContext(DbContextOptions options, IUser user) : base(options)
    {
        _user = user;
        ChangeTracker.StateChanged += OnChanged;
        ChangeTracker.Tracked += OnChanged;
    }

    private void OnChanged(object? sender, EntityEntryEventArgs e)
    {
        if (e.Entry.Entity is IOperationEntity entity)
        {
            if (e.Entry.State == EntityState.Added)
            {
                entity.CreateUserId = _user.Id;
                entity.CreateTime = DateTime.Now;
            }
            else if (e.Entry.State == EntityState.Modified)
            {
                entity.UpdateUserId = _user.Id;
                entity.UpdateTime = DateTime.Now;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(TableEntity).Assembly;
        var models = assembly.GetTypes()
                             .Where(x => !x.IsAbstract)
                             .Where(x => !x.IsInterface)
                             .Where(x => x.IsAssignableTo(typeof(IDbEntity)))
                             .ToList();

        var tables = models.Where(x => x.IsAssignableTo(typeof(ITable))).ToList();
        var views = models.Where(x => x.IsAssignableTo(typeof(IView))).ToList();

        tables.ForEach(m => modelBuilder.Entity(m));
        views.ForEach(m => modelBuilder.Entity(m).HasNoKey().ToView(m.Name));

        tables.Where(x => x.IsAssignableTo(typeof(IKeyEntity))).ToList().ForEach(m =>
        {
            //设置主键
            _ = modelBuilder.Entity(m)
                            .HasKey(nameof(IKeyEntity.Id));
            //生成主键值
            _ = modelBuilder.Entity(m)
                            .Property(nameof(IKeyEntity.Id))
                            .HasValueGenerator((prop, ent) => new SnowflakeIdValueGenerator())
                            .ValueGeneratedNever()
                            .IsRequired(true);
        });

        tables.Where(x => x.IsAssignableTo(typeof(IOperationEntity))).ToList().ForEach(m =>
        {
            //设置创建人单个导航属性
            _ = modelBuilder.Entity(m)
                            .HasOne(nameof(TableEntity.CreateUser))
                            .WithMany()
                            .HasForeignKey(nameof(IOperationEntity.CreateUserId))
                            .OnDelete(DeleteBehavior.Restrict)
                            .IsRequired(false);

            //设置修改人单个导航属性
            _ = modelBuilder.Entity(m)
                            .HasOne(nameof(TableEntity.UpdateUser))
                            .WithMany()
                            .HasForeignKey(nameof(IOperationEntity.UpdateUserId))
                            .OnDelete(DeleteBehavior.Restrict)
                            .IsRequired(false);
        });

        //设置软删除全局筛选器
        tables.Where(x => x.IsAssignableTo(typeof(IDeletedFlagEntity))).ToList().ForEach(m =>
        {
            var px = Expression.Parameter(m);
            var prop = Expression.Property(px, nameof(IDeletedFlagEntity.IsDeleted));
            var notprop = Expression.Not(prop);
            var lambda = Expression.Lambda(notprop, px);
            _ = modelBuilder.Entity(m).HasQueryFilter(lambda);
        });

        _ = modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}
