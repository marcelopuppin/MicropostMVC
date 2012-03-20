using System.Web.Mvc;
using Machine.Specifications;
using MicropostMVC.BusinessServices;
using MicropostMVC.Controllers;
using MicropostMVC.Framework.Common;
using MicropostMVC.Models;
using Moq;
using It = Machine.Specifications.It;

namespace MicropostTest.Specifications.Sessions
{
    [Subject(typeof(SessionsController))]
    public class when_signing_in_with_valid_user
    {
        private static SessionsController controller;
        private static ActionResult result;
        private static IUserBS userBS;
        private static UserModel user;
        private static UserModel userSaved;
        
        Establish context = () =>
        {
            user = new UserModel() { Id = new BoRef(), Name = "name", Email = "name@email.com", Password = "abcedf" };
            userSaved = new UserModel() { Id = new BoRef("1"), Name = user.Name, Email = user.Email, Password = user.Password };
                                    
            var userBSMock = new Mock<IUserBS>();
            userBS = userBSMock.Object;
            userBSMock.Setup(u => u.Login(user)).Returns(userSaved);
            userBSMock.Setup(u => u.Authenticate(userSaved));

            controller = new SessionsController(userBS);
        };

        Because of = () =>
        {
            result = controller.SignIn(user);
        };

        It should_return_show_view = () =>
        {
            result.ShouldBe(typeof(RedirectToRouteResult));
            ((RedirectToRouteResult)result).RouteValues.ContainsValue("Show");
        };
    }
}