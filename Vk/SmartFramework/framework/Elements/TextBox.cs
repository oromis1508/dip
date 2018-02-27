using OpenQA.Selenium;

namespace demo.framework.Elements
{
    public class TextBox : BaseElement
    {
        public TextBox(By locator, string name) : base(locator, name)
        {
        }

        public void SetText(string text)
        {
            WaitForElementPresent();
            GetElement().Click();
            GetElement().SendKeys(text);
            Log.Info($"{Name} :: type text '{text}'");
        }
    }
}
