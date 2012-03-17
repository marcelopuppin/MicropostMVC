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
            UserModel newUser = _userBS.Save(user);
            if (newUser.Id.IsEmpty()) 
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
