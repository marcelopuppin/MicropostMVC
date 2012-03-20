using System.Web.Mvc;

namespace MicropostMVC.Controllers
{
    [AllowAnonymous]
    public class PagesController : Controller
    {
        //
        // GET: /Home/

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
