using System;
using System.Globalization;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MicropostTest.Features.stepDefinitions
{
    [Binding]
    public class SignInStepDefinitions
    {
        [Given("a sign in page")]
        public void GivenASignInPage()
        {
            WebBrowser.Current.GoTo(WebBrowser.UrlMicropostMvc + "/Sessions/SignIn");
            WebBrowser.Current.WaitForComplete();
        }

        [When(@"filling the fields with known user")]
        public void WhenFillingTheFieldsWithKnownUser()
        {
            WebBrowser.Current.TextField("Email").Value = "name@test.com";
            WebBrowser.Current.TextField("Password").Value = "abcdefghijk";
        }

        [When(@"filling the fields with unknown user")]
        public void WhenFillingTheFieldsWithUnknownUser()
        {
            Random randomizer = new Randomizer();
            WebBrowser.Current.TextField("Email").Value = randomizer.Next().ToString(CultureInfo.InvariantCulture) + "@test.com";
            WebBrowser.Current.TextField("Password").Value = "abcdefghijk";
        }
        
    }
}
