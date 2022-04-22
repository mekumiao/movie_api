using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IHostConfigUpdateMapper
{
    Expression<Func<HostConfigUpdate, HostConfig>> Projection { get; }
    HostConfig Map(HostConfigUpdate source);
    HostConfig Map(HostConfigUpdate source, HostConfig target);
}
