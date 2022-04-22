using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieAPI.Web;

/// <summary>
/// 禁用模型绑定
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class DisableModelBindingAttribute : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        var factories = context.ValueProviderFactories;
        factories.RemoveType<FormValueProviderFactory>();
        factories.RemoveType<FormFileValueProviderFactory>();
        factories.RemoveType<JQueryFormValueProviderFactory>();
        factories.RemoveType<RouteValueProviderFactory>();
        factories.RemoveType<QueryStringValueProviderFactory>();
    }

    public void OnResourceExecuted(ResourceExecutedContext context) { }
}

