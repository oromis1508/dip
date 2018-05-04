using System.Collections.Generic;
using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;
using smart.framework.Utils.Support;
using TestProject.Enums;
using TestProject.Models;

namespace TestProject.Pages
{
    public class ProjectPage : BaseForm
    {
        private readonly Table _testsTable = new Table(By.Id("allTests"), "table with all project's tests");
        private readonly Button _btnAddTest = new Button(By.XPath("//button[contains(@data-target, 'addTest')]"), "add test button");

        private readonly By _testNameTextLocator = By.TagName("a");
        private readonly By _testResultTextLocator = By.TagName("span");

        private const string TestNameLinkTemplate = "//td//a[text()='{0}']";

        public ProjectPage() : base(By.Id("allTests"), "Project page")
        {
        }

        public List<test> GetTestsOnCurrentView()
        {
            var testsList = new List<test>();
            for (var i = 1; i < _testsTable.GetRowCount(); i++)
            {
                var test = new test()
                {
                    name = _testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestName), _testNameTextLocator),
                    method = _testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestMethod)),
                    status = _testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestResult), _testResultTextLocator),
                    startTime = _testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestStart)),
                    endTime = _testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestEnd)),
                    duration = _testsTable.GetCellText(new Cell(i, (int) TableHeaders.Duration))
                };
                testsList.Add(test);
            }

            return testsList;
        }

        public bool IsTestsSorted(bool increase)
        {
            var startDates = _testsTable.GetColumnText((int) TableHeaders.TestStart);
            return ListUtil.IsListSorted(startDates, increase);
        }

        public void ClickAddButton() => _btnAddTest.Click();

        private static Link GetTestLink(string testName) => new Link(By.XPath(string.Format(TestNameLinkTemplate, testName)),
            "link for move to test");

        public bool IsTestExist(string testName) => GetTestLink(testName).IsPresent();

        public void MoveToTest(string testName) => GetTestLink(testName).Click();
    }
}
