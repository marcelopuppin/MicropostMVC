﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.261
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MicropostTest.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Signing in users")]
    public partial class SigningInUsersFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "signin.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Signing in users", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Sign in a known user")]
        public virtual void SignInAKnownUser()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sign in a known user", ((string[])(null)));
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
  testRunner.Given("a sign in page");
#line 5
 testRunner.When("filling the fields with known user");
#line 6
 testRunner.And("I click the \'SignIn\' button");
#line 7
 testRunner.Then("the result is the \'Show\' page");
#line 8
 testRunner.And("the title is \'Show\'");
#line 9
 testRunner.And("contains a \'Sign out\' link button");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Sign out a known user")]
        public virtual void SignOutAKnownUser()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sign out a known user", ((string[])(null)));
#line 11
this.ScenarioSetup(scenarioInfo);
#line 12
  testRunner.Given("a sign in page");
#line 13
 testRunner.When("filling the fields with known user");
#line 14
 testRunner.And("I click the \'SignIn\' button");
#line 15
 testRunner.And("I click the \'Sign out\' link");
#line 16
 testRunner.Then("the result is the \'Home\' page");
#line 17
 testRunner.And("the title is \'Home\'");
#line 18
 testRunner.And("contains a \'Sign in\' link button");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Sign in a unknown user")]
        public virtual void SignInAUnknownUser()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Sign in a unknown user", ((string[])(null)));
#line 20
this.ScenarioSetup(scenarioInfo);
#line 21
  testRunner.Given("a sign in page");
#line 22
 testRunner.When("filling the fields with unknown user");
#line 23
 testRunner.And("I click the \'SignIn\' button");
#line 24
 testRunner.Then("the result is the \'Sign in\' page");
#line 25
 testRunner.And("the \'error\' message contains \'email/password\'");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
