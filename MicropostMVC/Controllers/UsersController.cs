using System.Web.Mvc;

namespace MicropostMVC.Controllers
{
    public class UsersController : Controller
    {
        public string SignIn()
        {
            return "Sign in";
        }

        public string SignUp()
        {
            return "Sign up";
        }
    }
}
