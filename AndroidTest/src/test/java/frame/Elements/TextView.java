package frame.Elements;

import org.openqa.selenium.By;

public class TextView extends BaseElement {
    public TextView(By loc) {
        super(loc);
    }

    public TextView(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public TextView(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public TextView(String xpath) {
        super(xpath);
    }
}
