using System.Text.RegularExpressions;
using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;

namespace TestProject.Pages
{
    public class MainPage : BaseForm
    {
        private readonly ListItems _projectsList = new ListItems(By.ClassName("list-group"), "list of projects");
        private readonly Button _addProject = new Button(By.XPath("//a[contains(@class, 'btn')]"), "button to add new project");

        public MainPage() : base(By.XPath("//a[@href='addProject']"), "project page")
        {
        }

        public void MoveToProject(string projectName) => _projectsList.ClickSubItem(projectName);

        public string GetProjectId(string projectName)
        {
            var projectHref = _projectsList.GetSubItemAttribute(projectName, "href");
            return Regex.Match(projectHref, ".*projectId=(\\d)\\D*").Groups[1].Value;
        }

        public void ClickAddProject() => _addProject.Click();

        public bool IsProjectExists(string projectName) => _projectsList.IsSubItemExists(projectName);
    }
}
