using System;
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
            var browserName = Configuration.RemoteBrowser ?? Configuration.LocalBrowser;

            if (Configuration.IsRemote)
            {
                var map = new Dictionary<string, object> { { CapabilityType.BrowserName, browserName.ToLower() } };
                if (browserName == "Firefox")
                {
                    map.Add("marionette", false);
                }
                return new RemoteWebDriver(new DesiredCapabilities(map));
            }

            switch (browserName)
            {
                case "Chrome":
                    return new ChromeDriver();
                case "Firefox":
                   return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), new FirefoxOptions(), TimeSpan.FromMinutes(3));
                default:
                    Log.Info($"Invalid name of browser: {browserName}, choosed default browser Chrome");
                    return new ChromeDriver();
            }
        }
    }
}
