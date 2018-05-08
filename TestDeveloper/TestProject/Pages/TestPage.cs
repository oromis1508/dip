using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;
using smart.framework.Utils.Support;
using TestProject.Models;

namespace TestProject.Pages
{
    public class TestPage : BaseForm
    {
        private readonly ListItems _testInfoItems = new ListItems(By.XPath("//div[contains(@class, 'panel') and .//div[contains(text(), 'Common info')]]"), "list of test attributes");

        private const string ProjectNameSubLocator = "//div[contains(@class, 'list-group-item') and .//h4[contains(text(), 'Project name')]]//p";
        private const string TestNameSubLocator = "//div[contains(@class, 'list-group-item') and .//h4[contains(text(), 'Test name')]]//p";
        private const string TestMethodNameSubLocator = "//div[contains(@class, 'list-group-item') and .//h4[contains(text(), 'Test method name')]]//p";
        private const string StatusSubLocator = "//div[contains(@class, 'list-group-item') and .//h4[contains(text(), 'Status')]]//span";
        private const string StartTimeSubLocator = "//div[contains(@class, 'list-group-item') and .//h4[contains(text(), 'Time info')]]//p[contains(text(), 'Start time')]";
        private const string EndTimeSubLocator = "//div[contains(@class, 'list-group-item') and .//h4[contains(text(), 'Time info')]]//p[contains(text(), 'End time')]";
        private const string EnvironmentSubLocator = "//div[contains(@class, 'list-group-item') and .//h4[contains(text(), 'Environment')]]//p";
        private const string BrowserSubLocator = "//div[contains(@class, 'list-group-item') and .//h4[contains(text(), 'Browser')]]//p";

        private readonly WebImage _attachedImage = new WebImage(By.ClassName("thumbnail"), "attached image");

        public TestPage() : base(By.Id("failReason0_chosen"), "test page")
        {
        }

        private string GetSubItemText
            (string locator) => _testInfoItems.GetSubItemText(By.XPath(locator));

        public AddedTest GetAddedTestObject()
        {
            var startTime = GetSubItemText(StartTimeSubLocator);
            var endTime = GetSubItemText(EndTimeSubLocator);

            return new AddedTest
            {
                TestName = GetSubItemText(TestNameSubLocator),
                TestMethod = GetSubItemText(TestMethodNameSubLocator),
                Status = GetSubItemText(StatusSubLocator),
                StartTime = Regex.Match(startTime, ".*Start time: (.*)\\..*").Groups[1].Value,
                EndTime = Regex.Match(endTime, ".*End time: (.*)\\..*").Groups[1].Value,
                Environment = GetSubItemText(EnvironmentSubLocator),
                Browser = GetSubItemText(BrowserSubLocator)
            };
        }

        public string ProjectName => GetSubItemText(ProjectNameSubLocator);

        public bool IsImageValid(Bitmap sourceImage)
        {
            var base64ImageCode = Regex.Match(_attachedImage.ImageBase64, ".*,(.*)").Groups[1].Value;
            var stream = new MemoryStream(Convert.FromBase64String(base64ImageCode));
            var postedImage = new Bitmap(Image.FromStream(stream));
            return BitmapUtil.ArePixelsEqual(sourceImage, postedImage, "Compare attached image to test");
        }
    }
}
