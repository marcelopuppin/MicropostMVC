using System.Collections.Generic;
using System.Web.Mvc;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Common;
using MicropostMVC.Models;

namespace MicropostMVC.Controllers
{
    public class MicropostsController : Controller
    {
        private readonly IUserBS _userBS;
        private readonly IMicropostBS _micropostBS;

        public MicropostsController(IUserBS userBS, IMicropostBS micropostBS)
        {
            _userBS = userBS;
            _micropostBS = micropostBS;
        }

        public ActionResult Index()
        {
            UserModel loggedUser = GetUserModelForAuthenticatedUser();
            MicropostsForUserModel micropostsForUser = _micropostBS.GetMicropostsForUser(loggedUser);
            return View(micropostsForUser);
        }

        [HttpPost]
        public ActionResult Create()
        {
            string content = Request.Params["micropostNew"];
            
            bool isEmpty = string.IsNullOrEmpty(content);
            ViewBag.FlashMessage = (isEmpty) ? "Micropost cannot be blank!" : "Micropost created!";
            ViewBag.FlashClass = (isEmpty) ? "error" : "success";

            UserModel user = GetUserModelForAuthenticatedUser();
            var micropostsForUser = new MicropostsForUserModel { User = user };
            if (!isEmpty)
            {
                bool saved = _micropostBS.Save(user, content);        
                if (!saved)
                {
                    ViewBag.FlashMessage = "Micropost could not be created!";
                    ViewBag.FlashClass = "error";
                } 
                else
                {
                    user = _userBS.GetUser(user.Id);
                    micropostsForUser = _micropostBS.GetMicropostsForUser(user);
                } 
            }

            return View("Index", micropostsForUser);
        }

        [HttpPost]
        public JsonResult Remove(string userId, string micropostId)
        {
            UserModel user = _userBS.GetUser(new BoRef(userId));
            if (!user.Id.IsEmpty())
            {
                bool deleted = _micropostBS.Delete(user, new BoRef(micropostId));
                if (deleted)
                {
                    return Json("Micropost deleted!"); // View("Index", user); 
                }
            }
            return Json("Micropost could not be deleted!");
        }

        private UserModel GetUserModelForAuthenticatedUser()
        {
            string id = User.Identity.Name;
            return _userBS.GetUser(new BoRef(id));
        }
    }
}
