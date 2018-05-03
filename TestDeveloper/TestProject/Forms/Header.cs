using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;

namespace TestProject.Forms
{
    public class Header : BaseForm
    {
        private string projectNameTemplate = "//li[contains(text(), '{0}')]";

        public Header() : base(By.ClassName("breadcrumb"), "page header")
        {
        }

        public bool IsProjectOpened(string projectName) => new Label(By.XPath(string.Format(projectNameTemplate, projectName)), "project name label").IsPresent();
    }
}
