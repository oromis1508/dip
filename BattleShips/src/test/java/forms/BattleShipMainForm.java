package forms;

import org.openqa.selenium.By;
import webdriver.BaseForm;
import webdriver.elements.Button;
import webdriver.elements.Link;

import java.util.Random;

public class BattleShipMainForm extends BaseForm {

	private static final String RANDOM_PLACESHIPS_XPATH = "//li[contains(@class, 'placeships-variant__randomly')]";

	private final Link linkRandomOpponent = new Link(By.className("battlefield-start-choose_rival-variant-link"), "Random Opponent");

	private final Link linkRandomPlaceShips = new Link(By.xpath(RANDOM_PLACESHIPS_XPATH), "Random Place Ships");

	private final Button btnPlay = new Button(By.className("battlefield-start-button"), "Button for start game");

	public BattleShipMainForm(){
		super(By.xpath(RANDOM_PLACESHIPS_XPATH), "Battleship Main Form");
	}

	public void selectRandomOpponent(){
		linkRandomOpponent.clickAndWait();
	}

	public void selectRandomPlaceShips(int numMaxClicks) {
		int numClicks = new Random().nextInt(numMaxClicks) + 1;

		for (int i = 0; i<numClicks; i++) {
			linkRandomPlaceShips.click();
		}
	}

	public void clickPlayGame() {
		btnPlay.click();
	}
}
