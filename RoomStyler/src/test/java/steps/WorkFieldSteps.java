package steps;

import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import forms.WorkField;
import org.testng.Assert;

public class WorkFieldSteps {

    @When("^I click '(.*)' on the item from the work field$")
    public void deleteItem(String imageDeleteIconName) {
        new WorkField().clickItem(imageDeleteIconName);
    }

    @Then("^The work field is not contains the item '(.*)'$")
    public void checkOnFieldEmpty(String imageName) {
        Assert.assertFalse(new WorkField().isItemPlaced(imageName), "The placed item deleted from the work field");
    }

    @Then("^The item '(.*)' is displayed correctly on the work field$")
    public void checkItemExist(String imageName) {
        Assert.assertTrue(new WorkField().isItemPlaced(imageName), "The item correctly displayed on the work field");
    }

    @When("^I click the item '(.*)' on the work field$")
    public void clickItem(String imageName) {
        new WorkField().clickItem(imageName);
    }
}
