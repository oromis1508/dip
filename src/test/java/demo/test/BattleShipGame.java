package demo.test;

import demo.test.forms.Side;
import demo.test.forms.Table;
import org.openqa.selenium.By;
import webdriver.BaseForm;
import webdriver.elements.Label;

public class BattleShipGame extends BaseForm{

    //if win
    //Игра закончена. Поздравляем, вы победили!

    //if lose
    //Игра закончена. Вы проиграли.

    //if leave
    //Противник покинул игру. Дальнейшая игра невозможна.

    //if server in not available
    //Сервер недоступен.

    //if game error
    //Непредвиденная ошибка. Дальнейшая игра невозможна.

    private final static String enemyTableXPath = "//div[contains(@class, 'battlefield__rival')]//table[contains(@class, 'battlefield-table')]";
    private Table enemyTable = new Table(By.xpath(enemyTableXPath), "Table with enemy ships");
    //private Table MyShipsPlace = new Table(By.xpath("//div[contains(@class, 'battlefield__self.battlefield__wait')]//table[contains(@class, 'battlefield-table')]"), "Table with my ships");

    private Label lblGameMessage = new Label(By.xpath("//div[contains(@class, 'notification-message')]"), "Message about game results");

    private int rowCount = 10;
    private int columnCount = 10;

    private final int TIMEOUT_FOR_ENEMY = 30;
    private int clickingRow = 3;
    private int clickingColumn = 0;

    protected BattleShipGame() {
        super(By.xpath(enemyTableXPath), "Enemy field");
    }

    public String PlayGame() {

        while(GameEndMessage().equals("")) {
            if(lblGameMessage.getElement().findElementByXPath("..").getAttribute("class").contains("none"))
                continue;

            if(lblGameMessage.getText().contains("Ваш ход.") || lblGameMessage.getText().contains("ваш ход.")) {
                    clickStrategy();
                }
            }
            return GameEndMessage();
    }

    private void clickStrategy() {

        clickCell(clickingRow, clickingColumn);

        if(isHit(clickingRow, clickingColumn)) {
            if(isKill(clickingRow, clickingColumn)) {
                clickingRow += 4;
            } else {
                if(!isOutsideField(Side.Left, clickingColumn - 1)) {
                    if(isCellEmpty(clickingRow, clickingColumn - 1)) {
                        enemyTable.clickCell(clickingRow, clickingColumn - 1);
                        return;
                    }
                }

                if(!isOutsideField(Side.Right, clickingColumn + 1)) {
                    if(isCellEmpty(clickingRow, clickingColumn + 1)) {
                        enemyTable.clickCell(clickingRow, clickingColumn + 1);
                        return;
                    }
                }

                if(!isOutsideField(Side.Top, clickingRow - 1)) {
                    if(isCellEmpty(clickingRow - 1, clickingColumn)) {
                        enemyTable.clickCell(clickingRow - 1, clickingColumn);
                        return;
                    }
                }


                if(!isOutsideField(Side.Bottom, clickingRow + 1)) {
                    if(isCellEmpty(clickingRow + 1, clickingColumn)) {
                        enemyTable.clickCell(clickingRow + 1, clickingColumn);
                        return;
                    }
                }
            }
        }
        else {

            clickingRow += 4;

            if(isOutsideField(Side.Bottom, clickingRow)) {
                clickingRow -= 13;
                clickingColumn++;
            }

            if(isOutsideField(Side.Top, clickingRow)) {
                clickingRow += 4;
            }

            //After first passing the field
            if(clickingColumn == 9 && clickingRow == 6) {
                clickingColumn = 0;
                clickingRow = 1;
            }

            //After second passing the field
            if(clickingColumn == 9 && clickingRow == 8) {
                clickingColumn = 0;
                clickingRow = 0;
            }

            //After third passing the field
            if(clickingColumn == 9 && clickingRow == 7) {
                clickingColumn = 0;
                clickingRow = 2;
            }
        }

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
        return enemyTable.getCellClass(row, column).contains("cell__done");
    }

    public void clickCell(int row, int column) {
        if(isCellEmpty(row, column)) {
            enemyTable.clickCell(row, column);
        }
    }

    public boolean isHit(int row, int column) {
        String cellStatus = enemyTable.getCellClass(row, column);
        return cellStatus.contains("cell__hit");
    }

    public boolean isCellEmpty(int row, int column) {
        String cellStatus = enemyTable.getCellClass(row, column);
        return !isHit(row, column) || !cellStatus.contains("cell__miss");
    }

    private String GameEndMessage() {

        if(!lblGameMessage.isPresent(10))
            return "";

        System.out.println(lblGameMessage.getElement().findElementByXPath("..").getAttribute("class"));

        if(lblGameMessage.getElement().findElementByXPath("..").getAttribute("class").contains("none"))
            return "";

        String infoMessage = lblGameMessage.getText();
        System.out.println(infoMessage);
        if (infoMessage.equals(getLoc("loc.battleship.enemyLeave")) ||
                infoMessage.equals(getLoc("loc.battleship.gameError")) ||
                infoMessage.equals(getLoc("loc.battleship.serverError")) ||
                infoMessage.equals(getLoc("loc.battleship.gameWin")) ||
                infoMessage.equals(getLoc("loc.battleship.gameLose"))) {
            return lblGameMessage.getText();
        } else
            return "";
    }
}
