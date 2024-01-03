using Microsoft.AspNetCore.Mvc;

namespace Genealogy.Web.Utilities;

public static class UrlHelperExtensions
{
    public static string? Action<T>(this IUrlHelper urlHelper, string actionName, object? values = null) where T : ControllerBase
    {
        string controllerName = typeof(T).Name;
        if(controllerName.EndsWith("Controller", StringComparison.Ordinal))
        {
            controllerName = controllerName[..^10];
        }        
        return urlHelper.Action(actionName, controllerName, values);
    }
}
