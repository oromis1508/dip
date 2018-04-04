package secretCloset.runner;

import cucumber.api.CucumberOptions;
import cucumber.api.testng.AbstractTestNGCucumberTests;

@CucumberOptions(
        features = "src/test/java/secretCloset/features",
        glue = "secretCloset/steps",
        format = {
                "pretty",
                "html:target/cucumber-reports/cucumber-html",
                "json:target/cucumber-reports/CucumberTestReport.json",
        })
public class SecretClosetRunner extends AbstractTestNGCucumberTests {
}
