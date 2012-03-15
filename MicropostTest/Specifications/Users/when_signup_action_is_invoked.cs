using System.Web.Mvc;
using Machine.Specifications;
using MicropostMVC.Controllers;

namespace MicropostTest.Specifications.Users
{
    [Subject(typeof(UsersController))]
    public class when_signup_action_is_invoked
    {
        static UsersController controller;
        static ActionResult result;

        Establish context = () => controller = new UsersController();

        Because of = () => result = controller.SignUp();

        It should_return_a_view = () =>
                                      {
                                          result.ShouldBe(typeof(ViewResult));
                                          ((ViewResult)result).ViewName.ShouldBeEmpty();
                                      };
    }
}