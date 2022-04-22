using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IMovieMapper
{
    Expression<Func<Movie, MovieDto>> Projection { get; }
    MovieDto Map(Movie source);
    MovieDto Map(Movie source, MovieDto target);
}
