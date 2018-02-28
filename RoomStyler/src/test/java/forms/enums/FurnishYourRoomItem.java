package forms.enums;

public enum FurnishYourRoomItem {

    ACCESSORIES("Accessories", "new-1152"),
    ARCHITECTURE("Architecture", "new-2035"),
    BATHROOM("Bathroom", "new-1186"),
    BEDROOM("Bedroom", "new-1018"),
    CHRISTMAS("Christmas", "new-1693"),
    DINING_ROOM("Dining room", "new-1849"),
    DIY("Diy", "new-1996"),
    ELECTRICALS("Electricals", "new-1872"),
    FURNITURE("Furniture", "new-1128"),
    GARDEN_AND_OUTDOOR("Garden & outdoor", "new-1316"),
    GYM("Gym", "new-2806"),
    HOME_ENTERTAINMENT("Home entertainment", "new-1498"),
    HOME_OFFICE("Home office", "new-1019"),
    IRRELEVANT_PRODUCTS("Irrelevant products", "new-1942"),
    KIDS_ROOM("Kids' room", "new-1017"),
    KITCHEN("Kitchen", "new-1020"),
    LIGHTING("Lighting", "new-1230"),
    LIVING_ROOM("Living room", "new-1033"),
    PEOPLE_AND_ANIMALS("People & animals", "new-2846");

    private final String text;
    private final String name;

    FurnishYourRoomItem(final String name, final String text) {
        this.name = name;
        this.text = text;
    }

    @Override
    public String toString() {
        return text;
    }

    public static FurnishYourRoomItem getMenu(String menuName) {
        for (FurnishYourRoomItem item :values()) {
            if(item.name.equals(menuName)) {
                return item;
            }
        }
        return null;
    }
}
