using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IHostConfigAddMapper
{
    Expression<Func<HostConfigAdd, HostConfig>> Projection { get; }
    HostConfig Map(HostConfigAdd source);
    HostConfig Map(HostConfigAdd source, HostConfig target);
}
