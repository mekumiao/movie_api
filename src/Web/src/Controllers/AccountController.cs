using System.Security.Claims;
using EntityFramework.Exceptions.Common;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Common;
using MovieAPI.DAL;
using MovieAPI.Model;
using MovieAPI.Model.Mappers;
using MovieAPI.Repository;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Controllers;

/// <summary>
/// 账号
/// </summary>
public class AccountController : MyBaseController
{
    private readonly IUserMapper _mapper;
    private readonly AccountRepository accountRepository;
    private readonly MovieDbContext _dbContext;

    public AccountController(
        AccountRepository accountRepository,
        MovieDbContext dbContext,
        IUserMapper mapper)
    {
        this.accountRepository = accountRepository;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    private async Task<long?> ValidateLoginAsync(LoginAdd input)
    {
        try
        {
            return await accountRepository.ValidateLoginAsync(input);
        }
        catch (UsernameNotExistsException)
        {
            HttpContext.AddErrorCode(ErrorCodes.UsernameNoExists);
            return null;
        }
        catch (PasswordErrorException)
        {
            HttpContext.AddErrorCode(ErrorCodes.PasswordError);
            return null;
        }
    }

    [NonAction]
    public static ClaimsPrincipal CreateClaimsPrincipal(string scheme, UserDto userDto)
    {
        var identity = new ClaimsIdentity(scheme, JwtClaimTypes.Name, JwtClaimTypes.Role);

        foreach (var roleName in userDto.RoleNames)
        {
            identity.AddClaim(new Claim(JwtClaimTypes.Role, roleName));
        }

        identity.AddClaim(new Claim(JwtClaimTypes.Subject, userDto.Id.ToString()));
        identity.AddClaim(new Claim(JwtClaimTypes.Name, userDto.FullName));
        identity.AddClaim(new Claim(JwtClaimTypes.NickName, userDto.NickName));
        identity.AddClaim(new Claim(JwtClaimTypes.Gender, userDto.Gender.ToString()));
        identity.AddClaim(new Claim(JwtClaimTypes.Email, userDto.Email));

        return new ClaimsPrincipal(identity);
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(OutputResult<UserDto>), StatusCodes.Status200OK)]
    public async Task<UserDto?> LoginAsync([FromBody] LoginAdd input)
    {
        var userId = await ValidateLoginAsync(input);

        if (userId is null)
        {
            return null;
        }

        var user = await accountRepository.GetUserDtoAsync(userId.Value);

        if (user is null)
        {
            HttpContext.AddErrorCode(ErrorCodes.UsernameNoExists);
            return null;
        }

        var principal = CreateClaimsPrincipal(CookieAuthenticationDefaults.AuthenticationScheme, user);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return user;
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(OutputResult<bool>), StatusCodes.Status200OK)]
    public async Task<bool> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return true;
    }

    [HttpPut]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> UpdateAsync(
        [FromServices] IUser user,
        [FromBody] AccountUpdate input)
    {
        var us = await _dbContext.Users.Include(x => x.UserSecret)
                                       .SingleOrDefaultAsync(x => x.Id == user.Id);
        if (us is null)
        {
            HttpContext.AddErrorCode(ErrorCodes.NotExists);
            return default;
        }

        if (us.UserSecret is null)
        {
            HttpContext.AddErrorCode(ErrorCodes.NotExists);
            return default;
        }

        var password = SHA256Helper.HashToHexString($"{us.UserSecret.Salt}{input.OldPassword}");
        if (us.UserSecret.Password != password)
        {
            HttpContext.AddErrorCode(ErrorCodes.PasswordError);
            return default;
        }

        var newPassword = SHA256Helper.HashToHexString($"{us.UserSecret.Salt}{input.NewPassword}");

        us.UserSecret.Password = newPassword;

        var result = await _dbContext.SaveChangesAsync();
        return result;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(OutputResult<long>), StatusCodes.Status200OK)]
    public async Task<long> AddAsync([FromBody] AccountAdd input)
    {
        var exists = await _dbContext.Users.AsTracking().AnyAsync(x => x.Name == input.Username);
        if (exists)
        {
            HttpContext.AddErrorCode(ErrorCodes.AlreadyExists, "用户名已存在");
            return default;
        }

        var secret = new UserSecret
        {
            Username = input.Username,
            Salt = Guid.NewGuid().ToString("N"),
        };

        secret.Password = SHA256Helper.HashToHexString($"{secret.Salt}{input.Password}");

        var roleUser = new RoleUser
        {
            RoleId = MyConst.Role.UserId
        };

        var user = new DAL.User
        {
            Name = input.Username,
            FullName = input.Username,
            NickName = input.Username,
            UserSecret = secret,
            RoleUsers = new List<RoleUser> { roleUser },
        };

        await _dbContext.Users.AddAsync(user);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (UniqueConstraintException)
        {
            HttpContext.AddErrorCode(ErrorCodes.AlreadyExists, "用户名已存在");
            return default;
        }

        return user.Id;
    }
}
