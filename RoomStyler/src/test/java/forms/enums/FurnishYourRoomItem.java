package forms.enums;

public enum FurnishYourRoomItem {

    ACCESSORIES("new-1152"),
    ARCHITECTURE("new-2035"),
    BATHROOM("new-1186"),
    BEDROOM("new-1018"),
    CHRISTMAS("new-1693"),
    DINING_ROOM("new-1849"),
    DIY("new-1996"),
    ELECTRICALS("new-1872"),
    FURNITURE("new-1128"),
    GARDEN_AND_OUTDOOR("new-1316"),
    GYM("new-2806"),
    HOME_ENTERTAINMENT("new-1498"),
    HOME_OFFICE("new-1019"),
    IRRELEVANT_PRODUCTS("new-1942"),
    KIDS_ROOM("new-1017"),
    KITCHEN("new-1020"),
    LIGHTING("new-1230"),
    LIVING_ROOM("new-1033"),
    PEOPLE_AND_ANIMALS("new-2846");

    private final String text;

    FurnishYourRoomItem(final String text) {
        this.text = text;
    }

    @Override
    public String toString() {
        return text;
    }
}
