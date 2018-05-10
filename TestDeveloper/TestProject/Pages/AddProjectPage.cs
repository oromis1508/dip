using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;

namespace TestProject.Pages
{
    public class AddProjectPage : BaseForm
    {
        private readonly TextBox _tbxProjectName = new TextBox(By.Id("projectName"), "add project text field");
        private readonly Button _btnSaveProject = new Button(By.XPath("//button[@type='submit']"), "button to add project");
        private readonly Label _lblAlertMessage = new Label(By.XPath("//div[contains(@class, 'alert')]"), "add project alert message");

        public AddProjectPage() : base(By.Id("addProjectForm"), "add project page")
        {
        }

        public AddProjectPage SetProjectName(string name)
        {
            _tbxProjectName.SetText(name);
            return this;
        }

        public void ClickSaveProject() => _btnSaveProject.Click();

        public string GetAlertMessage => _lblAlertMessage.Text.Trim();
    }
}
