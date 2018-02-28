package steps;

import cucumber.api.java.After;
import cucumber.api.java.Before;
import webdriver.Browser;
import webdriver.Logger;

public class BaseRoomStylerSteps{

    protected static int waitForItem = Integer.parseInt(Logger.getTestProperty("waitingTimeout"));

    @Before
    public void openMainPage() {
        Browser.getInstance().windowMaximise();
        Browser.getInstance().navigate(Browser.getBaseUrl());
    }

    @After
    public void closeBrowser() {
        if (Browser.getInstance().isBrowserAlive()) {
            Browser.getInstance().exit();
        }
    }
}
