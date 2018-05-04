using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smart.framework;
using smart.framework.BaseEntities;
using smart.framework.Utils.Support;
using smart.framework.Utils.TestPortal;
using smart.framework.Utils.TestUtils;
using TestProject.Forms;
using TestProject.Models;
using TestProject.Pages;

namespace TestProject.Tests
{
    [TestClass]
    public class PortalFunctionsTest : BaseTest
    {
        private NetworkCredential _credentials;
        private string _baseUrl;
        private string _variant;
        private string _projectName;
        private string _pathTestAttachment;

        private const int AddedProjectNameLength = 10;

        [TestInitialize]
        [Priority(2)]
        public void VariablesInit()
        {
            _credentials = new NetworkCredential(Configuration.PortalLogin, Configuration.PortalPassword);
            _baseUrl = Configuration.BaseUrl;
            _variant = Configuration.Variant;
            _projectName = Configuration.OpenedProject;
            _pathTestAttachment = Configuration.PathTestAttachment;
        }

        [TestCleanup]
        [Priority(1)]
        public void DeleteFile()
        {
            File.Delete(_pathTestAttachment);
        }

        [TestMethod]
        public void TestPortal()
        {
            Log.Step("Get the token");
            var tokenResponseStream = PortalApiUtil.SendRequest(PortalMethod.GetToken, new[] { $"variant={_variant}" });
            var token = new StreamReader(tokenResponseStream).ReadToEnd();

            Log.Step("Check that token was generated");
            Asserts.IsTrue(token != string.Empty, "Checking that token is not empty");

            Log.Step("Navigate to the main page with basic authorization and add token to cookie");
            Browser.MoveToUrlWithBasicAuth(_baseUrl, _credentials);
            Browser.AddCookie("token", token);
            Browser.Refresh();

            Log.Step($"Check that variant on project page is valid (V-{_variant})");
            var footer = new Footer();
            Asserts.AreEqual(_variant, footer.GetVariantNumber(), "Checking that variant is valid (V-{variant})");

            Log.Step($"Navigate to project {_projectName} page and get project id");
            var mainPage = new MainPage();
            var projectId = mainPage.GetProjectId(_projectName);
            mainPage.MoveToProject(_projectName);

            Log.Step($"Check that project {_projectName} opened");
            var header = new Header();
            Asserts.IsTrue(header.IsProjectOpened(_projectName), $"Checking that project {_projectName} opened");

            Log.Step("Get list of tests by api request in xml format");
            var responseXmlStream = PortalApiUtil.SendRequest(PortalMethod.GetXmlTestsList, new[] { $"projectId={projectId}" });
            var responseXmlDoc = XmlUtil.GetXmlWithoutRootNode(responseXmlStream);
            var testsList = XmlUtil.GetObjectsFromXml<test>(responseXmlDoc);

            Log.Step("Check that list sorted");
            var projectPage = new ProjectPage();
            Asserts.IsTrue(projectPage.IsTestsSorted(false), "Checking that list sorted");

            Log.Step("Check that all displayed projects is exists in the api response");
            Asserts.IsListContains(testsList, projectPage.GetTestsOnCurrentView(), "Checking that projects is exists in the api response");

            Log.Step("Navigate to main page, save number of opened tabs and click add project");
            Browser.NavigateToUrl(_baseUrl);
            var tabsOpenedBeforeAddProject = Browser.GetTabs.Count;
            mainPage.ClickAddProject();

            Log.Step("Check that new tab opened");
            Asserts.AreNotEqual(tabsOpenedBeforeAddProject, Browser.GetTabs.Count, "Checking that new tab opened");

            Log.Step("Choose opened tab and add project");
            Browser.ChooseOtherTab(Browser.GetTabs.Last());
            var addProjectPage = new AddProjectPage();
            var addedProjectName = Randoms.GetRandomString(AddedProjectNameLength);
            addProjectPage.SetProjectName(addedProjectName).ClickSaveProject();

            Log.Step("Check that alert message displayed and valid");
            Asserts.AreEqual($"Project {addedProjectName} saved", addProjectPage.GetAlertMessage, "Checking that alert message is valid");

            Log.Step("Close opened tab and refresh page");
            Browser.Instance.Close();
            Browser.ChooseOtherTab(Browser.GetTabs.First());
            Browser.Refresh();

            Log.Step("Check that tab closed");
            Asserts.AreEqual(tabsOpenedBeforeAddProject, Browser.GetTabs.Count, "Checking that active tab closed");

            Log.Step($"Check that project {addedProjectName} added");
            Asserts.IsTrue(mainPage.IsProjectExists(addedProjectName), "Check project added on main page");

            Log.Step($"Move to {addedProjectName} project and click add button");
            mainPage.MoveToProject(addedProjectName);
            projectPage.ClickAddButton();

            Log.Step("Fill fields and add test");
            File.WriteAllBytes(_pathTestAttachment, Browser.TakeScreenshot());
            var addedTest = AddedTest.GetRandomModel();
            var addTestWindow = new AddTestModalWindow();
            addTestWindow.SetTestName(addedTest.TestName).SetStatus(addedTest.Status)
                .SetTestMethod(addedTest.TestMethod).SetStartTime(addedTest.StartTime).SetEndTime(addedTest.EndTime)
                .SetEnvironment(addedTest.Environment).SetBrowser(addedTest.Browser).AddAttachment(_pathTestAttachment)
                .SaveTest();

            Log.Step("Check that alert about test saving displayed");
            Asserts.IsTrue(addTestWindow.IsProjectSaveAlertDisplayed(addedTest.TestName), "Checking that saving alert correct");

            Log.Step("Close modal window");
            addTestWindow.CloseWindowByJsClickOutside();

            Log.Step("Check that test added");
            Asserts.IsTrue(projectPage.IsTestExist(addedTest.TestName), "Checking that test exists in project");

            Log.Step($"Move to test {addedTest.TestName}");
            projectPage.MoveToTest(addedTest.TestName);

            Log.Step("Checking that test attributes and attached image is valid");
            var testPage = new TestPage();
            Asserts.AreEqual(addedTest, testPage.GetAddedTestObject(), "Cheking information of added test");
            Asserts.AreEqual(addedProjectName, testPage.ProjectName, "Cheking project name of added test");
            Asserts.IsTrue(testPage.IsImageValid(new Bitmap(Image.FromFile(_pathTestAttachment))), "Cheking the attached image");
        }
    }
}
