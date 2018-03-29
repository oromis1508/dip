package secretCloset.page;

import frame.Elements.BasePage;
import frame.Elements.TextField;
import frame.Elements.TextView;
import io.appium.java_client.MobileBy;

public class SelectRegionActivity extends BasePage{

    private TextField fieldSearchRegion = new TextField(MobileBy.id("etSearchTest"), "Text field for search region");

    private String txvSelectedCityXpath = "//android.widget.TextView[@text='%s']";

    public SelectRegionActivity() {
        super(MobileBy.xpath("//android.widget.TextView[contains(@text, 'Select city')]"), "Select region page");
    }

    public void searchRegion(String region) {
        fieldSearchRegion.sendKeys(region);
    }

    public void selectRegion(String region) {
        new TextView(MobileBy.xpath(String.format(txvSelectedCityXpath, region)), String.format("Text field %s region", region)).tap();
    }
}
