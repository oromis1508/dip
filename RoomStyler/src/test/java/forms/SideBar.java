package forms;

import forms.enums.FurnishYourRoomItem;
import forms.enums.ImageItem;
import forms.enums.SceneInfoItem;
import forms.enums.SidebarItem;
import org.openqa.selenium.By;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.sikuli.script.FindFailed;
import org.sikuli.script.Pattern;
import org.sikuli.script.Screen;
import webdriver.BaseForm;
import webdriver.RegExp;
import webdriver.elements.Button;
import webdriver.elements.Label;
import webdriver.elements.Link;

import java.util.ArrayList;

public class SideBar extends BaseForm{


    private Label productDimensions = new Label(By.xpath("//p[@class='dimensions']"));
    private String sceneItemXpath = "//div[@class='scene']//b[@class='%s']";
    private String sideBarItemXpath = "//a[@title='%s']";

    private final String regExpForProductDimension = "(\\D*)([0-9]+(.[0-9]+)?)(\\D*)";
    private final int groupForProductRegExp = 2;

    private int waitForItem = 0;

    public SideBar() {
        super(By.id("sidebar"), "Menu side bar");
    }

    public void selectBarItem(SidebarItem sidebarItem) {
        new Button(By.xpath(String.format(sideBarItemXpath, sidebarItem)),"Side bar item").click();
    }

    public void selectFurnishRoomItem(FurnishYourRoomItem furnishYourRoomItem) {
        new Link(By.id(furnishYourRoomItem.toString()), "Furnish your room menu item").click();
    }

    public ArrayList<String> getProductDimensions() {
        String productDimension = productDimensions.getText();
        return RegExp.getValuesRegExp(regExpForProductDimension, productDimension, groupForProductRegExp);
    }

    public int getSceneItemCount(SceneInfoItem sceneInfoItem) {
        String itemCount = new Label(By.xpath(String.format(sceneItemXpath, sceneInfoItem)), "Scene info item").getText();
        return Integer.parseInt(itemCount);
    }

    public void dragAndDrop(ImageItem dragItem, ImageItem dropItem) {
        try {
            new Screen().dragDrop(new Pattern(dragItem.toString()), new Pattern(dropItem.toString()));
        } catch (FindFailed findFailed) {
            logger.error(String.format("Error of find image:\n %s", findFailed.getMessage()));
        }
    }

    public boolean isItemExist(ImageItem item, int waitingTimeout) {
        waitForItem = waitingTimeout;
        boolean result = isItemExist(item);
        waitForItem = 0;
        return result;
    }

    public boolean isItemExist(ImageItem item) {
        try {
            WebDriverWait wait = new WebDriverWait(browser.getDriver(), waitForItem);
            wait.until(webDriver -> {
                try {
                    return new Screen().find(item.toString());
                } catch (FindFailed findFailed) {
                    return false;
                }
            });
            //new Screen().find(item.toString()).wait(item.toString(), waitForItem);
            return true;
        } catch (Exception findFailed) {
            return false;
        }
    }
}
