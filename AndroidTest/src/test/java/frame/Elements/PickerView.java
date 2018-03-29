package frame.Elements;

import org.openqa.selenium.By;

public class PickerView extends BaseElement {
    public PickerView(By loc) {
        super(loc);
    }

    public PickerView(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public PickerView(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public PickerView(String xpath) {
        super(xpath);
    }

    public String getElementType() {
        return getLoc("loc.Picker");
    }

    public void setValue(String value){
        logger.info(getLoc("loc.setting.value")+" "+value);
        getElement().sendKeys(value);
    }
}
