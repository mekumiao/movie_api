using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieAPI.DAL;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Filters;

/// <summary>
/// PaginatedTotal拦截器
/// </summary>
public class PaginatedTotalResultFilterAttribute : ResultFilterAttribute
{
    /// <summary>
    /// 生成结果前拦截
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult and { StatusCode: StatusCodes.Status200OK or null } result)
        {
            if (result.Value is IPaginatedTotal and { HasTotal: true } paginated)
            {
                result.Value = new PaginatedResult
                {
                    No = paginated.No,
                    Size = paginated.Size,
                    MaxNo = paginated.MaxNo,
                    Total = paginated.Total,
                    Items = (IList)paginated,
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
