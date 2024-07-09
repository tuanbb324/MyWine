using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Data;

namespace WebCore.Filters
{
    public class TokenFilterAttribute : ActionFilterAttribute
    {
        //public Role[] AllowedRoles { get; }

        //public Role AccessLevel { get; private set; }

        //public TokenFilterAttribute(Role accessLevel)
        //{
        //    //AccessLevel = accessLevel;
        //    //AllowedRoles = new Role[] { accessLevel };
        //}

        //public TokenFilterAttribute(params Role[] roles)
        //{
        //    AllowedRoles = roles;
        //}


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var tokenStr = (string)filterContext.HttpContext.Session.GetString("LoginSessionName");
            //var token = string.IsNullOrEmpty(tokenStr) ? null : LoginSessionName;
            if (tokenStr == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Areas="Admin", controller = "Account", action = "Login" }));
                return;
            }

            //if (!IsAllowAccess(token.AccessLevel))
            //{
            //    var fullActionRoles = filterContext?.ActionDescriptor?.EndpointMetadata?.OfType<TokenFilterAttribute>().SelectMany(x => x.AllowedRoles)?.ToList();
            //    if (!fullActionRoles.Contains(token.AccessLevel))
            //    {
            //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            //    }
            //}

            //if (filterContext.Controller is BaseController currentController)
            //{
            //    currentController.CurrentAccessType = token.AccessLevel;
            //    currentController.CurrentUserId = token.UserId;
            //    currentController.CurrentUsername = token.Username;
            //}

            base.OnActionExecuting(filterContext);
        }

        //private bool IsAllowAccess(Role type)
        //{
        //    if (type == Role.SuperAdmin)
        //    {
        //        return true;
        //    }

        //    return AllowedRoles == null || AllowedRoles.Any(allowedRole => allowedRole == type);
        //}

    }
}
