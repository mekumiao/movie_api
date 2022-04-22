using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IMovieAddMapper
{
    Expression<Func<MovieAdd, Movie>> Projection { get; }
    Movie Map(MovieAdd source);
    Movie Map(MovieAdd source, Movie target);
}
