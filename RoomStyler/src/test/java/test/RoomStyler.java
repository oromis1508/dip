package test;

import forms.SideBar;
import forms.StartDialog;
import forms.WorkField;
import forms.enums.FurnishYourRoomItem;
import forms.enums.ImageItem;
import forms.enums.SceneInfoItem;
import forms.enums.SidebarItem;
import org.testng.Assert;
import webdriver.BaseTest;

public class RoomStyler extends BaseTest {

	@Override
	public void runTest(){
        int waitForItem = Integer.parseInt(getTestProperty("waitingTimeout"));
		StartDialog startDialog = new StartDialog();
        startDialog.closeDialog();
        Assert.assertTrue(startDialog.isDialogClosed(), "Start dialog closed");
		SideBar sideBar = new SideBar();
		sideBar.selectBarItem(SidebarItem.FURNISH_YOUR_ROOM);
        sideBar.selectFurnishRoomItem(FurnishYourRoomItem.DINING_ROOM);
        sideBar.dragAndDrop(ImageItem.DRAG_ITEM, ImageItem.DROP_ITEM);
		WorkField workField = new WorkField();
		Assert.assertTrue(workField.isItemPlaced(ImageItem.PLACED_ITEM, waitForItem), "The item correctly displayed on the work field");
        workField.clickItem(ImageItem.PLACED_ITEM);
        Assert.assertTrue(sideBar.isItemExist(ImageItem.DRAG_ITEM, waitForItem), "The image displayed in the description of the selected item");
        for (String dimension : sideBar.getProductDimensions()) {
            Assert.assertNotEquals(dimension, "0", "The dimensions of the item not equal 0");
        }
        workField.clickItem(ImageItem.DELETE_ITEM_ICON);
        Assert.assertFalse(workField.isItemPlaced(ImageItem.PLACED_ITEM, waitForItem), "The placed item deleted from the work field");
        Assert.assertFalse(workField.isItemPlaced(ImageItem.DRAG_ITEM, waitForItem), "The image not displayed in the description of the selected item");
        for (SceneInfoItem sceneInfoItem: SceneInfoItem.values()) {
            Assert.assertEquals(sideBar.getSceneItemCount(sceneInfoItem), 0, "The sceneInfoItem equals 0");
        }
    }
}
