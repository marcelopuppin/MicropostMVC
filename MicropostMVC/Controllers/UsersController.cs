using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
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
            
            UserModel userSaved = _userBS.Save(user, true);
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

        public ActionResult Edit()
        {
            var loggedId = new BoRef(User.Identity.Name);
            UserModel user = _userBS.GetUser(loggedId);
            return View("Settings", user);
        }

        [HttpPost]
        public ActionResult Update(UserModel user, HttpPostedFileBase avatarFile)
        {
            UserModel loggedUser = _userBS.GetUser(user.Id);
            if (loggedUser.Email != user.Email && 
                _userBS.IsEmailUsedBySomeone(user))
            {
                ViewBag.FlashMessage = "Email is already used by someone.";
                ViewBag.FlashClass = "alert";
                return View("Settings", user);
            }

            if (TryUpdateModel(loggedUser))
            {
                bool avatarChanged = (avatarFile != null && avatarFile.ContentLength > 0);
                if (avatarChanged)
                {
                    loggedUser.Avatar = FormatAvatarName(loggedUser, avatarFile);
                    string path = Server.MapPath(GetAvatarPath(loggedUser));
                    avatarFile.SaveAs(path);
                }
                loggedUser = _userBS.Save(loggedUser, true);
            }
            return RedirectToAction("Show", "Users", new { id = loggedUser.Id.Value });
        }

        public ActionResult Follow(string id)
        {
            var loggedId = new BoRef(User.Identity.Name);
            
            var userId = new BoRef(id);
            UserModel user = _userBS.GetUser(userId);
            var micropostsForUser = new MicropostsForUserModel { User = user };

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

        #region [ Avatar ]
        public static string GetAvatarPath(UserModel user)
        {
            return "~/Content/images/avatars/" + GetAvatarName(user);
        }

        private static string GetAvatarName(UserModel user)
        {
            return (string.IsNullOrEmpty(user.Avatar) || user.Id.IsEmpty())
                    ? "noavatar.png"
                    : user.Avatar;
        }

        private string FormatAvatarName(UserModel user, HttpPostedFileBase file)
        {
            return string.Format("avatar_{0}.{1}", user.Id.Value, file.ContentType.Split('/')[1]);
        }
        #endregion
    }
}
