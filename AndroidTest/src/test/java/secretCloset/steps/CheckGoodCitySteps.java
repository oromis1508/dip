package secretCloset.steps;

import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import org.testng.Assert;
import secretCloset.page.GoodInfoActivity;
import secretCloset.page.GoodsListActivity;
import secretCloset.page.MainActivity;
import secretCloset.page.SpecialSearchActivity;

public class CheckGoodCitySteps {

    @When("^I click the category '(.*)' on the main activity$")
    public void moveToCategory(String categoryName) {
        new MainActivity().selectCategory(categoryName);
    }

    @When("^I click '(.*)' on the panel with the good type on the special search activity$")
    public void setFilterForSearch(String filterName) {
        new SpecialSearchActivity().selectSearchCategory(filterName);
    }

    @When("^I click the search button on the special search activity$")
    public void searchGoods() {
        new SpecialSearchActivity().startSearch();
    }

    @When("^I click any of the found goods on the goods list activity$")
    public void openRandomGood() {
        new GoodsListActivity().tapRandomGood();
    }

    @Then("^The label '(.*)' of the good is '(.*)' on the good info activity$")
    public void checkGood(String comparedAttribute, String attributeValue) {
        String actualValue = new GoodInfoActivity().getSellerInfo(comparedAttribute);
        Assert.assertEquals(attributeValue, actualValue, String.format("Check that %s of a good is valid", comparedAttribute));
    }
}
