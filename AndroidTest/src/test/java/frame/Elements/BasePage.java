package frame.Elements;

import frame.Helpers;
import frame.utils.JamtException;
import org.apache.commons.io.FileUtils;
import org.openqa.selenium.By;

import java.io.File;

/**
 * Base class, describing forms and pages
 */
public abstract class BasePage extends Helpers {

    protected By titleLocator;
    protected String title;
    protected String pageName;


    public By getTitleLocator() {
        return titleLocator;
    }

    public String getTitle() {
        return title;
    }

    public String getPageName() {
        return pageName;
    }

    public BasePage(By loc, String titleOf){
        titleLocator = loc;
        title = titleOf;
        Label titleLabel = new Label(titleLocator,title);
        try {
            BaseElement.wait(titleLocator);
        } catch (Exception e) {
            throw new JamtException(String.format(getLoc("loc.form.doesnt.appears"), title)
                    +"\n"+e.getMessage());
        }
        if (shouldCreateScreenEveryStep()) {
            formReportInfo();
        } else {
            logger.info(String.format(getLoc("loc.form.appears"), title));
        }
    }

    /**
     * Form report block
     */
    private void formReportInfo() {
        String timestamp = date.toString();
        String newFilePath;
        String newFilePathRelated = this.getClass().getPackage().getName() + "." + this.getClass().getSimpleName()+String.format("%1$s.png",timestamp);
        try {
            String pathCreatedScreen = makeScreen(this.getClass(),false);
            newFilePath = pathCreatedScreen.replaceAll(File.separator+".png", timestamp + File.separator+".png");
            FileUtils.moveFile(new File(pathCreatedScreen), new File(newFilePath));
        } catch (Exception e) {
            logger.warn(e.getMessage());
        }
        logger.debug("<table border=\"3\" bordercolor=\"black\" style=\"background-color:#EBEBEB\" cellpadding=\"1\" cellspacing=\"3\"><tr>" +
                "<th colspan = \"1\"><h3>" +
                String.format(getLoc("loc.form.appears"), title)+
                "<tr>" +
                "<td>" +
                String.format("<a href=\"Screenshots/%1$s\">"+
                        "<img height=\"250\" width=\"350\" title=\"Actual screenshot\" src=\"Screenshots/%1$s\">"+
                        "</a>",newFilePathRelated)+
                "</td>" +
                "</tr>"+
                "</table>");
    }


}
