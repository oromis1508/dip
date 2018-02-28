package steps;

import cucumber.api.java.en.And;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import forms.SideBar;
import forms.enums.FurnishYourRoomItem;
import forms.enums.ImageItem;
import forms.enums.SceneInfoItem;
import forms.enums.SidebarItem;
import org.testng.Assert;

public class SideBarSteps {

    @Then("^The image on the sidebar is same with the image of the dragged item$")
    public void checkImageOnSideBar() {
        Assert.assertTrue(new SideBar().isItemExist(ImageItem.DRAG_ITEM, BaseRoomStylerSteps.waitForItem), "The image displayed in the description of the selected item");
    }

    @When("^I click the button '(\\D+)' on sidebar$")
    public void clickSidebarButton(String sideBarButton) {
        new SideBar().selectBarItem(SidebarItem.getMenu(sideBarButton));
    }

    @And("^I click the link '(\\D+)' in the opened menu$")
    public void clickMenu(String menuName) {
        new SideBar().selectFurnishRoomItem(FurnishYourRoomItem.getMenu(menuName));
    }

    @And("^I drag an item from sidebar to the work field$")
    public void dragItemToWorkField() {
        new SideBar().dragAndDrop(ImageItem.DRAG_ITEM, ImageItem.DROP_ITEM);
    }

    @And("^The dimensions of the item on the sidebar not equal null$")
    public void checkItemDimensions() {
        SideBar sideBar = new SideBar();
        for (String dimension : sideBar.getProductDimensions()) {
            Assert.assertNotEquals(dimension, "0", "The dimensions of the item not equal 0");
        }
    }

    @And("^The sidebar not contains the image of the dragged item$")
    public void checkImageOnSideBarNotExist() {
        Assert.assertFalse(new SideBar().isItemExist(ImageItem.DRAG_ITEM, BaseRoomStylerSteps.waitForItem), "The image not displayed in the description of the selected item");
    }

    @And("^The scene information contains '(\\d+)' in the all fields$")
    public void checkSceneInformation(int sceneInfoValue) {
        SideBar sideBar = new SideBar();
        for (SceneInfoItem sceneInfoItem: SceneInfoItem.values()) {
            Assert.assertEquals(sideBar.getSceneItemCount(sceneInfoItem), sceneInfoValue, "The sceneInfoItem equals 0");
        }
    }
}
