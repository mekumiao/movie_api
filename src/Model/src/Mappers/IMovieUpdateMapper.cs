using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IMovieUpdateMapper
{
    Expression<Func<MovieUpdate, Movie>> Projection { get; }
    Movie Map(MovieUpdate source);
    Movie Map(MovieUpdate source, Movie target);
}
