package forms;

import org.openqa.selenium.By;
import webdriver.BaseForm;
import webdriver.elements.Button;

public class StartDialog extends BaseForm{

    private Button btnCloseDialog = new Button(By.className("popin-close-title"), "to close start dialog");

    public StartDialog() {
        super(By.id("popin-background"), "Start dialog");
    }

    public void closeDialog() {
        btnCloseDialog.clickAndWait();
    }

    public boolean isDialogClosed() {
        return isFormClosed();
    }
}
