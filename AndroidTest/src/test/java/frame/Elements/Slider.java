package frame.Elements;

import org.openqa.selenium.By;
import org.openqa.selenium.Dimension;
import org.openqa.selenium.Point;

public class Slider extends BaseElement {

    public Slider(By loc) {
        super(loc);
    }

    public Slider(By loc, String nameOf) {
        super(loc, nameOf);
    }

    public Slider(String xpath, String nameOf) {
        super(xpath, nameOf);
    }

    public Slider(String xpath) {
        super(xpath);
    }

    public void setPercent(String perc){
        logger.info(getLoc("loc.setting.value")+" "+perc);
        getElement().setValue(perc);
    }

    /**
     * Sets slider value in percents via real swipe
     * @param perc int percents
     */
    //FIXME Accuracy for android
    public void setValueViaSlide(int perc){
        int startX, startY, endX, endY, startOffset, elStartVal;
        float startValue;
        //elStartVal = Integer.parseInt(getElement().getText().replace("%","").trim());
        //if(elStartVal == 0){
        startValue = 0;
        //} else {
        //    startValue = elStartVal / 100;
        //}

        Dimension size = getElement().getSize();
        int width = size.getWidth();
        int height = size.getHeight();
        Point location = getLocation();

        //logger.info(getLoc("loc.slider.start") + elStartVal + "%");
        startOffset = (int) ((width - 60) * startValue);
        startX = location.getX() + 30 + startOffset;
        startY = location.getY() + height / 2;

        if (startValue == 1) startValue = startValue * 100;

        if ((float)perc/100 > startValue/100) {
            endX = startX + (width - 30) * perc / 100;
        } else if ((float)perc/100 < startValue/100){
            endX = (width - 30) * perc / 100;
        } else if (perc == 0){
            endX =  location.getX() + 30;
        } else return;

        endY = startY;

        swipe(startX, startY, endX, endY);
    }
    public String getElementType() {
        return getLoc("loc.slider");
    }
}
