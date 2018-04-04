package frame;

import frame.utils.GlobalSwipe;
import frame.utils.JamtException;
import io.appium.java_client.TouchAction;
import io.appium.java_client.android.AndroidDriver;
import org.apache.commons.io.FileUtils;
import org.openqa.selenium.OutputType;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.io.File;
import java.io.IOException;
import java.util.Date;
import java.util.Enumeration;

import static io.appium.java_client.remote.AndroidMobileCapabilityType.APP_ACTIVITY;

public abstract class Helpers {
    /**
     * This class contains base methods for work with Appium-driver and functions that are not related to any specific UI
     */


    private static AndroidDriver driver;

    public static AndroidDriver getDriver() {
        return driver;
    }

    public static void setDriver(AndroidDriver driver) {
        Helpers.driver = driver;
    }

    protected static WebDriverWait driverWait;

    public static Logger logger = Logger.getInstance();

    protected static String getLoc(final String key) {
        return Logger.getLoc(key);
    }

    protected int stepNumber = 1;

    public static PropertiesResourceManager appiumProperties = new PropertiesResourceManager("appium.properties");

    public static PropertiesResourceManager runProperties = new PropertiesResourceManager("run.properties");

    public static final Date date = new Date();

    private static final String CREATE_SCREEN_EVERY_STEP = "org.uncommons.reportng.everystepscreen";
    public static int DEVICE_WIDTH;
    public static int DEVICE_HEIGHT;

    public static String getAppPath(){
        return System.getProperty("basedir",System.getProperty("user.dir"))
                +String.format("%1$ssrc%1$stest%1$sresources%1$sapplications%1$s", File.separator)
                +appiumProperties.getProperty("appFileName");
    }

    /**
     * Initialize the webdriver. Must be called before using any helper methods. *
     */
    public static void init(AndroidDriver webDriver) {
        driver = webDriver;
        int timeoutInSeconds = Integer.parseInt(appiumProperties.getProperty("appiumTimeout","5"));
        driverWait = new WebDriverWait(webDriver, timeoutInSeconds);
        File screenshot = getDriver().getScreenshotAs(OutputType.FILE);
        try {
            DEVICE_WIDTH = javax.imageio.ImageIO.read(screenshot).getWidth();
            DEVICE_HEIGHT = javax.imageio.ImageIO.read(screenshot).getHeight();
        } catch (IOException e) {
            throw new JamtException("Can't get screen resolution\n"+e.getMessage());
        }
    }

    public static DesiredCapabilities makeCapabilities(){
        DesiredCapabilities capabilities = new DesiredCapabilities();
        for (Enumeration e = appiumProperties.getProperties(); e.hasMoreElements();){
            String capElement = e.nextElement().toString();
            if (!appiumProperties.getProperty(capElement).isEmpty()) {
                capabilities.setCapability(capElement, appiumProperties.getProperty(capElement));
            }
        }
        capabilities.setCapability("name", appiumProperties.getProperty("name") + date);
        return capabilities;
    }

    public static DesiredCapabilities makeCapabilities(String appActivity){
        DesiredCapabilities capabilities = new DesiredCapabilities();
        for (Enumeration e = appiumProperties.getProperties(); e.hasMoreElements();){
            String capElement = e.nextElement().toString();
            if (!appiumProperties.getProperty(capElement).isEmpty()) {
                capabilities.setCapability(capElement, appiumProperties.getProperty(capElement));
            }
        }
        capabilities.setCapability("name", appiumProperties.getProperty("name") + date);
        capabilities.setCapability(APP_ACTIVITY,appActivity);
        return capabilities;
    }

    public static void reinstallApp(){
        try {
            ProcessBuilder uninstall = new ProcessBuilder(
                    "/Volumes/Data/Development/android-sdk-macosx/platform-tools/adb"
                    ,"uninstall"
                    ,appiumProperties.getProperty("appPackage"));
            uninstall.inheritIO().start().waitFor();
        } catch (InterruptedException | IOException ex) {
            throw new JamtException("Application uninstall failure\n"+ex.getMessage());
        }

        try {
            ProcessBuilder install = new ProcessBuilder(
                    "/Volumes/Data/Development/android-sdk-macosx/platform-tools/adb"
                    ,"install"
                    ,getAppPath());
            install.inheritIO().start().waitFor();
        } catch (InterruptedException | IOException ex) {
            throw new JamtException("Application install failure\n"+ex.getMessage());
        }
    }
    /**
     * Use this swipe instead of swipe on element
     * swipe in webView has a bug, and here is the solution
     */
    public static void swipe(int startx, int starty, int endx, int endy){
        logger.debug(String.format(getLoc("loc.logger.swipe"), startx, starty, endx, endy));
        TouchAction touchAction = new TouchAction(getDriver());
        touchAction
                .press(startx, starty)
                .moveTo(endx, endy)
                .release();
        touchAction.perform();
    }

