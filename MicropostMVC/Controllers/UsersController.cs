using System.Web.Mvc;
using System.Web.Security;
using MicropostMVC.BusinessServices;
using MicropostMVC.Models;

namespace MicropostMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserBS _userBS;

        public UsersController(IUserBS userBS)
        {
            _userBS = userBS;
        }

        
        [HttpGet, AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult SignUp(UserModel user)
        {
            if (_userBS.IsEmailUsedBySomeone(user))
            {
                ModelState.AddModelError(string.Empty, "Email is already used by someone!");
                return View(user);
            }
            
            UserModel userSaved = _userBS.Save(user);
            if (userSaved.Id.IsEmpty()) 
            {
                ModelState.AddModelError(string.Empty, "Sign up failure!");
                return View(user);
            }

            _userBS.Authenticate(userSaved);
            return RedirectToAction("Show", "Users", userSaved);
        }

        public ActionResult Show(UserModel user)
        {
            return View(user);
        }
    }
}
