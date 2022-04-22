using Microsoft.EntityFrameworkCore;
using MovieAPI.Common;
using MovieAPI.DAL;

namespace MovieAPI.Web.IntegrationTest;

public static class Utilities
{
    public static void InitializeDbForTests(MovieDbContext dbContext)
    {
        dbContext.Movies.AddRange(GetSeedingMovies());
        dbContext.UserMovies.AddRange(GetSeedingUserMovies());
        dbContext.MovieFiles.AddRange(GetSeedingMovieFiles());
        dbContext.UserSearchHistories.AddRange(GetSeedingUserSearchHistories());
        dbContext.Actors.AddRange(GetSeedingActors());
        dbContext.HostConfigs.AddRange(GetSeedingHostConfigs());
        dbContext.ApkVersions.AddRange(GetSeedingApkVersions());
        dbContext.SaveChanges();
    }

    public static void ReinitializeDbForTests(MovieDbContext dbContext)
    {
        dbContext.ApkVersions.RemoveRange(dbContext.ApkVersions.IgnoreQueryFilters());
        dbContext.HostConfigs.RemoveRange(dbContext.HostConfigs.IgnoreQueryFilters());
        dbContext.Actors.RemoveRange(dbContext.Actors.IgnoreQueryFilters());
        dbContext.UserSearchHistories.RemoveRange(dbContext.UserSearchHistories.IgnoreQueryFilters());
        dbContext.MovieFiles.RemoveRange(dbContext.MovieFiles.IgnoreQueryFilters());
        dbContext.UserMovies.RemoveRange(dbContext.UserMovies.IgnoreQueryFilters());
        dbContext.Movies.RemoveRange(dbContext.Movies.IgnoreQueryFilters());
        InitializeDbForTests(dbContext);
    }

    public static List<Movie> GetSeedingMovies()
    {
        return new()
        {
            new() { Id = 1, Name = "TEST RECORD: You're standing on my scarf." },
            new() { Id = 2, Name = "TEST RECORD: Would you like a jelly baby?" },
            new() { Id = 3, Name = "TEST RECORD: Test" },
        };
    }

    public static List<UserMovie> GetSeedingUserMovies()
    {
        return new()
        {
            new() { Id = 1, UserId = MyConst.User.AnonymousId, MovieId = 1 },
            new() { Id = 2, UserId = MyConst.User.AnonymousId, MovieId = 2 },
            new() { Id = 3, UserId = MyConst.User.AnonymousId, MovieId = 3 },
        };
    }

    public static List<MovieFile> GetSeedingMovieFiles()
    {
        return new()
        {
            new() { Id = 1, FileName = "TEST RECORD: You're standing on my scarf." },
            new() { Id = 2, FileName = "TEST RECORD: Would you like a jelly baby?" },
            new() { Id = 3, FileName = "TEST RECORD: Test" },
        };
    }

    public static List<UserSearchHistory> GetSeedingUserSearchHistories()
    {
        return new()
        {
            new() { Id = 1, Tag = "Movie", Value = "v1", UserId = MyConst.User.AdminId },
            new() { Id = 2, Tag = "58p", Value = "v2", UserId = MyConst.User.AdminId },
            new() { Id = 3, Tag = "MovieType", Value = "v3", UserId = MyConst.User.AdminId },
            new() { Id = 4, Tag = "Movie", Value = "v1", UserId = MyConst.User.AdminId },
            new() { Id = 5, Tag = "Movie", Value = "v1", UserId = MyConst.User.AdminId },
            new() { Id = 6, Tag = "Movie", Value = "v1", UserId = MyConst.User.AdminId },
            new() { Id = 7, Tag = "Movie", Value = "v1", UserId = MyConst.User.AdminId },
        };
    }

    public static List<Actor> GetSeedingActors()
    {
        return new()
        {
            new() { Id = 1, Name = "1", Remark = "xx" },
            new() { Id = 2, Name = "2", Remark = "xx" },
            new() { Id = 3, Name = "3", Remark = "xx" },
            new() { Id = 4, Name = "4", Remark = "xx" },
            new() { Id = 5, Name = "5", Remark = "xx" },
            new() { Id = 6, Name = "6", Remark = "xx" },
            new() { Id = 7, Name = "7", Remark = "xx" },
        };
    }

    public static List<HostConfig> GetSeedingHostConfigs()
    {
        return new()
        {
            new() { Id = 1, Name = "1", Remark = "xx" },
            new() { Id = 2, Name = "2", Remark = "xx" },
            new() { Id = 3, Name = "3", Remark = "xx" },
            new() { Id = 4, Name = "4", Remark = "xx" },
            new() { Id = 5, Name = "5", Remark = "xx" },
            new() { Id = 6, Name = "6", Remark = "xx" },
            new() { Id = 7, Name = "7", Remark = "xx" },
        };
    }

    public static List<ApkVersion> GetSeedingApkVersions()
    {
        return new()
        {
            new() { Id = 1, Name = "1", Remark = "xx", FileDiskURL = "test.apk" },
            new() { Id = 2, Name = "2", Remark = "xx", FileDiskURL = "test.apk" },
            new() { Id = 3, Name = "3", Remark = "xx", FileDiskURL = "test.apk" },
            new() { Id = 4, Name = "4", Remark = "xx", FileDiskURL = "test.apk" ,IsActived = true },
            new() { Id = 5, Name = "5", Remark = "xx", FileDiskURL = "test.apk" },
            new() { Id = 6, Name = "6", Remark = "xx", FileDiskURL = "test.apk" },
            new() { Id = 7, Name = "7", Remark = "xx", FileDiskURL = "test.apk" },
        };
    }
}
