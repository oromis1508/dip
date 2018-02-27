package forms;

import forms.enums.ImageItem;
import org.openqa.selenium.By;
import org.sikuli.script.FindFailed;
import org.sikuli.script.Pattern;
import org.sikuli.script.Screen;
import webdriver.BaseForm;

public class WorkField extends BaseForm {

    private int waitForItem = 0;

    public WorkField() {
        super(By.id("view-floor"), "Work field");
    }

    public boolean isItemPlaced(ImageItem item, int waitingTimeout) {
        waitForItem = waitingTimeout;
        boolean result = isItemPlaced(item);
        waitForItem = 0;
        return result;
    }

    public boolean isItemPlaced(ImageItem item) {
        try {
            new Screen().find(item.toString()).wait(item.toString(), waitForItem);
            return true;
        } catch (Exception findFailed) {
            return false;
        }
    }

    public void clickItem(ImageItem item) {
        Screen screen = new Screen();
        try {
            screen.click(new Pattern(item.toString()));
        } catch (FindFailed findFailed) {
            logger.error(String.format("Error of find image:\n %s", findFailed.getMessage()));
        }
    }
}
