using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using smart.framework.BaseEntities;
using smart.framework.Utils.TestUtils;

namespace smart.framework
{
    public class BrowserFactory : BaseEntity
    {
        public static IWebDriver SetupBrowser()
        {
            if (Configuration.IsRemote)
            {
                var map = new Dictionary<string, object> { { CapabilityType.BrowserName, Configuration.Browser } };
                return new RemoteWebDriver(new DesiredCapabilities(map));
            }

            var browserName = Configuration.LocalBrowser;
            switch (browserName)
            {
                case "Chrome":
                    return new ChromeDriver();
                case "Firefox":
                   return new FirefoxDriver();
                default:
                    Log.Info($"Invalid name of browser: {browserName}, choosed default browser Chrome");
                    return new ChromeDriver();
            }
        }
    }
}
