package forms.enums;

public enum SceneInfoItem {

    WALLS("wallsCount"),
    DOORS("doorsCount"),
    WINDOWS("windowsCount"),
    PRODUCTS("componentsCount"),
    MATERIALS("materialsCount");

    private final String text;

    SceneInfoItem(final String text) {
        this.text = text;
    }

    @Override
    public String toString() {
        return text;
    }
}
