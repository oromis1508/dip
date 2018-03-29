package frame.Elements;

import org.openqa.selenium.By;

public class PageControl extends BaseElement {
    public PageControl(By loc) {
        super(loc);
    }

    public PageControl(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public PageControl(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public PageControl(String xpath) {
        super(xpath);
    }

    public void switchToPage(String num){
        logger.info(String.format(getLoc("loc.PageControl.switching"),num));
        String realNum = String.valueOf(Integer.parseInt(num) - 1);
        getElement().sendKeys(realNum);
    }

    public String getElementType() {
        return getLoc("loc.PageControl");
    }
}
