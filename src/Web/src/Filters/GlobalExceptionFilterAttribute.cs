using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Web.Filters;

/// <summary>
/// API异常拦截器
/// </summary>
public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<GlobalExceptionFilterAttribute> _logger;

    public GlobalExceptionFilterAttribute(
        IWebHostEnvironment env,
        ILogger<GlobalExceptionFilterAttribute> logger)
    {
        _environment = env;
        _logger = logger;
    }
    /// <summary>
    /// 捕获全部异常
    /// </summary>
    /// <param name="context"></param>
    public override void OnException(ExceptionContext context)
    {
        context.Result = ExceptionHandle(context.Exception);
    }
    /// <summary>
    /// 异常处理
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    protected IActionResult ExceptionHandle(Exception exception)
    {
        Debug.Fail(exception.GetType().FullName, exception.Message);
        switch (exception)
        {
            case DbUpdateException ex:
                _logger.LogError(ex, "违法数据库约束");
                _ = _environment.IsDevelopment() ? throw exception : true;
                ;
                return new ObjectResult("违法数据库约束")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            case TaskCanceledException ex:
                _logger.LogError(ex, "系统繁忙");
                return new ObjectResult("系统繁忙")
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable
                };
            case AggregateException ex:
                if (ex.InnerExceptions.Any())
                {
                    return ExceptionHandle(ex.InnerExceptions[0]);
                }
                else
                {
                    _logger.LogError(exception, "未捕获到的异常");
                    return new ObjectResult("未捕获到的异常")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            default:
                _logger.LogError(exception, "服务器出错了");
                return new ObjectResult("服务器出错了")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
        }
    }
    /// <summary>
    /// 捕获全部异常(异步)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        OnException(context);
        return Task.CompletedTask;
    }
}
