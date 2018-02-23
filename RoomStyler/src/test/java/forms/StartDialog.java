package forms;

import org.openqa.selenium.By;
import webdriver.BaseForm;
import webdriver.elements.Button;

public class StartDialog extends BaseForm{

    private Button btnCloseDialog = new Button(By.className("popin-close-title"), "to close start dialog");

    protected StartDialog() {
        super(By.id("welcome-popup"), "Start dialog");
    }

    public void closeDialog() {
        btnCloseDialog.click();
    }
}
