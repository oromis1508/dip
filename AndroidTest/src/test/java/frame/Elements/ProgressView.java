package frame.Elements;

import org.openqa.selenium.By;

public class ProgressView extends BaseElement {
    public ProgressView(By loc) {
        super(loc);
    }

    public ProgressView(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public ProgressView(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public ProgressView(String xpath) {
        super(xpath);
    }

    //TODO Wait until 100%

    public String getElementType() {
        return getLoc("loc.Progress");
    }
}
