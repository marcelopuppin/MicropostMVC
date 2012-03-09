using System.Web.Mvc;
using Machine.Specifications;
using MicropostMVC.Controllers;

namespace MicropostTest.Specifications
{
    [Subject(typeof(PagesController))]
    public class when_contact_action_is_invoked
    {
        static PagesController controller;
        static ActionResult result;

        Establish context = () => controller = new PagesController();

        Because of = () => result = controller.Contact();

        It should_return_a_view = () =>
                                      {
                                          result.ShouldBe(typeof(ViewResult));
                                          ((ViewResult)result).ViewName.ShouldBeEmpty();
                                      };
    }
}