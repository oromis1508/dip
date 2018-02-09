using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;
using OpenQA.Selenium;
using demo.framework;

namespace demo.framework
{
    [TestFixture]
    public class BaseTest : BaseEntity
    {
       [SetUp]
        public void SetUp()
       {
            Browser.GetInstance();
            Browser.GetDriver().Navigate().GoToUrl(Configuration.GetBaseUrl());
        }

        [TearDown]
        public void TearDown()
        {
            var processes = Process.GetProcessesByName(Configuration.GetBrowser());
            foreach (var process in processes)
            {
                process.Kill();
            }
            Browser.GetDriver().Quit();
        }
    }
}
