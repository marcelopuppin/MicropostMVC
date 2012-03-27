using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Common;
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
                ViewBag.FlashMessage = "Email is already used by someone.";
                ViewBag.FlashClass = "alert";
                return View(user);
            }
            
            UserModel userSaved = _userBS.Save(user);
            if (userSaved.Id.IsEmpty()) 
            {
                ModelState.AddModelError(string.Empty, "Sign up failure!");
                return View(user);
            }

            _userBS.Authenticate(userSaved);
            return RedirectToAction("Show", "Users", new { id = userSaved.Id.Value, isNewUser = true });
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
            
            IEnumerable<UserModel> users = _userBS.GetUsers(0, int.MaxValue); //TODO: Users per page
            if (search != "*")
            {
                users = users.Where(u => u.Name.ToLower().Contains(search));
            }
            return PartialView("Users", users);
        }

        public ActionResult Show(string id, bool isNewUser = false)
        {
            ViewBag.FlashMessage = (isNewUser) ? "Welcome to the Sample App!" : string.Empty;
            ViewBag.FlashClass = "success";
            UserModel user = _userBS.GetUser(new BoRef(id));
            return View(user);
        }

        public static string GetAvatarPath(UserModel user)
        {
            int number = 0;
            if (!user.Id.IsEmpty())
            {
                int.TryParse(user.Id.Value.Last().ToString(CultureInfo.InvariantCulture), out number);
            }
            var avatarImage = (number > 0) ? string.Format("avatar{0}.png", number) : "noavatar.png";
            return "~/Content/images/avatars/" + avatarImage;
        }
    }
}
