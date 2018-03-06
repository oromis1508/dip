using FormsAndLocators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxFramework;
using RxFramework.Elements;
using Assert = RxFramework.Assert;

namespace Tests
{
    [TestClass]
    public class SampleCalculatorTest : BaseTest
    {
        private string _buttonsToPress;
        private string _expectedValue;
        [TestInitialize]
        public void Init()
        {
            _buttonsToPress = TestContext.DataRow["Input"].ToString();
            _expectedValue = TestContext.DataRow["ExpectedValue"].ToString();

        }

        [TestMethod]
        [DeploymentItem("TestsData\\TestData.xlsx")]
        [DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\TestData.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", "" + "CalcTest$", DataAccessMethod.Sequential)]
        public override void RunTest()
        {
            LogStep(1, "Start application");
            StartApplication("AppName");

            LogStep(2, $"Press menu File - New");
            var applicationMenu = new ApplicationMenu();
            applicationMenu.OpenMenu("File - New", " - ");

            LogStep(3, $"Input text");
        }
    }
}
