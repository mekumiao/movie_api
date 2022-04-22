using System.Net.Mime;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Controllers;

/// <summary>
/// 控制器基类
/// </summary>
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[Authorize]
public abstract class MyBaseController : ControllerBase
{
    private string? _schemeHost;

    protected string SchemeHost
    {
        get
        {
            if (_schemeHost is null)
            {
                _schemeHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}";
            }
            return _schemeHost;
        }
    }

    protected void SchemeHostAdaptTo<T>(T? dest) where T : class
    {
        if (dest is not null)
        {
            var src = new SchemeHostValue { Value = SchemeHost };
            src.Adapt(dest);
        }
    }

    protected void SchemeHostAdaptToRange<T>(IEnumerable<T>? dests)
    {
        if (dests is not null)
        {
            var src = new SchemeHostValue { Value = SchemeHost };
            foreach (var item in dests)
            {
                src.Adapt(item);
            }
        }
    }
}
