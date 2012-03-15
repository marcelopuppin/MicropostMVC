using System.Web.Mvc;
using MicropostMVC.Models;

namespace MicropostMVC.Controllers
{
    public class UsersController : Controller
    {
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
            UserModel newUser = user.Save();
            if (string.IsNullOrEmpty(newUser.Id)) 
            {
                ViewBag.Error = "Sign up failure!";
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
