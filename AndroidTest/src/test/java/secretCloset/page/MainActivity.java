package secretCloset.page;

import frame.Elements.BasePage;
import frame.Elements.Button;
import io.appium.java_client.MobileBy;

public class MainActivity extends BasePage {

    private Button btnRegionSelect = new Button(MobileBy.id("tvToolbarCity"),"Button for select region");

    public MainActivity() {
        super(MobileBy.id("tvToolbarCity"), "App main page");
    }

    public void selectCategory(String categoryName){
        new Button(MobileBy.id(CategoryMenu.getByName(categoryName)),
                String.format("Search by %s category button", categoryName)).tap();
    }

    public String getSelectedRegion() {
        return btnRegionSelect.getButtonText();
    }

    public void clickSelectRegion() {
        btnRegionSelect.tap();
    }
}
