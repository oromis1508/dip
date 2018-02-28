package steps;

import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import forms.WorkField;
import forms.enums.ImageItem;
import org.testng.Assert;

public class WorkFieldSteps {

    @When("^I delete the dragged item from the work field$")
    public void deleteItem() {
        new WorkField().clickItem(ImageItem.DELETE_ITEM_ICON);
    }

    @Then("^The work field is not contains the dragged item$")
    public void checkOnFieldEmpty() {
        Assert.assertFalse(new WorkField().isItemPlaced(ImageItem.PLACED_ITEM, BaseRoomStylerSteps.waitForItem), "The placed item deleted from the work field");
    }

    @Then("^The dragged item is displayed correctly on the work field$")
    public void checkItemExist() {
        Assert.assertTrue(new WorkField().isItemPlaced(ImageItem.PLACED_ITEM), "The item correctly displayed on the work field");
    }

    @When("^I click the dragged item on the work field$")
    public void clickItem() {
        new WorkField().clickItem(ImageItem.PLACED_ITEM);
    }
}
