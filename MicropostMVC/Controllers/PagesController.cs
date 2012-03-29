using System.Web.Mvc;
using MicropostMVC.Framework.Common;
using MicropostMVC.Models;

namespace MicropostMVC.Controllers
{
    [AllowAnonymous]
    public class PagesController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
