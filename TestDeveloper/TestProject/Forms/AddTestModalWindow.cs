using System.Drawing;
using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;
using smart.framework.Utils.JS;

namespace TestProject.Forms
{
    public class AddTestModalWindow : BaseForm
    {
        private readonly TextBox _tbxTestName = new TextBox(By.Id("testName"), "text field for test name");
        private readonly TextBox _tbxTestmethod = new TextBox(By.Id("testMethod"), "text field for test method");
        private readonly TextBox _tbxStartTime = new TextBox(By.Id("startTime"), "text field for test start time");
        private readonly TextBox _tbxEndTime = new TextBox(By.Id("endTime"), "text field for test end time");
        private readonly TextBox _tbxEnvironment = new TextBox(By.Id("environment"), "text field for test environment");
        private readonly TextBox _tbxBrowser = new TextBox(By.Id("browser"), "text field for browser name");

        private readonly Button _btnSave = new Button(By.XPath("//button[contains(text(), 'Save')]"), "text field for test name");
        private readonly Select _selectStatus = new Select(By.Id("testStatus"), "select to set test result");
        private readonly FileInput _inputAttachment = new FileInput(By.Id("attachment"), "file chooser to add attachment");

        private readonly Label _lblSuccessAlert = new Label(By.Id("success"), "alert message when test saved");


        public AddTestModalWindow() : base(By.ClassName("modal-content"), "modal window to add test")
        {
        }

        public AddTestModalWindow SetTestName(string testName)
        {
            _tbxTestName.SetText(testName);
            return this;
        }

        public AddTestModalWindow SetStatus(string status)
        {
            _selectStatus.SetValue(status);
            return this;
        }
        public AddTestModalWindow SetTestMethod(string testMethod)
        {
            _tbxTestmethod.SetText(testMethod);
            return this;
        }
        public AddTestModalWindow SetStartTime(string startTime)
        {
            _tbxStartTime.SetText(startTime);
            return this;
        }
        public AddTestModalWindow SetEndTime(string endTime)
        {
            _tbxEndTime.SetText(endTime);
            return this;
        }
        public AddTestModalWindow SetEnvironment(string environment)
        {
            _tbxEnvironment.SetText(environment);
            return this;
        }
        public AddTestModalWindow SetBrowser(string browser)
        {
            _tbxBrowser.SetText(browser);
            return this;
        }
        public AddTestModalWindow AddAttachment(string fileName)
        {
            _inputAttachment.ChooseFile(fileName);
            return this;
        }
        public AddTestModalWindow SaveTest()
        {
            _btnSave.Click();
            return this;
        }

        public bool IsProjectSaveAlertDisplayed(string testName)
        {
            return _lblSuccessAlert.Text.Contains(testName);
        }

        public void CloseWindowByJsClickOutside() => JSExecutor.JavaScriptClickByCoordinates(new Point(GetFormLocation.X - 5, GetFormLocation.Y - 5));
    }
}
