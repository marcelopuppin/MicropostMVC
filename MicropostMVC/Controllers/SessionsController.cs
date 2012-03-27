using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Security;
using MicropostMVC.Models;

namespace MicropostMVC.Controllers
{
    public class SessionsController : Controller
    {
        private readonly IUserBS _userBS;

        public SessionsController(IUserBS userBS)
        {
            _userBS = userBS;
        }

        [HttpGet, AllowAnonymous]
        public ActionResult SignIn()
        {
            string returnUrl = Request.Params[CustomAuthentication.RedirectKey];
            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.FlashMessage = "Please sign in to access this page.";
                ViewBag.FlashClass = "notice";
                TempData.Add(CustomAuthentication.RedirectKey, returnUrl);
            }
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult SignIn(UserModel user)
        {
            UserModel userLogged = _userBS.Login(user);
            if (userLogged.Id.IsEmpty())
            {
                ViewBag.FlashMessage = "Invalid email/password combination.";
                ViewBag.FlashClass = "error";
                return View(user);
            }
            
            _userBS.Authenticate(userLogged);

            if (TempData.ContainsKey(CustomAuthentication.RedirectKey))
            {
                return Redirect(TempData[CustomAuthentication.RedirectKey].ToString());
            }
            return RedirectToAction("Show", "Users", new { id = userLogged.Id.Value });
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Pages");
        }

    }
}
