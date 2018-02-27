using demo.framework.BaseEntities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace demo.framework.Utils
{
    public class BrowserFactory : BaseEntity
    {
        private const string DriverPath = "../../resources/";

        /// <summary>
        /// setup webdriver. chromedriver is a default value
        /// </summary>
        /// <returns>driver</returns>
        public static IWebDriver SetupBrowser()
        {
            var browserName = Configuration.GetBrowser();
            switch (browserName)
            {
                case "chrome":
                    return new ChromeDriver(System.IO.Path.GetFullPath(DriverPath));
                case "iexplore":
                    return new InternetExplorerDriver(System.IO.Path.GetFullPath(DriverPath));
                case "firefox":
                    return new FirefoxDriver();
            }
            return new ChromeDriver(System.IO.Path.GetFullPath(DriverPath));
        }
    }
}
