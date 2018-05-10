using System.Drawing;
using smart.framework.Elements;
using OpenQA.Selenium;

namespace smart.framework.BaseEntities
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

        public Point GetFormLocation => BaseElement.GetElementLocation(_locator);
    }
}
