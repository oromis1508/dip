package test;

import forms.ComponentInfo;
import forms.SideBar;
import forms.StartDialog;
import forms.WorkField;
import forms.enums.FurnishYourRoomItem;
import forms.enums.ImageItem;
import forms.enums.SidebarItem;
import org.junit.Assert;
import webdriver.BaseTest;

import java.io.File;
import java.util.ArrayList;

public class RoomStyler extends BaseTest {

	//private final int numMaxRandomPlacementShips = Integer.parseInt(getTestProperty("numMaxRandomPlacementShips"));

	@Override
	public void runTest(){

		logger.step(1, "To click link \"Select random opponent\"");
		File f = new File("");
		System.out.println(f.getAbsolutePath());
		StartDialog startDialog = new StartDialog();
        startDialog.closeDialog();
        Assert.assertTrue(startDialog.isDialogClosed());
		SideBar sideBar = new SideBar();
		sideBar.selectBarItem(SidebarItem.FURNISH_YOUR_ROOM);
		sideBar.selectFurnishRoomItem(FurnishYourRoomItem.DINING_ROOM);
		ComponentInfo componentInfo = new ComponentInfo();
		componentInfo.dragAndDrop(ImageItem.DRAG_ITEM, ImageItem.DROP_ITEM);
		logger.info("12345");
		WorkField workField = new WorkField();
		Assert.assertTrue("asdzxc", workField.isItemPlaced(ImageItem.PLACED_ITEM));
        logger.info("12345");
        workField.clickPlacedItem();
		Assert.assertTrue("ise", componentInfo.isItemExist(ImageItem.DRAG_ITEM));
        ArrayList al = componentInfo.getProductDimensions();
        System.out.println("sdfsfd");
	}
}
