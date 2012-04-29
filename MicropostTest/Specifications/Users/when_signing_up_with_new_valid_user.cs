using System.Web.Mvc;
using System.Web.Security;
using Machine.Specifications;
using MicropostMVC.BusinessServices;
using MicropostMVC.Controllers;
using MicropostMVC.Framework.Common;
using MicropostMVC.Models;
using Moq;
using It = Machine.Specifications.It;

namespace MicropostTest.Specifications.Users
{
    [Subject(typeof(UsersController))]
    public class when_signing_up_with_new_valid_user
    {
        private static UsersController controller;
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
            userBSMock.Setup(u => u.IsEmailUsedBySomeone(user)).Returns(false);
            userBSMock.Setup(u => u.Save(user, true)).Returns(userSaved);
            userBSMock.Setup(u => u.Authenticate(user));

            var micropostBsMock = new Mock<IMicropostBS>();

            controller = new UsersController(userBS, micropostBsMock.Object);
        };

        Because of = () =>
        {
            result = controller.SignUp(user);
        };

        It should_return_show_view = () =>
        {
            result.ShouldBe(typeof(RedirectToRouteResult));
            ((RedirectToRouteResult)result).RouteValues.ContainsValue("Show");
        };
    }
}