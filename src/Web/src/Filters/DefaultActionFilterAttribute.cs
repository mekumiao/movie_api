using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieAPI.Web.Filters;

/// <summary>
/// API方法拦截器
/// </summary>
public class DefaultActionFilterAttribute : ActionFilterAttribute
{
    /// <summary>
    /// 方法执行前拦截
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context) { }
    /// <summary>
    /// 方法执行后拦截
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuted(ActionExecutedContext context) { }
    /// <summary>
    /// 实现异步接口
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        OnActionExecuting(context);
        OnActionExecuted(await next.Invoke());
    }
}
