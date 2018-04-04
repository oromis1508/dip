package secretCloset.page;

import frame.Elements.BasePage;
import frame.Elements.ListView;
import io.appium.java_client.MobileBy;

import java.util.Random;

public class GoodsListActivity extends BasePage{

    private ListView lvwGoodItem = new ListView(MobileBy.id("listView"));
    private String goodItemId = "ivListItemPhoto";

    public GoodsListActivity() {
        super(MobileBy.xpath("//android.widget.TextView[contains(@text, 'Sort')]"), "Goods list page");
    }

    public void tapRandomGood() {
        int goodsCount = lvwGoodItem.getSubElementsCount(MobileBy.id(goodItemId), "Good item in list");
        int randomGoodNumber = 0;
        if(goodsCount > 1) {
            randomGoodNumber = new Random().nextInt(goodsCount - 1);
        }
        lvwGoodItem.tapSubElement(MobileBy.id(goodItemId), "Random good item", randomGoodNumber);
    }
}
