using System.Web.Mvc;
using Machine.Specifications;
using MicropostMVC.BusinessServices;
using MicropostMVC.Controllers;
using Moq;
using It = Machine.Specifications.It;

namespace MicropostTest.Specifications.Users
{
    [Subject(typeof(UsersController))]
    public class when_signup_action_is_invoked
    {
        static UsersController controller;
        static ActionResult result;

        Establish context = () =>
                                {
                                    var userBSMock = new Mock<IUserBS>();
                                    var micropostBsMock = new Mock<IMicropostBS>();
                                    controller = new UsersController(userBSMock.Object, micropostBsMock.Object);
                                };

        Because of = () => result = controller.SignUp();

        It should_return_a_view = () =>
                                      {
                                          result.ShouldBe(typeof(ViewResult));
                                          ((ViewResult)result).ViewName.ShouldBeEmpty();
                                      };
    }
}