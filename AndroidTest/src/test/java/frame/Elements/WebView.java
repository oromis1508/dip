package frame.Elements;

import org.openqa.selenium.By;

public class WebView extends BaseElement {
    public WebView(By loc) {
        super(loc);
    }

    public WebView(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public WebView(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public WebView(String xpath) {
        super(xpath);
    }
}
