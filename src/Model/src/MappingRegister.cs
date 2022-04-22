using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model;

public record MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.Default.MaxDepth(1)
                      .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                      .IgnoreNullValues(true);

        config.NewConfig<MovieType, MovieTypeDto>()
              .Map(dest => dest.Count, src => src.Movies!.Count());

        config.NewConfig<MovieWithUserMovie, MovieDto>()
              .MaxDepth(2)
              .Map(dest => dest.IsStar, src => src.UserMovie!.IsStar, should => should.UserMovie != null)
              .Map(dest => dest.IsDislike, src => src.UserMovie!.IsDislike, should => should.UserMovie != null)
              .Map(dest => dest.MovieFileURL, src => src.Movie.MovieFile!.DiskURL, should => should.Movie.MovieFile != null)
              .Map(dest => dest, src => src.Movie);

        config.NewConfig<Movie, MovieDto>()
              .MaxDepth(2)
              .Map(dest => dest.MovieFileURL, src => src.MovieFile!.DiskURL, should => should.MovieFile != null);

        config.NewConfig<MovieFile, MovieFileDto>();

        config.NewConfig<MovieUpdate, Movie>();

        config.NewConfig<ActorAdd, Actor>();

        config.NewConfig<HostConfigAdd, HostConfig>();

        config.NewConfig<HostConfigUpdate, HostConfig>();

        config.NewConfig<ApkVersionUpdate, ApkVersion>();

        config.NewConfig<User, UserDto>()
              .Map(dest => dest.RoleNames, src => src.Roles!.Select(x => x.Name), should => should.Roles != null);
    }
}
