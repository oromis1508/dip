package frame;

import io.appium.java_client.android.AndroidDriver;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Parameters;
import org.testng.annotations.Test;

import java.util.concurrent.TimeUnit;

public abstract class AppiumTest extends Helpers implements IAnalys{

    public abstract void runTest();

    @Test
    public void xTest() throws Throwable {
        Class<? extends AppiumTest> currentClass = this.getClass();
        try {
            logger.logTestName(currentClass.getName());
            runTest();
            logger.logTestEnd(currentClass.getName());
        } catch (Throwable e) {
            if (shouldAnalys()) {
                invokeAnalys(e, getDriver().getPageSource());
            } else {
                logger.warn("");
                logger.warnRed(getLoc("loc.test.failed"));
                logger.warn("");
                throw e;
            }
        }
        makeScreen(currentClass);
        }

    @BeforeClass
    @Parameters({"appActivity","reinstall"})
    public void before(String appActivity,String reinstall) {
        if (reinstall.equals("true")){
            reinstallApp();
        }
        try {
            setDriver(new AndroidDriver(SoftwareRunner.getAppiumProcess(), makeCapabilities(appActivity)));
        } catch (Exception e) {
            logger.warn(e.getMessage());
        }

        getDriver().manage().timeouts().implicitlyWait(Long.parseLong(appiumProperties.getProperty("appiumImplicityTimeout", "5")), TimeUnit.SECONDS);
        Helpers.init(getDriver());
    }

    @AfterClass
    public void after(){
        if (getDriver() != null) {
            try {
                getDriver().closeApp();
            } catch (Exception ignored){}

            getDriver().quit();
        }
    }
}
