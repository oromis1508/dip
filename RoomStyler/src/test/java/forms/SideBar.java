package forms;

import forms.enums.FurnishYourRoomItem;
import forms.enums.SidebarItem;
import org.openqa.selenium.By;
import webdriver.elements.Button;
import webdriver.elements.Link;

public class SideBar {

    private String sideBarItemXpath = "//a[@title='%s']";


    public void selectBarItem(SidebarItem sidebarItem) {
        new Button(By.xpath(String.format(sideBarItemXpath, sidebarItem)),"Side bar item").click();
    }

    public void selectFurnishRoomItem(FurnishYourRoomItem furnishYourRoomItem) {
        new Link(By.id(furnishYourRoomItem.toString()), "Furnish your room menu item").click();
    }
}
