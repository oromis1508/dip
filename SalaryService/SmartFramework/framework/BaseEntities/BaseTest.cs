using System.Diagnostics;
using demo.framework.Utils;
using demo.framework.Utils.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace demo.framework.BaseEntities
{
    [TestClass]
    public class BaseTest : BaseEntity
    {
       [TestInitialize]
        public void SetUp()
       {
            Browser.GetInstance();
            Browser.Driver.Navigate().GoToUrl(Configuration.GetBaseUrl());
        }

        [TestCleanup]
        public void TearDown()
        {
            var processes = Process.GetProcessesByName(Configuration.GetBrowser());
            foreach (var process in processes)
            {
                process.Kill();
            }
            Browser.Driver.Quit();
        }
    }
}
