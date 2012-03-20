using System.Web.Mvc;
using System.Web.Security;
using MicropostMVC.BusinessServices;
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
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult SignIn(UserModel user)
        {
            UserModel userLogged = _userBS.Login(user);
            if (userLogged.Id.IsEmpty())
            {
                ModelState.AddModelError(string.Empty, "Email/Password combination is invalid!");
                return View(user);
            }
            
            _userBS.Authenticate(userLogged);
            return RedirectToAction("Show", "Users", userLogged);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Pages");
        }

    }
}
