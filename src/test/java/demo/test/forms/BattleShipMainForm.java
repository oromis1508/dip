package demo.test.forms;

import org.openqa.selenium.By;
import webdriver.BaseForm;
import webdriver.elements.Button;
import webdriver.elements.Link;

import java.util.Random;

/**
 * Created on 02.05.2017.
 */
public class BattleShipMainForm extends BaseForm {

	private static final String MAIN_LOCATOR = "//div[contains(@class, 'battlefield-start-button')]";

	private static final String LOCATOR_RANDOM_OPPONENT = String.format("//a[contains(text(), '%s')]", getLoc("loc.battleship.linkRandomOpponent"));
	private final Link linkRandomOpponent = new Link(By.xpath(LOCATOR_RANDOM_OPPONENT), "Random Opponent");

	private static final String LOCATOR_RANDOM_PLACE_SHIPS = String.format("//span[contains(text(), '%s')]", getLoc("loc.battleship.linkRandomPlaceShips"));
	private final Link linkRandomPlaceShips = new Link(By.xpath(LOCATOR_RANDOM_PLACE_SHIPS), "Random Place Ships");

	private final Button btnPlay = new Button(By.xpath("//div[contains(@class, 'battlefield-start-button')]"));

	public BattleShipMainForm(){
		super(By.xpath(MAIN_LOCATOR), "Battleship Main Form");
	}

	public void selectRandomOpponent(){
		linkRandomOpponent.clickAndWait();
	}

	public void selectRandomPlaceShips() {
		int numClicks = new Random().nextInt(15) + 1;

		for (int i = 0; i<numClicks; i++) {
			linkRandomPlaceShips.click();
		}
	}

	public void playGame() {
		btnPlay.clickAndWait();
	}
}
