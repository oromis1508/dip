using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ranorex;
using RxFramework.Utils;
using Assert = RxFramework.Utils.Assert;

namespace RxFramework.BaseEntities
{
    [TestClass]
    public abstract class BaseTest
    {
        public virtual TestContext TestContext { get; set; }
        private readonly List<string> _startedApps = new List<string>();

        [TestInitialize]
        public void Initialize()
        {
            Logger.Instance.Info("Test Started");
            Assert.SoftAssertBegin();
        }

        [TestMethod]
        public abstract void RunTest();

        [TestCleanup]
        public void CleanUp()
        {
            KillAllStartedProcesses();
            Assert.SoftAssertEnd();
            Logger.Instance.Info("Test Finished");
        }

        protected void StartApplication(string app)
        {
            var appName = ConfigurationManager.AppSettings[app];
            Host.Local.RunApplication(appName);
            _startedApps.Add(app);
            Logger.Instance.Info($"Application {app} started");
        }

        protected void AddAppToKillAfterTest(string appName) => _startedApps.Add(appName);

        protected void KillAllStartedProcesses()
        {
            foreach (var startedApp in _startedApps)
            {
                try
                {
                    var proc = Process.GetProcessesByName(startedApp);
                    foreach (var process in proc) process.Kill();
                }
                catch (Win32Exception e)
                {
                    Logger.Instance.Info($"Not all proccesses killed. The error is {e}");
                }
            }
        }

        protected void LogStep(int number, string step) => Logger.Instance.Info($"--== step {number}. {step}==--");
    }
}
