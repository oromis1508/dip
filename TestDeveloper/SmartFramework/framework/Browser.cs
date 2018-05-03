using System;
using System.Collections.ObjectModel;
using System.Net;
using smart.framework.BaseEntities;
using smart.framework.Utils.TestUtils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Cookie = OpenQA.Selenium.Cookie;

namespace smart.framework
{
    public class Browser : BaseEntity
    {
        private static IWebDriver Driver { get; set; }

        public static IWebDriver Instance => Driver ?? (Driver = BrowserFactory.SetupBrowser());

        public static string CurrentUri => Driver.Url;

        public static void AddCookie(string key, string value) => Driver.Manage().Cookies.AddCookie(new Cookie(key, value));

        public static byte[] TakeScreenshot() => ((ITakesScreenshot) Driver).GetScreenshot().AsByteArray;

        public static void WaitForPageToLoad()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.Timeout)));
            wait.Until(waiting =>
            {
                try
                {
                    var result = ((IJavaScriptExecutor)Driver).ExecuteScript("return document['readyState'] ? 'complete' == document.readyState : true");
                    return (bool)result;
                } catch (Exception)
                {
                    return false;
                }
            });
        }

        public static void NavigateToUrl(string url)
        {
            Instance.Navigate().GoToUrl(url);
        }

        public static void MoveToUrlWithBasicAuth(string url, NetworkCredential credentials)
        {
            //var webclient = new WebClient {Credentials = credentials};
            var credUrl = url.Replace("//", $"//{credentials.UserName}:{credentials.Password}@");
            NavigateToUrl(credUrl);
        }

        public static void Refresh() => Instance.Navigate().Refresh();

        public static ReadOnlyCollection<string> GetTabs => Instance.WindowHandles;

        public static void ChooseOtherTab(string windowName) => Instance.SwitchTo().Window(windowName);

    }
}
