using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Helpers
{
    public class DatatablesPagedResults : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.Controller is Controller)) return;
            else
            {
                //string actionName = filterContext.RouteData.Values["action"].ToString();
                //string controllerName = filterContext.RouteData.Values["controller"].ToString();
            }
        }
    }
}

