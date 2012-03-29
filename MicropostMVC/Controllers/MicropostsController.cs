using System;
using System.Web.Mvc;
using MicropostMVC.BusinessServices;
using MicropostMVC.Framework.Common;
using MicropostMVC.Models;

namespace MicropostMVC.Controllers
{
    public class MicropostsController : Controller
    {
        private readonly IUserBS _userBS;

        public MicropostsController(IUserBS userBS)
        {
            _userBS = userBS;
        }
        
        public ActionResult Index()
        {
            return View(GetUserModelForAuthenticatedUser());
        }

        [HttpPost]
        public ActionResult Create()
        {
            string newMicropost = Request.Params["micropostNew"];
            
            bool isEmpty = string.IsNullOrEmpty(newMicropost);
            ViewBag.FlashMessage = (isEmpty) ? "Micropost cannot be blank!" : "Micropost created!";
            ViewBag.FlashClass = (isEmpty) ? "error" : "success";

            UserModel user = GetUserModelForAuthenticatedUser();
            if (!isEmpty)
            {
                user.Microposts.Add(new MicropostModel {Content = newMicropost, CreatedAt = DateTime.Now});
            }
            user.Microposts.Add(new MicropostModel { Content = "aaaaaaaaaa", CreatedAt = DateTime.Now.AddHours(-1) });

            return View("Index", user);
        }

        private UserModel GetUserModelForAuthenticatedUser()
        {
            string id = User.Identity.Name;
            return _userBS.GetUser(new BoRef(id));
        }
    }
}
