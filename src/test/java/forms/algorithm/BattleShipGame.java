package forms.algorithm;

import forms.EnemyTable;
import org.openqa.selenium.By;
import webdriver.BaseForm;
import webdriver.elements.Label;

public class BattleShipGame extends BaseForm{

    private final static String enemyTableXPath = "//div[contains(@class, 'battlefield__rival')]//table[contains(@class, 'battlefield-table')]";
    private EnemyTable enemyTable = new EnemyTable(By.xpath(enemyTableXPath), "Table with enemy ships");


    private Label lblMyMove = new Label(By.xpath("//div[contains(@class, 'notification notification')and not( contains(@class, 'move-off'))]"));
    private Label lblGameStarted = new Label(By.xpath("//div[contains(@class, 'game-started')]"));

    private Label lblGameMessage = new Label(By.xpath("//div[contains(@class, 'notification-message')]"), "Message about game results");
    private Label lblEnemyLeave = new Label(By.xpath(String.format("//div[contains(text(), '%s')]", getLoc("loc.battleship.enemyLeave"))), "Enemy leave message");
    private Label lblGameError = new Label(By.xpath(String.format("//div[contains(text(), '%s')]", getLoc("loc.battleship.gameError"))), "Game error message");
    private Label lblServerError = new Label(By.xpath(String.format("//div[contains(text(), '%s')]", getLoc("loc.battleship.serverError"))), "Server error message");
    private Label lblGameWin = new Label(By.xpath(String.format("//div[contains(text(), '%s')]", getLoc("loc.battleship.gameWin"))), "Game win message");
    private Label lblGameLose = new Label(By.xpath(String.format("//div[contains(text(), '%s')]", getLoc("loc.battleship.gameLose"))), "Game lose message");

