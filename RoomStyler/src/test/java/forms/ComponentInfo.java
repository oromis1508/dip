package forms;

import forms.enums.ImageItem;
import forms.enums.SceneInfoItem;
import org.openqa.selenium.By;
import org.sikuli.script.FindFailed;
import org.sikuli.script.Pattern;
import org.sikuli.script.Screen;
import webdriver.RegExp;
import webdriver.elements.Label;

import java.util.ArrayList;

public class ComponentInfo {

    private Label productDimensions = new Label(By.xpath("//p[@class='dimensions']"));
    private String sceneItemXpath = "//div[@class='scene']//b[@class='%s']";

    private final String regExpForProductDimension = "(\\D*)([0-9]+(.[0-9]+)?)(\\D*)";
    private final int groupForProductRegExp = 2;

    private Screen screen = new Screen();

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
            screen.dragDrop(new Pattern(dragItem.toString()), new Pattern(dropItem.toString()));
        } catch (FindFailed findFailed) {
            findFailed.printStackTrace();
        }
    }

    public boolean isItemExist(ImageItem item) {
        try {
            screen.find(item);
            return true;
        } catch (FindFailed findFailed) {
            return false;
        }
    }
}
