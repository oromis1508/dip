package test;

import forms.algorithm.BattleShipGame;
import forms.BattleShipMainForm;
import org.testng.Assert;
import webdriver.BaseTest;

public class BattleShipTest extends BaseTest {

	@Override
	public void runTest(){

		logger.step(1);
		BattleShipMainForm battleShipMainForm = new BattleShipMainForm();
		battleShipMainForm.selectRandomOpponent();

		logger.step(2);
		battleShipMainForm.selectRandomPlaceShips();

		logger.step(3);
		battleShipMainForm.clickPlayGame();

		logger.step(4);
		BattleShipGame game = new BattleShipGame();
		game.playGame();

		logger.step(5);
		String endMessage = game.getGameEndMessage();
		logger.info("Game ends with message: " + endMessage);
		Assert.assertEquals(endMessage, getLoc("loc.battleship.gameWin"), "Game won");
	}
}
