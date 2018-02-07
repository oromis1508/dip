package demo.test;

import demo.test.forms.BattleShipMainForm;
import webdriver.BaseTest;

/**
 * Created on 02.05.2017.
 */
public class BattleShipTest extends BaseTest {

/*
    private final String searchString = "A1QA";
*/

	@Override
	public void runTest(){

		logger.step(1);
		BattleShipMainForm battleShipMainForm = new BattleShipMainForm();
		battleShipMainForm.selectRandomOpponent();

		logger.step(2);
		battleShipMainForm.selectRandomPlaceShips();

		logger.step(3);
		battleShipMainForm.playGame();

		logger.step(4);
		BattleShipGame game = new BattleShipGame();
		game.PlayGame();
	}
}
