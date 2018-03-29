package frame.Elements;

import frame.Helpers;
import io.appium.java_client.MobileBy;
import io.appium.java_client.MobileElement;
import io.appium.java_client.android.AndroidElement;
import org.openqa.selenium.By;
import org.openqa.selenium.Dimension;
import org.openqa.selenium.Point;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public abstract class BaseElement extends Helpers {

    protected String  name;
    protected By locator;
    protected AndroidElement element;

    /**
     * The main constructor
     * @param loc By Locator
     */
    protected BaseElement(By loc){
        locator = loc;
        logger.warn("Choose name for element!");
    }

    /**
     * The main constructor
     * @param loc By Locator
     * @param nameOf Output in logs
     */
    protected BaseElement(By loc, String nameOf){
        locator = loc;
        name = nameOf;
    }

    /**
     * The main constructor
     * @param xpath xpath as string
     * @param nameOf Output in logs
     */
    protected BaseElement(String xpath, String nameOf){
        locator = MobileBy.xpath(xpath);
        name = nameOf;
    }

    /**
     * The main constructor
     * @param xpath xpath as string
     */
    public BaseElement(String xpath) {
        locator = MobileBy.xpath(xpath);
        logger.warn("Choose name for element!");
    }

    public String getText(){
        return getElement().getText();
    }

    /**
     * Getter for name
     * @return name of element
     */
    public String getName() {
        return name;
    }

    /**
     * Getter for locator
     * @return locator
     */
    public By getLocator() {
        return locator;
    }

    /**
     * Getter for element
     * initialises element by driver
     * @return element
     */
    public AndroidElement getElement() {
        return element(locator);
    }

    /**
     * Getter for size
     * @return width and height
     */
    public Dimension getSize()  {
        return getElement().getSize();
    }

    /**
     * Getter for location point
     * @return upper left point out of element
     */
    public Point getLocation()  {
        return getElement().getLocation();
    }

    /**
     * Getter for coordinates of 4 points inside of element
     * @return map of 4 int with coordinates of element
     */
    public Map<String,Integer> getCoordinates(){
        Map<String,Integer> coordinates = new HashMap<>(4);
        coordinates.put("x1",getLocation().getX());
        coordinates.put("y1",getLocation().getY());
        coordinates.put("x2",getLocation().getX()+getSize().getWidth()-1);
        coordinates.put("y2",getLocation().getY()+getSize().getHeight()-1);
        return coordinates;
    }

    public String getValue(){
        return getElement().getAttribute("value");
    }

    public int getValueAsInt() {
        try {
            return Integer.parseInt(getValue());
        } catch (Exception e){
            logger.warn(e.getMessage());
        }
        return 0;
    }

    public void sendKeys(String text){
        logger.info(getLoc("loc.logger.typing")+" "+text);
        getElement().clear();
        getElement().sendKeys(text);
    }

    public void setValueViaCommand(String value){
        logger.info(getLoc("loc.setting.value")+" "+value);
        getElement().setValue(value);
    }

    public void tap(){
        assertIsPresent();
        logger.info(getLoc("loc.element.tap")+" " + name);
        getElement().click();
    }

    public boolean isPresent(){
        try {
            waitForElementExists();
            try {
                return getDriver().findElement(locator).isDisplayed();
            } catch (Exception e) {
                logger.warn(e.getMessage());
                return false;
            }
        } catch (Exception e) {
            logger.warn(e.getMessage());
            return false;
        }
    }

    public void assertIsPresent() {
        if (!isPresent()) {
            logger.fatal(name+ getLoc("loc.is.absent"));
        } else {
            logger.info(name + " " + getLoc("loc.is.present"));
        }
    }

    public boolean isEnabled(){
        return getElement().isEnabled();
    }

    /**
     * Wrap WebElement in MobileElement *
     */
    private static AndroidElement w(WebElement element) {
        return (AndroidElement) element;
    }

    /**
     * Wrap WebElement in MobileElement *
     */
    private static List<AndroidElement> w(List<WebElement> elements) {
        List<AndroidElement> list = new ArrayList<AndroidElement>(elements.size());
        for (WebElement element : elements) {
            list.add(w(element));
        }
        return list;
    }

    /**
     * Return an element by locator *
     */
    public AndroidElement element(By locator) {
        try{
            return w(getDriver().findElement(locator));
        } catch (org.openqa.selenium.NoSuchElementException ex){
            logger.warn(ex.getMessage());
            logger.fatal(getLoc("loc.element")+" "+name+" "+getLoc("loc.is.absent"));
            return null;
        }
    }

    /**
     * Return a list of elements by locator *
     */
    public static List<AndroidElement> elements(By locator) {
        return w(getDriver().findElements(locator));
    }

    /**
     * Press the back button *
     */
    public static void back() {
        getDriver().navigate().back();
    }

    /**
     * Return a list of elements by tag name *
     */
    public static List<AndroidElement> tags(String tagName) {
        return elements(for_tags(tagName));
    }

    /**
     * Return a tag name locator *
     */
    public static By for_tags(String tagName) {
        return By.className(tagName);
    }

    /**
     * Return a static text element by xpath index *
     */
    public MobileElement text(int xpathIndex) {
        return element(for_text(xpathIndex));
    }

    /**
     * Return a static text locator by xpath index *
     */
    public static By for_text(int xpathIndex) {
        return By.xpath("//android.widget.TextView[contains(@text, '" + xpathIndex + "')]");
    }

    /**
     * Return a static text element that contains text *
     */
    public MobileElement text(String text) {
        return element(for_text(text));
    }

    /**
     * Return a static text locator that contains text *
     */
    public static By for_text(String text) {
        return By.xpath("//android.widget.TextView[contains(@text, '" + text + "')]");
    }

    /**
     * Return a static text element by exact text *
     */
    public MobileElement text_exact(String text) {
        return element(for_text_exact(text));
    }

    /**
     * Return a static text locator by exact text *
     */
    public static By for_text_exact(String text) {
        return By.xpath("//android.widget.TextView[@text='" + text + "']");
    }

    /**
     * Wait 30 seconds for locator to find an element *
     */
    public static MobileElement wait(By locator) {
        return w(driverWait.until(ExpectedConditions.visibilityOfElementLocated(locator)));
    }

    public void waitForElementIsVisible(){
        driverWait.until(ExpectedConditions.visibilityOfElementLocated(locator));
    }

    public void waitForElementExists(){
        driverWait.until(ExpectedConditions.presenceOfElementLocated(locator));
    }

    public void waitForNotExist(){
        driverWait.until(ExpectedConditions.invisibilityOfElementLocated(locator));
    }
    /**
     * Wait 60 seconds for locator to find all elements *
     */
    public static List<AndroidElement> waitAll(By locator) {
        return w(driverWait.until(ExpectedConditions.visibilityOfAllElementsLocatedBy(locator)));
    }

    public static By for_find(String value) {
        return By.xpath("//*[@content-desc=\"" + value + "\" or @resource-id=\"" + value +
                "\" or @text=\"" + value + "\"] | //*[contains(translate(@content-desc,\"" + value +
                "\",\"" + value + "\"), \"" + value + "\") or contains(translate(@text,\"" + value +
                "\",\"" + value + "\"), \"" + value + "\") or @resource-id=\"" + value + "\"]");
    }

    public static boolean waitInvisible(By locator) {
        return driverWait.until(ExpectedConditions.invisibilityOfElementLocated(locator));
    }

}
