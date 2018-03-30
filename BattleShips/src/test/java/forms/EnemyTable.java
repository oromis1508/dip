package forms;

import forms.algorithm.Cell;
import forms.algorithm.CellStatus;
import org.openqa.selenium.By;
import org.openqa.selenium.support.ui.WebDriverWait;
import webdriver.Browser;
import webdriver.elements.Table;

import java.util.ArrayList;
import java.util.List;

public class EnemyTable extends Table {

    private int rowCount = getRowCount();
    private int columnCount = getColumnCount();
    private final int waitForClick = 3;

    private List<Cell> emptyCells = new ArrayList<>();

    public EnemyTable(By locator, String name) {
        super(locator, name);
        initList();
    }

    private void initList() {
        for (int i=0; i<rowCount; i++)
            for (int j=0; j<columnCount; j++)
                emptyCells.add(new Cell(i, j));
    }

    public boolean isCellStatus(Cell cell, CellStatus status) {
        return getCellClassName(cell).contains(status.toString());
    }

    public boolean isCellExist(Cell cell) {
        return cell.getRow() >= 0 && cell.getColumn() >= 0 && cell.getRow() < rowCount && cell.getColumn() < columnCount;
    }

    public boolean isCellEmpty(Cell cell) {
        return emptyCells.contains(cell);
    }

    public void removeNotEmptyCell(Cell cell) {
        emptyCells.remove(cell);
    }

    public Cell getEmptyCell(int number) {
        return emptyCells.get(number);
    }

    public int getNumEmptyCells() {
        return emptyCells.size();
    }

    @Override
    public void clickCell(Cell cell) {
        if(isCellEmpty(cell)) {
            removeNotEmptyCell(cell);
            WebDriverWait wait = new WebDriverWait(Browser.getInstance().getDriver(), waitForClick);
            wait.until((driver -> !getElement().findElementByXPath("../../..").getAttribute("class").contains("wait")));
            super.clickCell(cell);
            wait.until((driver -> !getCellClassName(cell).contains(CellStatus.EMPTY.toString())));
        }
    }
}
