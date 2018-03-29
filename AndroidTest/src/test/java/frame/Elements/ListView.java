package frame.Elements;

import org.openqa.selenium.By;

public class ListView extends BaseElement{

    public ListView(By loc) {
        super(loc);
    }

    public ListView(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public ListView(String xpath, String nameOf) {
        super(xpath,nameOf);
    }

    public ListView(String xpath) {
        super(xpath);
    }

    public String getElementType() {
        return getLoc("loc.listView");
    }

    public void tapSubElement(By loc, String name, int elementIndex) {
        try {
            getElement().findElements(loc).get(elementIndex).click();
            logger.info(String.format("Element %s founded", name));
        } catch (Exception e) {
            logger.warn(String.format("Element with %s locator and index %s was not found", loc, elementIndex));
        }
    }

    public int getSubElementsCount(By loc, String name) {
        int elementsCount = getElement().findElements(loc).size();

        if (elementsCount == 0) {
            logger.warn(String.format("Elements with %s locator was not found", loc));
        } else {
            logger.info(String.format("Element %s founded", name));
        }
        return elementsCount;
    }

}
