package frame.Elements;

import org.openqa.selenium.By;

public class ToolBar extends BaseElement {
    public ToolBar(By loc) {
        super(loc);
    }

    public ToolBar(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public ToolBar(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public ToolBar(String xpath) {
        super(xpath);
    }
}
