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
        private int _saveFileNameMaxLenght;
        private string _fileName;
        private string _fullFilePath;

        [TestInitialize]
        public void Init()
        {
            _inputedHtmlText = TestContext.DataRow["Input"].ToString();
            _inputedTagName = Regex.Match(_inputedHtmlText, "^.*</(.*)>.*$").Groups[1].Value;
            _inputedHtmlInnerText = TestContext.DataRow["ExpectedValue"].ToString();
            _saveFileNameMaxLenght = int.Parse(ConfigurationManager.AppSettings["maxLenghtFileName"]);

            var saveFilePath = ConfigurationManager.AppSettings["saveFilePath"];
            _fileName = $"{Randoms.String(_saveFileNameMaxLenght)}.html";
            _fullFilePath = Path.Combine(saveFilePath, _fileName);
        }

        [TestMethod]
        [DeploymentItem("TestsData\\TestData.xlsx")]
        [DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\TestData.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", "" + "NotepadPlusPlusTest$", DataAccessMethod.Sequential)]
        public override void RunTest()
        {
            LogStep(1, "Start application");
            StartApplication("notepad++.exe");

            LogStep(2, "Verify that the application has been started");
            var textEditor = new TextEditor();
            Assert.IsTrue(textEditor.IsFormExist(), "Application started");

            LogStep(3, $"Open a new tab by the menu File - New");
            var beforeOpenNewTabName = textEditor.ActiveTabName;
            var applicationMenu = new ApplicationMenu();
            applicationMenu.File.New.OpenMenu();

            LogStep(4, $"Verify that a new tab has been opened and active");
            var afterOpenNewTabName = textEditor.ActiveTabName;
            Assert.AreNotEqual(beforeOpenNewTabName, afterOpenNewTabName, "Opened new tab");

            LogStep(5, $"Input text in the editor");
            textEditor.PrintText(_inputedHtmlText);

            LogStep(6, $"Verify that the text has been inputed");
            Assert.AreEqual(_inputedHtmlText, textEditor.DisplayedText, "Text displayed correctly");

            LogStep(7, $"Save file by the menu File - Save As");
            applicationMenu.File.SaveAs.OpenMenu();
            new FileSave().SaveFile(_fullFilePath);

            LogStep(8, $"Verify that the created file exists on the drive");
            Assert.IsTrue(FileUtil.IsFileHasCertainStatus(_fullFilePath, WatcherChangeTypes.Created), $"File {_fullFilePath} exists");

            LogStep(9, $"Launch in IE the saved file by the menu Run - Launch in IE");
            applicationMenu.Run.LaunchInIe.OpenMenu();

            LogStep(10, $"Verify that the text displayed without tags");
            var ieWindow = new IEWindow();
            var tagValue = ieWindow.GetPageTagValue(_fileName, _inputedTagName);
            Assert.AreEqual(_inputedHtmlInnerText, tagValue, $"Text {_inputedHtmlInnerText} displayed without tag <{_inputedTagName}>");

            LogStep(11, $"Close applications and delete created file");
            ieWindow.CloseApp();
            applicationMenu.File.Exit.OpenMenu();
            File.Delete(_fullFilePath);

            LogStep(12, $"Verify that file has been deleted and applications has been closed");
            Assert.Batch(
                () => Assert.IsTrue(FileUtil.IsFileHasCertainStatus(_fullFilePath, WatcherChangeTypes.Deleted), "The created file not exist"),
                () => Assert.IsFalse(ieWindow.IsFormExist(), "The IE window has been closed"),
                () => Assert.IsFalse(textEditor.IsFormExist(), "The notepad window has been closed"));
        }
    }
}
