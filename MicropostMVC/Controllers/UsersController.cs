using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Common;
using MicropostMVC.Models;

namespace MicropostMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserBS _userBS;
        private readonly IMicropostBS _micropostBS;

        public UsersController(IUserBS userBS, IMicropostBS micropostBS)
        {
            _userBS = userBS;
            _micropostBS = micropostBS;
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
                ViewBag.FlashMessage = "Sign up failure!";
                ViewBag.FlashClass = "error";
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

            //IEnumerable<UserModel> users = _userBS.GetUsers(filter, 0, int.MaxValue);
            //if (search != "*")
            //{
            //    users = users.Where(u => u.Name.ToLower().StartsWith(search));
            //}
            
            IEnumerable<UserModel> users = _userBS.GetUsersByName(search, 0, 300);
            return PartialView("Users", users);
        }

        public ActionResult Show(string id, bool isNewUser = false)
        {
            ViewBag.FlashMessage = (isNewUser) ? "Welcome to the Sample App!" : string.Empty;
            ViewBag.FlashClass = "success";
            UserModel user = _userBS.GetUser(new BoRef(id));
            MicropostsForUserModel micropostsForUser = _micropostBS.GetMicropostsForUser(user);
            return View(micropostsForUser);
        }

        public ActionResult Follow(string id)
        {
            var loggedId = new BoRef(User.Identity.Name);
            
            var userId = new BoRef(id);
            UserModel user = _userBS.GetUser(userId);
            var micropostsForUser = new MicropostsForUserModel() { User = user };

            bool followed = user.Followers.Any(r => r.Value == loggedId.Value);
            bool result = (followed)
                              ? _userBS.Unfollow(loggedId, user)
                              : _userBS.Follow(loggedId, user);
            if (result)
            {
                user = _userBS.GetUser(userId);
                micropostsForUser = _micropostBS.GetMicropostsForUser(user);
            } 
            else
            {
                ViewBag.FlashMessage = "Follow/Unfollow failure!";
                ViewBag.FlashClass = "error";
            }
            return View("Show", micropostsForUser);
        }

        public ActionResult Following()
        {
            ViewBag.Title = "Following";
            UserModel user = _userBS.GetUser(new BoRef(User.Identity.Name));
            IEnumerable<UserModel> following = _userBS.GetUsers(user.Following);
            return View("Following", following);
        }

        public ActionResult Followers()
        {
            ViewBag.Title = "Followers";
            UserModel user = _userBS.GetUser(new BoRef(User.Identity.Name));
            IEnumerable<UserModel> followers = _userBS.GetUsers(user.Followers);
            return View("Following", followers);
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
