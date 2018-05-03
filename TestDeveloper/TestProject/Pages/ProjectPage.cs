using System.Collections.Generic;
using OpenQA.Selenium;
using smart.framework.BaseEntities;
using smart.framework.Elements;
using smart.framework.Utils.Support;
using TestProject.Entities;
using TestProject.Enums;

namespace TestProject.Pages
{
    public class ProjectPage : BaseForm
    {
        private Table testsTable = new Table(By.Id("allTests"), "table with all project's tests");

        private By testNameTextLocator = By.TagName("a");
        private By testResultTextLocator = By.TagName("span");

        public ProjectPage() : base(By.Id("allTests"), "Project page")
        {
        }

        public List<test> GetTestsOnCurrentView()
        {
            var testsList = new List<test>();
            for (var i = 1; i < testsTable.GetRowCount(); i++)
            {
                var test = new test()
                {
                    name = testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestName), testNameTextLocator),
                    method = testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestMethod)),
                    status = testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestResult), testResultTextLocator),
                    startTime = testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestStart)),
                    endTime = testsTable.GetCellText(new Cell(i, (int) TableHeaders.TestEnd)),
                    duration = testsTable.GetCellText(new Cell(i, (int) TableHeaders.Duration))
                };
                testsList.Add(test);
            }

            return testsList;
        }

        public bool IsTestsSorted(bool increase)
        {
            var startDates = testsTable.GetColumnText((int) TableHeaders.TestStart);
            return ListUtil.IsListSorted(startDates, increase);
        }
    }
}
