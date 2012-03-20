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
    public class when_signing_in_with_invalid_user
    {
        private static SessionsController controller;
        private static ActionResult result;
        private static IUserBS userBS;
        private static UserModel user;
        
        Establish context = () =>
        {
            user = new UserModel() { Id = new BoRef(), Name = "name", Email = "name@email.com", Password = "abcedf" };
            
            var userBSMock = new Mock<IUserBS>();
            userBS = userBSMock.Object;
            userBSMock.Setup(u => u.Login(user)).Returns(user);

            controller = new SessionsController(userBS);
        };

        Because of = () =>
        {
            result = controller.SignIn(user);
        };

        It should_return_sign_in_view_invalid_user = () =>
        {
            result.ShouldBe(typeof(ViewResult));
            var user = ((ViewResult) result).Model as UserModel;
            user.ShouldNotBeNull();
            if (user != null)
            {
                user.Id.Value.ShouldBeEqualIgnoringCase("");    
            }
        };
    }
}