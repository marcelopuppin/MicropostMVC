namespace MicropostTest.Features
{
    public partial class AccessingLinksAtTheRootPageFeature
    {
        [NUnit.Framework.TestFixtureTearDownAttribute]
        public virtual void MyScenarioTeardown()
        {
            WebBrowser.Current.Close();
        }
    }
}