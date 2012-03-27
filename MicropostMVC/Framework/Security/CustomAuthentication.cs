using System.Web.Mvc;
using System.Web.Security;

namespace MicropostMVC.Framework.Security
{
    public class CustomAuthentication : ActionFilterAttribute
    {
        public static string RedirectKey
        {
            get { return "RedirectUrl"; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool allowAnonymous = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                                  filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            bool authorize = filterContext.ActionDescriptor.IsDefined(typeof(AuthorizeAttribute), true) ||
                             filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AuthorizeAttribute), true); 
            
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated && (!allowAnonymous || authorize))
            {
                string loginUrl = FormsAuthentication.LoginUrl;
                if (filterContext.HttpContext.Request.Url != null && 
                    filterContext.HttpContext.Request.Url.AbsolutePath != loginUrl)
                {
                    string redirectOnSuccess = filterContext.HttpContext.Request.Url.AbsolutePath;
                    loginUrl += string.Format("?{0}={1}", RedirectKey, redirectOnSuccess);
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.RedirectLocation = string.Empty;
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Redirect(loginUrl, true);
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
 
    }
}