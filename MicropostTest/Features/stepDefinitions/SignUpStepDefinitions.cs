using System;
using System.Globalization;
using Machine.Specifications;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace MicropostTest.Features.stepDefinitions
{
    [Binding]
    public class SignUpStepDefinitions
    {
        [Given("a sign up page")]
        public void GivenASignUpPage()
        {
            WebBrowser.Current.GoTo(WebBrowser.UrlMicropostMvc + "/Users/SignUp");
            WebBrowser.Current.WaitForComplete();
        }

        [When(@"filling the fields with valid data")]
        public void WhenFillingTheFieldsWithValidData()
        {
            Random randomizer = new Randomizer();
            WebBrowser.Current.TextField("Name").Value = "name";
            WebBrowser.Current.TextField("Email").Value = randomizer.Next().ToString(CultureInfo.InvariantCulture) + "@test.com";
            WebBrowser.Current.TextField("Password").Value = "abcdefghijk";
            WebBrowser.Current.TextField("PasswordConfirmation").Value = "abcdefghijk";
        }

        [When(@"Email is already in database")]
        public void WhenEmailIsAlreadyInDatabase()
        {
            WebBrowser.Current.TextField("Email").Value = "name@test.com";
        }
        
        [When(@"I click the '(.*)' button")]
        public void WhenIClickTheCreateButton(string buttonName)
        {
            WebBrowser.Current.Button(buttonName).Click();
        }

        [Then(@"the '(.*)' message contains '(.*)'")]
        public void ThenMessageContainsText(string className, string messageText)
        {
            WebBrowser.Current.Div(Find.ByClass(className)).Text.ShouldContain(messageText);
        }

    }
}
