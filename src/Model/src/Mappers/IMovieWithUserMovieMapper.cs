using System.Linq.Expressions;
using Mapster;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IMovieWithUserMovieMapper
{
    Expression<Func<MovieWithUserMovie, MovieDto>> Projection { get; }
    MovieDto Map(MovieWithUserMovie source);
    MovieDto Map(MovieWithUserMovie source, MovieDto target);
}
