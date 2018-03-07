using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using FormsAndLocators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxFramework.BaseEntities;
using RxFramework.Utils;
using Assert = RxFramework.Utils.Assert;

namespace Tests.TestClasses
{
    [TestClass]
    public class NotepadPlusPlusTest : BaseTest
    {
        private string _inputedHtmlText;
        private string _inputedTagName;
        private string _inputedHtmlInnerText;
        private string _saveFilePath;
        private int _saveFileNameMaxLenght;

        [TestInitialize]
        public void Init()
        {
            _inputedHtmlText = TestContext.DataRow["Input"].ToString();
            _inputedTagName = Regex.Match(_inputedHtmlText, "^.*</(.*)>.*$").Groups[1].Value;
            _inputedHtmlInnerText = TestContext.DataRow["ExpectedValue"].ToString();
            _saveFilePath = ConfigurationManager.AppSettings["saveFilePath"];
            _saveFileNameMaxLenght = int.Parse(ConfigurationManager.AppSettings["maxLenghtFileName"]);
        }

        [TestMethod]
        [DeploymentItem("TestsData\\TestData.xlsx")]
        [DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\TestData.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", "" + "CalcTest$", DataAccessMethod.Sequential)]
        public override void RunTest()
        {
            LogStep(1, "Start application");
            StartApplication("AppName");

            LogStep(2, "Verify that the application has been started");
            var textEditor = new TextEditor();
            //Assert.IsTrue(textEditor.IsFormExist(), "Application started");

            LogStep(3, $"Open a new tab by the menu File - New");
            var beforeOpenNewTabName = textEditor.ActiveTabName;
            var applicationMenu = new ApplicationMenu();
            applicationMenu.OpenMenu(new []{"File", "New"});

            LogStep(4, $"Verify that a new tab has been opened and active");
            var afterOpenNewTabName = textEditor.ActiveTabName;
            Assert.AreNotEqual(beforeOpenNewTabName, afterOpenNewTabName, "Opened new tab");

            LogStep(5, $"Input text in the editor");
            textEditor.PrintText(_inputedHtmlText);

            LogStep(6, $"Verify that the text has been inputed");
            Assert.AreEqual(_inputedHtmlText, textEditor.DisplayedText, "Text displayed correctly");

            LogStep(7, $"Save file by the menu File - Save As");
            applicationMenu.OpenMenu(new [] {"File", "Save As"});
            var fileName = $"{Randoms.String(_saveFileNameMaxLenght)}.html";
            var fullFilePath = Path.Combine(_saveFilePath, fileName);
            new FileSave().SaveFile(fullFilePath);

            LogStep(8, $"Verify that the created file exists on the drive");
            Assert.IsTrue(FileUtil.IsFileStatus(fullFilePath, WatcherChangeTypes.Created), $"File {fullFilePath} exists");

            LogStep(9, $"Launch in IE the saved file by the menu Run - Launch in IE");
            applicationMenu.OpenMenu(new []{"Run", "Launch in IE" });

            LogStep(10, $"Verify that the text displayed without tags");
            var ieWindow = new IEWindow();
            var tagValue = ieWindow.GetPageTagValue(fileName, _inputedTagName);
            Assert.AreEqual(_inputedHtmlInnerText, tagValue, $"Text {_inputedHtmlInnerText} displayed without tag <{_inputedTagName}>");

            LogStep(11, $"Close applications and delete created file");
            ieWindow.CloseApp();
            applicationMenu.OpenMenu(new []{ "File", "Exit" });
            File.Delete(fullFilePath);

            LogStep(12, $"Verify that file has been deleted and applications has been closed");
            Assert.IsTrue(FileUtil.IsFileStatus(fullFilePath, WatcherChangeTypes.Deleted), "The created file not exist");
            Assert.IsFalse(ieWindow.IsFormExist(), "The IE window has been closed");
            Assert.IsFalse(textEditor.IsFormExist(), "The notepad window has been closed");
        }
    }
}
