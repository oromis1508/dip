package forms;

import forms.enums.ImageItem;
import org.openqa.selenium.By;
import org.sikuli.script.FindFailed;
import org.sikuli.script.Match;
import org.sikuli.script.Screen;
import webdriver.BaseForm;

public class WorkField extends BaseForm {

    private Screen screen = new Screen();
    private Match placedItem;

    public WorkField() {
        super(By.id("view-floor"), "Work field");
    }

    public boolean isItemPlaced(ImageItem item) {
        try {
            placedItem = screen.find(item);
            return true;
        } catch (FindFailed findFailed) {
            return false;
        }
    }

    public void clickPlacedItem() {
        placedItem.click();
    }
}
