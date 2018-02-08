package forms;

import forms.algorithm.Side;
import org.openqa.selenium.By;
import org.openqa.selenium.support.ui.WebDriverWait;
import webdriver.Browser;
import webdriver.elements.Table;

public class EnemyTable extends Table {

    private int rowCount = getRowCount();
    private int columnCount = getColumnCount();
    private final int waitForClick = 3;

    public EnemyTable(By locator, String name) {
        super(locator, name);
    }

    public boolean isOutsideField(Side side, int clickingValue) {
        switch (side) {
            case Top:
            case Left:
                return clickingValue < 0;
            case Right:
                return clickingValue >= columnCount;
            case Bottom:
                return clickingValue >= rowCount;
            default:
                return true;
        }
    }

    public boolean isKill(int row, int column) {
        return getCellClassName(row, column).contains("cell__done");
    }

    public boolean isHit(int row, int column) {
        return getCellClassName(row, column).contains("cell__hit");
    }

    public boolean isCellEmpty(int row, int column) {
        return !isHit(row, column) && !getCellClassName(row, column).contains("cell__miss");
    }

    @Override
    public void clickCell(int row, int column) {
        WebDriverWait wait = new WebDriverWait(Browser.getInstance().getDriver(), waitForClick);
        wait.until((driver -> !getElement().findElementByXPath("../../..").getAttribute("class").contains("wait")));
        super.clickCell(row, column);
    }
}
