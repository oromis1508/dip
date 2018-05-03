using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using smart.framework.Utils.TestUtils;

namespace smart.framework.BaseEntities
{
    [TestClass]
    public class BaseTest : BaseEntity
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        [Priority(1)]
        public void SetUp()
        {
            Browser.Instance.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TearDown()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                Trace.Write("Done");
            }

            var processes = Process.GetProcessesByName(Configuration.LocalBrowser);
            foreach (var process in processes)
            {
                process.Kill();
            }
            Browser.Instance.Quit();
        }
    }
}
