using System.Collections.ObjectModel;
using System.Net;
using smart.framework.BaseEntities;
using OpenQA.Selenium;
using Cookie = OpenQA.Selenium.Cookie;

namespace smart.framework
{
    public class Browser : BaseEntity
    {
        private static IWebDriver Driver { get; set; }

        public static IWebDriver Instance => Driver ?? (Driver = BrowserFactory.SetupBrowser());


        public static void AddCookie(string key, string value) => Driver.Manage().Cookies.AddCookie(new Cookie(key, value));

        public static byte[] TakeScreenshot() => ((ITakesScreenshot) Driver).GetScreenshot().AsByteArray;

        public static void NavigateToUrl(string url)
        {
            Instance.Navigate().GoToUrl(url);
        }

        public static void MoveToUrlWithBasicAuth(string url, NetworkCredential credentials)
        {
            var credUrl = url.Replace("//", $"//{credentials.UserName}:{credentials.Password}@");
            NavigateToUrl(credUrl);
        }

        public static void Refresh() => Instance.Navigate().Refresh();

        public static ReadOnlyCollection<string> GetTabs => Instance.WindowHandles;

        public static void ChooseOtherTab(string windowName) => Instance.SwitchTo().Window(windowName);
    }
}
