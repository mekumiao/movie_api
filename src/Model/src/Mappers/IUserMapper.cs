using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IUserMapper
{
    Expression<Func<User, UserDto>> Projection { get; }
    UserDto Map(User source);
    UserDto Map(User source, UserDto target);
}
