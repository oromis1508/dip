package frame.Elements;

import org.openqa.selenium.By;

public class Image extends BaseElement {
    public Image(By loc) {
        super(loc);
    }

    public Image(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public Image(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public Image(String xpath) {
        super(xpath);
    }
}
