package webdriver;

import java.util.ArrayList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class RegExp {

    public static ArrayList<String> getValuesRegExp(final String regex, final String text, final int group) {
        Pattern pattern = Pattern.compile(regex);
        Matcher matcher = pattern.matcher(text);

        ArrayList<String> result = new ArrayList();
        while(matcher.find()) {
            result.add(matcher.group(group));
        }
        return result;
    }
}
