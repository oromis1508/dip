package demo.test;

import demo.test.forms.Table;
import org.openqa.selenium.By;
import webdriver.BaseForm;
import webdriver.elements.Label;

public class BattleShipGame extends BaseForm{

    private final static String ENEMY_LEAVE_XPATH = String.format("//div[contains(@class, '%s')]", getLoc("loc.battleship.enemyLeave"));
    private Label enemyLeave = new Label(ENEMY_LEAVE_XPATH, "enemy leave message");
    //Начать новую игру
    //Игра закончена. Вы проиграли.
    private boolean gameEnd = false;
    private Table enemyTable = new Table(By.xpath("//div[contains(@class, 'battlefield__rival')]//table[contains(@class, 'battlefield-table')]"), "Table with enemy ships");
    private Table MyShipsPlace = new Table(By.xpath("//div[contains(@class, 'battlefield__self.battlefield__wait')]//table[contains(@class, 'battlefield-table')]"), "Table with my ships");

    private int rowCount = enemyTable.getRowCount();
    private int columnCount = enemyTable.getColumnCount();

    private final int TIMEOUT_FOR_ENEMY = 30;
    private int clickingRow = 3;
    private int clickingColumn = 0;

    protected BattleShipGame(By locator, String formTitle) {
        super(locator, formTitle);
    }

    public void PlayGame() {
        try {
            while(!gameEnd || !IsEnemyLeave()) {
                if(MyShipsPlace.isPresent(TIMEOUT_FOR_ENEMY)) {
                    clickStrategy();
                }
            }
        } catch (EnemyLeaveException e) {
            logger.warn("Opponent leave game");
        }
    }

    private void clickStrategy() {

        if(IsHit(clickingRow, clickingColumn)) {
            if()
        }
        else {

            clickingRow += 4;

            if(clickingRow >= rowCount) {
                clickingRow -= 13;
                clickingColumn++;
            }

            if(clickingRow < 0) {
                clickingRow += 4;
            }

            if(clickingColumn >= columnCount) {
                clickingColumn = 0;
                clickingRow = 1;
            }


        }

    }

    public void ClickCell(int row, int column) {
        if(IsCellEmpty(row, column)) {
            enemyTable.clickCell(row, column);
        }
    }

    public boolean IsHit(int row, int column) {
        String cellStatus = enemyTable.getCellClass(row, column);
        return cellStatus.contains("battlefield-cell__hit");
    }

    public boolean IsCellEmpty(int row, int column) {
        String cellStatus = enemyTable.getCellClass(row, column);
        return !IsHit(row, column) || !cellStatus.contains("battlefield-cell__miss");
    }

    private boolean IsEnemyLeave() throws EnemyLeaveException {
        if(enemyLeave.isPresent()) {
            throw new EnemyLeaveException();
        }
        return false;
    }
}
