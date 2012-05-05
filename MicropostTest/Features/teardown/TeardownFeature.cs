using MicropostMVC.App_Start;

namespace MicropostTest.Features
{
    public abstract class TeardownFeature
    {
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void MyScenarioSetup()
        {
            StructuremapMvc.Start();
        }
 
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void MyScenarioTeardown()
        {
            WebBrowser.Current.Close();
        }
    }

    
}