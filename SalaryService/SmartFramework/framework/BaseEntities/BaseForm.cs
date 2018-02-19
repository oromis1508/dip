using demo.framework.Elements;
using OpenQA.Selenium;

namespace demo.framework.BaseEntities
{
    public class BaseForm : BaseEntity
    {
        private readonly string _name;
        private readonly By _locator;
        protected BaseForm(By locator, string name)
        {   _locator = locator;
            _name = name;
            AssertIsPresent();
        }

        private void AssertIsPresent()
        {
            BaseElement.WaitForElementPresent(_locator, "Form " + _name);
            Log.Info($"Form '{_name}' has appeared");
        }

        public void CheckTextOnForm(string text)
        {
            BaseElement.WaitForElementPresent(By.XPath("//*[contains(.,'" + text + "')]"), text);
            Log.Info($"Text '{text}' is shown on the page");
        }
    }
}
