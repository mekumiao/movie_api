using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieAPI.Common;
using MovieAPI.DAL;
using MovieAPI.Model;
using MovieAPI.Model.Mappers;

namespace MovieAPI.Repository;

public class UsernameNotExistsException : RepositoryException
{
    public UsernameNotExistsException() : base("用户名不存在")
    {
    }
}
public class PasswordErrorException : RepositoryException
{
    public PasswordErrorException() : base("密码错误")
    {
    }
}

public class AccountRepository : IRepository
{
    private readonly IServiceScope serviceScope;
    private readonly MovieDbContext dbContext;
    private readonly IUserMapper userMapper;
    private bool disposedValue;

    public AccountRepository(
        IServiceScopeFactory serviceScopeFactory,
        IUserMapper userMapper)
    {
        this.serviceScope = serviceScopeFactory.CreateScope();
        this.dbContext = this.serviceScope.ServiceProvider.GetRequiredService<MovieDbContext>();
        this.userMapper = userMapper;
    }

    /// <summary>
    /// 登录验证并返回用户ID
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="UsernameNotExistsException"></exception>
    /// <exception cref="PasswordErrorException"></exception>
    /// <returns></returns>
    public async Task<long> ValidateLoginAsync(LoginAdd input)
    {
        var secret = await dbContext.UserSecrets.AsNoTracking()
                                                .SingleOrDefaultAsync(x => x.Username == input.Username);
        if (secret is null)
        {
            throw new UsernameNotExistsException();
        }

        var password = SHA256Helper.HashToHexString($"{secret.Salt}{input.Password}");
        if (secret.Password != password)
        {
            throw new PasswordErrorException();
        }

        return secret.UserId;
    }

    public async Task<UserDto?> GetUserDtoAsync(long id)
    {
        return await dbContext.Users.AsNoTracking()
                                    .Select(userMapper.Projection)
                                    .SingleOrDefaultAsync(x => x.Id == id);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                this.dbContext.Dispose();
                this.serviceScope.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
