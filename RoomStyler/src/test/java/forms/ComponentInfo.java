package forms;

import forms.enums.SceneInfoItem;
import org.openqa.selenium.By;
import webdriver.RegExp;
import webdriver.elements.Label;

import java.util.ArrayList;

public class ComponentInfo {

    private Label productDimensions = new Label(By.xpath("//p[@class='dimensions']"));
    private String sceneItemXpath = "//div[@class='scene']//b[@class='%s']";

    private final String regExpForProductDimension = "(\\D*)([0-9]+(.[0-9]+)?)(\\D*)";
    private final int groupForProductRegExp = 2;

    public ArrayList<String> getProductDimensions() {
        String productDimension = productDimensions.getText();
        return RegExp.getValuesRegExp(regExpForProductDimension, productDimension, groupForProductRegExp);
    }

    public int getSceneItemCount(SceneInfoItem sceneInfoItem) {
        String itemCount = new Label(By.xpath(String.format(sceneItemXpath, sceneInfoItem)), "Scene info item").getText();
        return Integer.parseInt(itemCount);
    }
}
