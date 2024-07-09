using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebCore.Filters
{
    public class TokenFilterAttribute : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var tokenStr = (string)filterContext.HttpContext.Session.GetString("LoginSessionName");
            if (tokenStr == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Areas="Admin", controller = "Account", action = "Login" }));
                return;
            }
            base.OnActionExecuting(filterContext);
        }

     

    }
}
