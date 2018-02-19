using System;
using demo.framework.BaseEntities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace demo.framework.Utils.Browser
{
    public class Browser : BaseEntity
    {
        protected static Browser _browser;

        public static IWebDriver Driver { get; private set; }

        public static Browser GetInstance()
        {
           Driver = BrowserFactory.SetupBrowser();
            return new Browser();
        }

        public static string CurrentUri => Driver.Url;

        public static void WaitForPageToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.GetTimeout())));
            wait.Until(waiting =>
            {
                try
                {
                    var result = ((IJavaScriptExecutor)Driver).ExecuteScript("return document['readyState'] ? 'complete' == document.readyState : true");
                    return result is bool b && b;
                } catch (Exception)
                {
                    return false;
                }
            });
        }        
    }
}
