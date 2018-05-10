using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smart.framework.Utils.Services;
using smart.framework.Utils.TestUtils;

namespace smart.framework.BaseEntities
{
    [TestClass]
    public class BaseTest : BaseEntity
    {
        public TestContext TestContext { get; set; }
        private string _railProjectId;
        private string _railCaseId;
        private string _exceptionMessage;

        [TestInitialize]
        [Priority(1)]
        public void SetUp()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                var ex = (Exception) args.ExceptionObject;
                _exceptionMessage += $"{ex.Message}\n";
            };

            _railProjectId = Configuration.TestRailProjectId;
            _railCaseId = Configuration.TestRailCaseId;
            Browser.Instance.Manage().Window.Maximize();
        }

        [TestCleanup]
        [Priority(2)]
        public void TearDown()
        {
            var runId = TestRailUtil.AddRun(_railProjectId);
            var testResult = TestRailResultCodes.Passed;
            var comment = "Test completed";
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                testResult = TestRailResultCodes.Failed;
                var photoUrl = CloudinaryUtil.UploadPhoto("Name", new MemoryStream(Browser.TakeScreenshot())).Url;
                comment = $"{_exceptionMessage}{TestRailUtil.GetImageStringForAddToComment(photoUrl)}";
            }

            TestRailUtil.AddResultForCase(runId, _railCaseId, testResult, comment);

            var processes = Process.GetProcessesByName(Configuration.LocalBrowser);
            foreach (var process in processes)
            {
                process.Kill();
            }
            Browser.Instance.Quit();
        }
    }
}