    private final int TIMEOUT_FOR_ENEMY = 60;
    private int clickingRow = 3;
    private int clickingColumn = 0;

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
        lblGameStarted.waitForIsElementPresent();
        try {
                while(lblMyMove.isPresent(TIMEOUT_FOR_ENEMY) && getGameEndMessage().equals(messageWhenEnemyAFK)) {
                    clickStrategy();
                }
        } catch (Exception e) {
            e.printStackTrace();
            logger.info("Game end with message: " + getGameEndMessage());
        }
    }

    /**
     * The whole strategy of the battle
     */
    private void clickStrategy(){

        if(enemyTable.isCellEmpty(clickingRow, clickingColumn)) {
            
            enemyTable.clickCell(clickingRow, clickingColumn);
        }

        if(enemyTable.isHit(clickingRow, clickingColumn)) {
            if(!enemyTable.isKill(clickingRow, clickingColumn)) {

                /**
                 * If touched enemy ship, check the left cells
                 */
                if(!enemyTable.isOutsideField(Side.Left, clickingColumn - 1)) {
                    if(enemyTable.isCellEmpty(clickingRow, clickingColumn - 1)) {
                        
                        enemyTable.clickCell(clickingRow, clickingColumn - 1);
                        return;
                    } else if (enemyTable.isHit(clickingRow, clickingColumn - 1)) {
                        if(!enemyTable.isOutsideField(Side.Left, clickingColumn - 2)) {
                            if (enemyTable.isCellEmpty(clickingRow, clickingColumn - 2)) {
                                
                                enemyTable.clickCell(clickingRow, clickingColumn - 2);
                                return;
                            } else {
                                if (enemyTable.isHit(clickingRow, clickingColumn - 2)) {
                                    if(!enemyTable.isOutsideField(Side.Left, clickingColumn - 3)) {
                                        if(enemyTable.isCellEmpty(clickingRow, clickingColumn - 3)) {
                                            
                                            enemyTable.clickCell(clickingRow, clickingColumn - 3);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                /**
                 * If touched enemy ship, check the right cells
                 */
                if(!enemyTable.isOutsideField(Side.Right, clickingColumn + 1)) {
                    if(enemyTable.isCellEmpty(clickingRow, clickingColumn + 1)) {
                        
                        enemyTable.clickCell(clickingRow, clickingColumn + 1);
                        return;
                    } else if (enemyTable.isHit(clickingRow, clickingColumn + 1)) {
                        if(!enemyTable.isOutsideField(Side.Right, clickingColumn + 2)) {
                            if (enemyTable.isCellEmpty(clickingRow, clickingColumn + 2)) {
                                
                                enemyTable.clickCell(clickingRow, clickingColumn + 2);
                                return;
                            } else {
                                if (enemyTable.isHit(clickingRow, clickingColumn + 2)) {
                                    if(!enemyTable.isOutsideField(Side.Right, clickingColumn + 3)) {
                                        if(enemyTable.isCellEmpty(clickingRow, clickingColumn + 3)) {
                                            
                                            enemyTable.clickCell(clickingRow, clickingColumn + 3);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                /**
                 * If touched enemy ship, check the top cells
                 */
                if(!enemyTable.isOutsideField(Side.Top, clickingRow - 1)) {
                    if(enemyTable.isCellEmpty(clickingRow - 1, clickingColumn)) {
                        
                        enemyTable.clickCell(clickingRow - 1, clickingColumn);
                        return;
                    } else if (enemyTable.isHit(clickingRow - 1, clickingColumn)) {
                        if(!enemyTable.isOutsideField(Side.Top, clickingRow - 2)) {
                            if (enemyTable.isCellEmpty(clickingRow - 2, clickingColumn)) {
                                
                                enemyTable.clickCell(clickingRow - 2, clickingColumn);
                                return;
                            } else {
                                if (enemyTable.isHit(clickingRow - 2, clickingColumn)) {
                                    if(!enemyTable.isOutsideField(Side.Top, clickingRow - 3)) {
                                        if(enemyTable.isCellEmpty(clickingRow - 3, clickingColumn)) {
                                            
                                            enemyTable.clickCell(clickingRow - 3, clickingColumn);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                /**
                 * If touched enemy ship, check the bottom cells
                 */
                if(!enemyTable.isOutsideField(Side.Bottom, clickingRow + 1)) {
                    if(enemyTable.isCellEmpty(clickingRow + 1, clickingColumn)) {
                        
                        enemyTable.clickCell(clickingRow + 1, clickingColumn);
                        return;
                    } else if (enemyTable.isHit(clickingRow + 1, clickingColumn)) {
                        if(!enemyTable.isOutsideField(Side.Bottom, clickingRow + 2)) {
                            if (enemyTable.isCellEmpty(clickingRow + 2, clickingColumn)) {
                                
                                enemyTable.clickCell(clickingRow + 2, clickingColumn);
                                return;
                            } else {
                                if (enemyTable.isHit(clickingRow + 2, clickingColumn)) {
                                    if(!enemyTable.isOutsideField(Side.Bottom, clickingRow + 3)) {
                                        if(enemyTable.isCellEmpty(clickingRow + 3, clickingColumn)) {
                                            
                                            enemyTable.clickCell(clickingRow + 3, clickingColumn);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /**
         * After first passing the field
         */
        if(clickingColumn == 9 && clickingRow == 6) {
            clickingColumn = 0;
            clickingRow = 1;
            return;
        }

        /**
         * After second passing the field
         */
        if(clickingColumn == 9 && clickingRow == 8) {
            clickingColumn = 0;
            clickingRow = 0;
            return;
        }

        /**
         * After third passing the field
         */
        if(clickingColumn == 9 && clickingRow == 7) {
            clickingColumn = 0;
            clickingRow = 2;
            return;
        }

        clickingRow += 4;

        if(enemyTable.isOutsideField(Side.Bottom, clickingRow)) {
                clickingRow -= 13;
                clickingColumn++;
            }

        if(enemyTable.isOutsideField(Side.Top, clickingRow)) {
            clickingRow += 4;
        }
    }

    /**
     * To get message when error in game or game end, or enemy does nothing
     */
    public String getGameEndMessage(){

        if(lblEnemyLeave.isPresent() || lblGameError.isPresent() || lblServerError.isPresent() || lblGameWin.isPresent() || lblGameLose.isPresent()) {
            return lblGameMessage.getText();
        } else {
            return messageWhenEnemyAFK;
        }
    }
}
