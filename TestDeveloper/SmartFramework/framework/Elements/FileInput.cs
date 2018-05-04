using OpenQA.Selenium;

namespace smart.framework.Elements
{
    public class FileInput : BaseElement
    {
        public FileInput(By locator, string name) : base(locator, name)
        {
        }

        public void ChooseFile(string filePath) => GetElement().SendKeys(filePath);
    }
}
