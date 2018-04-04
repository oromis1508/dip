package frame.Elements;

import org.openqa.selenium.By;

public class Label extends BaseElement {

    public Label(By loc) {
        super(loc);
    }

    public Label(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public Label(String xpath, String nameOf) {
        super(xpath,nameOf);
    }

    public Label(String xpath) {
        super(xpath);
    }

    public String getElementType() {
        return getLoc("loc.label");
    }
}
