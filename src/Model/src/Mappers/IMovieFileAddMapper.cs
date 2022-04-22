using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IMovieFileAddMapper
{
    Expression<Func<MovieFileAdd, MovieFile>> Projection { get; }
    MovieFile Map(MovieFileAdd source);
    MovieFile Map(MovieFileAdd source, MovieFile target);
}
