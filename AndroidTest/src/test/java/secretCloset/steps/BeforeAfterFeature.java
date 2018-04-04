package secretCloset.steps;

import cucumber.api.java.After;
import cucumber.api.java.Before;
import frame.Helpers;
import frame.SoftwareRunner;
import io.appium.java_client.android.AndroidDriver;
import java.util.concurrent.TimeUnit;

import static frame.Helpers.*;

public class BeforeAfterFeature {

    private boolean isReinstall = Boolean.parseBoolean(runProperties.getProperty("reinstallApp"));
    private String appActivity = runProperties.getProperty("appMainActivity");

    @Before
    public void init() {
        SoftwareRunner.runGenymotion();
        SoftwareRunner.runAppium();

        if (isReinstall){
            Helpers.reinstallApp();
        }

        try {
            setDriver(new AndroidDriver(SoftwareRunner.getAppiumProcess(), Helpers.makeCapabilities(appActivity)));
        } catch (Exception e) {
            logger.warn(e.getMessage());
        }

        getDriver().manage().timeouts().implicitlyWait(Long.parseLong(appiumProperties.getProperty("appiumImplicityTimeout", "5")), TimeUnit.SECONDS);
        Helpers.init(getDriver());
    }

    @After
    public void destroy() {
        if (getDriver() != null) {
            try {
                getDriver().closeApp();
            } catch (Exception ignored){}

            getDriver().quit();
        }
        SoftwareRunner.closeAppium();
        SoftwareRunner.closeGenymotion();
    }
}
