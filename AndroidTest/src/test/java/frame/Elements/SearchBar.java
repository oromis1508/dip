package frame.Elements;

import org.openqa.selenium.By;

public class SearchBar extends BaseElement {
    public SearchBar(By loc) {
        super(loc);
    }

    public SearchBar(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public SearchBar(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public SearchBar(String xpath) {
        super(xpath);
    }
}
