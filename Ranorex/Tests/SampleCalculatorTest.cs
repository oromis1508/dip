using FormsAndLocators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxFramework;
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
            _buttonsToPress = TestContext.DataRow["Press"].ToString();
            _expectedValue = TestContext.DataRow["ExpectedValue"].ToString();
        }

        [TestMethod]
        [DeploymentItem("TestsData\\TestData.xlsx")]
        [DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\TestData.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", "" + "CalcTest$", DataAccessMethod.Sequential)]
        public override void RunTest()
        {
            LogStep(1, "Start application");
            StartApplication("Calculator");

            LogStep(2, $"Press {_buttonsToPress}");
            var mainForm = new MainForm();
            mainForm.PressButtons(_buttonsToPress);

            LogStep(3, $"Assert is {_expectedValue}");
            //soft assert example
            Assert.SoftAreEqual(_expectedValue, mainForm.GetResult(), "SA actual values is not as expected");

            //assert batch example
            Assert.Batch(
                () => Assert.AreEqual(_expectedValue, mainForm.GetResult(), "AB1 actual values is not as expected"),
                () => Assert.AreEqual(_expectedValue, mainForm.GetResult(), "AB2 actual values is not as expected")
            );

            //assert example
            Assert.AreEqual(_expectedValue, mainForm.GetResult(), "A actual values is not as expected");
        }
    }
}
