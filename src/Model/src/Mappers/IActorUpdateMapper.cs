using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IActorUpdateMapper
{
    Expression<Func<ActorUpdate, Actor>> Projection { get; }
    Actor Map(ActorUpdate source);
    Actor Map(ActorUpdate source, Actor target);
}
