package forms;

import org.openqa.selenium.By;
import org.sikuli.script.FindFailed;
import org.sikuli.script.Screen;
import webdriver.BaseForm;
import webdriver.SikuliUtil;

public class WorkField extends BaseForm {


    public WorkField() {
        super(By.id("view-floor"), "Work field");
    }

    public void clickItem(final String imageName) {
        Screen screen = new Screen();
        try {
            screen.click(SikuliUtil.getImagePattern(imageName));
        } catch (FindFailed findFailed) {
            logger.error(String.format("Error of find image:\n %s", findFailed.getMessage()));
        }
    }
}
