using OpenQA.Selenium;

namespace smart.framework.Elements
{
    public class WebImage : BaseElement
    {
        public WebImage(By locator, string name) : base(locator, name)
        {
        }

        public string ImageBase64 => GetAttribute("src");
    }
}
