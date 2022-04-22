using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IActorAddMapper
{
    Expression<Func<ActorAdd, Actor>> Projection { get; }
    Actor Map(ActorAdd source);
    Actor Map(ActorAdd source, Actor target);
}
