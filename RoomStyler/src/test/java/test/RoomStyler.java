package test;

import webdriver.BaseTest;

public class RoomStyler extends BaseTest {

	private final int numMaxRandomPlacementShips = Integer.parseInt(getTestProperty("numMaxRandomPlacementShips"));

	@Override
	public void runTest(){

		logger.step(1, "To click link \"Select random opponent\"");


	}
}
