package webdriver;

import org.sikuli.script.Pattern;

public class SikuliUtil {

    private static String screenPath = "src/test/resources/screenshots/%s.png";

    public static Pattern getImagePattern(String imageName) {
        return new Pattern(String.format(screenPath, imageName));
    }
}
