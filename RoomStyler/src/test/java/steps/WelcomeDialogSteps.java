package steps;

import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import forms.StartDialog;
import org.testng.Assert;

public class WelcomeDialogSteps {

    @When("^I click the close button on the welcome dialog$")
    public void closeDialog() {
        new StartDialog().closeDialog();
    }

    @Then("^The welcome dialog closed$")
    public void checkDialogClosed() {
        Assert.assertTrue(new StartDialog().isDialogClosed(), "Start dialog closed");
    }
}
