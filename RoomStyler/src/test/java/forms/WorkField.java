package forms;

import org.openqa.selenium.By;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.sikuli.script.FindFailed;
import org.sikuli.script.Screen;
import webdriver.BaseForm;
import webdriver.SikuliUtil;

public class WorkField extends BaseForm {


    public WorkField() {
        super(By.id("view-floor"), "Work field");
    }

    public boolean isItemPlaced(final String imageName) {
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

    public void clickItem(final String imageName) {
        Screen screen = new Screen();
        try {
            screen.click(SikuliUtil.getImagePattern(imageName));
        } catch (FindFailed findFailed) {
            logger.error(String.format("Error of find image:\n %s", findFailed.getMessage()));
        }
    }
}
