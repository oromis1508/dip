using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smart.framework;
using smart.framework.BaseEntities;
using smart.framework.Utils.Services;
using smart.framework.Utils.Support;
using smart.framework.Utils.TestPortal;
using smart.framework.Utils.TestUtils;
using TestProject.Entities;
using TestProject.Forms;
using TestProject.Pages;

namespace TestProject
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        private NetworkCredential credentials;
        private string baseUrl;
        private string variant;
        private string projectName;
        private int addedProjectNameLength = 10;

        [TestInitialize]
        [Priority(2)]
        public void VariablesInit()
        {
            //97a5c0b2fe584312a50692671cb3ac47
            credentials = new NetworkCredential(Configuration.PortalLogin, Configuration.PortalPassword);
            baseUrl = Configuration.BaseUrl;
            variant = Configuration.Variant;
            projectName = Configuration.OpenedProject;
        }

        [TestMethod]
        public void TestMethod1()
        {
            Log.Step("Get the token");
            var tokenResponseStream = PortalApiUtil.SendRequest(PortalMethod.GetToken, new[] { $"variant={variant}" });
            var token = new StreamReader(tokenResponseStream).ReadToEnd();

            Log.Step("Check that token was generated");
            Asserts.IsTrue(token != null && token != string.Empty, "Checking that token is not null and not empty");

            Log.Step("Navigate to the main page with basic authorization and add token to cookie");
            Browser.MoveToUrlWithBasicAuth(baseUrl, credentials);
            Browser.AddCookie("token", token);
            Browser.Refresh();

            Log.Step("Check that variant on project page is valid");
            var footer = new Footer();
            Asserts.AreEqual(variant, footer.GetVariantNumber(), "Checking that variant is valid");

            Log.Step("Navigate to project page and get project id");
            var mainPage = new MainPage();
            var projectId = mainPage.GetProjectId(projectName);
            mainPage.MoveToProject(projectName);

            Log.Step("Check that valid project opened");
            var header = new Header();
            Asserts.IsTrue(header.IsProjectOpened(projectName), "Checking that valid project opened");

            Log.Step("Get list of tests by api request in xml format");
            var responseXmlStream = PortalApiUtil.SendRequest(PortalMethod.GetXmlTestsList, new[] { $"projectId={projectId}" });
            var responseXmlDoc = XmlUtil.GetXmlWithoutRootNode(responseXmlStream);
            var testsList = XmlUtil.GetTestsFromXml<test>(responseXmlDoc);

            Log.Step("Check that list sorted");
            var projectPage = new ProjectPage();
            Asserts.IsTrue(projectPage.IsTestsSorted(false), "Checking that list sorted");

            Log.Step("Check that all displayed projects is exists in the api response");
            Asserts.IsListContains(testsList, projectPage.GetTestsOnCurrentView(), "Checking that projects is exists in the api response");

            Log.Step("Navigate to main page, save number of opened tabs and click add project");
            Browser.NavigateToUrl(baseUrl);
            var tabsOpenedBeforeAddProject = Browser.GetTabs.Count;
            mainPage.ClickAddProject();

            Log.Step("Check that new tab opened");
            Asserts.AreNotEqual(tabsOpenedBeforeAddProject, Browser.GetTabs.Count, "Checking that new tab opened");

            Log.Step("Choose opened tab and add project");
            Browser.ChooseOtherTab(Browser.GetTabs.Last());
            var addProjectPage = new AddProjectPage();
            var addedProjectName = Randoms.GetRandomString(addedProjectNameLength);
            addProjectPage.SetProjectName(addedProjectName);
            addProjectPage.ClickSaveProject();

            Log.Step("Check that alert message displayed and valid");
            Asserts.AreEqual($"Project {addedProjectName} saved", addProjectPage.GetAlertMessage, "Checking that alert message is valid");

            Log.Step("Close opened tab and refresh page");
            Browser.Instance.Close();
            Browser.Refresh();

            Log.Step("Check that tab closed");
            Asserts.AreEqual(tabsOpenedBeforeAddProject, Browser.GetTabs.Count, "Checking that active tab closed");

            Log.Step("Check that project added");


            var res = CloudinaryUtil.UploadPhoto("Name", new MemoryStream(Browser.TakeScreenshot()));
            Debug.Write(res.Url);
        }
    }
}
