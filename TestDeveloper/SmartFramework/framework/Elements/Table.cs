using System.Collections.Generic;
using OpenQA.Selenium;

namespace smart.framework.Elements
{
    public class Table : BaseElement
    {
        public Table(By locator, string name) : base(locator, name)
        {
        }

        private IWebElement GetRow(int row)
        {
            return GetElement().FindElements(By.TagName("tr"))[row];
        }

        private IWebElement GetCellElement(Cell cell)
        {
            return GetRow(cell.Row).FindElements(By.TagName("td"))[cell.Column];
        }

        public int GetRowCount()
        {
            return GetElement().FindElements(By.TagName("tr")).Count;
        }

        public int GetColumnCount()
        {
            return GetRow(0).FindElements(By.TagName("td")).Count;
        }

        public string GetCellText(Cell cell, By childElement = null) => childElement == null ? GetCellElement(cell).Text : GetCellElement(cell).FindElement(childElement).Text;

        public List<string> GetColumnText(int column, int firstRow = 0, By childElement = null)
        {
            var result = new List<string>();

            for (var i = firstRow; i < GetColumnCount(); i++)
            {
                result.Add(GetCellText(new Cell(firstRow, column), childElement));
            }

            return result;
        }
    }
}
