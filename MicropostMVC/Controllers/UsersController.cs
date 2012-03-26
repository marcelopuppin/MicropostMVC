using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index()
        {
            if (!Request.IsAjaxRequest())
            {
                return View();
            }
            
            string search = Request.Params["search"].Trim().ToLower();
            if (search == "")
            {
                return PartialView("Users");
            }
            
            IEnumerable<UserModel> users = _userBS.GetUsers();
            if (search != "*")
            {
                users = users.Where(u => u.Name.ToLower().Contains(search));
            }
            return PartialView("Users", users);
        }

        public ActionResult Show(UserModel user)
        {
            return View(user);
        }

        public static string GetAvatarPath(UserModel user)
        {
            var avatarImage = (user.Id != null) ? string.Format("{0}.png", user.Id.Value) : "noavatar.png";
            return "~/Content/images/avatars/" + avatarImage;
        }
    }
}
