using System;
using smart.framework.BaseEntities;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using smart.framework.Utils.TestUtils;

namespace smart.framework.Elements
{
    public abstract class BaseElement : BaseEntity
    {
        protected string Name { get; }
        protected By Locator { get; }

        protected BaseElement(By locator, string name)
        {
            Name = name;
            Locator = locator;
        }

        protected RemoteWebElement GetElement()
        {   
            WaitForElementPresent();
            return (RemoteWebElement)Browser.Instance.FindElement(Locator); ;
        }   

        public void Click()
        {
            WaitForElementPresent();
            GetElement().Click();
            Log.Info($"{Name} :: click");
        }

        public bool IsPresent()
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.Timeout)));

            try
            {
                wait.Until(waiting =>
                {
                    var webElements = Browser.Instance.FindElements(Locator);
                    return webElements.Count != 0 && webElements[0].Displayed && webElements[0].Enabled;
                });
                Log.Info($"{Name} : is present");
                return true;
            }
            catch (TimeoutException)
            {
                Log.Info($"{Name} : is not present");
                return false;
            }
        }

        protected void WaitForElementPresent()
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.Timeout)));
            try
            {
                wait.Until(waiting =>
                {
                    var webElements = Browser.Instance.FindElements(Locator);
                    return webElements.Count != 0;
                });
            }
            catch (TimeoutException)
            {
                Log.Fatal($"Element with locator: '{Locator}' does not exists!");
            }
        }

        public static void WaitForElementPresent(By locator, string name)
        {
            var wait = new WebDriverWait(Browser.Instance, TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.Timeout)));
            try
            {
                wait.Until(waiting =>
                {
                    try
                    {
                        var webElements = Browser.Instance.FindElements(locator);
                        return webElements.Count != 0;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });
            }
            catch (TimeoutException)
            {
                Log.Fatal($"Element with locator: '{locator}' does not exists!");
            }
        }

        public string Text => GetElement().Text;

        public string GetAttribute(string attributeName) => GetElement().GetAttribute(attributeName);        
    }
}
