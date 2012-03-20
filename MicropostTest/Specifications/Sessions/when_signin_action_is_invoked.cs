using System.Web.Mvc;
using Machine.Specifications;
using MicropostMVC.BusinessServices;
using MicropostMVC.Controllers;
using Moq;
using It = Machine.Specifications.It;

namespace MicropostTest.Specifications.Sessions
{
    [Subject(typeof(SessionsController))]
    public class when_signin_action_is_invoked
    {
        static SessionsController controller;
        static ActionResult result;

        Establish context = () =>
        {
            var userBSMock = new Mock<IUserBS>();
            controller = new SessionsController(userBSMock.Object);
        };

        Because of = () => result = controller.SignIn();

        It should_return_a_view = () =>
        {
            result.ShouldBe(typeof(ViewResult));
            ((ViewResult)result).ViewName.ShouldBeEmpty();
        };
    }
}