using TechTalk.SpecFlow;
using WatiN.Core;

namespace MicropostTest.Features
{
    public static class WebBrowser
    {
        public static IE Current
        {
            get
            {
                string key = "browser";
                if (!FeatureContext.Current.ContainsKey(key))
                {
                    FeatureContext.Current[key] = new IE();
                }
                return FeatureContext.Current[key] as IE;
            }
        }

        public static string UrlMicropostsMvc
        {
            get { return "http://localhost:10351"; }
        }
    }
}

