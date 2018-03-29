package frame.utils;

import io.appium.java_client.MobileBy;
import io.appium.java_client.MobileSelector;
import org.openqa.selenium.By;

public abstract class UiAutomatorBy extends MobileBy {

    private static final String resourceId = "resourceId(\"$1%s\")";
    private static final String text = "text(\"$1%s\")";
    private static final String textMatches = "textMatches(\"$1%s\")";
    private static final String checked = "checked($1%s)";
    private static final String className = "className(\"$1%s\")";
    private static final String description = "description(\"$1%s\")";
    private static final String descriptionContains = "descriptionContains(\"$1%s\")";
    private static final String textContains = "textContains(\"$1%s\")";
    private static final String index = "index($1%d)";

    protected UiAutomatorBy(MobileSelector selector, String locatorString) {
        super(selector, locatorString);
    }

    public static By resourceId(final String resourceIdText){
        if (resourceIdText == null) {
            throw new IllegalArgumentException("Must supply an Android UIAutomator string");
        }

        return new ByAndroidUIAutomator(String.format(resourceId,resourceIdText));
    }

    public static By text(final String t){
        if (t == null) {
            throw new IllegalArgumentException("Must supply an Android UIAutomator string");
        }

        return new ByAndroidUIAutomator(String.format(text,t));
    }

    public static By textContains(final String t){
        if (t == null) {
            throw new IllegalArgumentException("Must supply an Android UIAutomator string");
        }

        return new ByAndroidUIAutomator(String.format(text,t));
    }

    public static By textMatches(final String regexp){
        if (regexp == null) {
            throw new IllegalArgumentException("Must supply an Android UIAutomator string");
        }

        return new ByAndroidUIAutomator(String.format(textMatches,regexp));
    }

    public static By checked(final boolean bol){
        return new ByAndroidUIAutomator(String.format(checked,bol));
    }

    public static By className(final String name){
        if (name == null) {
            throw new IllegalArgumentException("Must supply an Android UIAutomator string");
        }

        return new ByAndroidUIAutomator(String.format(className,name));
    }

    public static By description(final String desc){
        if (desc == null) {
            throw new IllegalArgumentException("Must supply an Android UIAutomator string");
        }

        return new ByAndroidUIAutomator(String.format(description,desc));
    }

    public static By descriptionContains(final String desc){
        if (desc == null) {
            throw new IllegalArgumentException("Must supply an Android UIAutomator string");
        }

        return new ByAndroidUIAutomator(String.format(descriptionContains,desc));
    }

    public static By index(final int ind){
        return new ByAndroidUIAutomator(String.format(index,ind));
    }


}
