package frame.Elements;

import org.openqa.selenium.By;

public class Segmented extends BaseElement {
    public Segmented(By loc) {
        super(loc);
    }

    public Segmented(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public Segmented(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public Segmented(String xpath) {
        super(xpath);
    }
}
