using Machine.Specifications;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace MicropostTest.Features.stepDefinitions
{
    [Binding]
    public class PagesStepDefinitions
    {
        [Given("a startup root page")]
        public void GivenAStartupRootPage()
        {
            WebBrowser.Current.GoTo(WebBrowser.UrlMicropostMvc);
            WebBrowser.Current.WaitForComplete();
        }

        [When(@"I click the '(.*)' link")]
        public void WhenIClickTheLink(string linkName)
        {
            WebBrowser.Current.Link(Find.ByText(linkName)).Click();				
        }

        [Then(@"the result is the '(.*)' page")]
        public void ThenTheResultIsThePage(string pageName)
        {
            WebBrowser.Current.ContainsText(pageName);
        }

        [Then(@"the title is '(.*)'")]
        public void ThenTheTitleIs(string title)
        {
            WebBrowser.Current.Title.ShouldContain(title);
        }

        [Then(@"contains a '(.*)' link button")]
        public void ThenContainsALinkButton(string linkButtonName)
        {
            WebBrowser.Current.Link(Find.ByText(linkButtonName)).ShouldBe(typeof(Link));
        }
    }
}
