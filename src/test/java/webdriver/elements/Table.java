package webdriver.elements;

import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

public class Table extends BaseElement{

    public Table(final By locator, final String name) {
        super(locator, name);
    }

    public Table(String string, String name) {
        super(string, name);
    }

    public Table(By locator) {
        super(locator);
    }

    @Override
    protected String getElementType() {
        return "Table";
    }

    public int getRowCount() {
        return getElement().findElements(By.tagName("tr")).size();
    }

    public WebElement getRow(int row) {
        return getElement().findElements(By.tagName("tr")).get(row);
    }

    public int getColumnCount() {
        return getRow(0).findElements(By.tagName("td")).size();
    }

    public void clickCell(int row, int column) {
        WebElement cell = getRow(row).findElements(By.tagName("td")).get(column);
        cell.click();
    }

    public String getCellClassName(int row, int column) {
        return getRow(row).findElements(By.tagName("td")).get(column).getAttribute("class");
    }
}
