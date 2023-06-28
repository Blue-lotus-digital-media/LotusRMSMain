using Microsoft.AspNetCore.Mvc.Rendering;

namespace LotusRMSweb.Helper
{
    public static class MvcExtensions
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper,string areas=null, string controllers = null, string actions = null, string cssClass = "active-route")
        {
            var currentArea = htmlHelper?.ViewContext.RouteData.Values["area"] as string;
            var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
            var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;

            var acceptedAreas = (areas ?? currentArea ?? "").Split(',');
            var acceptedControllers = (controllers ?? currentController ?? "").Split(',');
            var acceptedActions = (actions ?? currentAction ?? "").Split(',');

            return acceptedAreas.Contains(currentArea) && acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
                ? cssClass
                : "";
        }
    }
}
