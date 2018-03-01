package forms;

import forms.enums.FurnishYourRoomItem;
import forms.enums.SceneInfoItem;
import forms.enums.SidebarItem;
import org.openqa.selenium.By;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.sikuli.script.FindFailed;
import org.sikuli.script.Screen;
import webdriver.BaseForm;
import webdriver.RegExp;
import webdriver.SikuliUtil;
import webdriver.elements.Button;
import webdriver.elements.Label;
import webdriver.elements.Link;

import java.util.ArrayList;

public class SideBar extends BaseForm {

    private Label productDimensions = new Label(By.xpath("//p[@class='dimensions']"));
    private String sceneItemXpath = "//div[@class='scene']//b[@class='%s']";
    private String sideBarItemXpath = "//a[@title='%s']";

    private final String regExpForProductDimension = "(\\D*)([0-9]+(.[0-9]+)?)(\\D*)";
    private final int groupForProductRegExp = 2;

    public SideBar() {
        super(By.id("sidebar"), "Menu side bar");
    }

    public void selectBarItem(final SidebarItem sidebarItem) {
        new Button(By.xpath(String.format(sideBarItemXpath, sidebarItem)), "Side bar item").click();
    }

    public void selectFurnishRoomItem(final FurnishYourRoomItem furnishYourRoomItem) {
        new Link(By.id(furnishYourRoomItem.toString()), "Furnish your room menu item").click();
    }

    public ArrayList<String> getProductDimensions() {
        String productDimension = productDimensions.getText();
        return RegExp.getValuesRegExp(regExpForProductDimension, productDimension, groupForProductRegExp);
    }

    public int getSceneItemCount(final SceneInfoItem sceneInfoItem) {
        String itemCount = new Label(By.xpath(String.format(sceneItemXpath, sceneInfoItem)), "Scene info item").getText();
        return Integer.parseInt(itemCount);
    }

    public void dragAndDrop(final String dragImageName, final String dropImageName) {
        try {
            new Screen().dragDrop(SikuliUtil.getImagePattern(dragImageName), SikuliUtil.getImagePattern(dropImageName));
        } catch (FindFailed findFailed) {
            logger.error(String.format("Error of find image:\n %s", findFailed.getMessage()));
        }
    }

    public boolean isItemExist(final String imageName) {
        try {
            WebDriverWait wait = new WebDriverWait(browser.getDriver(), waitingTimeout);
            wait.until(webDriver -> {
                try {
                    return new Screen().find(SikuliUtil.getImagePattern(imageName));
                } catch (FindFailed findFailed) {
                    return false;
                }
            });
            return true;
        } catch (Exception findFailed) {
            return false;
        }
    }
}
