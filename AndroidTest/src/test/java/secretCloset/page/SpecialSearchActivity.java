package secretCloset.page;

import frame.Elements.BasePage;
import frame.Elements.Button;
import io.appium.java_client.MobileBy;

public class SpecialSearchActivity extends BasePage{

    private String btnCategoryXPath = "//android.widget.TextView[contains(@text, '%s')]";
    private Button btnSearchGoods = new Button(MobileBy.id("rlSearchButton"), "Button for search goods");

    public SpecialSearchActivity() {
        super(MobileBy.id("rlSearchButton"), "Search goods page");
    }

    public void selectSearchCategory(String categoryName) {
        new Button(MobileBy.xpath(String.format(btnCategoryXPath, categoryName)),
                String.format("Button for select category %s filter", categoryName)).tap();
    }

    public void startSearch() {
        btnSearchGoods.tap();
    }
}
