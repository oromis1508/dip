package secretCloset.steps;

import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import org.testng.Assert;
import secretCloset.page.MainActivity;
import secretCloset.page.PleaseDoNotForget;
import secretCloset.page.SelectRegionActivity;

public class SelectRegionSteps {
    @Then("^The label on the main activity equal '(.*)'$")
    public void checkRegionChanged(String expectedRegion) {
        String actualRegion = new MainActivity().getSelectedRegion();
        Assert.assertEquals(expectedRegion, actualRegion, "Check that the region changed successfully");
    }

    @When("^I click the label to select region on the main activity$")
    public void moveToSelectRegion() {
        new MainActivity().clickSelectRegion();
    }

    @When("^I click ok on the please do not forget activity$")
    public void submitCaution() {
        new PleaseDoNotForget().submitCaution();
    }

    @When("^I input into the search field '(.*)' on the select region activity$")
    public void searchRegion(String region) {
        new SelectRegionActivity().searchRegion(region);
    }

    @When("^I click on the found label '(.*)' on the select region activity$")
    public void chooseRegion(String region) {
        new SelectRegionActivity().selectRegion(region);
    }
}
