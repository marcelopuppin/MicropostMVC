using System.Web.Mvc;
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

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserModel user)
        {
            if (_userBS.IsEmailUsedBySomeone(user))
            {
                ModelState.AddModelError(string.Empty, "Email is already used by someone!");
                return View(user);
            }
            
            UserModel newUser = _userBS.Save(user);
            if (newUser.Id.IsEmpty()) 
            {
                ModelState.AddModelError(string.Empty, "Sign up failure!");
                return View(user);
            }

            return View("Show", newUser);
        }

        public ActionResult Show(UserModel user)
        {
            return View(user);
        }
    }
}
