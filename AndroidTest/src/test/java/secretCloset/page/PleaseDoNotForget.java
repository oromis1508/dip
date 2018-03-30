package secretCloset.page;

import frame.Elements.BasePage;
import frame.Elements.Button;
import io.appium.java_client.MobileBy;

public class PleaseDoNotForget extends BasePage{

    private Button btnOk = new Button(MobileBy.id("button1"),"Submit caution button");

    public PleaseDoNotForget() {
        super(MobileBy.id("alertTitle"), "Region caution dialog");
    }

    public void submitCaution() {
        btnOk.tap();
    }
}
