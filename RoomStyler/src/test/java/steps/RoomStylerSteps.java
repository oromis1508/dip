package steps;

import cucumber.api.PendingException;
import cucumber.api.java.en.And;
import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import webdriver.BaseEntity;

public class RoomStylerSteps extends BaseEntity{

    @Given("^Page '(.*)' opened$")
    public void openMainPage(String url) {
        browser.navigate(url);
    }

    @Then("^The image on the sidebar is same with the image of the dragged item$")
    public void theImageOnTheSidebarIsSameWithTheImageOfTheDraggedItem() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @When("^The close button clicked in the welcome dialog$")
    public void theCloseButtonClickedInTheWelcomeDialog() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @Then("^The welcome dialog closed$")
    public void theWelcomeDialogClosed() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @When("^Clicked the button 'Furnish your room' on sidebar$")
    public void clickedTheButtonFurnishYourRoomOnSidebar() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @And("^Clicked the link 'Dining room'$")
    public void clickedTheLinkDiningRoom() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @And("^Drag an item to the work field$")
    public void dragAnItemToTheWorkField() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @Then("^The dragged item is displayed correctly on the work field$")
    public void theDraggedItemIsDisplayedCorrectlyOnTheWorkField() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @When("^The item on the work field clicked$")
    public void theItemOnTheWorkFieldClicked() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @And("^The dimensions of the item on the sidebar not equal null$")
    public void theDimensionsOfTheItemOnTheSidebarNotEqualNull() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @When("^The item deleted from the work field$")
    public void theItemDeletedFromTheWorkField() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @Then("^The work field is empty$")
    public void theWorkFieldIsEmpty() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @And("^The sidebar not contains the image of the dragged item$")
    public void theSidebarNotContainsTheImageOfTheDraggedItem() throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @And("^The scene information contains '(\\d+)' in the all fields$")
    public void theSceneInformationContainsInTheAllFields(int arg0) throws Throwable {
        // Write code here that turns the phrase above into concrete actions
        throw new PendingException();
    }

    @Override
    protected String formatLogMsg(String message) {
        return null;
    }
}
