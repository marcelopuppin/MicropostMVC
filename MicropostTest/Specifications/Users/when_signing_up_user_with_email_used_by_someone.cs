using System.Web.Mvc;
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
    public class when_signing_up_user_with_email_used_by_someone
    {
        private static UsersController controller;
        private static ActionResult result;
        private static IUserBS userBS;
        private static UserModel user;
        
        Establish context = () =>
        {
            user = new UserModel() { Id = new BoRef(), Name = "name", Email = "name@email.com", Password = "abcedf" };
            
            var userBSMock = new Mock<IUserBS>();
            userBS = userBSMock.Object;
            userBSMock.Setup(u => u.IsEmailUsedBySomeone(user)).Returns(true);
            
            controller = new UsersController(userBS);
        };

        Because of = () =>
        {
            result = controller.SignUp(user);
        };

        It should_return_sign_up_view_same_unsaved_user = () =>
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