package forms.enums;

public enum ImageItem {

    DRAG_ITEM("src/test/resources/screenshots/dragItem.png"),
    DROP_ITEM("src/test/resources/screenshots/dropItem.png"),
    PLACED_ITEM("src/test/resources/screenshots/placedItem.png");

    private final String text;

    ImageItem(final String text) {
        this.text = text;
    }

    @Override
    public String toString() {
        return text;
    }

}
