using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Filters;

/// <summary>
/// OutputResult拦截器
/// </summary>
public class OutputResultFilterAttribute : ResultFilterAttribute
{
    /// <summary>
    /// 生成结果前拦截
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult and { StatusCode: StatusCodes.Status200OK or null } result)
        {
            var errorCode = context.HttpContext.GetLastErrorCode();
            if (errorCode is not null)
            {
                result.Value = new OutputResult
                {
                    Code = errorCode.Code,
                    Msg = errorCode.Error,
                    Result = result.Value,
                };
            }
            else
            {
                result.Value = new OutputResult
                {
                    Code = 0,
                    Msg = "successful",
                    Result = result.Value,
                };
            }
        }
    }
    /// <summary>
    /// 生成结果后拦截
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuted(ResultExecutedContext context) { }
    /// <summary>
    /// 实现异步接口
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        OnResultExecuting(context);
        OnResultExecuted(await next.Invoke());
    }
}
