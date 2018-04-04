package frame.Elements;

import org.openqa.selenium.By;

public class TextField extends BaseElement {
    public TextField(By loc) {
        super(loc);
    }

    public TextField(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public TextField(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public TextField(String xpath) {
        super(xpath);
    }
}
