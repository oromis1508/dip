package steps;

import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import forms.SideBar;
import forms.enums.FurnishYourRoomItem;
import forms.enums.SceneInfoItem;
import forms.enums.SidebarItem;
import org.testng.Assert;

public class SideBarSteps {

    @Then("^The image on the sidebar is same with the image '(.*)'$")
    public void checkImageOnSideBar(String imageName) {
        Assert.assertTrue(new SideBar().isItemExist(imageName), "The image displayed in the description of the selected item");
    }

    @When("^I click the button '(.*)' on sidebar$")
    public void clickSidebarButton(String sideBarButton) {
        new SideBar().selectBarItem(SidebarItem.getMenu(sideBarButton));
    }

    @When("^I click the link '(.*)' in the opened menu$")
    public void clickMenu(String menuName) {
        new SideBar().selectFurnishRoomItem(FurnishYourRoomItem.getMenu(menuName));
    }

    @When("^I drag the item '(.*)' from sidebar to the work field '(.*)'$")
    public void dragItemToWorkField(String dragImageName, String dropImageName) {
        new SideBar().dragAndDrop(dragImageName, dropImageName);
    }

    @Then("^The dimensions of the item on the sidebar not equal '(.*)'$")
    public void checkItemDimensions(int notExpectedValue) {
        SideBar sideBar = new SideBar();
        for (String dimension : sideBar.getProductDimensions()) {
            Assert.assertNotEquals(dimension, String.valueOf(notExpectedValue), String.format("The dimensions of the item not equal %s", notExpectedValue));
        }
    }

    @Then("^The sidebar not contains the item '(.*)'$")
    public void checkImageOnSideBarNotExist(String imageName) {
        Assert.assertFalse(new SideBar().isItemExist(imageName), "The image not displayed in the description of the selected item");
    }

    @Then("^The scene information contains '(.*)' in the all fields$")
    public void checkSceneInformation(int sceneInfoValue) {
        SideBar sideBar = new SideBar();
        for (SceneInfoItem sceneInfoItem: SceneInfoItem.values()) {
            Assert.assertEquals(sideBar.getSceneItemCount(sceneInfoItem), sceneInfoValue, String.format("The sceneInfoItem equals %s", sceneInfoValue));
        }
    }
}
