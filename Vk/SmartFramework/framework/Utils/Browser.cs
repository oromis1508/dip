using System;
using demo.framework.BaseEntities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace demo.framework.Utils
{

    public class Browser : BaseEntity
    {
        protected static Browser _browser;
        private static IWebDriver _driver;
       
        public static Browser GetInstance()
        {
           _driver = BrowserFactory.SetupBrowser();
           
            return new Browser();
        }

        public static IWebDriver GetDriver()
        {
            return _driver;
        }

        public static string CurrentUri => GetDriver().Url;

        public static void WaitForPageToLoad()
        {
            var wait = new WebDriverWait(GetDriver(), TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.GetTimeout())));
            try
            {
                wait.Until(waiting =>
                {
                    try
                    {
                        var result = ((IJavaScriptExecutor)GetDriver()).ExecuteScript("return document['readyState'] ? 'complete' == document.readyState : true");
                        return result is bool b && b;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
            }
            catch (Exception)
            {
            }
        }

    }
}
