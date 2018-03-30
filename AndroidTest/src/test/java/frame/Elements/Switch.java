package frame.Elements;

import org.openqa.selenium.By;

public class Switch extends BaseElement {
    public Switch(By loc) {
        super(loc);
    }

    public Switch(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public Switch(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public Switch(String xpath) {
        super(xpath);
    }
}
