package forms.algorithm;

import forms.EnemyTable;
import org.openqa.selenium.By;
import org.openqa.selenium.TimeoutException;
import webdriver.BaseForm;
import webdriver.elements.Label;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class BattleShipGame extends BaseForm{

    private final static String enemyTableXPath = "//div[contains(@class, 'battlefield__rival')]//table[contains(@class, 'battlefield-table')]";
    private EnemyTable enemyTable = new EnemyTable(By.xpath(enemyTableXPath), "Table with enemy ships");

    private Label lblMyMove = new Label(By.xpath("//div[contains(@class, 'notification notification')and contains(@class, 'move-on')]"));
    private Label lblEndGameNotification = new Label(By.xpath("//div[contains(@class, 'notification notification')and not(contains(@class, 'move'))]"), "Notification about game end or error");

    private final int TIMEOUT_FOR_ENEMY = 60;
    private Cell clickingCell = new Cell(3,0);
    private List<Cell> killingShip = new ArrayList<>();
    private Side sideToFindShip = Side.Left;

    private boolean remainedOnlyOneDecks = false;
    private String messageWhenEnemyAFK = getLoc("loc.battleship.enemyAFK");

    public BattleShipGame() {
        super(By.xpath(enemyTableXPath), "Enemy field");
    }

    /**
     * When
     * TIMEOUT_FOR_ENEMY seconds enemy does nothing
     * or the game win/lose
     * or an error in game
     * or the enemy leave
     * the game will end
     */
    public void playGame() {
        try {
                while(lblMyMove.isPresent(TIMEOUT_FOR_ENEMY) && getGameEndMessage().equals(messageWhenEnemyAFK)) {
                    clickStrategy();
                }
        } catch (TimeoutException e) {
            logger.info("Game end with message: " + getGameEndMessage());
        } catch (Exception e) {
            logger.error("Error: \n" + e.getMessage());
        }
    }

    /**
     * The whole strategy of the battle
     */
    private void clickStrategy(){

        if(enemyTable.isCellStatus(clickingCell, CellStatus.HIT) && !enemyTable.isCellStatus(clickingCell, CellStatus.KILL)) {
            cellHit(clickingCell, sideToFindShip);
        }
        enemyTable.clickCell(clickingCell);

        if (enemyTable.isCellStatus(clickingCell, CellStatus.KILL)) {
            addHitCell(clickingCell);

            for (Cell cellOfShip : killingShip) {
                for (int i = 0, row = cellOfShip.getRow() - 1; i < 3; i++, row++) {
                    for (int j = 0, column = cellOfShip.getColumn() - 1; j < 3; j++, column++) {
                        enemyTable.removeNotEmptyCell(new Cell(row, column));
                    }
                }
            }
            killingShip.clear();
            sideToFindShip = Side.Left;
        }

        if(!enemyTable.isCellStatus(clickingCell, CellStatus.HIT) || enemyTable.isCellStatus(clickingCell, CellStatus.KILL)) {
            initNewCell();
        }
    }

    /**
     * Add cell to array if ship was hit
     */
    public void addHitCell(Cell cell) {
        if(!killingShip.contains(cell))
            killingShip.add(cell.getCopy());
    }

    /**
     * Call if ship was hit
     */
    private void cellHit(Cell clickingCell, Side side) {
        Cell hitCell = clickingCell.getCopy();
        for(int i=1; i<4 && enemyTable.isCellStatus(hitCell, CellStatus.HIT); i++) {

            addHitCell(hitCell);
            switch (side) {
                case Left:
                    hitCell.setColumn(hitCell.getColumn() - 1);
                    break;
                case Right:
                    hitCell.setColumn(hitCell.getColumn() + 1);
                    break;
                case Bottom:
                    hitCell.setRow(hitCell.getRow() + 1);
                    break;
                case Top:
                    hitCell.setRow(hitCell.getRow() - 1);
                    break;
            }
            if(enemyTable.isCellEmpty(hitCell)) {
                enemyTable.clickCell(hitCell);
            } else {
                break;
            }
        }
        sideToFindShip = side.next();
    }

    /**
     * Call for getting new cell
     * if was miss click of ship was kill
     */
    private void initNewCell() {

        if(remainedOnlyOneDecks) {
            Random random = new Random();
            int randomCell = random.nextInt(enemyTable.getNumEmptyCells());
            clickingCell = enemyTable.getEmptyCell(randomCell);
            return;
        }

        /**
         * After first passing field
         */
        if(clickingCell.getColumn() == 9 && clickingCell.getRow() == 6) {
            clickingCell = new Cell(1, 0);
            return;
        }

        /**
         * After second passing field
         */
        if(clickingCell.getColumn() == 9 && clickingCell.getRow() == 8) {
            remainedOnlyOneDecks = true;
            return;
        }

        clickingCell.setRow(clickingCell.getRow() + 4);

        if(!enemyTable.isCellExist(clickingCell)) {
            clickingCell.setRow(clickingCell.getRow() - 13);
            clickingCell.setColumn(clickingCell.getColumn() + 1);
        }

        if(!enemyTable.isCellExist(clickingCell)) {
            clickingCell.setRow(clickingCell.getRow() + 4);
        }
    }

    /**
     * To get message when error in game or game end, or enemy does nothing
     */
    public String getGameEndMessage(){

        if(lblEndGameNotification.isPresent()) {
            return lblEndGameNotification.getElement().findElement(By.tagName("div")).getText();
        } else {
            return messageWhenEnemyAFK;
        }
    }

    public boolean isGameWon() {
        return lblEndGameNotification.getAttribute("class").contains("win");
    }
}
