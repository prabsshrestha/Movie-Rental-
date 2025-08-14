using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prabesh.Security
{
    public class AuthorizeExceptAttribute : AuthorizeAttribute
    {
        public string DeniedRoles { get; set; } 
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return to login page 
            if (!base.AuthorizeCore(httpContext))
                return false;

            var user = httpContext.User;
            if (!string.IsNullOrEmpty(DeniedRoles))
            {
                var roles = DeniedRoles.Split(',')
                                       .Select(r => r.Trim());
                if (roles.Any(user.IsInRole))
                {
                    return false;
                }
            }
            return true; 
        }

        // check what happens when unauthorized.
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/UnauthorizedPage.cshtml"
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