    public void switchToNextPage(){
        logger.info(getLoc("loc.page.switch.next"));
        GlobalSwipe.LEFT.swipe();
    }

    public void switchToPrevPage(){
        logger.info(getLoc("loc.page.switch.prev"));
        GlobalSwipe.RIGHT.swipe();
    }

    /**
     * Logging a step number.
     *
     * @param step
     *            - step number
     */
    public static void logStep(final int step) {
        logger.step(step);
    }

    public void logStep(){
        logStep(stepNumber++);
    }

    /**
     * Logging steps with info
     */
    protected void logStep(final String info) {
        logStep(stepNumber++);
        logger.info(String.format("----==[ %1$s ]==----", info));
    }

    /**
     * Logging a several steps in a one action
     *
     * @param fromStep
     *            - the first step number to be logged
     * @param toStep
     *            - the last step number to be logged
     */
    public void logStep(final int fromStep, final int toStep) {
        logger.step(fromStep, toStep);
    }

    /**
     * Assert Objects are Equal
     *
     * @param expected
     *            Expected Value
     * @param actual
     *            Actual Value
     */
    public void assertEquals(final Object expected, final Object actual) {
        if (!expected.equals(actual)) {
            logger.fatal("Expected value: '" + expected + "', but was: '" + actual
                    + "'");
        }
    }


    /**
     * Assert Objects are Equal, doesn't fail the secretCloset
     *
     * @param passMessage
     *            Pass Message
     * @param failMessage
     *            Fail Message
     * @param expected
     *            Expected Value
     * @param actual
     *            Actual Value
     */
    public void assertEqualsNoFail(final String passMessage,
                                   final String failMessage, final Object expected, final Object actual) {
        if (expected.equals(actual)) {
            logger.info(passMessage);
        } else {
            logger.error(failMessage);
            logger.error("Expected value: '" + expected + "', but was: '" + actual
                    + "'");
        }
    }

    /**
     * Assert Objects are Equal
     *
     * @param message
     *            Fail Message
     * @param expected
     *            Expected Value
     * @param actual
     *            Actual Value
     */
    public void assertEquals(final String message, final Object expected,
                             final Object actual) {
        if (!expected.equals(actual)) {
            logger.fatal(message);
        }
    }

    /**
     * Assert Objects are Equal
     *
     * @param passMessage
     *            Pass Message
     * @param message
     *            Fail Message
     * @param expected
     *            Expected Value
     * @param actual
     *            Actual Value
     */
    public void assertEquals(final String passMessage, final String message, final Object expected,
                             final Object actual) {
        if (!expected.equals(actual)) {
            logger.fatal(message);
        } else logger.info(passMessage);
    }

    /**
     * In report: If "true": when opening page screenshot is taken
     * @return boolean
     */
    public boolean shouldCreateScreenEveryStep(){
        return System.getProperty(CREATE_SCREEN_EVERY_STEP, "false").equalsIgnoreCase("true");
    }

    /**
     * screenshot.
     *
     * @param name
     *            Name of class
     * @return String path to screen
     */
    protected String makeScreen(final Class<? extends Helpers> name) {
        return makeScreen(name,true);
    }

    // ==============================================================================================
    //
    /**
     * screenshot.
     *
     * @param name
     *            Name of class
     * @param additionalInfo
     *            additionalInfo
     * @return String path to screen
     */
    protected String makeScreen(final Class<? extends Helpers> name, final boolean additionalInfo) {
        String fileName = name.getPackage().getName() + "." + name.getSimpleName();
        try {
            File screen = getDriver().getScreenshotAs(OutputType.FILE);
            File addedNewFile = new File(String.format("surefire-reports%2$shtml%2$sScreenshots/%1$s.png", fileName,File.separator));
            FileUtils.copyFile(screen, addedNewFile);
        } catch (Exception e) {
            logger.warn(getLoc("loc.screenshot.failed"));
        }
        if (additionalInfo) {
            String formattedName = String.format(
                    "<a href='Screenshots/%1$s.png'>%2$s</a>", fileName,getLoc("loc.screenshot"));
            logger.info(formattedName);
            logger.printDots(formattedName.length());
        }
        return new File(String.format(
                "surefire-reports%2$shtml%2$sScreenshots/%1$s.png", fileName,File.separator))
                .getAbsolutePath();
    }
}
