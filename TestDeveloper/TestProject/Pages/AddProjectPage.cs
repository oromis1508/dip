using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;

namespace TestProject.Pages
{
    public class AddProjectPage : BaseForm
    {
        private TextBox tbxProjectName = new TextBox(By.Id("projectName"), "add project text field");
        private Button btnSaveProject = new Button(By.XPath("//button[@type='submit']"), "button to add project");
        private Label lblAlertMessage = new Label(By.XPath("//div[contains(@class, 'alert')]"), "add project alert message");

        public AddProjectPage() : base(By.Id("addProjectForm"), "add project page")
        {
        }

        public void SetProjectName(string name) => tbxProjectName.SetText(name);

        public void ClickSaveProject() => btnSaveProject.Click();

        public string GetAlertMessage => lblAlertMessage.Text.Trim();
    }
}
