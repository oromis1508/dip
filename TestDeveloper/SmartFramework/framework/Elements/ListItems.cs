using OpenQA.Selenium;

namespace smart.framework.Elements
{
    public class ListItems : BaseElement
    {
        public ListItems(By locator, string name) : base(locator, name)
        {
        }

        private IWebElement GetSubItem(string name) => GetElement().FindElementByXPath($"//*[text()='{name}']");

        public void ClickSubItem(string name) => GetSubItem(name).Click();

        public bool IsSubItemExists(string name) => GetSubItem(name).Displayed;

        public string GetSubItemAttribute(string subItemName, string attributeName) =>
            GetSubItem(subItemName).GetAttribute(attributeName);
    }
}
