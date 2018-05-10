using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace smart.framework.Elements
{
    public class Select : BaseElement
    {
        public Select(By locator, string name) : base(locator, name)
        {
        }

        public void SetValue(string value) => new SelectElement(GetElement()).SelectByText(value);
    }
}
