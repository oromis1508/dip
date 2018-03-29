package secretCloset.page;

import frame.Elements.BasePage;
import frame.Elements.TextView;
import io.appium.java_client.MobileBy;

public class GoodInfoActivity extends BasePage{

    public GoodInfoActivity() {
        super(MobileBy.id("tvItemBrand"), "Good info page");
    }

    public String getSellerInfo(String infoName) {
        return new TextView(MobileBy.id(SellerInfo.getByName(infoName)),
                String.format("Text view about seller %s", infoName)).getText();
    }
}
