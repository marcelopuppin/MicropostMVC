namespace MicropostTest.Features
{
    public abstract class TeardownFeature
    {
        [NUnit.Framework.TestFixtureTearDownAttribute]
        public virtual void MyScenarioTeardown()
        {
            WebBrowser.Current.Close();
        }
    }
}