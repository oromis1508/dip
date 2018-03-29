using System;
using demo.framework.BaseEntities;
using demo.framework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace demo.framework.Elements
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
            return (RemoteWebElement)Browser.Driver.FindElement(Locator); ;
        }   

        public void Click()
        {
            WaitForElementPresent();
            GetElement().Click();
            Log.Info($"{Name} :: click");
        }

        public bool IsPresent()
        {
            try
            {
                var isPresent = Browser.Driver.FindElement(Locator).Displayed;
                Log.Info($"{Name} : is present : {isPresent}");
                return isPresent;
            }
            catch (Exception)
            {
                Log.Info($"{Name} : is not present");
                return false;
            }
        }

        protected void WaitForElementPresent()
        {
            var wait = new WebDriverWait(Browser.Driver, TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.GetTimeout())));
            try
            {
                wait.Until(waiting =>
                {
                    var webElements = Browser.Driver.FindElements(Locator);
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
            var wait = new WebDriverWait(Browser.Driver, TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.GetTimeout())));
            try
            {
                wait.Until(waiting =>
                {
                    try
                    {
                        var webElements = Browser.Driver.FindElements(locator);
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
    }
}
