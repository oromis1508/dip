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
        private readonly string _name;
        private readonly By _locator;

        protected BaseElement(By locator, string name)
        {
            _name = name;
            _locator = locator;
        }

        protected RemoteWebElement GetElement()
        {   
            WaitForElementPresent();
            return (RemoteWebElement)Browser.GetDriver().FindElement(_locator); ;
        }

        protected string GetName()
        {
            return _name;
        }

        protected By GetLocator()
        {
            return _locator;
        }

        public void Click()
        {
            WaitForElementPresent();
            GetElement().Click();
            Log.Info($"{GetName()} :: click");
        }

        public bool IsPresent()
        {
            try
            {
                var isPresent = Browser.GetDriver().FindElement(_locator).Displayed;
                Log.Info($"{GetName()} : is present : {isPresent}");
                return isPresent;
            }
            catch (Exception)
            {
                Log.Info($"{GetName()} : is not present");
                return false;
            }
        }

        protected void WaitForElementPresent()
        {
            var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.GetTimeout())));
            try
            {
                wait.Until(waiting =>
                {
                    var webElements = Browser.GetDriver().FindElements(_locator);
                    return webElements.Count != 0;
                });
            }
            catch (TimeoutException)
            {
                Log.Fatal($"Element with locator: '{_locator}' does not exists!");
            }
        }

        public static void WaitForElementPresent(By locator, string name)
        {
            var wait = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromMilliseconds(Convert.ToDouble(Configuration.GetTimeout())));
            try
            {
                wait.Until(waiting =>
                {
                    try
                    {
                        var webElements = Browser.GetDriver().FindElements(locator);
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
