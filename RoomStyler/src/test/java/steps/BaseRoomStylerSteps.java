package steps;

import cucumber.api.java.After;
import cucumber.api.java.Before;
import webdriver.Browser;

public class BaseRoomStylerSteps{

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
