package test;

import forms.algorithm.BattleShipGame;
import forms.BattleShipMainForm;
import org.testng.Assert;
import webdriver.BaseTest;

public class BattleShipTest extends BaseTest {

	private final int numMaxRandomPlacementShips = Integer.parseInt(getTestProperty("numMaxRandomPlacementShips"));

	@Override
	public void runTest(){

		logger.step(1, "To click link \"Select random opponent\"");
		BattleShipMainForm battleShipMainForm = new BattleShipMainForm();
		battleShipMainForm.selectRandomOpponent();

		logger.step(2, String.format("To click link random place ships no more than %s times", numMaxRandomPlacementShips));
		battleShipMainForm.selectRandomPlaceShips(numMaxRandomPlacementShips);

		logger.step(3, "To click button for start game");
		battleShipMainForm.clickPlayGame();

		logger.step(4, "To play game using written game strategy");
		BattleShipGame game = new BattleShipGame();
		game.playGame();

		String endMessage = game.getGameEndMessage();
		logger.step(5, "Game ends with message: " + endMessage);
		Assert.assertTrue(game.isGameWon(), "Game won");
	}
}
