package frame.Elements;

import org.openqa.selenium.By;

public class Alert extends BaseElement {
    public Alert(By loc) {
        super(loc);
    }

    public Alert(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public Alert(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public Alert(String xpath) {
        super(xpath);
    }

    public String getElementType() {
        return getLoc("loc.Alert");
    }
}
