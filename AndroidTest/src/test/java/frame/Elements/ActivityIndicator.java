package frame.Elements;

import org.openqa.selenium.By;

public class ActivityIndicator extends BaseElement {
    public ActivityIndicator(By loc) {
        super(loc);
    }

    public ActivityIndicator(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public ActivityIndicator(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public ActivityIndicator(String xpath) {
        super(xpath);
    }

    public String getElementType() {
        return getLoc("loc.Activity");
    }
}
