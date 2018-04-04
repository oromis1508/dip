package runner;

import cucumber.api.CucumberOptions;
import cucumber.api.testng.AbstractTestNGCucumberTests;

@CucumberOptions(
        features = "src/test/java/features",
        glue = "steps",
        format = {
                "pretty",
                "html:target/cucumber-reports/cucumber-html",
                "json:target/cucumber-reports/CucumberTestReport.json",
        })
public class TestRunner extends AbstractTestNGCucumberTests {
}

