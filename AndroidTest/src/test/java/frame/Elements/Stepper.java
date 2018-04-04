package frame.Elements;

import org.openqa.selenium.By;

public class Stepper extends BaseElement {
    public Stepper(By loc) {
        super(loc);
    }

    public Stepper(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public Stepper(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public Stepper(String xpath) {
        super(xpath);
    }
}
